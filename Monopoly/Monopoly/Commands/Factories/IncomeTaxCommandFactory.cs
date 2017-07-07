using BoardGame.Commands.Factories;
using BoardGame.Money;

namespace Monopoly.Commands.Factories
{
    public class IncomeTaxCommandFactory : BalanceModificationCommandFactory
    {
        public const int SpaceIndex = 4;
        private const int PenaltyPercentage = 10;
        private const int PenaltyCap = 200;
        public IncomeTaxCommandFactory(IAccountRegistry accounts)
            : base(accounts, new ProportionalPenaltyWithCap(PenaltyPercentage, PenaltyCap))
        {
        }
    }
}
