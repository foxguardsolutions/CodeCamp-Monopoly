using System.Linq;

using BoardGame.Commands;
using BoardGame.Commands.Decorators;

using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;

using Tests.Support.Extensions;
using UserInterface;

namespace BoardGame.Tests.CommandsTests.DecoratorsTests
{
    public class VerboseCommandDecoratorTests : CommandDecoratorTests
    {
        private Mock<ITextWriter> _mockTextWriter;
        private Mock<ICommandLogger> _mockCommandLogger;

        protected override void SetUpDecoratorDependencies()
        {
            _mockTextWriter = Fixture.Mock<ITextWriter>();
            _mockCommandLogger = Fixture.Mock<ICommandLogger>();
            MockDecoratedCommand.Setup(dc => dc.Logger)
                .Returns(_mockCommandLogger.Object);
        }

        [Test]
        public void Execute_GivenDecoratedCommandLogIsNotEmpty_WritesSummaryToTextWriter()
        {
            Given_DecoratedCommandLogIsNotEmpty();

            DecoratorCommand.Execute();

            _mockTextWriter.Verify(tw => tw.Write(It.IsAny<string>()));
        }

        [Test]
        public void Execute_GivenDecoratedCommandHasNoSummary_DoesNotWriteToTextWriter()
        {
            Given_DecoratedCommandLoggerIsEmpty();

            DecoratorCommand.Execute();

            _mockTextWriter.Verify(tw => tw.Write(It.IsAny<string>()), Times.Never);
        }

        [Test]
        public void GetSubsequentCommands_ReturnsVerboseCommands()
        {
            var subsequentCommands = DecoratorCommand.GetSubsequentCommands().ToArray();

            Assert.That(subsequentCommands, Is.Not.Empty);
            Assert.That(subsequentCommands, Is.All.TypeOf<VerboseCommandDecorator>());
        }

        protected override ICommand Given_DecoratorCommand()
        {
            return Fixture.Create<VerboseCommandDecorator>();
        }

        private void Given_DecoratedCommandLogIsNotEmpty()
        {
            _mockCommandLogger.Setup(l => l.IsEmpty).Returns(false);
        }

        private void Given_DecoratedCommandLoggerIsEmpty()
        {
            _mockCommandLogger.Setup(l => l.IsEmpty).Returns(true);
        }
    }
}