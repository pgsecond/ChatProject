using System;

namespace Shared.Models
{
    [Serializable]
    public class Batch
    {
        public string CommandName { get; set; }
        public string CommandText { get; set; }

        public Batch(string commandName, string commandText)
        {
            CommandName = commandName;
            CommandText = commandText;
        }
    }
}
