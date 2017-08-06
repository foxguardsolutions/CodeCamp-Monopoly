using System;

using BoardGame.RealEstate;

namespace BoardGame.Commands.Factories
{
    public class MortgageOptionCommandFactory : IMortgageOptionCommandFactory
    {
        private readonly WithdrawalCommandFactory _withdrawalCommandFactory;
        private readonly DepositCommandFactory _depositCommandFactory;
        private readonly Func<IPlayer, IProperty, ITransactionCommandFactory, UnmortgageOptionCommand> _innerUnmortgageFactory;
        private readonly Func<IPlayer, IProperty, ITransactionCommandFactory, MortgageOptionCommand> _innerMortgageFactory;

        public MortgageOptionCommandFactory(
            WithdrawalCommandFactory withdrawalCommandFactory,
            DepositCommandFactory depositCommandFactory,
            Func<IPlayer, IProperty, ITransactionCommandFactory, UnmortgageOptionCommand> innerUnmortgageFactory,
            Func<IPlayer, IProperty, ITransactionCommandFactory, MortgageOptionCommand> innerMortgageFactory)
        {
            _withdrawalCommandFactory = withdrawalCommandFactory;
            _depositCommandFactory = depositCommandFactory;
            _innerUnmortgageFactory = innerUnmortgageFactory;
            _innerMortgageFactory = innerMortgageFactory;
        }

        public ICommand Create(IPlayer player, IProperty property)
        {
            if (property.IsMortgaged)
                return _innerUnmortgageFactory(player, property, _withdrawalCommandFactory);
            return _innerMortgageFactory(player, property, _depositCommandFactory);
        }
    }
}
