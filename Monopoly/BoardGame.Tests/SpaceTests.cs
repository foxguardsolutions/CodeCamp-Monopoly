using NUnit.Framework;
using Ploeh.AutoFixture;

using Tests.Support;

namespace BoardGame.Tests
{
    public class SpaceTests : BaseTest
    {
        [Test]
        public void Number_ReturnsSpaceNumber()
        {
            var spaceNumber = Fixture.Freeze<uint>();
            var space = Fixture.Create<Space>();

            Assert.That(space, Has.Property(nameof(Space.Number)).EqualTo(spaceNumber));
        }
    }
}
