namespace UserInterface.Choices
{
    public interface IPrompt
    {
        string GetInput<TEnum>(string message)
            where TEnum : struct;
    }
}
