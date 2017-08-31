using System.Collections.Generic;
using System.Linq;

using UserInterface;

namespace BoardGame.Commands.Decorators
{
    public class VerboseCommandDecorator : Command
    {
        private readonly ICommand _decoratedCommand;
        private readonly ITextWriter _textWriter;

        public VerboseCommandDecorator(ICommand decoratedCommand, ITextWriter textWriter)
            : base(decoratedCommand.Logger)
        {
            _decoratedCommand = decoratedCommand;
            _textWriter = textWriter;
        }

        public override void Execute()
        {
            _decoratedCommand.Execute();

            if (!Logger.IsEmpty)
                _textWriter.Write(Logger.Get());
        }

        public override IEnumerable<ICommand> GetSubsequentCommands()
        {
            return _decoratedCommand
                .GetSubsequentCommands()
                .Select(c => new VerboseCommandDecorator(c, _textWriter));
        }
    }
}