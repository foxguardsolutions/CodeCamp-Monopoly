using System.Collections.Generic;
using System.Linq;

using BoardGame.Boards;
using BoardGame.Commands.Factories;

namespace BoardGame.Commands.Decorators
{
    public class CompletedLapsRewardingCommandDecorator : Command
    {
        private readonly IPlayer _player;
        private readonly ICommand _decoratedCommand;
        private readonly ILapCounter _lapCounter;
        private readonly ICommandFactory _rewardCommandFactory;

        public CompletedLapsRewardingCommandDecorator(
            IPlayer player,
            ICommand decoratedCommand,
            ILapCounter lapCounter,
            ICommandFactory rewardCommandFactory)
        {
            _player = player;
            _decoratedCommand = decoratedCommand;
            _lapCounter = lapCounter;
            _rewardCommandFactory = rewardCommandFactory;
        }

        public override void Execute()
        {
            _lapCounter.Reset();
            _decoratedCommand.Execute();
            AddRewardForEachLap();
        }

        private void AddRewardForEachLap()
        {
            var numberOfLaps = _lapCounter.GetLapsCompleted();
            for (var lap = 0; lap < numberOfLaps; lap++)
                AddReward();
        }

        private void AddReward()
        {
            var reward = _rewardCommandFactory.CreateFor(_player);
            SubsequentCommands.Add(reward);
        }

        public override IEnumerable<ICommand> GetSubsequentCommands()
        {
            return SubsequentCommands.Concat(_decoratedCommand.GetSubsequentCommands());
        }
    }
}
