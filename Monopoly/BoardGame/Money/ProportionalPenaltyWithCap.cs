using System;

namespace BoardGame.Money
{
    public class ProportionalPenaltyWithCap : IBalanceModification
    {
        private readonly int _penaltyPercentage;
        private readonly int _maximumPenalty;

        public ProportionalPenaltyWithCap(int penaltyPercentage, int maximumPenalty)
        {
            _penaltyPercentage = penaltyPercentage;
            _maximumPenalty = maximumPenalty;
        }

        public int GetNewBalance(int initialBalance)
        {
            var penalty = GetPenalty(initialBalance);
            return initialBalance - penalty;
        }

        private int GetPenalty(int initialBalance)
        {
            var calculatedPenalty = CalculatePenalty(initialBalance);
            return Math.Min(calculatedPenalty, _maximumPenalty);
        }

        private int CalculatePenalty(int initialBalance)
        {
            return initialBalance * _penaltyPercentage / 100;
        }
    }
}
