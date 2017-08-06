using System.Collections.Generic;

using BoardGame.Commands.Factories;
using BoardGame.RealEstate;

namespace BoardGame.Commands
{
    public class OfferMortgageOptionCommand : Command
    {
        private readonly IPlayer _player;
        private readonly IEnumerable<IPropertyGroup> _propertyGroups;
        private readonly IMortgageOptionCommandFactory _mortgageOptionCommandFactory;

        public OfferMortgageOptionCommand(IPlayer player, IEnumerable<IPropertyGroup> propertyGroups, IMortgageOptionCommandFactory mortgageOptionCommandFactory)
        {
            _player = player;
            _propertyGroups = propertyGroups;
            _mortgageOptionCommandFactory = mortgageOptionCommandFactory;
        }

        public override void Execute()
        {
            foreach (var propertyGroup in _propertyGroups)
                OfferMortgageOptions(propertyGroup);
        }

        private void OfferMortgageOptions(IPropertyGroup propertyGroup)
        {
            foreach (var property in propertyGroup)
                OfferMortgageOption(property);
        }

        private void OfferMortgageOption(IProperty property)
        {
            if (_player == property.Owner)
                SubsequentCommands.Add(_mortgageOptionCommandFactory.Create(_player, property));
        }
    }
}
