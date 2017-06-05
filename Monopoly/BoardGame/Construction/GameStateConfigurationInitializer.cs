using System.Collections.Generic;

using BoardGame.Money;

namespace BoardGame.Construction
{
    public class GameStateConfigurationInitializer : IGameStateConfigurationInitializer
    {
        private readonly IInitialPlacementHandler _initialPlacementHandler;
        private readonly IAccountRegistry _accountRegistry;
        private readonly ISpaceCommandFactoryBinder _spaceCommandFactoryBinder;

        public GameStateConfigurationInitializer(
            IInitialPlacementHandler initialPlacementHandler,
            IAccountRegistry accountRegistry,
            ISpaceCommandFactoryBinder spaceCommandFactoryBinder)
        {
            _initialPlacementHandler = initialPlacementHandler;
            _accountRegistry = accountRegistry;
            _spaceCommandFactoryBinder = spaceCommandFactoryBinder;
        }

        public void ConfigureGame(IEnumerable<IPlayer> players)
        {
            foreach (var player in players)
                PlaceAndRegister(player);
            _spaceCommandFactoryBinder.BindCommandFactoriesToSpaces();
        }

        private void PlaceAndRegister(IPlayer player)
        {
            _initialPlacementHandler.Place(player);
            _accountRegistry.RegisterAccount(player);
        }
    }
}
