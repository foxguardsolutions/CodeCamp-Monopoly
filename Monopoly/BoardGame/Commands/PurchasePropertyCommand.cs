using BoardGame.Commands.Factories;
using BoardGame.RealEstate;

namespace BoardGame.Commands
{
    public class PurchasePropertyCommand : Command
    {
        private readonly IPlayer _player;
        private readonly IProperty _property;
        private readonly IPaymentCommandFactory _paymentCommandFactory;

        public PurchasePropertyCommand(IPlayer player, IProperty property, IPaymentCommandFactory paymentCommandFactory)
        {
            _player = player;
            _property = property;
            _paymentCommandFactory = paymentCommandFactory;
        }

        public override void Execute()
        {
            var paymentCommand = _paymentCommandFactory.CreateWithdrawalCommand(_player, _property.PurchasePrice);
            SubsequentCommands.Add(paymentCommand);
            _property.Owner = _player;

            Summary = $"\t{_player.Name} purchases the property for ${_property.PurchasePrice}.";
        }
    }
}
