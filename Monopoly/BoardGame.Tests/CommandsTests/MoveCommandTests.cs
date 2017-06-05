using BoardGame.Commands;
using BoardGame.Locations;

using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;

using Tests.Support;
using Tests.Support.Extensions;

namespace BoardGame.Tests.CommandsTests
{
    public abstract class MoveCommandTests : BaseTest
    {
        protected IPlayer Player { get; private set; }
        protected Mock<IPlayerMover> MockPlayerMover { get; private set; }

        protected MoveCommand Command { get; set; }

        [SetUp]
        public void Setup()
        {
            Player = Fixture.Freeze<IPlayer>();
            MockPlayerMover = Fixture.Mock<IPlayerMover>();
        }

        [Test]
        public void GetSubsequentCommands_GivenLandingSpaceWithoutCommand_YieldsNoCommands()
        {
            GivenDestinationSpaceWithoutCommandFactory();
            Command.Execute();
            var subsequentCommands = Command.GetSubsequentCommands();

            Assert.That(subsequentCommands, Is.Empty);
        }

        [Test]
        public void GetSubsequentCommands_GivenLandingSpaceWithCommand_YieldsACommand()
        {
            GivenDestinationSpaceWithCommandFactory();
            Command.Execute();
            var subsequentCommands = Command.GetSubsequentCommands();

            Assert.That(subsequentCommands, Is.Not.Empty);
        }

        protected abstract void GivenDestinationSpaceWithoutCommandFactory();

        protected abstract void GivenDestinationSpaceWithCommandFactory();
    }
}
