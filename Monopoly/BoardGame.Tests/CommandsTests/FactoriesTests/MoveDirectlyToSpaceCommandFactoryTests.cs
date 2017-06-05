using BoardGame.Commands;
using BoardGame.Commands.Factories;

using NUnit.Framework;
using Ploeh.AutoFixture;

using Tests.Support;

namespace BoardGame.Tests.CommandsTests.FactoriesTests
{
    public class MoveDirectlyToSpaceCommandFactoryTests : BaseTest
    {
        [Test]
        public void Create_MoveDirectlyToSpaceCommandFactory_ReturnsMoveDirectlyToSpaceCommand()
        {
            var factory = Fixture.Create<MoveDirectlyToSpaceCommandFactory>();
            var player = Fixture.Create<IPlayer>();

            var command = factory.CreateFor(player);

            Assert.That(command, Is.TypeOf<MoveDirectlyToSpaceCommand>());
        }
    }
}
