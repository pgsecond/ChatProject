using ChatWorkerServer.Interfaces;
using Shared.Models;
using Shared.Results;

namespace Shared.Commands
{
    partial class SendMessageCommand : IExecutableClientCommand
    {
        public CommandResult Execute(Player player) => new SendMessageResult {Player = player, Message = Message, Success = true};
    }
}
