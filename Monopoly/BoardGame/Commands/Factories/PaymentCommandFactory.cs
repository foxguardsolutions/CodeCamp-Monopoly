using System.Collections.Generic;

namespace BoardGame.Commands.Factories
{
    public class PaymentCommandFactory : IPaymentCommandFactory
    {
        private readonly ITransactionCommandFactory _withdrawalCommandFactory;
        private readonly ITransactionCommandFactory _depositCommandFactory;

        public PaymentCommandFactory(
            ITransactionCommandFactory withdrawalCommandFactory,
            ITransactionCommandFactory depositCommandFactory)
        {
            _withdrawalCommandFactory = withdrawalCommandFactory;
            _depositCommandFactory = depositCommandFactory;
        }

        public IEnumerable<ICommand> CreatePaymentCommands(IPlayer sender, IPlayer recipient, uint amount)
        {
            yield return CreateWithdrawalCommand(sender, amount);
            yield return _depositCommandFactory.Create(recipient, amount);
        }

        public ICommand CreateWithdrawalCommand(IPlayer player, uint amount)
        {
            return _withdrawalCommandFactory.Create(player, amount);
        }
    }
}
