using BoardGame.Boards;
using BoardGame.Locations;

using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;

using Tests.Support;
using Tests.Support.Extensions;

namespace BoardGame.Tests.LocationsTests
{
    public class PlayerMoverTests : BaseTest
    {
        private IPlayer _player;
        private ISpace _initialSpace;
        private Mock<IPlayerLocationMap> _mockPlayerLocationMap;
        private Mock<IBoard> _mockBoard;

        private ushort _spacesToMove;
        private ISpace _finalSpace;

        private PlayerMover _playerMover;

        [SetUp]
        public void SetUp()
        {
            _player = Fixture.Create<IPlayer>();
            _initialSpace = Fixture.Create<ISpace>();
            GivenMockPlayerLocationMap(_player, _initialSpace);

            _mockBoard = Fixture.Mock<IBoard>();

            _spacesToMove = Fixture.Create<ushort>();
            _finalSpace = Fixture.Create<ISpace>();
            _mockBoard.Setup(b => b.GetOffsetSpace(_initialSpace, _spacesToMove))
                .Returns(_finalSpace);

            _playerMover = Fixture.Create<PlayerMover>();
        }

        private void GivenMockPlayerLocationMap(IPlayer player, ISpace space)
        {
            _mockPlayerLocationMap = Fixture.Mock<IPlayerLocationMap>();
            _mockPlayerLocationMap.Setup(p => p.Locate(player))
                .Returns(space);
        }

        [Test]
        public void Move_LocatesPlayerOnMap()
        {
            _playerMover.Move(_player, _spacesToMove);

            _mockPlayerLocationMap.Verify(m => m.Locate(_player));
        }

        [Test]
        public void Move_GetsDestinationFromBoard()
        {
            _playerMover.Move(_player, _spacesToMove);

            _mockBoard.Verify(b => b.GetOffsetSpace(_initialSpace, _spacesToMove));
        }

        [Test]
        public void Move_SetsNewPlayerLocationToDestinationSpace()
        {
            _playerMover.Move(_player, _spacesToMove);

            _mockPlayerLocationMap.Verify(m => m.SetLocation(_player, _finalSpace));
        }

        [Test]
        public void Move_ReturnsDestinationSpace()
        {
            var actualDestination = _playerMover.Move(_player, _spacesToMove);

            Assert.That(actualDestination, Is.EqualTo(_finalSpace));
        }

        [Test]
        public void Place_SetsPlayerLocationOnMap()
        {
            _playerMover.Place(_player, _finalSpace);

            _mockPlayerLocationMap.Verify(m => m.SetLocation(_player, _finalSpace));
        }
    }
}
