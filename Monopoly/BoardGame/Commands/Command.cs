using System.Collections.Generic;

namespace BoardGame.Commands
{
    public abstract class Command : ICommand
    {
        public virtual string Summary { get; protected set; }
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
