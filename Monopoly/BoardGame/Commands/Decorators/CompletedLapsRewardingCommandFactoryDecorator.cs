using BoardGame.Boards;
using BoardGame.Commands.Factories;

namespace BoardGame.Commands.Decorators
{
    public class CompletedLapsRewardingCommandFactoryDecorator : ICommandFactory
    {
        private readonly ICommandFactory _decoratedCommandFactory;
        private readonly ILapCounter _lapCounter;
        private readonly ICommandFactory _lapRewardCommandFactory;

        public CompletedLapsRewardingCommandFactoryDecorator(ICommandFactory decoratedCommandFactory, ILapCounter lapCounter, ICommandFactory lapRewardCommandFactory)
        {
            _decoratedCommandFactory = decoratedCommandFactory;
            _lapCounter = lapCounter;
            _lapRewardCommandFactory = lapRewardCommandFactory;
        }

        public ICommand CreateFor(IPlayer player)
        {
            var decoratedCommand = _decoratedCommandFactory.CreateFor(player);
            var turnInitializationCommand = new CompletedLapsRewardingCommandDecorator(player, decoratedCommand, _lapCounter, _lapRewardCommandFactory);
            return turnInitializationCommand;
        }
    }
}
