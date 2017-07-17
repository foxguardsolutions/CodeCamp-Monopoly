namespace UserInterface.Choices
{
    public class OptionSelector : IOptionSelector
    {
        public const string DefaultMessage = "Select an option:";
        private readonly IPrompt _userPrompt;
        private readonly IOptionParser _parser;

        public OptionSelector(IPrompt userPrompt, IOptionParser optionParser)
        {
            _userPrompt = userPrompt;
            _parser = optionParser;
        }

        public TEnum ChooseOption<TEnum>(string message = DefaultMessage)
            where TEnum : struct
        {
            string input;
            TEnum selectedOption;

            do input = _userPrompt.GetInput<TEnum>(message);
            while (!_parser.TryParse(input, out selectedOption));

            return selectedOption;
        }

        public TEnum ChooseOption<TEnum>(TEnum defaultOption, string message = DefaultMessage)
            where TEnum : struct
        {
            var input = _userPrompt.GetInput<TEnum>(message);
            return _parser.ParseOrDefault(input, defaultOption);
        }
    }
}
