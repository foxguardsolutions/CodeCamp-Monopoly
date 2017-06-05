using System.Collections.Generic;

namespace BoardGame.Commands
{
    public interface ICommand
    {
        void Execute();
        IEnumerable<ICommand> GetSubsequentCommands();
    }
}
