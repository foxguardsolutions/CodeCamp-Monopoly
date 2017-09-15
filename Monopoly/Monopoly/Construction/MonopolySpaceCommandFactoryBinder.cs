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
        protected sealed override IEnumerable<ICommandFactory> CommandFactories { get; set; }

        public MonopolySpaceCommandFactoryBinder(
            IEnumerable<ISpace> spaces,
            IncomeTaxCommandFactory incomeTaxCommandFactory,
            GoToJailCommandFactory goToJailCommandFactory,
            LuxuryTaxCommandFactory luxuryTaxCommandFactory,
            MonopolyPropertyCommandFactories propertyCommandFactories)
            : base(spaces)
        {
            var commandFactories =
                new List<ICommandFactory>(new ICommandFactory[spaces.Count()])
                {
                    [IncomeTaxCommandFactory.SpaceIndex] = incomeTaxCommandFactory,
                    [GoToJailCommandFactory.SpaceIndex] = goToJailCommandFactory,
                    [LuxuryTaxCommandFactory.SpaceIndex] = luxuryTaxCommandFactory
                };

            foreach (var keyValuePair in propertyCommandFactories)
                commandFactories[keyValuePair.Key] = keyValuePair.Value;

            CommandFactories = commandFactories;
        }
    }
}
