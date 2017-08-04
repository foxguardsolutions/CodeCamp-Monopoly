using System.Collections.Generic;
using System.Linq;

using UserInterface;

namespace BoardGame.Commands.Decorators
{
    public class VerboseCommandDecorator : Command
    {
        private readonly ICommand _decoratedCommand;
        private readonly ITextWriter _textWriter;

        public override string Summary => _decoratedCommand.Summary;

        public VerboseCommandDecorator(ICommand decoratedCommand, ITextWriter textWriter)
        {
            _decoratedCommand = decoratedCommand;
            _textWriter = textWriter;
        }

        public override void Execute()
        {
            _decoratedCommand.Execute();

            if (_decoratedCommand.Summary != default(string))
                _textWriter.WriteLine(_decoratedCommand.Summary);
        }

        public override IEnumerable<ICommand> GetSubsequentCommands()
        {
            return _decoratedCommand
                .GetSubsequentCommands()
                .Select(c => new VerboseCommandDecorator(c, _textWriter));
        }
    }
}