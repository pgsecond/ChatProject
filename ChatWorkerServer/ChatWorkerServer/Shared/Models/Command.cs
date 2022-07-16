using System;
using System.Collections.Generic;

namespace Shared.Models
{
    [Serializable]
    public abstract class Command
    {
        public abstract string GetCommandName();
        public abstract Type GetResultType();
        public abstract bool IsEditor();
        public abstract void SetArgs(List<string> args);
    }
}
