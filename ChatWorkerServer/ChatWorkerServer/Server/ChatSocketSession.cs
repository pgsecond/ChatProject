using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using ChatWorkerServer.Interfaces;
using ChatWorkerServer.Managers;
using DryIoc;
using NetCoreServer;
using Newtonsoft.Json;
using Shared.Commands;
using Shared.Models;
using Shared.Results;

namespace ChatWorkerServer.Server
{
    public class ChatSocketSession : TcpSession
    {
        public ChatSocketSession(TcpServer server) : base(server) { }

        protected override void OnConnected()
        {
            Logger.Info($"Client Session connection established with Id: {Id}");
        }

        protected override void OnReceived(byte[] buffer, long offset, long size)
        {
            try
            {
                string message = Encoding.UTF8.GetString(buffer, (int)offset, (int)size);

                Logger.Info($"Receiving message. Size: [{size}] Message: {message} Id: {Id}");

                var batch = JsonConvert.DeserializeObject<Batch>(message);

                var commandType = Type.GetType(batch.CommandName);

                var command = JsonConvert.DeserializeObject(batch.CommandText, commandType ?? throw new InvalidOperationException());

                commandType = Type.GetType(batch.CommandName);

                if (commandType == null)
                {
                    Logger.Error($"Invalid command type. Id: {Id}");
                    return;
                }

                switch (command)
                {
                    case IExecutableClientCommand clientCommand:
                    {
                        switch (clientCommand)
                        {
                            case LoginCommand loginCommand:
                            {
                                PlayerManager.AddPlayer(loginCommand.Player, Id);

                                var response = clientCommand.Execute(loginCommand.Player);

                                ChatSocketServer.ReturnResult(this, response);

                                ChatSocketServer.BroadcastResult(this, new GetPlayersCommand().Execute(null));

                                break;
                            }
                            case SendMessageCommand sendMessageCommand:
                            {
                                sendMessageCommand.Message.DateCreate = DateTime.Now;

                                if (sendMessageCommand.Message.Text.Contains("/NoOffline"))
                                {
                                    PlayerManager.ClearOffline();

                                    ChatSocketServer.BroadcastResult(this, new GetPlayersResult { PlayerList = PlayerManager.GetPlayerList, PlayerStatusList = PlayerManager.GetPlayerStatusList });
                                    ChatSocketServer.ReturnResult(this, new GetPlayersResult { PlayerList = PlayerManager.GetPlayerList, PlayerStatusList = PlayerManager.GetPlayerStatusList });
                                    
                                    sendMessageCommand.Message.Text = sendMessageCommand.Message.Text.Replace("/NoOffline", String.Empty);
                                }

                                MessageManager.AddMessage(sendMessageCommand.Message);

                                ChatSocketServer.BroadcastResult(this, new SendMessageCommand(sendMessageCommand.Player, sendMessageCommand.Message).Execute(sendMessageCommand.Player));

                                Task.Run(() => ChatSocketServer.IocContainer.Resolve<IDataRepository<Message>>().AddAsync(sendMessageCommand.Message));

                                break;
                            }
                            default:
                            {
                                Logger.Warning($"Unknown command: {batch.CommandText} Id: {Id}");

                                break;
                            }
                        }

                        break;
                    }
                    default:
                    {
                        Logger.Warning($"Unknown command: {batch.CommandText} Id: {Id}");

                        break;
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Error($"Exception: {e} Id: {Id}");
            }
        }

        protected override void OnError(SocketError error)
        {
            Logger.Error($"System report connection error. Error: {error} Id: {Id}");
        }
    }
}