using System;
using System.Collections.Generic;
using System.Linq;

using BoardGame.Play;

using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;

using Tests.Support;
using Tests.Support.Extensions;

namespace BoardGame.Tests.PlayTests
{
    public class PlayCoordinatorTests : BaseTest
    {
        private Mock<ITurnFactory> _mockTurnFactory;
        private Mock<IEndConditionDetector> _mockEndConditionDetector;
        private List<IPlayer> _players;

        private PlayCoordinator _playCoordinator;

        [SetUp]
        public void SetUp()
        {
            _mockTurnFactory = GivenMockTurnFactory();
            _mockEndConditionDetector = GivenMockEndConditionDetector();
            _players = GivenMany<IPlayer>();

            _playCoordinator = Fixture.Create<PlayCoordinator>();
        }

        private Mock<IEndConditionDetector> GivenMockEndConditionDetector()
        {
            var mockEndConditionDetector = Fixture.Mock<IEndConditionDetector>();
            mockEndConditionDetector.Setup(e => e.IsInEndState()).Returns(true);
            return mockEndConditionDetector;
        }

        private Mock<ITurnFactory> GivenMockTurnFactory()
        {
            var mockTurnFactory = Fixture.Mock<ITurnFactory>();
            mockTurnFactory.Setup(t => t.CreateFor(It.IsAny<IPlayer>()))
                .Returns(Fixture.Create<Turn>());
            return mockTurnFactory;
        }

        private List<T> GivenMany<T>()
        {
            var items = Fixture.CreateMany<T>().ToList();
            Fixture.Register<IReadOnlyList<T>>(() => items);
            return items;
        }

        [Test]
        public void PlayRound_TakesATurnForEveryPlayer()
        {
            _playCoordinator.PlayRound();

            VerifyATurnWasTakenForEach(_players);
        }

        private void VerifyATurnWasTakenForEach(IEnumerable<IPlayer> players)
        {
            VerifyTurnsWereTakenForEach(players, Times.Once);
        }

        [Test]
        public void PlayRound_RaisesRoundCompletedEvent()
        {
            var roundWasCompleted = false;
            _playCoordinator.RoundCompleted += (sender, args) => roundWasCompleted = true;

            _playCoordinator.PlayRound();

            Assert.True(roundWasCompleted);
        }

        [Test]
        public void Play_SubscribesEndConditionDetector()
        {
            _playCoordinator.Play();

            _mockEndConditionDetector.Verify(e => e.Subscribe(_playCoordinator));
        }

        [Test]
        public void Play_GivenEndConditionTriggered_TakesNoTurns()
        {
            GivenEndConditionTriggered();

            _playCoordinator.Play();

            VerifyTurnsWereTakenForEach(_players, Times.Never);
        }

        private void GivenEndConditionTriggered()
        {
            GivenEndConditionTriggeredAtRound(0);
        }

        [Test]
        public void Play_GivenEndConditionNotTriggered_TakesTurns()
        {
            var roundsToPlay = Fixture.Create<int>();
            GivenEndConditionTriggeredAtRound(roundsToPlay);

            _playCoordinator.Play();

            VerifyTurnsWereTakenForEach(_players, Times.Exactly(roundsToPlay));
        }

        private void GivenEndConditionTriggeredAtRound(int roundsToPlay)
        {
            var roundsPlayed = 0;
            _mockEndConditionDetector.Setup(e => e.IsInEndState())
                .Returns(() => roundsPlayed >= roundsToPlay)
                .Callback(() => roundsPlayed++);
        }

        private void VerifyTurnsWereTakenForEach(IEnumerable<IPlayer> players, Func<Times> times)
        {
            VerifyTurnsWereTakenForEach(players, times());
        }

        private void VerifyTurnsWereTakenForEach(IEnumerable<IPlayer> players, Times times)
        {
            foreach (var player in players)
                _mockTurnFactory.Verify(t => t.CreateFor(player), times);
        }

        [Test]
        public void PlayMultipleRounds_TakesTurnsInSameOrderEveryTime()
        {
            var turnLog = SetUpTurnLogging(_players);

            PlayMultipleRounds(_playCoordinator);

            AssertTurnOrderWasConstant(turnLog);
        }

        private IList<string> SetUpTurnLogging(IEnumerable<IPlayer> players)
        {
            var log = new List<string>();

            foreach (var player in players)
                LogAName(log, player);

            return log;
        }

        private void LogAName(ICollection<string> log, IPlayer player)
        {
            var name = Fixture.Create<string>();
            _mockTurnFactory.Setup(t => t.CreateFor(player))
                .Callback(() => log.Add(name))
                .Returns(Fixture.Create<Turn>());
        }

        private void AssertTurnOrderWasConstant(IList<string> turnLog)
        {
            for (var turn = 0; turn < turnLog.Count; turn++)
                AssertConformsWithOriginalTurnOrder(turnLog, turn);
        }

        private void AssertConformsWithOriginalTurnOrder(IList<string> turnLog, int turnNumber)
        {
            var playerCount = _players.Count;
            var playerWhoTookThisTurnInFirstRound = turnLog[turnNumber % playerCount];
            var playerWhoTookThisTurnThisRound = turnLog[turnNumber];
            Assert.That(playerWhoTookThisTurnThisRound, Is.EqualTo(playerWhoTookThisTurnInFirstRound));
        }

        private void PlayMultipleRounds(IPlayCoordinator playCoordinator)
        {
            var roundsToComplete = Fixture.Create<uint>();
            PlayRounds(playCoordinator, roundsToComplete);
        }

        private static void PlayRounds(IPlayCoordinator playCoordinator, uint roundsToComplete)
        {
            for (var i = 0; i < roundsToComplete; i++)
                playCoordinator.PlayRound();
        }
    }
}
