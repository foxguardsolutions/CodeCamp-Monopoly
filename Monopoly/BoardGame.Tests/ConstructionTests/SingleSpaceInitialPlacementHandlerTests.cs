using BoardGame.Construction;
using BoardGame.Locations;

using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;

using Tests.Support;
using Tests.Support.Extensions;

namespace BoardGame.Tests.ConstructionTests
{
    public class SingleSpaceInitialPlacementHandlerTests : BaseTest
    {
        private Space _initialSpace;
        private Mock<IPlayerMover> _mockPlayerMover;
        private IPlayer _player;

        private SingleSpaceInitialPlacementHandler _initialPlacementHandler;

        [SetUp]
        public void SetUp()
        {
            _initialSpace = Fixture.Freeze<Space>();
            _mockPlayerMover = Fixture.Mock<IPlayerMover>();
            _initialPlacementHandler = Fixture.Create<SingleSpaceInitialPlacementHandler>();

            _player = Fixture.Create<IPlayer>();
        }

        [Test]
        public void Place_GivenInitialSpaceNumberAndPlayerMover_PlacesPlayerOnBoard()
        {
            _initialPlacementHandler.Place(_player);

            _mockPlayerMover.Verify(p => p.Place(_player, _initialSpace));
        }
    }
}
