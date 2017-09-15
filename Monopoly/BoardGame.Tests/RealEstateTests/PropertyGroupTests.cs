using System.Collections.Generic;
using System.Linq;

using BoardGame.RealEstate;
using BoardGame.RealEstate.Rent;

using NUnit.Framework;
using Ploeh.AutoFixture;

using Tests.Support;
using Tests.Support.Extensions;

namespace BoardGame.Tests.RealEstateTests
{
    public class PropertyGroupTests : BaseTest
    {
        private IEnumerable<IProperty> _properties;
        private IProperty _thisProperty;
        private int _expectedRent;

        private PropertyGroup _propertyGroup;

        [SetUp]
        public void SetUp()
        {
            _properties = Fixture.CreateMany<IProperty>().ToList();
            Fixture.Register(() => _properties);

            _thisProperty = Fixture.SelectFrom(_properties);
            var otherProperties = _properties.Except(new[] { _thisProperty });
            _expectedRent = Fixture.Create<int>();

            var mockRentStrategy = Fixture.Mock<IRentStrategy>();
            mockRentStrategy.Setup(r => r.GetRentValue(_thisProperty, otherProperties))
                .Returns(() => _expectedRent);

            _propertyGroup = Fixture.Create<PropertyGroup>();
        }

        [Test]
        public void GetRentFor_GivenAPropertyInTheGroup_GetsRentFromRentStrategy()
        {
            var actualRent = _propertyGroup.GetRentFor(_thisProperty);

            Assert.That(actualRent, Is.EqualTo(_expectedRent));
        }

        [Test]
        public void Contains_GivenAPropertyInTheGroup_ReturnsTrue()
        {
            Assert.True(_propertyGroup.Contains(_thisProperty));
        }

        [Test]
        public void Contains_GivenAPropertyNotInTheGroup_ReturnsFalse()
        {
            var propertyNotInTheGroup = Fixture.Create<IProperty>();
            Assert.False(_propertyGroup.Contains(propertyNotInTheGroup));
        }

        [Test]
        public void Enumerate_YieldsPropertiesPassedToConstructor()
        {
            Assert.That(_propertyGroup, Is.EquivalentTo(_properties));
        }
    }
}
