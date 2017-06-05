namespace BoardGame.Commands.Factories
{
    public interface ICommandFactory
    {
        ICommand CreateFor(IPlayer player);
    }
}
