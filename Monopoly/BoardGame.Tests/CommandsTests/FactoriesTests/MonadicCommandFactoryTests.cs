using System;

using BoardGame.Commands;
using BoardGame.Commands.Factories;

using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;

using Tests.Support;
using Tests.Support.Extensions;

namespace BoardGame.Tests.CommandsTests.FactoriesTests
{
    public class MonadicCommandFactoryTests : BaseTest
    {
        private Mock<Func<IPlayer, ICommand>> _mockInnerCommandFactory;
        private IPlayer _player;

        private MonadicCommandFactory<ICommand> _subject;

        [SetUp]
        public void SetUp()
        {
            _mockInnerCommandFactory = Fixture.Mock<Func<IPlayer, ICommand>>();
            _player = Fixture.Create<IPlayer>();

            _subject = Fixture.Create<MonadicCommandFactory<ICommand>>();
        }

        [Test]
        public void Create_CreatesCommandUsingInnerCommandFactory()
        {
            _subject.CreateFor(_player);

            _mockInnerCommandFactory.Verify(icf => icf(_player));
        }
    }
}
