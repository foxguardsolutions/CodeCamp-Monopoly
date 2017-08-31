using System;

namespace UserInterface.IO
{
    public class ConsoleReaderWriter : ITextReaderWriter
    {
        public void Write(string format, params object[] args)
        {
            Console.Write(format, args);
        }

        public void WriteLine(string format, params object[] args)
        {
            Console.WriteLine(format, args);
        }

        public string ReadLine()
        {
            return Console.ReadLine();
        }
    }
}
