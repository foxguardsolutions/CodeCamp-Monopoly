using BoardGame.Money;

namespace BoardGame.Commands.Factories
{
    public class BalanceModificationCommandFactory : ICommandFactory
    {
        private readonly IAccountRegistry _accounts;
        private readonly IBalanceModification _balanceModification;

        public BalanceModificationCommandFactory(IAccountRegistry accounts, IBalanceModification balanceModification)
        {
            _accounts = accounts;
            _balanceModification = balanceModification;
        }

        public ICommand CreateFor(IPlayer player)
        {
            return new UpdatePlayerBalanceCommand(player, _accounts, _balanceModification);
        }
    }
}
