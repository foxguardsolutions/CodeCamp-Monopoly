namespace UserInterface
{
    public interface ITextWriter
    {
        void Write(string format, params object[] args);
        void WriteLine(string format, params object[] args);
    }
}