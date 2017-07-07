using BoardGame.Money;

namespace BoardGame.Commands.Factories
{
    public class WithdrawalCommandFactory : ITransactionCommandFactory
    {
        private readonly IPaymentFactory _paymentFactory;
        private readonly IAccountRegistry _accounts;

        public WithdrawalCommandFactory(IPaymentFactory paymentFactory, IAccountRegistry accounts)
        {
            _paymentFactory = paymentFactory;
            _accounts = accounts;
        }

        public ICommand Create(IPlayer player, uint amount)
        {
            var payment = _paymentFactory.Create(amount);
            return new UpdatePlayerBalanceCommand(player, _accounts, payment.Withdrawal);
        }
    }
}
