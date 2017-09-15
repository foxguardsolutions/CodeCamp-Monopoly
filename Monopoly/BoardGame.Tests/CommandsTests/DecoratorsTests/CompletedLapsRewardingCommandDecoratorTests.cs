using System.Collections.Generic;
using System.Linq;

using BoardGame.Boards;
using BoardGame.Commands;
using BoardGame.Commands.Decorators;
using BoardGame.Commands.Factories;

using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;

using Tests.Support.Extensions;

namespace BoardGame.Tests.CommandsTests.DecoratorsTests
{
    public class CompletedLapsRewardingCommandDecoratorTests : CommandDecoratorTests
    {
        private Mock<ILapCounter> _mockLapCounter;
        private uint _lapsComplete;

        private Mock<ICommandFactory> _mockCommandFactory;
        private ICommand _rewardCommand;

        protected override void SetUpDecoratorDependencies()
        {
            _mockLapCounter = Fixture.Mock<ILapCounter>();
            _lapsComplete = GivenLapsCountedBy(_mockLapCounter);

            _mockCommandFactory = Fixture.Mock<ICommandFactory>();
            _rewardCommand = GivenRewardCommandCreatedBy(_mockCommandFactory);
        }

        [Test]
        public void Execute_ResetsLapCounter()
        {
            DecoratorCommand.Execute();

            _mockLapCounter.Verify(l => l.Reset());
        }

        [Test]
        public void Execute_CreatesNewRewardCommandForEachLapCompleted()
        {
            DecoratorCommand.Execute();

            _mockLapCounter.Verify(l => l.GetLapsCompleted());
            _mockCommandFactory.Verify(
                c => c.CreateFor(It.IsAny<IPlayer>()),
                Times.Exactly((int)_lapsComplete));
        }

        [Test]
        public void GetSubsequentCommands_GivenDecoratorCommandExecuted_YieldsRewardCommandAndSubsequentCommandsFromDecoratedCommand()
        {
            DecoratorCommand.Execute();

            var commandsResultingFromExecutionOfRewardDecorator = DecoratorCommand.GetSubsequentCommands();

            Assert.That(
                commandsResultingFromExecutionOfRewardDecorator,
                Is.EqualTo(RewardCommands.Concat(AdditionalCommandsProducedByDecoratedCommand)));
        }

        protected override ICommand Given_DecoratorCommand()
        {
            return Fixture.Create<CompletedLapsRewardingCommandDecorator>();
        }

        private uint GivenLapsCountedBy(Mock<ILapCounter> mockLapCounter)
        {
            var lapsComplete = Fixture.Create<uint>();
            mockLapCounter.Setup(l => l.GetLapsCompleted())
                .Returns(lapsComplete);
            return lapsComplete;
        }

        private ICommand GivenRewardCommandCreatedBy(
            Mock<ICommandFactory> mockCommandFactory)
        {
            var rewardCommand = Fixture.Create<ICommand>();
            mockCommandFactory.Setup(
                    c => c.CreateFor(It.IsAny<IPlayer>()))
                .Returns(rewardCommand);
            return rewardCommand;
        }

        private IEnumerable<ICommand> RewardCommands
        {
            get
            {
                for (var lap = 0; lap < _lapsComplete; lap++)
                    yield return _rewardCommand;
            }
        }
    }
}
