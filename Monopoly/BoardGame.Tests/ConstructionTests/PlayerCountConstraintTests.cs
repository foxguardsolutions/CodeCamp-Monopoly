using System.Collections.Generic;
using System.Linq;

using BoardGame.Construction;

using NUnit.Framework;
using Ploeh.AutoFixture;

using Tests.Support;
using Tests.Support.Extensions;

namespace BoardGame.Tests.ConstructionTests
{
    public class PlayerCountConstraintTests : BaseTest
    {
        private IEnumerable<IPlayer> _players;

        [SetUp]
        public void SetUp()
        {
            _players = Fixture.CreateMany<IPlayer>();
        }

        [Test]
        public void IsSatisfied_GivenTooFewPlayers_ReturnsFalse()
        {
            var constraint = GivenConstraintThatRequiresMorePlayersThan(_players.Count());

            Assert.That(constraint.IsSatisfiedBy(_players), Is.False);
        }

        private PlayerCountConstraint GivenConstraintThatRequiresMorePlayersThan(int playerCount)
        {
            var minimumPlayerCount = playerCount + Fixture.Create<uint>();
            var maximumPlayerCount = minimumPlayerCount + Fixture.Create<uint>();
            return new PlayerCountConstraint((uint)minimumPlayerCount, (uint)maximumPlayerCount);
        }

        [Test]
        public void IsSatisfied_GivenTooManyPlayers_ReturnsFalse()
        {
            var constraint = GivenConstraintThatRequiresFewerPlayersThan(_players.Count());

            Assert.That(constraint.IsSatisfiedBy(_players), Is.False);
        }

        private PlayerCountConstraint GivenConstraintThatRequiresFewerPlayersThan(int playerCount)
        {
            var maximumPlayerCount = Fixture.CreateInRange(0, playerCount - 1);
            var minimumPlayerCount = Fixture.CreateInRange(0, maximumPlayerCount);
            return new PlayerCountConstraint((uint)minimumPlayerCount, (uint)maximumPlayerCount);
        }

        [Test]
        public void IsSatisfied_GivenNumberOfPlayersWithinAcceptableRange_ReturnsTrue()
        {
            var constraint = GivenConstraintThatAllowsPlayerCount(_players.Count());

            Assert.That(constraint.IsSatisfiedBy(_players), Is.True);
        }

        private PlayerCountConstraint GivenConstraintThatAllowsPlayerCount(int playerCount)
        {
            var minimumPlayerCount = Fixture.CreateInRange(0, playerCount);
            var maximumPlayerCount = playerCount + Fixture.Create<uint>();
            return new PlayerCountConstraint((uint)minimumPlayerCount, (uint)maximumPlayerCount);
        }
    }
}
