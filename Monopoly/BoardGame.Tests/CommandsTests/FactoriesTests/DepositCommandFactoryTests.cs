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
            Subject.Create(Player, Amount);

            MockInnerCommandFactory.Verify(icf => icf(Player, MockPayment.Object.Deposit));
        }

        protected override ITransactionCommandFactory GivenTransactionCommandFactory()
        {
            return Fixture.Create<DepositCommandFactory>();
        }
    }
}
