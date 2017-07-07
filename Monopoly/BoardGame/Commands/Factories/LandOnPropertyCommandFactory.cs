using BoardGame.RealEstate;
using BoardGame.RealEstate.Rent;

namespace BoardGame.Commands.Factories
{
    public class LandOnPropertyCommandFactory : ICommandFactory
    {
        private readonly IProperty _property;
        private readonly IRentCalculator _rentCalculator;
        private readonly IPaymentCommandFactory _paymentCommandFactory;

        public LandOnPropertyCommandFactory(IProperty property, IRentCalculator rentCalculator, IPaymentCommandFactory paymentCommandFactory)
        {
            _property = property;
            _rentCalculator = rentCalculator;
            _paymentCommandFactory = paymentCommandFactory;
        }

        public ICommand CreateFor(IPlayer player)
        {
            if (_property.Owner == null)
                return new PurchasePropertyCommand(player, _property, _paymentCommandFactory);
            return new AssessRentCommand(player, _property, _rentCalculator, _paymentCommandFactory);
        }
    }
}
