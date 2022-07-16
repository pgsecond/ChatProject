using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Shared.Models;

namespace ChatWorkerServer.Managers
{
    public static class PlayerManager
    {
        private static readonly ConcurrentDictionary<Guid, string> PlayerSessionList = new ConcurrentDictionary<Guid, string>();
        private static readonly ConcurrentDictionary<Player, bool> PlayerStatusList = new ConcurrentDictionary<Player, bool>();

        public static Dictionary<string, bool> GetPlayerStatusList => PlayerStatusList.ToDictionary(x => x.Key.Id, x => x.Value);
        public static List<Player> GetPlayerList => PlayerStatusList.Keys.ToList();

        public static void AddPlayer(Player player, Guid sessionId)
        {
            if (!PlayerSessionList.TryGetValue(sessionId, out _))
            {
                PlayerSessionList.TryAdd(sessionId, player.Id);
                PlayerStatusList.TryAdd(player, true);
            }
        }

        public static void RemovePlayerSession(Guid sessionId)
        {
            PlayerSessionList.TryRemove(sessionId, out var playerId);

            if (PlayerStatusList.Keys.FirstOrDefault(x => x.Id == playerId) is { } player)
            {
                PlayerStatusList[player] = false;
            }
        }

        public static void ClearOffline()
        {
            var playerOfflineList = PlayerStatusList.Where(x => !x.Value).Select(x => x.Key).ToList();

            foreach (var playerOffline in playerOfflineList)
            {
                PlayerStatusList.TryRemove(playerOffline, out _);
            }
        }
    }
}
