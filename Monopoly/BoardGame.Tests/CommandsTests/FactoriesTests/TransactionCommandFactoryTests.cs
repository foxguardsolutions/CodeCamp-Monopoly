using System;
using BoardGame.Commands;
using BoardGame.Commands.Factories;
using BoardGame.Money;

using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;
using Tests.Support;
using Tests.Support.Extensions;

namespace BoardGame.Tests.CommandsTests.FactoriesTests
{
    public abstract class TransactionCommandFactoryTests : BaseTest
    {
        protected IPlayer Player { get; private set; }
        protected uint Amount { get; private set; }
        protected Mock<IPayment> MockPayment { get; private set; }
        protected Mock<Func<IPlayer, IBalanceModification, UpdatePlayerBalanceCommand>> MockInnerCommandFactory { get; private set; }
        protected ITransactionCommandFactory Subject { get; private set; }
        [SetUp]
        public void SetUp()
        {
            Player = Fixture.Create<IPlayer>();
            Amount = Fixture.Create<uint>();

            MockPayment = Fixture.Mock<IPayment>();
            MockPayment.SetupAllProperties();
            var mockPaymentFactory = Fixture.Mock<IPaymentFactory>();
            mockPaymentFactory.Setup(f => f.Create(Amount)).Returns(MockPayment.Object);

            MockInnerCommandFactory = Fixture.Mock<Func<IPlayer, IBalanceModification, UpdatePlayerBalanceCommand>>();
            MockInnerCommandFactory.Setup(icf => icf(It.IsAny<IPlayer>(), It.IsAny<IBalanceModification>()))
                .ReturnsUsingFixture(Fixture);

            Subject = GivenTransactionCommandFactory();
        }

        protected abstract ITransactionCommandFactory GivenTransactionCommandFactory();

        [Test]
        public void Create_ReturnsUpdatePlayerBalanceCommand()
        {
            var command = Subject.Create(Player, Amount);

            Assert.That(command, Is.TypeOf<UpdatePlayerBalanceCommand>());
        }
    }
}
