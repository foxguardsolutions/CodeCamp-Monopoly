using BoardGame.RealEstate;
using BoardGame.RealEstate.Rent;

using NUnit.Framework;
using Ploeh.AutoFixture;

using Tests.Support;
using Tests.Support.Extensions;

namespace BoardGame.Tests.RealEstateTests.RentTests
{
    public class RentCalculatorTests : BaseTest
    {
        private IProperty _thisProperty;
        private int _expectedRent;

        private RentCalculator _rentCalculator;

        [SetUp]
        public void SetUp()
        {
            var groups = Fixture.MockMany<IPropertyGroup>();
            var groupContainingProperty = Fixture.SelectFrom(groups);

            _thisProperty = Fixture.Create<IProperty>();
            _expectedRent = Fixture.Create<int>();

            groupContainingProperty.Setup(g => g.Contains(_thisProperty)).Returns(true);
            groupContainingProperty.Setup(g => g.GetRentFor(_thisProperty))
                .Returns(() => _expectedRent);

            _rentCalculator = Fixture.Create<RentCalculator>();
        }

        [Test]
        public void GetRentFor_GivenAPropertyInAGroup_GetsRentFromPropertyGroup()
        {
            var actualRent = _rentCalculator.GetRentFor(_thisProperty);

            Assert.That(actualRent, Is.EqualTo(_expectedRent));
        }
    }
}
