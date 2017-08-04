using System.Linq;

using BoardGame.Commands;
using BoardGame.Commands.Decorators;

using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;

using Tests.Support.Extensions;
using UserInterface;

namespace BoardGame.Tests.CommandsTests.DecoratorsTests
{
    public class VerboseCommandDecoratorTests : CommandDecoratorTests
    {
        private Mock<ITextWriter> _mockTextWriter;

        protected override void SetUpDecoratorDependencies()
        {
            _mockTextWriter = Fixture.Mock<ITextWriter>();
        }

        [Test]
        public void Execute_GivenDecoratedCommandHasSummary_WritesSummaryToTextWriter()
        {
            Given_DecoratedCommandHasSummary();

            DecoratorCommand.Execute();

            _mockTextWriter.Verify(tw => tw.WriteLine(It.IsAny<string>()));
        }

        [Test]
        public void Execute_GivenDecoratedCommandHasNoSummary_DoesNotWriteToTextWriter()
        {
            Given_DecoratedCommandHasNoSummary();

            DecoratorCommand.Execute();

            _mockTextWriter.Verify(tw => tw.WriteLine(It.IsAny<string>()), Times.Never);
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

        private void Given_DecoratedCommandHasSummary()
        {
            MockDecoratedCommand.Setup(dc => dc.Summary).ReturnsUsingFixture(Fixture);
        }

        private void Given_DecoratedCommandHasNoSummary()
        {
            MockDecoratedCommand.Setup(dc => dc.Summary).Returns(default(string));
        }
    }
}