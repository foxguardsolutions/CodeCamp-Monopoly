using BoardGame.Commands.Factories;
using BoardGame.Money;

namespace Monopoly.Commands.Factories
{
    public class LuxuryTaxCommandFactory : BalanceModificationCommandFactory
    {
        public const int SpaceIndex = 38;
        private const int AssessedPenalty = -75;
        public LuxuryTaxCommandFactory(IAccountRegistry accounts)
            : base(accounts, new FixedBalanceModification(AssessedPenalty))
        {
        }
    }
}
