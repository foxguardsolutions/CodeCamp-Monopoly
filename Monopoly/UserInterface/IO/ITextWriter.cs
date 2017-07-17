namespace UserInterface
{
    public interface ITextWriter
    {
        void WriteLine(string format, params object[] args);
    }
}