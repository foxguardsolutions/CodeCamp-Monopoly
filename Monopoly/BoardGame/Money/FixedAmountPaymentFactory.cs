namespace BoardGame.Money
{
    public class FixedAmountPaymentFactory : IPaymentFactory
    {
        public IPayment Create(uint amount)
        {
            return new FixedAmountPayment(amount);
        }
    }
}
