using BoardGame.Commands;
using BoardGame.Commands.Factories;

using NUnit.Framework;
using Ploeh.AutoFixture;

using Tests.Support;

namespace BoardGame.Tests.CommandsTests.FactoriesTests
{
    public class CompletedLapsRewardingCommandFactoryDecoratorTests : BaseTest
    {
        [Test]
        public void Create_GivenDecoratedCommandFactory_ReturnsDecoratedCommand()
        {
            var factory = Fixture.Create<CompletedLapsRewardingCommandFactoryDecorator>();
            var player = Fixture.Create<IPlayer>();

            var command = factory.CreateFor(player);

            Assert.That(command, Is.TypeOf<CompletedLapsRewardingCommandDecorator>());
        }
    }
}
