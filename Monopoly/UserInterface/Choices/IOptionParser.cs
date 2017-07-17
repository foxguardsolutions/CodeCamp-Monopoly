namespace UserInterface.Choices
{
    public interface IOptionParser
    {
        TEnum ParseOrDefault<TEnum>(string value, TEnum defaultResult)
            where TEnum : struct;
        bool TryParse<TEnum>(string value, out TEnum result)
            where TEnum : struct;
    }
}
