using BoardGame.Money;

namespace BoardGame.Commands.Factories
{
    public class DepositCommandFactory : ITransactionCommandFactory
    {
        private readonly IPaymentFactory _paymentFactory;
        private readonly IAccountRegistry _accounts;

        public DepositCommandFactory(IPaymentFactory paymentFactory, IAccountRegistry accounts)
        {
            _paymentFactory = paymentFactory;
            _accounts = accounts;
        }

        public ICommand Create(IPlayer player, uint amount)
        {
            var payment = _paymentFactory.Create(amount);
            return new UpdatePlayerBalanceCommand(player, _accounts, payment.Deposit);
        }
    }
}
