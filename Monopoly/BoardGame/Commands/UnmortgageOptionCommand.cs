using BoardGame.Commands.Factories;
using BoardGame.RealEstate;
using UserInterface.Choices;

using static BoardGame.RealEstate.Choices.UnmortgagePropertyChoice;

namespace BoardGame.Commands
{
    public class UnmortgageOptionCommand : Command
    {
        private string Message => $"Would you like to unmortgage {_property}?";
        private readonly IPlayer _player;
        private readonly IProperty _property;
        private readonly ITransactionCommandFactory _withdrawalCommandFactory;
        private readonly IOptionSelector _optionSelector;

        public UnmortgageOptionCommand(IPlayer player, IProperty property, ITransactionCommandFactory withdrawalCommandFactory, IOptionSelector optionSelector, ICommandLogger logger)
            : base(logger)
        {
            _player = player;
            _property = property;
            _withdrawalCommandFactory = withdrawalCommandFactory;
            _optionSelector = optionSelector;
        }

        public override void Execute()
        {
            if (PropertyCanBeUnmortgagedByPlayer() &&
                    _optionSelector.ChooseOption(defaultOption: Yes, message: Message) == Yes)
                UnmortgageProperty();
        }

        private bool PropertyCanBeUnmortgagedByPlayer()
        {
            return _property.IsMortgaged && _player == _property.Owner;
        }

        private void UnmortgageProperty()
        {
            SubsequentCommands.Add(_withdrawalCommandFactory.Create(_player, _property.PurchasePrice));
            _property.IsMortgaged = false;

            Logger.Log($"\t{_player.Name} unmortgages property for ${_property.PurchasePrice}.");
        }
    }
}
