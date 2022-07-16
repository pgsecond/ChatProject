using System.Collections.Generic;
using Shared.Models;

namespace Shared.Results
{
    public class LoginResult : CommandResult
    {
        public Dictionary<string, bool> PlayerStatusList { get; set; }
        public List<Player> PlayerList { get; set; }
        public List<Message> LastMessageList { get; set; }

        public LoginResult()
        {
            PlayerStatusList = new Dictionary<string, bool>();
            PlayerList = new List<Player>();
            LastMessageList = new List<Message>();
        }
    }
}
