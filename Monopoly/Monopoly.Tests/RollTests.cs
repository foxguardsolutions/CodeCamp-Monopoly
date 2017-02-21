using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace Monopoly.Tests
{
    public class RollTests : BaseTests
    {
        [Test]
        public void RollValue_GivenManyDiceEachWithManySides_IsOneOfExpectedValues()
        {
            var dice = GivenManyDiceEachWithManySides();
            var allPossibleRollValues = GetAllPossibleRollValues(dice);

            var roll = Fixture.Create<Roll>();
            var rollValue = roll.Value;

            Assert.That(allPossibleRollValues, Has.Member(rollValue));
        }

        private IEnumerable<ushort>[] GivenManyDiceEachWithManySides()
        {
            var dice = Fixture.CreateMany<IEnumerable<ushort>>().ToArray();
            Fixture.Register(() => dice);
            return dice;
        }

        private IEnumerable<ushort> GetAllPossibleRollValues(IEnumerable<IEnumerable<ushort>> dice)
        {
            return from possibleRoll in dice.CartesianProduct()
                   select (ushort)possibleRoll.Sum(d => d);
        }
    }
}
