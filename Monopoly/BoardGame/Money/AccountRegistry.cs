using System.Collections.Generic;

namespace BoardGame.Money
{
    public class AccountRegistry : IAccountRegistry
    {
        private readonly Dictionary<IPlayer, IAccount> _accounts;
        private readonly IAccountFactory _accountFactory;

        public AccountRegistry(IAccountFactory accountFactory)
        {
            _accounts = new Dictionary<IPlayer, IAccount>();
            _accountFactory = accountFactory;
        }

        public IAccount GetAccount(IPlayer player)
        {
            return _accounts[player];
        }

        public void RegisterAccount(IPlayer player)
        {
            _accounts[player] = _accountFactory.Create();
        }
    }
}
