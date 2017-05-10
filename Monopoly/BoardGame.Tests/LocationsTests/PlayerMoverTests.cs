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
        private Space _initialSpace;
        private Mock<IPlayerLocationMap> _mockPlayerLocationMap;
        private Mock<IBoard> _mockBoard;

        private ushort _spacesToMove;
        private Space _finalSpace;

        private PlayerMover _playerMover;

        [SetUp]
        public void SetUp()
        {
            _player = Fixture.Create<IPlayer>();
            _initialSpace = Fixture.Create<Space>();
            GivenMockPlayerLocationMap(_player, _initialSpace);

            _mockBoard = Fixture.Mock<IBoard>();

            _spacesToMove = Fixture.Create<ushort>();
            _finalSpace = Fixture.Create<Space>();
            _mockBoard.Setup(b => b.GetOffsetSpace(_initialSpace, _spacesToMove))
                .Returns(_finalSpace);

            _playerMover = Fixture.Create<PlayerMover>();
        }

        private void GivenMockPlayerLocationMap(IPlayer player, Space space)
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
            _mockBoard.Verify(b => b.GetOffsetSpace(_initialSpace, _spacesToMove));
            _mockPlayerLocationMap.Verify(m => m.SetLocation(_player, _finalSpace));
        }

        [Test]
        public void Place_SetsPlayerLocationOnMap()
        {
            _playerMover.Place(_player, _finalSpace);

            _mockPlayerLocationMap.Verify(m => m.SetLocation(_player, _finalSpace));
        }
    }
}
