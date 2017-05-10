using BoardGame.Dice;
using BoardGame.Locations;
using BoardGame.Play;

using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;

using Tests.Support;
using Tests.Support.Extensions;

namespace BoardGame.Tests.PlayTests
{
    public class TurnTests : BaseTest
    {
        private ushort _rollValue;
        private Mock<IDice> _mockDice;
        private Mock<IPlayerMover> _mockPlayerMover;
        private IPlayer _player;

        private Turn _turn;

        [SetUp]
        public void SetUp()
        {
            _rollValue = Fixture.Create<ushort>();
            _mockDice = GivenMockDice(_rollValue);
            _mockPlayerMover = Fixture.Mock<IPlayerMover>();
            _player = Fixture.Freeze<IPlayer>();

            _turn = Fixture.Create<Turn>();
        }

        private Mock<IDice> GivenMockDice(ushort rollValue)
        {
            var mockRoll = GivenMockRoll(rollValue);

            var mockDice = Fixture.Mock<IDice>();
            mockDice.Setup(d => d.Roll()).Returns(mockRoll.Object);
            return mockDice;
        }

        private Mock<IRoll> GivenMockRoll(ushort rollValue)
        {
            var mockRoll = Fixture.Create<Mock<IRoll>>();
            mockRoll.Setup(r => r.Value).Returns(rollValue);
            return mockRoll;
        }

        [Test]
        public void TakeTurn_RollsDiceAndMovesPlayer()
        {
            _turn.Execute();

            _mockDice.Verify(d => d.Roll());
            _mockPlayerMover.Verify(p => p.Move(_player, _rollValue));
        }
    }
}
