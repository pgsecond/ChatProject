using ChatWorkerServer.Interfaces;
using ChatWorkerServer.Managers;
using Shared.Models;
using Shared.Results;

namespace Shared.Commands
{
    partial class LoginCommand : IExecutableClientCommand
    {
        public CommandResult Execute(Player player) => new LoginResult {PlayerList = PlayerManager.GetPlayerList, PlayerStatusList = PlayerManager.GetPlayerStatusList, LastMessageList = MessageManager.GetLastMessageList, Success = true};
    }
}
