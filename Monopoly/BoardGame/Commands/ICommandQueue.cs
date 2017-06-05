namespace BoardGame.Commands
{
    public interface ICommandQueue
    {
        void InitializeFor(IPlayer player);
        void ExecuteCommands();
    }
}
