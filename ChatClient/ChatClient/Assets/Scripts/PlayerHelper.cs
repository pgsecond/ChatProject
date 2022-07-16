using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Models;

namespace Assets.Scripts
{
    public static class PlayerHelper
    {
        public static Dictionary<Player, bool> GetPlayerStatus(Dictionary<string, bool> playerStatusDic, List<Player> playerList)
        {
            var playerStatus = new Dictionary<Player, bool>();

            foreach (var player in playerList)
            {
                if (!playerStatusDic.ContainsKey(player.Id))
                {
                    continue;
                }

                playerStatus.Add(player, playerStatusDic[player.Id]);
            }

            return playerStatus;
        }
    }
}
