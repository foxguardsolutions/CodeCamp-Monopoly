using System;

using BoardGame.Commands;
using BoardGame.Commands.Factories;
using BoardGame.RealEstate;

using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;

using Tests.Support;
using Tests.Support.Extensions;

namespace BoardGame.Tests.CommandsTests.FactoriesTests
{
    public class MortgageOptionCommandFactoryTests : BaseTest
    {
        private IPlayer _player;
        private Mock<IProperty> _mockProperty;
        private WithdrawalCommandFactory _withdrawalCommandFactory;
        private DepositCommandFactory _depositCommandFactory;
        private Mock<Func<IPlayer, IProperty, ITransactionCommandFactory, UnmortgageOptionCommand>> _mockInnerUnmortgageFactory;
        private Mock<Func<IPlayer, IProperty, ITransactionCommandFactory, MortgageOptionCommand>> _mockInnerMortgageFactory;

        private MortgageOptionCommandFactory _commandFactory;

        [SetUp]
        public void SetUp()
        {
            _player = Fixture.Create<IPlayer>();
            _mockProperty = Fixture.Mock<IProperty>();
            _withdrawalCommandFactory = Fixture.Freeze<WithdrawalCommandFactory>();
            _depositCommandFactory = Fixture.Freeze<DepositCommandFactory>();
            _mockInnerUnmortgageFactory = Given_MockInnerFactory<UnmortgageOptionCommand>();
            _mockInnerMortgageFactory = Given_MockInnerFactory<MortgageOptionCommand>();

            _commandFactory = Fixture.Create<MortgageOptionCommandFactory>();
        }

        [Test]
        public void Create_GivenPropertyIsMortgaged_ReturnsUnmortgageOptionCommand()
        {
            Given_PropertyIsMortgaged();

            var command = _commandFactory.Create(_player, _mockProperty.Object);

            _mockInnerUnmortgageFactory.Verify(iuf => iuf(_player, _mockProperty.Object, _withdrawalCommandFactory));
            Assert.That(command, Is.TypeOf<UnmortgageOptionCommand>());
        }

        [Test]
        public void Create_GivenPropertyIsNotMortgaged_ReturnsMortgageOptionCommand()
        {
            Given_PropertyIsNotMortgaged();

            var command = _commandFactory.Create(_player, _mockProperty.Object);

            _mockInnerMortgageFactory.Verify(imf => imf(_player, _mockProperty.Object, _depositCommandFactory));
            Assert.That(command, Is.TypeOf<MortgageOptionCommand>());
        }

        private Mock<Func<IPlayer, IProperty, ITransactionCommandFactory, TCommand>> Given_MockInnerFactory<TCommand>()
        {
            var mockInnerFactory = Fixture.Mock<Func<IPlayer, IProperty, ITransactionCommandFactory, TCommand>>();
            mockInnerFactory.Setup(f => f(
                    It.IsAny<IPlayer>(),
                    It.IsAny<IProperty>(),
                    It.IsAny<ITransactionCommandFactory>()))
                .ReturnsUsingFixture(Fixture);
            return mockInnerFactory;
        }

        private void Given_PropertyIsMortgaged()
        {
            _mockProperty.Setup(p => p.IsMortgaged).Returns(true);
        }

        private void Given_PropertyIsNotMortgaged()
        {
            _mockProperty.Setup(p => p.IsMortgaged).Returns(false);
        }
    }
}
