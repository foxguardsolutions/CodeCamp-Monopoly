using Ploeh.AutoFixture;

namespace Monopoly.Tests
{
    public static class AutoFixtureExtension
    {
        public static long CreateInRange(this IFixture fixture, int lowerLimit, int upperLimit)
        {
            if (lowerLimit == upperLimit)
                return lowerLimit;

            fixture.Customizations.Add(new RandomNumericSequenceGenerator(lowerLimit, upperLimit));
            var value = fixture.Create<long>();
            fixture.Customizations.RemoveAt(fixture.Customizations.Count - 1);
            return value;
        }
    }
}
