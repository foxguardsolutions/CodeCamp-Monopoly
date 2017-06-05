using BoardGame.Play;

using NUnit.Framework;
using Ploeh.AutoFixture;

using Tests.Support;

namespace BoardGame.Tests.PlayTests
{
    public class TurnFactoryTests : BaseTest
    {
        [Test]
        public void Create_ReturnsNewTurn()
        {
            var turnFactory = Fixture.Create<TurnFactory>();
            var player = Fixture.Create<IPlayer>();

            var turn = turnFactory.CreateFor(player);

            Assert.That(turn, Is.TypeOf<Turn>());
        }
    }
}
