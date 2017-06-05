using BoardGame.Commands;
using BoardGame.Commands.Factories;

using NUnit.Framework;
using Ploeh.AutoFixture;

using Tests.Support;

namespace BoardGame.Tests.CommandsTests.FactoriesTests
{
    public class RollAndMoveCommandFactoryTests : BaseTest
    {
        [Test]
        public void Create_GivenRollAndMoveCommandFactory_ReturnsRollAndMoveCommand()
        {
            var factory = Fixture.Create<RollAndMoveCommandFactory>();
            var player = Fixture.Create<IPlayer>();

            var command = factory.CreateFor(player);

            Assert.That(command, Is.TypeOf<RollAndMoveCommand>());
        }
    }
}
