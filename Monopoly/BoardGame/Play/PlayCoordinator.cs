using System;
using System.Collections.Generic;

namespace BoardGame.Play
{
    public class PlayCoordinator : IPlayCoordinator
    {
        public event EventHandler RoundCompleted;

        private readonly ITurnFactory _turnFactory;
        private readonly IEndConditionDetector _endConditionDetector;
        private readonly IReadOnlyList<IPlayer> _players;

        public PlayCoordinator(ITurnFactory turnFactory, IEndConditionDetector endConditionDetector, IReadOnlyList<IPlayer> players)
        {
            _turnFactory = turnFactory;
            _players = players;
            _endConditionDetector = endConditionDetector;
        }

        public void Play()
        {
            _endConditionDetector.Subscribe(this);
            while (!_endConditionDetector.IsInEndState())
                PlayRound();
        }

        public void PlayRound()
        {
            foreach (var player in _players)
                TakeATurn(player);
            RoundCompleted?.Invoke(this, EventArgs.Empty);
        }

        private void TakeATurn(IPlayer player)
        {
            var turn = _turnFactory.CreateFor(player);
            turn.Complete();
        }
    }
}
