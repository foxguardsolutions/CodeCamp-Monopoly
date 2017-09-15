using BoardGame.Money;

using NUnit.Framework;
using Ploeh.AutoFixture;

using Tests.Support;

namespace BoardGame.Tests.MoneyTests
{
    public class FixedAmountPaymentFactoryTests : BaseTest
    {
        [Test]
        public void Create_ReturnsNewPaymentForSpecifiedAmount()
        {
            var amount = Fixture.Create<uint>();
            var factory = Fixture.Create<FixedAmountPaymentFactory>();
            var expectedPayment = new FixedAmountPayment(amount);

            var payment = factory.Create(amount);

            Assert.That(payment, Is.EqualTo(expectedPayment));
        }
    }
}
