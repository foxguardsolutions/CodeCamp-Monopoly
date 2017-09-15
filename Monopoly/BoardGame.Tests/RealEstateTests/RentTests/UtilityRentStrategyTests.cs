using System.Collections.Generic;
using System.Linq;

using BoardGame.Dice;
using BoardGame.RealEstate;
using BoardGame.RealEstate.Rent;

using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;

using Tests.Support;
using Tests.Support.Extensions;

namespace BoardGame.Tests.RealEstateTests.RentTests
{
    public class UtilityRentStrategyTests : BaseTest
    {
        private Mock<IProperty> _mockThisUtility;
        private IEnumerable<Mock<IProperty>> _mockOtherUtilities;

        private ushort _lastRollValue;

        private UtilityRentStrategy _rentStrategy;

        [SetUp]
        public void SetUp()
        {
            _mockThisUtility = Fixture.Mock<IProperty>();
            _mockOtherUtilities = Fixture.MockMany<IProperty>();

            _lastRollValue = Fixture.Create<ushort>();
            GivenDiceThatRoll(_lastRollValue);

            _rentStrategy = Fixture.Create<UtilityRentStrategy>();
        }

        private void GivenDiceThatRoll(ushort rollValue)
        {
            var mockRoll = GivenRollWithValue(rollValue);
            var mockDice = Fixture.Mock<IDiceWithCache>();
            mockDice.Setup(d => d.GetLastRoll()).Returns(mockRoll.Object);
        }

        private Mock<IRoll> GivenRollWithValue(ushort rollValue)
        {
            var mockRoll = Fixture.Mock<IRoll>();
            mockRoll.Setup(r => r.Value).Returns(rollValue);
            return mockRoll;
        }

        [Test]
        public void GetRentValue_GivenOnlyThisUtilityOwned_ReturnsDieRollValueTimesSingleUtilityOwnedMultiplier()
        {
            GivenOnlyThisUtilityOwned();

            var actualRent = _rentStrategy.GetRentValue(
                _mockThisUtility.Object,
                _mockOtherUtilities.Select(m => m.Object));

            Assert.That(actualRent, Is.EqualTo(_lastRollValue * UtilityRentStrategy.SingleUtilityOwnedMultiplier));
        }

        private void GivenOnlyThisUtilityOwned()
        {
            _mockThisUtility.Setup(p => p.Owner).ReturnsUsingFixture(Fixture);
            foreach (var mockOtherUtility in _mockOtherUtilities)
                mockOtherUtility.Setup(p => p.Owner).Returns(default(IPlayer));
        }

        [Test]
        public void GetRentValue_GivenAllUtilitiesOwned_ReturnsDieRollValueTimesAllUtilitiesOwnedMultiplier()
        {
            GivenAllUtilitiesOwned();

            var actualRent = _rentStrategy.GetRentValue(
                _mockThisUtility.Object,
                _mockOtherUtilities.Select(m => m.Object));

            Assert.That(actualRent, Is.EqualTo(_lastRollValue * UtilityRentStrategy.AllUtilitiesOwnedMultiplier));
        }

        private void GivenAllUtilitiesOwned()
        {
            _mockThisUtility.Setup(p => p.Owner).ReturnsUsingFixture(Fixture);
            foreach (var mockOtherUtility in _mockOtherUtilities)
                mockOtherUtility.Setup(p => p.Owner).ReturnsUsingFixture(Fixture);
        }
    }
}
