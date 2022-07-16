using System;
using System.Collections.Generic;
using Shared.Models;
using Shared.Results;

namespace Shared.Commands
{
    public partial class LoginCommand : Command
    {
        public Player Player { get; set; }

        public LoginCommand(Player player) { Player = player; }
        public override string GetCommandName() => GetType().FullName;
        public override Type GetResultType() => typeof(LoginResult);
        public override bool IsEditor() => false;
        public override void SetArgs(List<string> args) { }
    }
}