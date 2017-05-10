using BoardGame.Dice;

using NUnit.Framework;
using Ploeh.AutoFixture;

using Tests.Support;

namespace BoardGame.Tests.DiceTests
{
    public class RollTests : BaseTest
    {
        [Test]
        public void Value_ReturnsRollValue()
        {
            var value = Fixture.Freeze<ushort>();
            var roll = Fixture.Create<Roll>();

            Assert.That(roll, Has.Property(nameof(Roll.Value)).EqualTo(value));
        }
    }
}
