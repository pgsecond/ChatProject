using Shared.Models;
using TMPro;
using UnityEngine;

public class PlayerStatusGameObject : MonoBehaviour
{
    public TextMeshProUGUI PlayerStatusText;

    public void SetMessageInfo(Player player, bool status)
    {
        string statusOnline = status ? "[*]" : "[x]";
        PlayerStatusText.text = $"{statusOnline} {player.Name}";

        ColorUtility.TryParseHtmlString("#" + player.Color, out var color);
        PlayerStatusText.color = color;

        PlayerStatusText.ForceMeshUpdate();
        GetComponent<RectTransform>().sizeDelta = new Vector2(0, PlayerStatusText.GetPreferredValues().y + 10);
        PlayerStatusText.ForceMeshUpdate();
    }
}
