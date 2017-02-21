using System.Collections.Generic;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace Monopoly.Tests
{
    public class PairOfSixSidedDiceTests : BaseTests
    {
        [Test]
        public void Roll_ReturnsNewRollEachTime()
        {
            var dice = Fixture.Create<PairOfSixSidedDice>();
            var numberOfRolls = Fixture.Create<uint>();

            var rolls = RollTheDice(numberOfRolls, dice);

            Assert.That(rolls, Is.Unique);
        }

        private IEnumerable<IRoll> RollTheDice(uint numberOfTimes, PairOfSixSidedDice dice)
        {
            for (uint i = 0; i < numberOfTimes; i++)
                yield return dice.Roll();
        }

        [Test]
        public void Roll_GivenDieFaceValues_ReturnsARollWithAFaceValueFromEachDie()
        {
            var dice = Fixture.Create<PairOfSixSidedDice>();

            var roll = dice.Roll();
            var rollValue = roll.Value;

            Assert.That(rollValue, Is.AtLeast(2).And.AtMost(12));
        }
    }
}
