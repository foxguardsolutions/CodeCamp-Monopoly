namespace BoardGame.Money
{
    public interface IPayment
    {
        IBalanceModification Deposit { get; }
        IBalanceModification Withdrawal { get; }
    }
}