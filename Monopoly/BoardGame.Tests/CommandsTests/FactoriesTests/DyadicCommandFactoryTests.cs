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
    public class DyadicCommandFactoryTests : BaseTest
    {
        private byte _argument;
        private Mock<Func<IPlayer, byte, ICommand>> _mockInnerCommandFactory;
        private IPlayer _player;

        private DyadicCommandFactory<byte, ICommand> _subject;

        [SetUp]
        public void SetUp()
        {
            _argument = Fixture.Freeze<byte>();
            _mockInnerCommandFactory = Fixture.Mock<Func<IPlayer, byte, ICommand>>();
            _player = Fixture.Create<IPlayer>();

            _subject = Fixture.Create<DyadicCommandFactory<byte, ICommand>>();
        }

        [Test]
        public void Create_CreatesCommandUsingInnerCommandFactory()
        {
            _subject.CreateFor(_player);

            _mockInnerCommandFactory.Verify(icf => icf(_player, _argument));
        }
    }
}
