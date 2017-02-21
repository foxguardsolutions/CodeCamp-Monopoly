using NUnit.Framework;
using Ploeh.AutoFixture;

namespace Monopoly.Tests
{
    public class RandomNumberGeneratorTests : BaseTests
    {
        [Test]
        public void Next_ReturnsValueBetweenMinValueAndMaxValue()
        {
            var minValue = Fixture.Create<int>();
            var valueGreaterThanMinValue = Fixture.Create<int>() + minValue;
            var randomNumberGenerator = Fixture.Create<RandomNumberGenerator>();

            var randomValue = randomNumberGenerator.Next(minValue, valueGreaterThanMinValue);

            Assert.That(randomValue, Is.AtLeast(minValue).And.AtMost(valueGreaterThanMinValue));
        }
    }
}
