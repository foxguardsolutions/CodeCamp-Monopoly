using System;

using BoardGame;
using BoardGame.Commands;
using BoardGame.Commands.Factories;
using BoardGame.Money;

namespace Monopoly.Commands.Factories
{
    public class LuxuryTaxCommandFactory : DyadicCommandFactory<IBalanceModification, UpdatePlayerBalanceCommand>
    {
        public const int SpaceIndex = 38;
        private const int AssessedPenalty = -75;
        public LuxuryTaxCommandFactory(Func<IPlayer, IBalanceModification, UpdatePlayerBalanceCommand> innerCommandFactory)
            : base(new FixedBalanceModification(AssessedPenalty), innerCommandFactory)
        {
        }
    }
}
