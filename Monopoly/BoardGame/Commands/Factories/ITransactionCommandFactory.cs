namespace BoardGame.Commands.Factories
{
    public interface ITransactionCommandFactory
    {
        ICommand Create(IPlayer player, uint amount);
    }
}