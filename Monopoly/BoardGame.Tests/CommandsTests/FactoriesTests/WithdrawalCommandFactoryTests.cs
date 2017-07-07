using BoardGame.Commands.Factories;

using NUnit.Framework;
using Ploeh.AutoFixture;

namespace BoardGame.Tests.CommandsTests.FactoriesTests
{
    public class WithdrawalCommandFactoryTests : TransactionCommandFactoryTests
    {
        [Test]
        public void Create_CreatesCommandUsingWithdrawalFromPaymentCreatedByPaymentFactory()
        {
            Factory.Create(Player, Amount);

            MockPayment.Verify(p => p.Withdrawal);
        }

        protected override ITransactionCommandFactory GivenTransactionCommandFactory()
        {
            return Fixture.Create<WithdrawalCommandFactory>();
        }
    }
}
