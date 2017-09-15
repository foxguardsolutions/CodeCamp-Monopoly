using System.Collections.Generic;
using System.Linq;

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
    public class StreetRentStrategyTests : BaseTest
    {
        private Mock<IProperty> _mockThisProperty;
        private IEnumerable<Mock<IProperty>> _mockOtherProperties;

        private int _baseRent;

        private StreetRentStrategy _rentStrategy;

        [SetUp]
        public void SetUp()
        {
            _mockThisProperty = Fixture.Mock<IProperty>();
            _mockOtherProperties = Fixture.MockMany<IProperty>();

            _baseRent = Fixture.Create<int>();
            _mockThisProperty.Setup(p => p.BaseRent).Returns(_baseRent);

            _rentStrategy = Fixture.Create<StreetRentStrategy>();
        }

        [Test]
        public void GetRentValue_GivenNoOtherPropertiesOwnedByThisPropertysOwner_ReturnsBaseRentFromThisProperty()
        {
            GivenNoOtherPropertiesOwnedByThisPropertysOwner();

            var actualRent = _rentStrategy.GetRentValue(
                _mockThisProperty.Object,
                _mockOtherProperties.Select(m => m.Object));

            Assert.That(actualRent, Is.EqualTo(_baseRent));
        }

        [Test]
        public void GetRentValue_GivenSomeButNotAllOtherPropertiesOwnedByThisPropertysOwner_ReturnsBaseRentFromThisProperty()
        {
            GivenSomeButNotAllOtherPropertiesOwnedByThisPropertysOwner();

            var actualRent = _rentStrategy.GetRentValue(
                _mockThisProperty.Object,
                _mockOtherProperties.Select(m => m.Object));

            Assert.That(actualRent, Is.EqualTo(_baseRent));
        }

        [Test]
        public void GetRentValue_GivenAllOtherPropertiesOwnedByThisPropertysOwner_ReturnsBaseRentFromThisPropertyTimesCompletedGroupMultiplier()
        {
            GivenAllOtherPropertiesOwnedByThisPropertysOwner();

            var actualRent = _rentStrategy.GetRentValue(
                _mockThisProperty.Object,
                _mockOtherProperties.Select(m => m.Object));

            Assert.That(actualRent, Is.EqualTo(_baseRent * StreetRentStrategy.CompletedGroupMultiplier));
        }

        private void GivenNoOtherPropertiesOwnedByThisPropertysOwner()
        {
            _mockThisProperty.Setup(p => p.Owner).ReturnsUsingFixture(Fixture);
            foreach (var mockOtherProperty in _mockOtherProperties)
                mockOtherProperty.Setup(p => p.Owner).ReturnsUsingFixture(Fixture);
        }

        private void GivenSomeButNotAllOtherPropertiesOwnedByThisPropertysOwner()
        {
            GivenAllOtherPropertiesOwnedByThisPropertysOwner();
            _mockOtherProperties.First().Setup(p => p.Owner).ReturnsUsingFixture(Fixture);
        }

        private void GivenAllOtherPropertiesOwnedByThisPropertysOwner()
        {
            _mockThisProperty.Setup(p => p.Owner).ReturnsUsingFixture(Fixture);
            foreach (var mockOtherProperty in _mockOtherProperties)
                mockOtherProperty.Setup(p => p.Owner).Returns(_mockThisProperty.Object.Owner);
        }
    }
}
