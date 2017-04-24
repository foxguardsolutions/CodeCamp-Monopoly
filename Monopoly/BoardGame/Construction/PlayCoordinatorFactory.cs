using System.Collections.Generic;
using System.Linq;

using BoardGame.Play;
using Shuffler;

namespace BoardGame.Construction
{
    public class PlayCoordinatorFactory : IPlayCoordinatorFactory
    {
        private readonly ITurnFactory _turnFactory;
        private readonly IEndConditionDetector _endConditionDetector;

        private readonly IPlayerCountConstraint _playerCountConstraint;
        private readonly IShuffler _playerShuffler;
        private readonly IInitialPlacementHandler _initialPlacementHandler;

        public PlayCoordinatorFactory(
            ITurnFactory turnFactory, IEndConditionDetector endConditionDetector, IPlayerCountConstraint playerCountConstraint, IShuffler playerShuffler, IInitialPlacementHandler initialPlacementHandler)
        {
            _turnFactory = turnFactory;
            _endConditionDetector = endConditionDetector;

            _playerCountConstraint = playerCountConstraint;
            _playerShuffler = playerShuffler;
            _initialPlacementHandler = initialPlacementHandler;
        }

        public IPlayCoordinator Create(IEnumerable<IPlayer> players)
        {
            var shuffledPlayers = _playerShuffler.Shuffle(players).ToList();
            if (!_playerCountConstraint.IsSatisfiedBy(shuffledPlayers))
                return null;

            PlaceOnBoard(shuffledPlayers);
            return new PlayCoordinator(_turnFactory, _endConditionDetector, shuffledPlayers);
        }

        private void PlaceOnBoard(List<IPlayer> players)
        {
            players.ForEach(player => _initialPlacementHandler.Place(player));
        }
    }
}
