using System.Collections.Generic;
using Shared.Models;

namespace Shared.Results
{
    public class GetPlayersResult : CommandResult
    {
        public Dictionary<string, bool> PlayerStatusList { get; set; }
        public List<Player> PlayerList { get; set; }

        public GetPlayersResult()
        {
            PlayerStatusList = new Dictionary<string, bool>();
            PlayerList = new List<Player>();
        }
    }
}
