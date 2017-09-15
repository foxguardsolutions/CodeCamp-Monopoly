using System.Collections.Generic;

namespace BoardGame.Commands
{
    public interface ICommand
    {
        ICommandLogger Logger { get; }
        void Execute();
        IEnumerable<ICommand> GetSubsequentCommands();
    }
}
