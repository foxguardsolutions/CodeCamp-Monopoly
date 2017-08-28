using System;

using BoardGame.Commands.Factories;

namespace BoardGame.Commands.Decorators
{
    public class CompletedLapsRewardingCommandFactoryDecorator : ICommandFactory
    {
        private readonly ICommandFactory _decoratedCommandFactory;
        private readonly ICommandFactory _lapRewardCommandFactory;
        private readonly Func<IPlayer, ICommand, ICommandFactory, CompletedLapsRewardingCommandDecorator> _innerCommandFactory;

        public CompletedLapsRewardingCommandFactoryDecorator(
            ICommandFactory decoratedCommandFactory,
            ICommandFactory lapRewardCommandFactory,
            Func<IPlayer, ICommand, ICommandFactory, CompletedLapsRewardingCommandDecorator> innerCommandFactory)
        {
            _decoratedCommandFactory = decoratedCommandFactory;
            _lapRewardCommandFactory = lapRewardCommandFactory;
            _innerCommandFactory = innerCommandFactory;
        }

        public ICommand CreateFor(IPlayer player)
        {
            var decoratedCommand = _decoratedCommandFactory.CreateFor(player);
            return _innerCommandFactory(player, decoratedCommand, _lapRewardCommandFactory);
        }
    }
}
