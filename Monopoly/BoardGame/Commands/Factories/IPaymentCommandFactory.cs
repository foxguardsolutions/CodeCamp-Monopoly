using System.Collections.Generic;

namespace BoardGame.Commands.Factories
{
    public interface IPaymentCommandFactory
    {
        IEnumerable<ICommand> CreatePaymentCommands(IPlayer sender, IPlayer recipient, uint amount);
        ICommand CreateWithdrawalCommand(IPlayer player, uint amount);
    }
}