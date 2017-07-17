using System;
using UserInterface.Extensions;

namespace UserInterface.Choices
{
    public class UserChoicePrompt : IPrompt
    {
        private readonly ITextReaderWriter _readerWriter;
        private readonly IEnum _enum;

        public UserChoicePrompt(ITextReaderWriter readerWriter, IEnum enumWrapper)
        {
            _readerWriter = readerWriter;
            _enum = enumWrapper;
        }

        public string GetInput<TEnum>(string message)
            where TEnum : struct
        {
            _readerWriter.WriteLine(message);
            WriteOptions<TEnum>();
            return _readerWriter.ReadLine();
        }

        private void WriteOptions<TEnum>()
            where TEnum : struct
        {
            foreach (TEnum option in _enum.GetValues(typeof(TEnum)))
                WriteOption(option);
        }

        private void WriteOption<TEnum>(TEnum option)
            where TEnum : struct
        {
            var optionNumber = GetOptionNumber(option);
            _readerWriter.WriteLine($"Enter \"{optionNumber}\" for {option.GetDescription()}.");
        }

        private object GetOptionNumber<TEnum>(TEnum option)
            where TEnum : struct
        {
            var underlyingType = _enum.GetUnderlyingType(option.GetType());
            return Convert.ChangeType(option, underlyingType);
        }
    }
}
