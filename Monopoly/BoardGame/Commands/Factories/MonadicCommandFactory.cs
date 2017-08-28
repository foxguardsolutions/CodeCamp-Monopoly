using System;

namespace BoardGame.Commands.Factories
{
    public class MonadicCommandFactory<TCommand> : ICommandFactory
        where TCommand : ICommand
    {
        private readonly Func<IPlayer, TCommand> _innerCommandFactory;

        public MonadicCommandFactory(Func<IPlayer, TCommand> innerCommandFactory)
        {
            _innerCommandFactory = innerCommandFactory;
        }

        public ICommand CreateFor(IPlayer player) => _innerCommandFactory(player);
    }
}
