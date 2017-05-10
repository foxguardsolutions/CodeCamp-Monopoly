using System.Collections.Generic;
using System.Linq;

using BoardGame.Construction;
using BoardGame.Play;

using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;

using Tests.Support;
using Tests.Support.Extensions;

namespace BoardGame.Tests
{
    public class RunnerTests : BaseTest
    {
        [Test]
        public void RunGame_GivenPlayerNames_CreatesPlayersCreatesGameAndPlaysGame()
        {
            var names = Fixture.CreateMany<string>().ToArray();

            var mockPlayerFactory = Fixture.Mock<IPlayerFactory>();
            var players = GivenPlayersCreatedBy(mockPlayerFactory, names).ToArray();

            var mockPlayCoordinatorFactory = Fixture.Mock<IPlayCoordinatorFactory>();
            var mockPlayCoordinator = GivenMockPlayCoordinatorCreatedBy(mockPlayCoordinatorFactory, players);

            var runner = Fixture.Create<Runner>();

            runner.RunGame(names);

            mockPlayerFactory.Verify(p => p.Create(names));
            mockPlayCoordinatorFactory.Verify(g => g.Create(players));
            mockPlayCoordinator.Verify(g => g.Play());
        }

        private IEnumerable<IPlayer> GivenPlayersCreatedBy(Mock<IPlayerFactory> mockPlayerFactory, IEnumerable<string> names)
        {
            var players = Fixture.CreateMany<IPlayer>();
            mockPlayerFactory.Setup(p => p.Create(names))
                .Returns(players);
            return players;
        }

        private Mock<IPlayCoordinator> GivenMockPlayCoordinatorCreatedBy(
            Mock<IPlayCoordinatorFactory> mockPlayCoordinatorFactory, IEnumerable<IPlayer> players)
        {
            var mockPlayCoordinator = Fixture.Mock<IPlayCoordinator>();
            mockPlayCoordinatorFactory.Setup(g => g.Create(players))
                .Returns(mockPlayCoordinator.Object);
            return mockPlayCoordinator;
        }
    }
}
