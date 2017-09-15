namespace BoardGame.Money
{
    public interface IPaymentFactory
    {
        IPayment Create(uint amount);
    }
}