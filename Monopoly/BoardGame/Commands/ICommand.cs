using System.Collections.Generic;

namespace BoardGame.Commands
{
    public interface ICommand
    {
        string Summary { get; }
        void Execute();
        IEnumerable<ICommand> GetSubsequentCommands();
    }
}
