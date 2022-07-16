using System;
using System.Collections.Generic;
using Shared.Models;
using UnityEngine;

public class ChatMessageController : MonoBehaviour
{
    public GameObject MyMessageGameObjectPrefab;
    public GameObject OtherMessageGameObjectPrefab;

    private List<KeyValuePair<bool, (Player, Message)>> _messageList = new List<KeyValuePair<bool, (Player, Message)>>();

    public static ChatMessageController Instance;

    private float _sizeY;

    void Awake() => Instance = this;

    public void AddMessage(bool mine, Player player, Message message) => _messageList.Add(new KeyValuePair<bool, (Player, Message)>(mine, (player, message)));

    void Update()
    {
        lock (_messageList)
        {
            foreach (var message in _messageList)
            {
                AddMessageToChat(message.Key, message.Value.Item1, message.Value.Item2);
            }

            _messageList = new List<KeyValuePair<bool, (Player, Message)>>();
        }
    }

    public void AddMessageToChat(bool mine, Player player, Message message)
    {
        if (mine)
        {
            GameObject messageGameObject = Instantiate(MyMessageGameObjectPrefab, transform);
            messageGameObject.GetComponent<ChatMessageGameObject>().SetChatMessage(player, message, true);
            _sizeY += messageGameObject.transform.GetComponent<RectTransform>().sizeDelta.y;
        }
        else
        {
            try
            {
                GameObject messageGameObject = Instantiate(OtherMessageGameObjectPrefab, transform);
                messageGameObject.GetComponent<ChatMessageGameObject>().SetChatMessage(player, message);
                _sizeY += messageGameObject.transform.GetComponent<RectTransform>().sizeDelta.y;
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }
        }

        transform.GetComponent<RectTransform>().sizeDelta = new Vector2(0, Math.Max(450, _sizeY + 10));

        if (_sizeY > 450)
        {
            transform.localPosition = new Vector3(0, _sizeY + 10 - 450);
        }
    }
}
