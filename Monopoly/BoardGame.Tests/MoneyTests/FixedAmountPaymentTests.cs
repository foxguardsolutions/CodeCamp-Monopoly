using BoardGame.Money;

using NUnit.Framework;
using Ploeh.AutoFixture;

using Tests.Support;

namespace BoardGame.Tests.MoneyTests
{
    public class FixedAmountPaymentTests : BaseTest
    {
        private uint _paymentAmount;
        private int _initialBalance;

        private FixedAmountPayment _payment;

        [SetUp]
        public void SetUp()
        {
            _paymentAmount = Fixture.Freeze<uint>();
            _initialBalance = Fixture.Create<int>();

            _payment = Fixture.Create<FixedAmountPayment>();
        }

        [Test]
        public void Deposit_ReturnsBalanceModificationThatIncreasesBalanceByDepositAmount()
        {
            var deposit = _payment.Deposit;

            var finalBalance = deposit.GetNewBalance(_initialBalance);

            Assert.That(finalBalance, Is.EqualTo(_initialBalance + _paymentAmount));
        }

        [Test]
        public void Withdrawal_ReturnsBalanceModificationThatDecreasesBalanceByWithdrawalAmount()
        {
            var withdrawal = _payment.Withdrawal;

            var finalBalance = withdrawal.GetNewBalance(_initialBalance);

            Assert.That(finalBalance, Is.EqualTo(_initialBalance - _paymentAmount));
        }
    }
}
