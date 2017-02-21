using System.Collections.Generic;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace Monopoly.Tests
{
    public class GameBuilderTests : BaseTests
    {
        private Board _board;
        private IDice _dice;
        private GameBuilder _builder;

        [SetUp]
        public void SetUp()
        {
            _board = Fixture.Create<Board>();
            _dice = Fixture.Create<IDice>();
            _builder = Fixture.Create<GameBuilder>();
        }

        [Test]
        public void Build_BeforeAddingPlayers_DoesNotReturnGame()
        {
            GivenDiceAndBoardAlreadySet();

            var newGame = _builder.BuildGame();

            Assert.That(newGame, Is.Not.TypeOf(typeof(Game)));
        }

        [Test]
        public void Build_BeforeAddingBoard_DoesNotReturnGame()
        {
            _builder.SetDice(_dice);
            foreach (var player in Fixture.CreateMany<Player>())
                _builder.AddPlayer(player);

            var newGame = _builder.BuildGame();

            Assert.That(newGame, Is.Not.TypeOf(typeof(Game)));
        }

        [Test]
        public void Build_BeforeAddingDice_DoesNotReturnGame()
        {
            _builder.SetBoard(_board);
            foreach (var player in Fixture.CreateMany<Player>())
                _builder.AddPlayer(player);

            var newGame = _builder.BuildGame();

            Assert.That(newGame, Is.Not.TypeOf(typeof(Game)));
        }

        [Test]
        public void Build_AfterAddingPlayers_ReturnsGameWithAllTheAddedPlayersShuffled()
        {
            GivenDiceAndBoardAlreadySet();
            var expectedPlayers = GivenManyPlayersAdded();

            var newGame = _builder.BuildGame();
            var actualPlayers = newGame.Players;

            AssertContainSameItemsInDifferentOrder(expectedPlayers, actualPlayers);
        }

        private static void AssertContainSameItemsInDifferentOrder<T>(IEnumerable<T> expected, IEnumerable<T> actual)
        {
            Assert.That(actual, Is.EquivalentTo(expected));
            Assert.That(actual, Is.Not.EqualTo(expected));
        }

        [Test]
        public void Build_AfterCompleteSetup_ReturnsGameWithAllPlayersOnBeginningSpace()
        {
            GivenDiceAndBoardAlreadySet();
            GivenManyPlayersAdded();

            var newGame = _builder.BuildGame();
            var players = newGame.Players;

            Assert.That(players, Has.All.With.Property("CurrentSpace").EqualTo(0));
        }

        private void GivenDiceAndBoardAlreadySet()
        {
            _builder.SetDice(_dice);
            _builder.SetBoard(_board);
        }

        private IEnumerable<IPlayer> GivenManyPlayersAdded()
        {
            var numberToAdd = Fixture.Create<int>();
            var players = Fixture.CreateMany<IPlayer>(numberToAdd);

            foreach (var player in players)
                _builder.AddPlayer(player);

            return players;
        }
    }
}
