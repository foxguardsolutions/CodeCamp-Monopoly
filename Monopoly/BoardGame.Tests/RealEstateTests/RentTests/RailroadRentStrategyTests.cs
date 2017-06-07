using System;
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
    public class RailroadRentStrategyTests : BaseTest
    {
        private Mock<IProperty> _mockThisRailroad;
        private IEnumerable<Mock<IProperty>> _mockOtherRailroads;

        private int _baseRent;

        private RailroadRentStrategy _rentStrategy;

        [SetUp]
        public void SetUp()
        {
            _mockThisRailroad = Fixture.Mock<IProperty>();
            _mockOtherRailroads = Fixture.MockMany<IProperty>();

            _baseRent = Fixture.Create<int>();
            _mockThisRailroad.Setup(p => p.BaseRent).Returns(_baseRent);

            _rentStrategy = Fixture.Create<RailroadRentStrategy>();
        }

        [Test]
        public void GetRentValue_GivenCountOfRailroadsOwnedByThisOwner_ReturnsBaseRentTimesTotalCountOfThisOwnersOwnedRailroads()
        {
            var countOfOtherPropertiesOwnedByThisRailroadsOwner = GivenCountOfOtherRailroadsOwnedByThisRailroadsOwner();
            var totalCountOfPropertiesOwnedByThisPropertysOwner = countOfOtherPropertiesOwnedByThisRailroadsOwner + 1;

            var actualRent = _rentStrategy.GetRentValue(
                _mockThisRailroad.Object,
                _mockOtherRailroads.Select(m => m.Object));

            Assert.That(actualRent, Is.EqualTo(_baseRent * totalCountOfPropertiesOwnedByThisPropertysOwner));
        }

        private int GivenCountOfOtherRailroadsOwnedByThisRailroadsOwner()
        {
            var random = new Random();
            var countOfOtherRailroadsOwnedByThisRailroadsOwner = random.Next(0, _mockOtherRailroads.Count() + 1);
            GivenRailroadsOwnedByThisRailroadsOwner(countOfOtherRailroadsOwnedByThisRailroadsOwner);
            return countOfOtherRailroadsOwnedByThisRailroadsOwner;
        }

        private void GivenRailroadsOwnedByThisRailroadsOwner(int count)
        {
            _mockThisRailroad.Setup(p => p.Owner).ReturnsUsingFixture(Fixture);
            foreach (var mockOtherProperty in _mockOtherRailroads.Take(count))
                mockOtherProperty.Setup(p => p.Owner).Returns(_mockThisRailroad.Object.Owner);
        }
    }
}
