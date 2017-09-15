using System;

using BoardGame.RealEstate;

namespace BoardGame.Commands.Factories
{
    public class LandOnPropertyCommandFactory : ICommandFactory
    {
        private readonly IProperty _property;
        private readonly Func<IPlayer, IProperty, PurchasePropertyCommand> _innerPurchaseFactory;
        private readonly Func<IPlayer, IProperty, AssessRentCommand> _innerRentFactory;

        public LandOnPropertyCommandFactory(
            IProperty property,
            Func<IPlayer, IProperty, PurchasePropertyCommand> innerPurchaseFactory,
            Func<IPlayer, IProperty, AssessRentCommand> innerRentFactory)
        {
            _property = property;
            _innerPurchaseFactory = innerPurchaseFactory;
            _innerRentFactory = innerRentFactory;
        }

        public ICommand CreateFor(IPlayer player)
        {
            if (_property.Owner == null)
                return _innerPurchaseFactory(player, _property);
            return _innerRentFactory(player, _property);
        }
    }
}
