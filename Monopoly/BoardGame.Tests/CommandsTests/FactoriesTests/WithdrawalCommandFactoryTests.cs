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
            Subject.Create(Player, Amount);

            MockInnerCommandFactory.Verify(icf => icf(Player, MockPayment.Object.Withdrawal));
        }

        protected override ITransactionCommandFactory GivenTransactionCommandFactory()
        {
            return Fixture.Create<WithdrawalCommandFactory>();
        }
    }
}
