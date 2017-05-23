namespace BoardGame.Money
{
    public interface IAccount
    {
        int Balance { get; }

        void Assess(IBalanceModification balanceModification);
        bool IsInGoodStanding();
    }
}