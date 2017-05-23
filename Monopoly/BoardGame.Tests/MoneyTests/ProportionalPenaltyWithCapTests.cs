using BoardGame.Money;

using NUnit.Framework;
using Ploeh.AutoFixture;

using Tests.Support;

namespace BoardGame.Tests.MoneyTests
{
    public class ProportionalPenaltyWithCapTests : BaseTest
    {
        private int _initialBalance;
        private int _penaltyPercentage;
        private int _maximumPenalty;

        [SetUp]
        public void SetUp()
        {
            _initialBalance = Fixture.Create<int>();
        }

        [Test]
        public void GetNewBalance_GivenCalculatedPenaltyBelowMaximumPenaltyValue_AssessesCalculatedPenalty()
        {
            GivenCalculatedPenaltyBelowCap();
            var penalty = new ProportionalPenaltyWithCap(_penaltyPercentage, _maximumPenalty);
            var expectedPenalty = _initialBalance * _penaltyPercentage / 100;

            var finalBalance = penalty.GetNewBalance(_initialBalance);

            Assert.That(finalBalance, Is.EqualTo(_initialBalance - expectedPenalty));
        }

        [Test]
        public void GetNewBalance_GivenCalculatedPenaltyGreaterThanOrEqualToMaximumPenaltyValue_AssessesMaximumPenalty()
        {
            GivenCalculatedPenaltyAtOrAboveCap();
            var penalty = new ProportionalPenaltyWithCap(_penaltyPercentage, _maximumPenalty);

            var finalBalance = penalty.GetNewBalance(_initialBalance);

            Assert.That(finalBalance, Is.EqualTo(_initialBalance - _maximumPenalty));
        }

        private void GivenCalculatedPenaltyAtOrAboveCap()
        {
            _maximumPenalty = Fixture.Create<int>();
            _penaltyPercentage = GivenValueGreaterThan(100 * _maximumPenalty / _initialBalance);
        }

        private void GivenCalculatedPenaltyBelowCap()
        {
            _penaltyPercentage = Fixture.Create<int>();
            _maximumPenalty = GivenValueGreaterThan(_initialBalance * _penaltyPercentage / 100);
        }

        private int GivenValueGreaterThan(int value)
        {
            return value + Fixture.Create<int>();
        }
    }
}
