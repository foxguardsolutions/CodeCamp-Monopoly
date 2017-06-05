using BoardGame.Commands;
using BoardGame.Commands.Factories;

using NUnit.Framework;
using Ploeh.AutoFixture;

using Tests.Support;

namespace BoardGame.Tests.CommandsTests.FactoriesTests
{
    public class BalanceModificationCommandFactoryTests : BaseTest
    {
        [Test]
        public void Create_GivenBalanceModificationCommandFactory_ReturnsUpdatePlayerBalanceCommand()
        {
            var factory = Fixture.Create<BalanceModificationCommandFactory>();
            var player = Fixture.Create<IPlayer>();

            var command = factory.CreateFor(player);

            Assert.That(command, Is.TypeOf<UpdatePlayerBalanceCommand>());
        }
    }
}
