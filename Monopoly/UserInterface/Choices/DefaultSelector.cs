namespace UserInterface.Choices
{
    public class DefaultSelector : IOptionSelector
    {
        public const string DefaultMessage = "";

        public TEnum ChooseOption<TEnum>(string message = DefaultMessage)
            where TEnum : struct
        {
            return default(TEnum);
        }

        public TEnum ChooseOption<TEnum>(TEnum defaultOption, string message = DefaultMessage)
            where TEnum : struct
        {
            return defaultOption;
        }
    }
}