using System.Collections.Generic;

using BoardGame.Construction;
using BoardGame.Play;

namespace BoardGame
{
    public class Runner
    {
        private readonly IPlayerFactory _playerFactory;
        private readonly IPlayCoordinatorFactory _playCoordinatorFactory;
        private IPlayCoordinator _playCoordinator;

        public Runner(IPlayerFactory playerFactory, IPlayCoordinatorFactory playCoordinatorFactory)
        {
            _playerFactory = playerFactory;
            _playCoordinatorFactory = playCoordinatorFactory;
        }

        public void RunGame(IEnumerable<string> playerNames)
        {
            _playCoordinator = ConstructPlayCoordinator(playerNames);
            _playCoordinator.Play();
        }

        private IPlayCoordinator ConstructPlayCoordinator(IEnumerable<string> playerNames)
        {
            var players = _playerFactory.Create(playerNames);
            return _playCoordinatorFactory.Create(players);
        }
    }
}
