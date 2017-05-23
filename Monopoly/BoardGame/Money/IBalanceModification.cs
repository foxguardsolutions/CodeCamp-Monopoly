namespace BoardGame.Money
{
    public interface IBalanceModification
    {
        int GetNewBalance(int initialBalance);
    }
}