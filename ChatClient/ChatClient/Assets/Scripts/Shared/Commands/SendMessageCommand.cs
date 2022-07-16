using System;
using System.Collections.Generic;
using Shared.Models;
using Shared.Results;

namespace Shared.Commands
{
    public partial class SendMessageCommand : Command
    {
        public Player Player { get; set; }
        public Message Message { get; set; }

        public SendMessageCommand(Player player, Message message) { Player = player; Message = message; }
        public override string GetCommandName() => GetType().FullName;
        public override Type GetResultType() => typeof(SendMessageResult);
        public override bool IsEditor() => false;
        public override void SetArgs(List<string> args) { }
    }
}