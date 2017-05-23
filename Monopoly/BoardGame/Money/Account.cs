namespace BoardGame.Money
{
    public class Account : IAccount
    {
        public int Balance { get; private set; }

        public Account(int initialBalance)
        {
            Balance = initialBalance;
        }

        public bool IsInGoodStanding()
        {
            return Balance >= 0;
        }

        public void Assess(IBalanceModification balanceModification)
        {
            Balance = balanceModification.GetNewBalance(Balance);
        }
    }
}
