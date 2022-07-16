using Shared.Models;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts
{
    public static class MessageHelper
    {
        public static List<KeyValuePair<Player, Message>> ClearLastMessage(List<Player> playerList, List<Message> lastMessageList)
        {
            var result = new List<KeyValuePair<Player, Message>>();

            var playerIds = playerList.Select(x => x.Id).ToList();

            foreach (var lastMessage in lastMessageList.Where(x => playerIds.Contains(x.PlayerId)).ToList())
            {
                result.Add(new KeyValuePair<Player, Message>(playerList.First(x => x.Id == lastMessage.PlayerId), lastMessage));
            }

            return result;
        }
    }
}
