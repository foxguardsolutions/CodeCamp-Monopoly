using System.Collections.Generic;
using System.Linq;

using BoardGame;
using BoardGame.Commands.Factories;
using BoardGame.Construction;
using Monopoly.Commands.Factories;

namespace Monopoly.Construction
{
    public class MonopolySpaceCommandFactoryBinder : SpaceCommandFactoryBinder
    {
        public const int IncomeTaxSpaceIndex = 4;
        public const int GoToJailSpaceIndex = 30;
        public const int LuxuryTaxSpaceIndex = 38;
        protected sealed override IEnumerable<ICommandFactory> CommandFactories { get; set; }

        public MonopolySpaceCommandFactoryBinder(
            IEnumerable<ISpace> spaces,
            IncomeTaxCommandFactory incomeTaxCommandFactory,
            GoToJailCommandFactory goToJailCommandFactory,
            LuxuryTaxCommandFactory luxuryTaxCommandFactory)
            : base(spaces)
        {
            CommandFactories =
                new List<ICommandFactory>(new ICommandFactory[spaces.Count()])
                {
                    [IncomeTaxSpaceIndex] = incomeTaxCommandFactory,
                    [GoToJailSpaceIndex] = goToJailCommandFactory,
                    [LuxuryTaxSpaceIndex] = luxuryTaxCommandFactory
                };
        }
    }
}
