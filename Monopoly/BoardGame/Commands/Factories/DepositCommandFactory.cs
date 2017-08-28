using System;

using BoardGame.Money;

namespace BoardGame.Commands.Factories
{
    public class DepositCommandFactory : ITransactionCommandFactory
    {
        private readonly IPaymentFactory _paymentFactory;
        private readonly Func<IPlayer, IBalanceModification, UpdatePlayerBalanceCommand> _innerCommandFactory;

        public DepositCommandFactory(
            IPaymentFactory paymentFactory,
            Func<IPlayer, IBalanceModification, UpdatePlayerBalanceCommand> innerCommandFactory)
        {
            _paymentFactory = paymentFactory;
            _innerCommandFactory = innerCommandFactory;
        }

        public ICommand Create(IPlayer player, uint amount)
        {
            var payment = _paymentFactory.Create(amount);
            return _innerCommandFactory(player, payment.Deposit);
        }
    }
}
