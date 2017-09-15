﻿using BoardGame.Commands.Factories;
using BoardGame.RealEstate;
using BoardGame.RealEstate.Rent;

namespace BoardGame.Commands
{
    public class AssessRentCommand : Command
    {
        private readonly IPlayer _player;
        private readonly IProperty _property;
        private readonly IRentCalculator _rentCalculator;
        private readonly IPaymentCommandFactory _paymentCommandFactory;

        public AssessRentCommand(IPlayer player, IProperty property, IRentCalculator rentCalculator, IPaymentCommandFactory paymentCommandFactory, ICommandLogger logger)
            : base(logger)
        {
            _player = player;
            _property = property;
            _rentCalculator = rentCalculator;
            _paymentCommandFactory = paymentCommandFactory;
        }

        public override void Execute()
        {
            if (_player == _property.Owner)
                Logger.Log($"\t{_player.Name} already owns this property.");
            else
                AddRentPaymentCommands();
        }

        private void AddRentPaymentCommands()
        {
            var rentValue = (uint)_rentCalculator.GetRentFor(_property);
            foreach (var paymentCommand in _paymentCommandFactory.CreatePaymentCommands(_player, _property.Owner, rentValue))
                SubsequentCommands.Add(paymentCommand);

            Logger.Log($"\t{_player.Name} pays ${rentValue} in rent to {_property.Owner.Name}.");
        }
    }
}
