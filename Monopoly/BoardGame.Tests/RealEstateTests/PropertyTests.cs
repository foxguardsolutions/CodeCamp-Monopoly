using BoardGame.RealEstate;

using NUnit.Framework;
using Ploeh.AutoFixture;

using Tests.Support;

namespace BoardGame.Tests.RealEstateTests
{
    public class PropertyTests : BaseTest
    {
        [Test]
        public void NewProperty_StoresBaseRentPassedToConstructor()
        {
            var baseRent = Fixture.Create<int>();

            var property = new Property(baseRent);

            Assert.That(property, Has.Property(nameof(Property.BaseRent)).EqualTo(baseRent));
        }
    }
}
