using ChatWorkerServer.Interfaces;
using ChatWorkerServer.Managers;
using Shared.Models;
using Shared.Results;

namespace Shared.Commands
{
    partial class GetPlayersCommand : IExecutableClientCommand
    {
        public CommandResult Execute(Player player) => new GetPlayersResult { PlayerList = PlayerManager.GetPlayerList, PlayerStatusList = PlayerManager.GetPlayerStatusList, Success = true};
    }
}
