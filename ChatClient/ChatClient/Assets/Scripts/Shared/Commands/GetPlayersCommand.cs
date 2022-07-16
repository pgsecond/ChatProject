using Shared.Results;
using System;
using System.Collections.Generic;
using Shared.Models;

namespace Shared.Commands
{
    public partial class GetPlayersCommand : Command
    {
        public override string GetCommandName() => GetType().FullName;
        public override Type GetResultType() => typeof(GetPlayersResult);
        public override bool IsEditor() => false;
        public override void SetArgs(List<string> args) { }
    }
}