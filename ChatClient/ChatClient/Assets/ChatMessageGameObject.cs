using Shared.Models;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChatMessageGameObject : MonoBehaviour
{
    public Image ChatMessageImageBg;
    public TextMeshProUGUI ChatMessageText;
    public TextMeshProUGUI ChatMessageDate;

    public void SetChatMessage(Player player, Message message, bool mine = false)
    {
        ChatMessageText.text = $"[{player.Name}] {message.Text}";

        ColorUtility.TryParseHtmlString("#" + player.Color, out var color);
        ChatMessageText.color = color;

        ChatMessageDate.text = $"{message.DateCreate.Hour}:{message.DateCreate.Minute}";

        ChatMessageImageBg.rectTransform.sizeDelta = mine
            ? new Vector2(Math.Min(764, ChatMessageText.GetPreferredValues().x + 10), 0)
            : new Vector2(Math.Min(764, ChatMessageText.GetPreferredValues().x + 60), 0);

        ChatMessageText.ForceMeshUpdate();
        GetComponent<RectTransform>().sizeDelta = new Vector2(764, ChatMessageText.GetPreferredValues().y + 10);
        ChatMessageText.ForceMeshUpdate();
    }
}
