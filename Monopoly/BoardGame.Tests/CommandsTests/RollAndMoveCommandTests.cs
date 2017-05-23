using BoardGame.Commands;
using BoardGame.Dice;

using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;

using Tests.Support.Extensions;

namespace BoardGame.Tests.CommandsTests
{
    public class RollAndMoveCommandTests : MoveCommandTests
    {
        private Mock<IDice> _mockDice;

        [SetUp]
        public void SetUp()
        {
            _mockDice = Fixture.Mock<IDice>();

            Command = Fixture.Create<RollAndMoveCommand>();
        }

        [Test]
        public void Execute_RollsDiceAndMovesPlayer()
        {
            var rollValue = GivenRollValueFrom(_mockDice);

            Command.Execute();

            _mockDice.Verify(d => d.Roll());
            MockPlayerMover.Verify(p => p.Move(Player, rollValue));
        }

        private ushort GivenRollValueFrom(Mock<IDice> mockDice)
        {
            var rollValue = Fixture.Create<ushort>();
            var mockRoll = GivenMockRollWithValue(rollValue);
            mockDice.Setup(d => d.Roll()).Returns(mockRoll.Object);
            return rollValue;
        }

        private Mock<IRoll> GivenMockRollWithValue(ushort rollValue)
        {
            var mockRoll = Fixture.Mock<IRoll>();
            mockRoll.Setup(r => r.Value).Returns(rollValue);
            return mockRoll;
        }

        protected override void GivenDestinationSpaceWithoutCommandFactory()
        {
            var mockSpace = GivenMockSpace();
            mockSpace.SetupProperty(s => s.CommandFactory, null);
        }

        protected override void GivenDestinationSpaceWithCommandFactory()
        {
            GivenMockSpace();
        }

        private Mock<ISpace> GivenMockSpace()
        {
            var mockSpace = Fixture.Mock<ISpace>();
            MockPlayerMover.Setup(p => p.Move(It.IsAny<IPlayer>(), It.IsAny<ushort>()))
                .Returns(mockSpace.Object);
            return mockSpace;
        }
    }
}
