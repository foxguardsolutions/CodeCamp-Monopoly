using System.Collections.Generic;

using BoardGame.Money;

namespace BoardGame.Construction
{
    public class GameStateConfigurationInitializer : IGameStateConfigurationInitializer
    {
        private readonly IInitialPlacementHandler _initialPlacementHandler;
        private readonly IAccountRegistry _accountRegistry;

        public GameStateConfigurationInitializer(
            IInitialPlacementHandler initialPlacementHandler,
            IAccountRegistry accountRegistry)
        {
            _initialPlacementHandler = initialPlacementHandler;
            _accountRegistry = accountRegistry;
        }

        public void ConfigureGame(IEnumerable<IPlayer> players)
        {
            players.ForEach(PlaceAndRegister);
        }

        private void PlaceAndRegister(IPlayer player)
        {
            _initialPlacementHandler.Place(player);
            _accountRegistry.RegisterAccount(player);
        }
    }
}
