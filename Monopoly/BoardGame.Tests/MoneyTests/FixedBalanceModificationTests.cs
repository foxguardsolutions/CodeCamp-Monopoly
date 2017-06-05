using BoardGame.Money;

using NUnit.Framework;
using Ploeh.AutoFixture;

using Tests.Support;

namespace BoardGame.Tests.MoneyTests
{
    public class FixedBalanceModificationTests : BaseTest
    {
        [Test]
        public void GetNewBalance_GivenInitialBalanceAndModificationValue_ReturnsSumOfValues()
        {
            var balanceModificationValue = Fixture.Create<int>();
            var balanceModification = new FixedBalanceModification(balanceModificationValue);

            var initialBalance = Fixture.Create<int>();
            var finalBalance = balanceModification.GetNewBalance(initialBalance);

            Assert.That(finalBalance, Is.EqualTo(initialBalance + balanceModificationValue));
        }
    }
}
