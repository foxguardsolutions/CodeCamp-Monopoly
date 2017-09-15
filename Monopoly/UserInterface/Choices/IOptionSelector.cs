namespace UserInterface.Choices
{
    public interface IOptionSelector
    {
        TEnum ChooseOption<TEnum>(string message = "")
            where TEnum : struct;
        TEnum ChooseOption<TEnum>(TEnum defaultOption, string message = "")
            where TEnum : struct;
    }
}
