using Moq;
using Ploeh.AutoFixture;

namespace Tests.Support.Extensions
{
    public static class AutoFixtureExtensions
    {
        public static int CreateInRange(this IFixture fixture, int lowerLimit, int upperLimit)
        {
            if (lowerLimit == upperLimit)
                return lowerLimit;

            fixture.Customizations.Add(new RandomNumericSequenceGenerator(lowerLimit, upperLimit));
            var value = fixture.Create<int>();
            fixture.Customizations.RemoveAt(fixture.Customizations.Count - 1);
            return value;
        }

        public static Mock<T> Mock<T>(this IFixture fixture)
            where T : class
        {
            var mock = fixture.Create<Mock<T>>();
            fixture.Register(() => mock.Object);
            return mock;
        }
    }
}
