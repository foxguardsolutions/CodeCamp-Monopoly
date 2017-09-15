using System;

using BoardGame.Commands;
using BoardGame.Commands.Decorators;
using BoardGame.Commands.Factories;

using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;

using Tests.Support;
using Tests.Support.Extensions;

namespace BoardGame.Tests.CommandsTests.DecoratorsTests
{
    public class MonadicCommandFactoryDecoratorTests : BaseTest
    {
        private IPlayer _player;
        private ICommand _decoratedCommand;
        private Mock<Func<ICommand, ICommand>> _mockInnerCommandFactory;

        private MonadicCommandFactoryDecorator<ICommand> _subject;

        [SetUp]
        public void SetUp()
        {
            _player = Fixture.Create<IPlayer>();
            var mockDecoratedCommandFactory = Fixture.Mock<ICommandFactory>();
            _decoratedCommand = Fixture.Create<ICommand>();
            mockDecoratedCommandFactory.Setup(dcf => dcf.CreateFor(_player)).Returns(_decoratedCommand);
            _mockInnerCommandFactory = Fixture.Mock<Func<ICommand, ICommand>>();

            _subject = Fixture.Create<MonadicCommandFactoryDecorator<ICommand>>();
        }

        [Test]
        public void Create_GivenDecoratedCommand_ReturnsCommandCreatedByInnerCommandFactory()
        {
            _subject.CreateFor(_player);

            _mockInnerCommandFactory.Verify(icf => icf(_decoratedCommand));
        }
    }
}