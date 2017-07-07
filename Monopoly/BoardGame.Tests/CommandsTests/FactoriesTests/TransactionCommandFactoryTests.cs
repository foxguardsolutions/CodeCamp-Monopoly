using BoardGame.Commands;
using BoardGame.Commands.Factories;
using BoardGame.Money;

using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;

using Tests.Support;
using Tests.Support.Extensions;

namespace BoardGame.Tests.CommandsTests.FactoriesTests
{
    public abstract class TransactionCommandFactoryTests : BaseTest
    {
        protected IPlayer Player { get; private set; }
        protected uint Amount { get; private set; }
        protected Mock<IPayment> MockPayment { get; private set; }
        protected ITransactionCommandFactory Factory { get; private set; }
        [SetUp]
        public void SetUp()
        {
            Player = Fixture.Create<IPlayer>();
            Amount = Fixture.Create<uint>();

            MockPayment = Fixture.Mock<IPayment>();
            var mockPaymentFactory = Fixture.Mock<IPaymentFactory>();
            mockPaymentFactory.Setup(f => f.Create(Amount)).Returns(MockPayment.Object);

            Factory = GivenTransactionCommandFactory();
        }

        protected abstract ITransactionCommandFactory GivenTransactionCommandFactory();

        [Test]
        public void Create_ReturnsUpdatePlayerBalanceCommand()
        {
            var command = Factory.Create(Player, Amount);

            Assert.That(command, Is.TypeOf<UpdatePlayerBalanceCommand>());
        }
    }
}
