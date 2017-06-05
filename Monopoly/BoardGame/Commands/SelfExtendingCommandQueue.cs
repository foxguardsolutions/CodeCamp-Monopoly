using System.Collections.Generic;

using BoardGame.Commands.Factories;

namespace BoardGame.Commands
{
    public class SelfExtendingCommandQueue : ICommandQueue
    {
        private readonly ICommandFactory _turnInitializationCommandFactory;
        private readonly Queue<ICommand> _commands;

        public SelfExtendingCommandQueue(ICommandFactory turnInitializationCommandFactory)
        {
            _turnInitializationCommandFactory = turnInitializationCommandFactory;
            _commands = new Queue<ICommand>();
        }

        public void InitializeFor(IPlayer player)
        {
            var initialCommand = _turnInitializationCommandFactory.CreateFor(player);
            _commands.Enqueue(initialCommand);
        }

        public void ExecuteCommands()
        {
            while (_commands.Count > 0)
            {
                var command = _commands.Dequeue();
                Process(command);
            }
        }

        private void Process(ICommand command)
        {
            command.Execute();

            var subsequentCommands = command.GetSubsequentCommands();
            EnqueueAll(subsequentCommands);
        }

        private void EnqueueAll(IEnumerable<ICommand> commands)
        {
            foreach (var command in commands)
                _commands.Enqueue(command);
        }
    }
}
