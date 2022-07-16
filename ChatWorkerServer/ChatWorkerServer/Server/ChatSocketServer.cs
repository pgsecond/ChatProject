using System.Collections.Generic;
using System.Net;
using ChatWorkerServer.Interfaces;
using ChatWorkerServer.Managers;
using ChatWorkerServer.Repositories;
using DryIoc;
using NetCoreServer;
using Newtonsoft.Json;
using Shared.Models;
using Shared.Results;

namespace ChatWorkerServer.Server
{
    public class ChatSocketServer : TcpServer
    {
        static readonly List<ChatSocketSession> SessionList = new List<ChatSocketSession>();

        public static Container IocContainer;

        public ChatSocketServer(IPAddress address, int port) : base(address, port)
        {   
            IocContainer = new Container();
            IocContainer.Register<IDataRepository<Message>, MessageRepository<Message>>(Reuse.Singleton);
        }

        protected override TcpSession CreateSession()
        {
            ChatSocketSession newSession = new ChatSocketSession(this);
            AddSession(newSession);
            return newSession;
        }

        void AddSession(ChatSocketSession session) => SessionList.Add(session);

        void RemoveSession(ChatSocketSession session) => SessionList.Remove(session);

        protected override void OnDisconnected(TcpSession session)
        {
            RemoveSession((ChatSocketSession) session);

            PlayerManager.RemovePlayerSession(session.Id);

            BroadcastResult((ChatSocketSession)session, new GetPlayersResult { PlayerList = PlayerManager.GetPlayerList, PlayerStatusList = PlayerManager.GetPlayerStatusList });

            base.OnDisconnected(session);
        }

        public static void BroadcastResult(ChatSocketSession origin, CommandResult commandResult)
        {
            var batch = new Batch(commandResult.ResultTypeName, JsonConvert.SerializeObject(commandResult));

            foreach (ChatSocketSession session in SessionList)
            {
                if (!IsOwnerSession(origin, session))
                {
                    session.SendAsync(JsonConvert.SerializeObject(batch));
                }
            }
        }

        public static void ReturnResult(ChatSocketSession origin, CommandResult commandResult)
        {
            var batch = new Batch(commandResult.ResultTypeName, JsonConvert.SerializeObject(commandResult));
            origin.SendAsync(JsonConvert.SerializeObject(batch));
        }

        public static bool IsOwnerSession(ChatSocketSession thisSession, ChatSocketSession otherSession) => thisSession == otherSession;
    }
}