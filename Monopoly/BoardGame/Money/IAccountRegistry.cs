namespace BoardGame.Money
{
    public interface IAccountRegistry
    {
        IAccount GetAccount(IPlayer player);
        void RegisterAccount(IPlayer player);
    }
}