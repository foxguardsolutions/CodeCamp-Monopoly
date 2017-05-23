using BoardGame.Commands.Factories;
using BoardGame.Money;

namespace Monopoly.Commands.Factories
{
    public class LuxuryTaxCommandFactory : BalanceModificationCommandFactory
    {
        private const int AssessedPenalty = -75;
        public LuxuryTaxCommandFactory(IAccountRegistry accounts)
            : base(accounts, new FixedBalanceModification(AssessedPenalty))
        {
        }
    }
}
