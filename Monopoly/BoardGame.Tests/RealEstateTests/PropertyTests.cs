using BoardGame.RealEstate;

using NUnit.Framework;
using Ploeh.AutoFixture;

using Tests.Support;

namespace BoardGame.Tests.RealEstateTests
{
    public class PropertyTests : BaseTest
    {
        [Test]
        public void NewProperty_StoresBaseRentAndPurchasePricePassedToConstructor()
        {
            var baseRent = Fixture.Create<int>();
            var purchasePrice = Fixture.Create<uint>();

            var property = new Property(baseRent, purchasePrice);

            Assert.That(
                property,
                Has.Property(nameof(Property.BaseRent)).EqualTo(baseRent)
                .And.Property(nameof(Property.PurchasePrice)).EqualTo(purchasePrice));
        }
    }
}
