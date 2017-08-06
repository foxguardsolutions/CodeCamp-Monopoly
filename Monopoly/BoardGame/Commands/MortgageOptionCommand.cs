using BoardGame.Commands.Factories;
using BoardGame.RealEstate;
using UserInterface.Choices;

using static BoardGame.RealEstate.Choices.MortgageProperty;

namespace BoardGame.Commands
{
    public class MortgageOptionCommand : Command
    {
        public const uint MortgageValuePercentage = 90;
        public const string Message = "Would you like to mortgage this property?";
        private readonly IPlayer _player;
        private readonly IProperty _property;
        private readonly ITransactionCommandFactory _depositCommandFactory;
        private readonly IOptionSelector _optionSelector;

        public MortgageOptionCommand(IPlayer player, IProperty property, ITransactionCommandFactory depositCommandFactory, IOptionSelector optionSelector)
        {
            _player = player;
            _property = property;
            _depositCommandFactory = depositCommandFactory;
            _optionSelector = optionSelector;
        }

        public override void Execute()
        {
            if (!PropertyCanBeMortgagedByPlayer())
                return;

            if (_optionSelector.ChooseOption(defaultOption: No, message: Message) == Yes)
                MortgageProperty();
        }

        private bool PropertyCanBeMortgagedByPlayer()
        {
            return !_property.IsMortgaged && _player == _property.Owner;
        }

        private void MortgageProperty()
        {
            var depositAmount = CalculateMortgageValue();
            SubsequentCommands.Add(_depositCommandFactory.Create(_player, depositAmount));
            _property.IsMortgaged = true;

            Summary = $"\t{_player.Name} mortgages property for ${depositAmount}.";
        }

        private uint CalculateMortgageValue()
        {
            return _property.PurchasePrice * MortgageValuePercentage / 100;
        }
    }
}
