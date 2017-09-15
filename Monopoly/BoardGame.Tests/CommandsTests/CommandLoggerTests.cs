using System;
using System.Collections.Generic;
using System.Linq;

using BoardGame.Commands;

using NUnit.Framework;
using Ploeh.AutoFixture;

using Tests.Support;

namespace BoardGame.Tests.CommandsTests
{
    public class CommandLoggerTests : BaseTest
    {
        private CommandLogger _commandLogger;
        private IEnumerable<string> _actionDescriptions;

        [SetUp]
        public void SetUp()
        {
            _commandLogger = Fixture.Create<CommandLogger>();
            _actionDescriptions = Fixture.CreateMany<string>();
        }

        [Test]
        public void IsEmpty_GivenNoLoggedActions_ReturnsTrue()
        {
            Assert.That(_commandLogger.IsEmpty);
        }

        [Test]
        public void IsEmpty_GivenActionsWereLogged_ReturnsFalse()
        {
            _commandLogger.Log(_actionDescriptions.First());

            Assert.That(!_commandLogger.IsEmpty);
        }

        [Test]
        public void LogActions_MakesActionsAvailableViaGet()
        {
            var expected = _actionDescriptions.Aggregate(
                string.Empty,
                (accumulator, next) => accumulator + next + Environment.NewLine);

            foreach (var actionDescription in _actionDescriptions)
                _commandLogger.Log(actionDescription);

            var actual = _commandLogger.Get();

            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
