using System.Collections.Generic;
using System.Linq;

using BoardGame.Boards;
using BoardGame.Commands;
using BoardGame.Commands.Decorators;
using BoardGame.Commands.Factories;

using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;

using Tests.Support;
using Tests.Support.Extensions;

namespace BoardGame.Tests.CommandsTests.DecoratorsTests
{
    public class CompletedLapsRewardingCommandDecoratorTests : BaseTest
    {
        private Mock<ILapCounter> _mockLapCounter;
        private uint _lapsComplete;

        private Mock<ICommand> _mockDecoratedCommand;
        private IEnumerable<ICommand> _additionalCommandsProducedByDecoratedCommand;

        private Mock<ICommandFactory> _mockCommandFactory;
        private ICommand _rewardCommand;

        private CompletedLapsRewardingCommandDecorator _decoratorCommand;

        [SetUp]
        public void SetUp()
        {
            _mockLapCounter = Fixture.Mock<ILapCounter>();
            _lapsComplete = GivenLapsCountedBy(_mockLapCounter);

            _mockDecoratedCommand = Fixture.Mock<ICommand>();
            _additionalCommandsProducedByDecoratedCommand = GivenCommandsResultingFromExecutionOf(_mockDecoratedCommand);

            _mockCommandFactory = Fixture.Mock<ICommandFactory>();
            _rewardCommand = GivenRewardCommandCreatedBy(_mockCommandFactory);

            _decoratorCommand = Fixture.Create<CompletedLapsRewardingCommandDecorator>();
        }

        private uint GivenLapsCountedBy(Mock<ILapCounter> mockLapCounter)
        {
            var lapsComplete = Fixture.Create<uint>();
            mockLapCounter.Setup(l => l.GetLapsCompleted())
                .Returns(lapsComplete);
            return lapsComplete;
        }

        private IEnumerable<ICommand> GivenCommandsResultingFromExecutionOf(
            Mock<ICommand> mockDecoratedCommand)
        {
            var subsequentCommands = Fixture.CreateMany<ICommand>().ToArray();
            mockDecoratedCommand.Setup(c => c.GetSubsequentCommands())
                .Returns(subsequentCommands);
            return subsequentCommands;
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

        [Test]
        public void Execute_ResetsLapCounter()
        {
            _decoratorCommand.Execute();

            _mockLapCounter.Verify(l => l.Reset());
        }

        [Test]
        public void Execute_ExecutesDecoratedCommand()
        {
            _decoratorCommand.Execute();

            _mockDecoratedCommand.Verify(c => c.Execute());
        }

        [Test]
        public void Execute_CreatesNewRewardCommandForEachLapCompleted()
        {
            _decoratorCommand.Execute();

            _mockLapCounter.Verify(l => l.GetLapsCompleted());
            _mockCommandFactory.Verify(
                c => c.CreateFor(It.IsAny<IPlayer>()),
                Times.Exactly((int)_lapsComplete));
        }

        [Test]
        public void GetSubsequentCommands_GivenDecoratorCommandExecuted_YieldsRewardCommandAndSubsequentCommandsFromDecoratedCommand()
        {
            _decoratorCommand.Execute();

            var commandsResultingFromExecutionOfRewardDecorator = _decoratorCommand.GetSubsequentCommands();

            Assert.That(
                commandsResultingFromExecutionOfRewardDecorator,
                Is.EqualTo(RewardCommands.Concat(_additionalCommandsProducedByDecoratedCommand)));
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
