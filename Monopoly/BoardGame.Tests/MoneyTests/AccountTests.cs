using System;

using BoardGame.Money;

using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;

using Tests.Support;
using Tests.Support.Extensions;

namespace BoardGame.Tests.MoneyTests
{
    public class AccountTests : BaseTest
    {
        private int _initialBalance;
        private Account _account;

        [SetUp]
        public void SetUp()
        {
            _initialBalance = Fixture.Create<int>();
            _account = GivenAccountWithBalance(_initialBalance);
        }

        [Test]
        public void Balance_GivenNoBalanceChanges_ReturnsInitialBalance()
        {
            Assert.That(_account, Has.Property(nameof(Account.Balance)).EqualTo(_initialBalance));
        }

        [Test]
        public void Assess_GivenBalanceModification_SetsBalanceToValueObtainedFromBalanceModification()
        {
            var mockBalanceModification = Fixture.Mock<IBalanceModification>();
            var expectedFinalBalance = GivenFinalBalanceFrom(mockBalanceModification);

            _account.Assess(mockBalanceModification.Object);

            Assert.That(_account, Has.Property(nameof(Account.Balance)).EqualTo(expectedFinalBalance));
        }

        private int GivenFinalBalanceFrom(Mock<IBalanceModification> mockBalanceModification)
        {
            var finalBalance = Fixture.Create<int>();
            mockBalanceModification.Setup(b => b.GetNewBalance(_initialBalance))
                .Returns(finalBalance);
            return finalBalance;
        }

        [Test]
        public void IsInGoodStanding_GivenNonnegativeBalance_ReturnsTrue()
        {
            GivenNonnegativeAccountBalance();

            Assert.True(_account.IsInGoodStanding());
        }

        [Test]
        public void IsInGoodStanding_GivenNegativeBalance_ReturnsFalse()
        {
            GivenNegativeAccountBalance();

            Assert.False(_account.IsInGoodStanding());
        }

        private void GivenNegativeAccountBalance()
        {
            var negativeBalance = -Math.Abs(_initialBalance);
            _account = GivenAccountWithBalance(negativeBalance);
        }

        private void GivenNonnegativeAccountBalance()
        {
            var positiveBalance = Math.Abs(_initialBalance);
            _account = GivenAccountWithBalance(positiveBalance);
        }

        private static Account GivenAccountWithBalance(int balance)
        {
            return new Account(balance);
        }
    }
}
