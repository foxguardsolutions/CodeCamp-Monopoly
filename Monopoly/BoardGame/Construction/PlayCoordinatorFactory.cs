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

        private readonly IGameStateConfigurationInitializer _gameStateConfigurationInitializer;

        public PlayCoordinatorFactory(
            ITurnFactory turnFactory,
            IEndConditionDetector endConditionDetector,
            IPlayerCountConstraint playerCountConstraint,
            IShuffler playerShuffler,
            IGameStateConfigurationInitializer gameStateConfigurationInitializer)
        {
            _turnFactory = turnFactory;
            _endConditionDetector = endConditionDetector;

            _playerCountConstraint = playerCountConstraint;
            _playerShuffler = playerShuffler;
            _gameStateConfigurationInitializer = gameStateConfigurationInitializer;
        }

        public IPlayCoordinator Create(IEnumerable<IPlayer> players)
        {
            var shuffledPlayers = _playerShuffler.Shuffle(players).ToList();
            if (!_playerCountConstraint.IsSatisfiedBy(shuffledPlayers))
                return default(IPlayCoordinator);

            _gameStateConfigurationInitializer.ConfigureGame(shuffledPlayers);
            return new PlayCoordinator(_turnFactory, _endConditionDetector, shuffledPlayers);
        }
    }
}
