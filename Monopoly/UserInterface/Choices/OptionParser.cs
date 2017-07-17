namespace UserInterface.Choices
{
    public class OptionParser : IOptionParser
    {
        private readonly IEnum _enum;

        public OptionParser(IEnum enumWrapper)
        {
            _enum = enumWrapper;
        }

        public bool TryParse<TEnum>(string value, out TEnum result)
            where TEnum : struct
        {
            return _enum.TryParse(value, out result)
                   && _enum.IsDefined(typeof(TEnum), result);
        }

        public TEnum ParseOrDefault<TEnum>(string value, TEnum defaultResult)
            where TEnum : struct
        {
            return TryParse(value, out TEnum result) ? result : defaultResult;
        }
    }
}
