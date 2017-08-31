namespace BoardGame.Commands
{
    public interface ICommandLogger
    {
        bool IsEmpty { get; }

        string Get();
        void Log(string action);
    }
}
