using Shared.Models;
using Shared.Results;

namespace ChatWorkerServer.Interfaces
{
    public interface IExecutableClientCommand
    {
        CommandResult Execute(Player player);
    }
}
