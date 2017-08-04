using System.Collections.Generic;
using System.Linq;

using BoardGame.Commands;

using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;

using Tests.Support;
using Tests.Support.Extensions;

namespace BoardGame.Tests.CommandsTests.DecoratorsTests
{
    public abstract class CommandDecoratorTests : BaseTest
    {
        protected Mock<ICommand> MockDecoratedCommand { get; private set; }
        protected IEnumerable<ICommand> AdditionalCommandsProducedByDecoratedCommand { get; private set; }

        protected ICommand DecoratorCommand { get; private set; }

        [SetUp]
        public void Setup()
        {
            MockDecoratedCommand = Fixture.Mock<ICommand>();
            AdditionalCommandsProducedByDecoratedCommand = GivenCommandsResultingFromExecutionOf(MockDecoratedCommand);

            SetUpDecoratorDependencies();

            DecoratorCommand = Given_DecoratorCommand();
        }

        [Test]
        public void Execute_ExecutesDecoratedCommand()
        {
            DecoratorCommand.Execute();

            MockDecoratedCommand.Verify(dc => dc.Execute());
        }

        [Test]
        public void GetSummary_GetsSummaryFromDecoratedCommand()
        {
            var summary = DecoratorCommand.Summary;

            MockDecoratedCommand.Verify(dc => dc.Summary);
        }

        [Test]
        public void GetSubsequentCommands_GetsSubsequentCommandsFromDecoratedCommand()
        {
            DecoratorCommand.GetSubsequentCommands();

            MockDecoratedCommand.Verify(dc => dc.GetSubsequentCommands());
        }

        protected abstract ICommand Given_DecoratorCommand();
        protected abstract void SetUpDecoratorDependencies();

        private IEnumerable<ICommand> GivenCommandsResultingFromExecutionOf(
            Mock<ICommand> mockDecoratedCommand)
        {
            var subsequentCommands = Fixture.CreateMany<ICommand>().ToArray();
            mockDecoratedCommand.Setup(c => c.GetSubsequentCommands())
                .Returns(subsequentCommands);
            return subsequentCommands;
        }
    }
}