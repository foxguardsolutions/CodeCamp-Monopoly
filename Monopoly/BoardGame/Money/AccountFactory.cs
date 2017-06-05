namespace BoardGame.Money
{
    public class AccountFactory : IAccountFactory
    {
        private readonly int _standardInitialBalance;

        public AccountFactory(int standardInitialBalance)
        {
            _standardInitialBalance = standardInitialBalance;
        }

        public IAccount Create()
        {
            return new Account(_standardInitialBalance);
        }
    }
}
