using System.Collections.Generic;
using System.Linq;

using BoardGame.Construction;

using NUnit.Framework;
using Ploeh.AutoFixture;

using Tests.Support;

namespace BoardGame.Tests.ConstructionTests
{
    public class PlayerFactoryTests : BaseTest
    {
        [Test]
        public void Create_YieldsAPlayerForEveryName()
        {
            var factory = Fixture.Create<PlayerFactory>();
            var names = Fixture.CreateMany<string>().ToArray();

            var players = factory.Create(names).ToArray();

            AssertPlayersMatchNames(players, names);
        }

        private void AssertPlayersMatchNames(IEnumerable<IPlayer> players, IEnumerable<string> names)
        {
            AssertThereIsAMatchingNameForEveryPlayer(players, names);
            AssertThereIsAMatchingPlayerForEveryName(players, names);
        }

        private void AssertThereIsAMatchingPlayerForEveryName(
            IEnumerable<IPlayer> players, IEnumerable<string> names)
        {
            foreach (var name in names)
                Assert.That(players, Has.Some.With.Property(nameof(IPlayer.Name)).EqualTo(name));
        }

        private static void AssertThereIsAMatchingNameForEveryPlayer(
            IEnumerable<IPlayer> players, IEnumerable<string> names)
        {
            foreach (var player in players)
                Assert.That(names, Has.Member(player.Name));
        }
    }
}
