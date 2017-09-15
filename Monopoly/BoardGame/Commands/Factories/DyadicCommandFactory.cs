using System;

namespace BoardGame.Commands.Factories
{
    public class DyadicCommandFactory<TArgument, TCommand> : ICommandFactory
        where TCommand : ICommand
    {
        private readonly TArgument _argument;
        private readonly Func<IPlayer, TArgument, TCommand> _innerCommandFactory;

        public DyadicCommandFactory(TArgument argument, Func<IPlayer, TArgument, TCommand> innerCommandFactory)
        {
            _argument = argument;
            _innerCommandFactory = innerCommandFactory;
        }

        public ICommand CreateFor(IPlayer player) => _innerCommandFactory(player, _argument);
    }
}
