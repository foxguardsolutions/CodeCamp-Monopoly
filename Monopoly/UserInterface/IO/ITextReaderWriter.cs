namespace UserInterface
{
    public interface ITextReaderWriter : ITextWriter
    {
        string ReadLine();
    }
}