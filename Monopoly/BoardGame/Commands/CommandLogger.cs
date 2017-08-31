using System.Text;

namespace BoardGame.Commands
{
    public class CommandLogger : ICommandLogger
    {
        private readonly StringBuilder _log;

        public bool IsEmpty => _log.Length == 0;

        public CommandLogger()
        {
            _log = new StringBuilder();
        }

        public void Log(string action) => _log.AppendLine(action);

        public string Get() => _log.ToString();
    }
}
