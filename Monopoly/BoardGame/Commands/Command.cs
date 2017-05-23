using System.Collections.Generic;

namespace BoardGame.Commands
{
    public abstract class Command : ICommand
    {
        protected ICollection<ICommand> SubsequentCommands { get; }

        protected Command()
        {
            SubsequentCommands = new List<ICommand>();
        }

        public abstract void Execute();

        public virtual IEnumerable<ICommand> GetSubsequentCommands()
        {
            return SubsequentCommands;
        }
    }
}
