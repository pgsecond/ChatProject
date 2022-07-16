namespace Shared.Results
{
    public class CommandResult
    {
        public bool Success { get; set; }
        public string ResultTypeName => GetType().FullName;
    }
}
