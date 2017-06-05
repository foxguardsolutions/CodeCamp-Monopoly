﻿using BoardGame.Money;

namespace BoardGame.Commands
{
    public class UpdatePlayerBalanceCommand : Command
    {
        private readonly IPlayer _player;
        private readonly IAccountRegistry _accounts;
        private readonly IBalanceModification _balanceModification;

        public UpdatePlayerBalanceCommand(IPlayer player, IAccountRegistry accounts, IBalanceModification balanceModification)
        {
            _player = player;
            _accounts = accounts;
            _balanceModification = balanceModification;
        }

        public override void Execute()
        {
            var account = _accounts.GetAccount(_player);
            account.Assess(_balanceModification);
        }
    }
}
