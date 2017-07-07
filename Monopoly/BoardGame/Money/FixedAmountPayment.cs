namespace BoardGame.Money
{
    public struct FixedAmountPayment : IPayment
    {
        private readonly uint _amount;
        public IBalanceModification Deposit => new FixedBalanceModification((int)_amount);
        public IBalanceModification Withdrawal => new FixedBalanceModification(-(int)_amount);

        public FixedAmountPayment(uint amount)
        {
            _amount = amount;
        }
    }
}
