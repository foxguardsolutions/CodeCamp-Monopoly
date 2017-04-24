using BoardGame.Locations;

using NUnit.Framework;
using Ploeh.AutoFixture;

using Tests.Support;

namespace BoardGame.Tests.LocationsTests
{
    public class PlayerLocationMapTests : BaseTest
    {
        [Test]
        public void LocatePlayer_ReturnsPlayersPlacementValue()
        {
            var map = Fixture.Create<PlayerLocationMap>();
            var player = Fixture.Create<IPlayer>();
            var initialSpace = Fixture.Create<Space>();

            map.SetLocation(player, initialSpace);
            var playerLocation = map.Locate(player);

            Assert.That(playerLocation, Is.EqualTo(initialSpace));
        }
    }
}
