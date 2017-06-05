namespace BoardGame.Money
{
    public class FixedBalanceModification : IBalanceModification
    {
        private readonly int _balanceModificationValue;

        public FixedBalanceModification(int balanceModificationValue)
        {
            _balanceModificationValue = balanceModificationValue;
        }

        public int GetNewBalance(int initialBalance)
        {
            return initialBalance + _balanceModificationValue;
        }
    }
}
