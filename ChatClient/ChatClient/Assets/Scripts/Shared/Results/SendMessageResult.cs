using Shared.Models;

namespace Shared.Results
{
    public class SendMessageResult : CommandResult
    {
        public Player Player { get; set; }
        public Message Message { get; set; }
    }
}
