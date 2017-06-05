using System.Collections.Generic;
using System.Linq;

using BoardGame.Commands;
using BoardGame.Commands.Factories;

using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;

using Tests.Support;
using Tests.Support.Extensions;

namespace BoardGame.Tests.CommandsTests
{
    public class SelfExtendingCommandQueueTests : BaseTest
    {
        private IPlayer _player;
        private Mock<ICommandFactory> _mockCommandFactory;
        private Mock<ICommand> _mockInitialCommand;
        private IEnumerable<Mock<ICommand>> _mockSubsequentCommands;

        private SelfExtendingCommandQueue _queue;

        [SetUp]
        public void SetUp()
        {
            _player = Fixture.Create<IPlayer>();
            _mockCommandFactory = Fixture.Mock<ICommandFactory>();
            _mockInitialCommand = GivenMockCommandCreatedBy(_mockCommandFactory);
            _mockSubsequentCommands = GivenSubsequentCommandsOf(_mockInitialCommand);

            _queue = Fixture.Create<SelfExtendingCommandQueue>();
        }

        [Test]
        public void InitializeFor_GivenPlayer_CreatesACommandForThePlayer()
        {
            _queue.InitializeFor(_player);

            _mockCommandFactory.Verify(c => c.CreateFor(_player));
        }

        [Test]
        public void ExecuteCommands_GivenInitializedQueue_ExecutesCreatedCommandAndAllItsSubsequentCommands()
        {
            _queue.InitializeFor(_player);

            _queue.ExecuteCommands();

            _mockInitialCommand.Verify(c => c.Execute());
            VerifyAllWereExecuted(_mockSubsequentCommands);
        }

        private static void VerifyAllWereExecuted(IEnumerable<Mock<ICommand>> mockCommands)
        {
            foreach (var mockCommand in mockCommands)
                mockCommand.Verify(c => c.Execute());
        }

        private Mock<ICommand> GivenMockCommandCreatedBy(Mock<ICommandFactory> mockCommandFactory)
        {
            var mockCommand = Fixture.Create<Mock<ICommand>>();
            mockCommandFactory.Setup(c => c.CreateFor(_player))
                .Returns(mockCommand.Object);
            return mockCommand;
        }

        private IEnumerable<Mock<ICommand>> GivenSubsequentCommandsOf(Mock<ICommand> mockCommand)
        {
            var mockSubsequentCommands = Fixture.CreateMany<Mock<ICommand>>().ToArray();
            mockCommand.Setup(c => c.GetSubsequentCommands())
                .Returns(mockSubsequentCommands.Select(c => c.Object));
            return mockSubsequentCommands;
        }
    }
}
