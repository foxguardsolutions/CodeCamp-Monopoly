using BoardGame.Locations;

namespace BoardGame.Construction
{
    public class SingleSpaceInitialPlacementHandler : IInitialPlacementHandler
    {
        private readonly ISpace _initialSpace;
        private readonly IPlayerMover _playerMover;

        public SingleSpaceInitialPlacementHandler(ISpace initialSpace, IPlayerMover playerMover)
        {
            _initialSpace = initialSpace;
            _playerMover = playerMover;
        }

        public void Place(IPlayer player)
        {
            _playerMover.Place(player, _initialSpace);
        }
    }
}
