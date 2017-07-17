using System.ComponentModel;

namespace UserInterface.Tests
{
    public enum ByteEnum
        : byte
    {
        One = 1,
        Two = 2,
        [Description("The number three!")]
        Three = 3
    }
}
