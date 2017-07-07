using BoardGame.Commands.Factories;

using NUnit.Framework;
using Ploeh.AutoFixture;

namespace BoardGame.Tests.CommandsTests.FactoriesTests
{
    public class DepositCommandFactoryTests : TransactionCommandFactoryTests
    {
        [Test]
        public void Create_CreatesCommandUsingDepositFromPaymentCreatedByPaymentFactory()
        {
            Factory.Create(Player, Amount);

            MockPayment.Verify(p => p.Deposit);
        }

        protected override ITransactionCommandFactory GivenTransactionCommandFactory()
        {
            return Fixture.Create<DepositCommandFactory>();
        }
    }
}
