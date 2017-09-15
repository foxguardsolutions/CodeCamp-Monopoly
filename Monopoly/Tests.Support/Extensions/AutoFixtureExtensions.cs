using System.Collections.Generic;
using System.Linq;
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

        public static T SelectFrom<T>(this IFixture fixture, IEnumerable<T> collection)
        {
            var items = collection.ToList();
            var elementIndex = fixture.CreateInRange(0, items.Count - 1);
            return items[elementIndex];
        }

        public static Mock<T> Mock<T>(this IFixture fixture)
            where T : class
        {
            var mock = fixture.Create<Mock<T>>();
            fixture.Register(() => mock.Object);
            return mock;
        }

        public static IEnumerable<Mock<T>> MockMany<T>(this IFixture fixture)
            where T : class
        {
            var mocks = fixture.CreateMany<Mock<T>>();
            fixture.Register(() => mocks.Select(mock => mock.Object));
            fixture.Register(() => mocks.Select(mock => mock.Object).ToArray());
            return mocks;
        }
    }
}
