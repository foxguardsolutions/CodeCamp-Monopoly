using System;

namespace BoardGame.Play
{
    public class RoundBasedEndConditionDetector : IEndConditionDetector
    {
        private readonly uint _roundLimit;
        private uint _roundsComplete;

        public RoundBasedEndConditionDetector(uint totalRoundsInAGame)
        {
            _roundLimit = totalRoundsInAGame;
        }

        public bool IsInEndState()
        {
            return _roundsComplete >= _roundLimit;
        }

        public void Subscribe(IPlayCoordinator playCoordinator)
        {
            playCoordinator.RoundCompleted += OnRoundComplete;
        }

        private void OnRoundComplete(object sender, EventArgs args) => _roundsComplete++;
    }
}
