using System;

using BoardGame.Commands.Factories;

namespace BoardGame.Commands.Decorators
{
    public class MonadicCommandFactoryDecorator<TDecoratedCommand> : ICommandFactory
        where TDecoratedCommand : ICommand
    {
        private readonly ICommandFactory _decoratedCommandFactory;
        private readonly Func<ICommand, TDecoratedCommand> _innerCommandFactory;

        public MonadicCommandFactoryDecorator(
            ICommandFactory decoratedCommandFactory,
            Func<ICommand, TDecoratedCommand> innerCommandFactory)
        {
            _decoratedCommandFactory = decoratedCommandFactory;
            _innerCommandFactory = innerCommandFactory;
        }

        public ICommand CreateFor(IPlayer player)
        {
            var decoratedCommand = _decoratedCommandFactory.CreateFor(player);
            return _innerCommandFactory(decoratedCommand);
        }
    }
}
