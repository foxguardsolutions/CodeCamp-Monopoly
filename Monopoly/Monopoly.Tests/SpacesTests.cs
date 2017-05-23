using System.Linq;

using BoardGame;

using NUnit.Framework;

namespace Monopoly.Tests
{
    [TestFixture]
    public class SpacesTests
    {
        [Test]
        public void Spaces_ReturnCorrectNames()
        {
            var spaces = new Spaces();

            AssertHaveCorrectNames(spaces);
        }

        private void AssertHaveCorrectNames(Spaces spaces)
        {
            foreach (var pair in spaces.Zip(Spaces.Names, (s, n) => new { Space = s, Name = n }))
                Assert.That(pair.Space, Has.Property(nameof(ISpace.Name)).EqualTo(pair.Name));
        }
    }
}
