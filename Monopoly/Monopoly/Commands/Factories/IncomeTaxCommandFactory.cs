using System;

using BoardGame;
using BoardGame.Commands;
using BoardGame.Commands.Factories;
using BoardGame.Money;

namespace Monopoly.Commands.Factories
{
    public class IncomeTaxCommandFactory : DyadicCommandFactory<IBalanceModification, UpdatePlayerBalanceCommand>
    {
        public const int SpaceIndex = 4;
        private const int PenaltyPercentage = 10;
        private const int PenaltyCap = 200;
        public IncomeTaxCommandFactory(Func<IPlayer, IBalanceModification, UpdatePlayerBalanceCommand> innerCommandFactory)
            : base(new ProportionalPenaltyWithCap(PenaltyPercentage, PenaltyCap), innerCommandFactory)
        {
        }
    }
}
