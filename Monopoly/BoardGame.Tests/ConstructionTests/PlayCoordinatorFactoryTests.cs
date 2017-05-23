using System.Collections.Generic;

using BoardGame.Construction;

using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;

using Shuffler;
using Tests.Support;
using Tests.Support.Extensions;

namespace BoardGame.Tests.ConstructionTests
{
    public class PlayCoordinatorFactoryTests : BaseTest
    {
        private IEnumerable<IPlayer> _players;
        private Mock<IShuffler> _mockPlayerShuffler;
        private Mock<IGameStateConfigurationInitializer> _mockConfigurationInitializer;

        [SetUp]
        public void SetUp()
        {
            _players = Fixture.CreateMany<IPlayer>();
            _mockPlayerShuffler = GivenMockShuffler();
            _mockConfigurationInitializer = Fixture.Mock<IGameStateConfigurationInitializer>();
        }

        private Mock<IShuffler> GivenMockShuffler()
        {
            var mockShuffler = Fixture.Mock<IShuffler>();
            mockShuffler.Setup(s => s.Shuffle(_players)).Returns(_players);
            return mockShuffler;
        }

        [Test]
        public void Create_GivenPlayersNotSatisfyingPlayerCountConstraint_DoesNotReturnPlayerCoordinatorOrPlacePlayersOnBoard()
        {
            var constraint = GivenConstraintNotSatisfiedBy(_players);
            var factory = GivenPlayerCoordinatorFactoryWithConstraint(constraint);

            var newPlayerCoordinator = factory.Create(_players);

            Assert.That(newPlayerCoordinator, Is.Null);
            _mockConfigurationInitializer.Verify(g => g.ConfigureGame(It.IsAny<IEnumerable<IPlayer>>()), Times.Never);
        }

        [Test]
        public void Create_GivenPlayersSatisfyingPlayerCountConstraint_ReturnsPlayerCoordinatorWithShuffledPlayers()
        {
            var constraint = GivenConstraintSatisfiedBy(_players);
            var factory = GivenPlayerCoordinatorFactoryWithConstraint(constraint);

            factory.Create(_players);
            _mockPlayerShuffler.Verify(p => p.Shuffle(_players));
            _mockConfigurationInitializer.Verify(g => g.ConfigureGame(_players));
        }

        private IPlayerCountConstraint GivenConstraintNotSatisfiedBy(IEnumerable<IPlayer> players)
        {
            return GivenConstraintThatReturns(players, false);
        }

        private IPlayerCountConstraint GivenConstraintSatisfiedBy(IEnumerable<IPlayer> players)
        {
            return GivenConstraintThatReturns(players, true);
        }

        private IPlayerCountConstraint GivenConstraintThatReturns(IEnumerable<IPlayer> players, bool satisfactionStatus)
        {
            var mockConstraint = Fixture.Mock<IPlayerCountConstraint>();
            mockConstraint.Setup(c => c.IsSatisfiedBy(players)).Returns(satisfactionStatus);
            return mockConstraint.Object;
        }

        private PlayCoordinatorFactory GivenPlayerCoordinatorFactoryWithConstraint(IPlayerCountConstraint constraint)
        {
            return Fixture.Create<PlayCoordinatorFactory>();
        }
    }
}
