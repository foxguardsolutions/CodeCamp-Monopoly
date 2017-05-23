using System.Collections.Generic;

using BoardGame.Construction;
using BoardGame.Money;

using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;

using Tests.Support;
using Tests.Support.Extensions;

namespace BoardGame.Tests.ConstructionTests
{
    public class GameStateConfigurationInitializerTests : BaseTest
    {
        private IEnumerable<IPlayer> _players;
        private Mock<IInitialPlacementHandler> _mockInitialPlacementHandler;
        private Mock<IAccountRegistry> _mockAccountRegistry;
        private InitialGameStateConfigurator _configurator;

        [SetUp]
        public void SetUp()
        {
            _players = Fixture.CreateMany<IPlayer>();

            _mockInitialPlacementHandler = Fixture.Mock<IInitialPlacementHandler>();
            _mockAccountRegistry = Fixture.Mock<IAccountRegistry>();

            _configurationInitializer = Fixture.Create<GameStateConfigurationInitializer>();
        }

        [Test]
        public void ConfigureGame_GivenPlayers_PlacesPlayersOnBoardRegistersAccountsAndBindsStrategiesToSpaces()
        {
            _configurationInitializer.ConfigureGame(_players);

            VerifyAllPlayersArePlaced(_mockInitialPlacementHandler, _players);
            VerifyAllPlayersHaveAccountsRegistered(_mockAccountRegistry, _players);
        }

        private static void VerifyAllPlayersArePlaced(
            Mock<IInitialPlacementHandler> mockInitialPlacementHandler, IEnumerable<IPlayer> players)
        {
            foreach (var player in players)
                mockInitialPlacementHandler.Verify(i => i.Place(player));
        }

        private static void VerifyAllPlayersHaveAccountsRegistered(
            Mock<IAccountRegistry> mockAccountRegistry, IEnumerable<IPlayer> players)
        {
            foreach (var player in players)
                mockAccountRegistry.Verify(a => a.RegisterAccount(player));
        }
    }
}
