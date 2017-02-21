using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace Monopoly.Tests
{
    public class GameTests : BaseTests
    {
        private Mock<IDice> _mockDice;
        private Game _game;

        [SetUp]
        public void SetUp()
        {
            _mockDice = Fixture.Freeze<Mock<IDice>>();
            _game = Fixture.Create<Game>();
        }

        [Test]
        public void IsFinished_BeforeTwentyRounds_ReturnsFalse()
        {
            var roundsToComplete = (uint)Fixture.CreateInRange(0, 19);

            PlayRounds(_game, roundsToComplete);
            var gameIsFinished = _game.IsFinished;

            Assert.That(gameIsFinished, Is.False);
        }

        [Test]
        public void IsFinished_AfterTwentyRounds_ReturnsTrue()
        {
            uint roundsToComplete = 20;

            PlayRounds(_game, roundsToComplete);
            var gameIsFinished = _game.IsFinished;

            Assert.That(gameIsFinished, Is.True);
        }

        private void PlayRounds(Game game, uint roundsToComplete)
        {
            for (int i = 0; i < roundsToComplete; i++)
                game.PlayRound();
        }

        [Test]
        public void PlayRound_TakesATurnForEveryPlayer()
        {
            var mockPlayers = Fixture.CreateMany<Mock<IPlayer>>();
            var game = GivenGameWithPlayers(mockPlayers);

            game.PlayRound();

            VerifyAllPlayersTookATurn(mockPlayers, game);
        }

        private Game GivenGameWithPlayers(IEnumerable<Mock<IPlayer>> mocks)
        {
            var players = GetPlayers(mocks);
            Fixture.Register(() => players);
            return Fixture.Create<Game>();
        }

        private IEnumerable<IPlayer> GetPlayers(IEnumerable<Mock<IPlayer>> mocks)
        {
            foreach (var mock in mocks)
                yield return mock.Object;
        }

        private void VerifyAllPlayersTookATurn(IEnumerable<Mock<IPlayer>> mockPlayers, Game game)
        {
            foreach (var mock in mockPlayers)
                mock.Verify(p => p.TakeATurn(game.Dice, game.Board));
        }
    }
}
