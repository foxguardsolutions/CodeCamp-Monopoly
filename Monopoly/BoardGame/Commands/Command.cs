using System.Collections.Generic;

namespace BoardGame.Commands
{
    public abstract class Command : ICommand
    {
        public ICommandLogger Logger { get; }
        protected ICollection<ICommand> SubsequentCommands { get; }

        protected Command(ICommandLogger logger)
        {
            Logger = logger;
            SubsequentCommands = new List<ICommand>();
        }

        public abstract void Execute();

        public virtual IEnumerable<ICommand> GetSubsequentCommands()
        {
            return SubsequentCommands;
        }
    }
}
