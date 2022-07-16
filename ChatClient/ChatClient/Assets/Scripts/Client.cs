using Assets.Scripts;
using NetCoreServer;
using Newtonsoft.Json;
using Shared.Commands;
using Shared.Models;
using Shared.Results;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Assets.Helpers;
using TMPro;
using UnityEngine;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;

public class Client : MonoBehaviour
{
    class ChatClient : TcpClient
    {
        bool _stop;

        private Dictionary<Player, bool> _playerStatus = new Dictionary<Player, bool>();

        public ChatClient(string address, int port) : base(address, port) { }

        public void DisconnectAndStop()
        {
            _stop = true;
            DisconnectAsync();
            while (IsConnected)
                Thread.Yield();
        }

        protected override void OnConnected()
        {
            Debug.Log("Connected.");
        }

        protected override void OnDisconnected()
        {
            Debug.Log("Disconnected.");
            if (_stop)
                Debug.Log("Trying to connect.");
            Thread.Sleep(1000);

            if (!_stop)
                ConnectAsync();
        }

        protected override void OnError(System.Net.Sockets.SocketError error)
        {
            Debug.Log(error.ToString());
        }

        protected override void OnReceived(byte[] buffer, long offset, long size)
        {
            string message = Encoding.UTF8.GetString(buffer, (int) offset, (int) size);

            Debug.Log(message);

            var batch = JsonConvert.DeserializeObject<Batch>(message);
            var commandResultType = Type.GetType(batch.CommandName);
            var result = JsonConvert.DeserializeObject(batch.CommandText, commandResultType);

            switch (result)
            {
                case LoginResult loginResult:
                {
                    _playerStatus = PlayerHelper.GetPlayerStatus(loginResult.PlayerStatusList, loginResult.PlayerList);

                    var lastMessageList = MessageHelper.ClearLastMessage(loginResult.PlayerList, loginResult.LastMessageList);

                    PlayerStatusListController.Instance.UpdatePlayerStatus(_playerStatus);

                    foreach (var lastMessage in lastMessageList)
                    {
                        ChatMessageController.Instance.AddMessage(false, lastMessage.Key, lastMessage.Value);
                    }

                    break;
                }
                case GetPlayersResult getPlayersResult:
                {
                    _playerStatus = PlayerHelper.GetPlayerStatus(getPlayersResult.PlayerStatusList, getPlayersResult.PlayerList);

                    PlayerStatusListController.Instance.UpdatePlayerStatus(_playerStatus);

                    break;
                }
                case SendMessageResult sendMessageResult:
                {
                    if (_playerStatus.Keys.FirstOrDefault(x => x.Id == sendMessageResult.Player.Id) is var player)
                    {
                        ChatMessageController.Instance.AddMessage(false, player, sendMessageResult.Message);
                    }

                    break;
                }
                default:
                {
                    Debug.Log(result);

                    break;
                }
            }
        }
    }

    private ChatClient _chatClient;
    private Player _player;

    public TMP_InputField InputChatMessage;
    public TMP_InputField InputPlayerName;
    public Button ButtonPlayerColor;
    public Button ButtonConnectionStatus;

    public void EnterRoomChat()
    {
        _player = new Player(Guid.NewGuid().ToString(), InputPlayerName.text, ColorUtility.ToHtmlStringRGB(ButtonPlayerColor.GetComponent<Image>().color));

        SendCommandToServer(new LoginCommand(_player));
    }

    void Start()
    {
        var connectionSettings = ConnectionSettingsHelper.LoadConnectionSettings();

        _chatClient = new ChatClient(connectionSettings.IpAddress, connectionSettings.Port);
        _chatClient.ConnectAsync();

        StartCoroutine(IsConnectionAlive());
    }

    IEnumerator IsConnectionAlive()
    {
        while (true)
        {
            ButtonConnectionStatus.image.color = _chatClient.IsConnected ? Color.green : Color.red;

            yield return new WaitForSeconds(3);
        }
    }

    public void SendCommandToServer(Command command)
    {
        var batch = new Batch(command.GetCommandName(), JsonConvert.SerializeObject(command));
        _chatClient.SendAsync(JsonConvert.SerializeObject(batch));
    }

    public void PressButtonSendMessage()
    {
        if (!_chatClient.IsConnected)
        {
            return;
        }

        string messageText = InputChatMessage.text;

        if (!string.IsNullOrWhiteSpace(messageText))
        {
            var message = new Message(_player.Id, messageText);

            SendCommandToServer(new SendMessageCommand(_player, message));
            ChatMessageController.Instance.AddMessage(true, _player, message);
            InputChatMessage.ActivateInputField();
            InputChatMessage.text = string.Empty;
        }
    }

    public void PressEnterSendMessage()
    {
        if (Input.GetKey(KeyCode.KeypadEnter) || Input.GetKey(KeyCode.Return))
        {
            PressButtonSendMessage();
        }
    }

    public void AddNoOfflineCommand()
    {
        InputChatMessage.text += " /NoOffline ";
    }

    void OnApplicationQuit()
    {
        _chatClient.DisconnectAndStop();
    }
}