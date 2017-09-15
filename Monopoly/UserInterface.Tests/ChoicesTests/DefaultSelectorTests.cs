using NUnit.Framework;
using Ploeh.AutoFixture;
using Tests.Support;
using UserInterface.Choices;

namespace UserInterface.Tests.ChoicesTests
{
    public class DefaultSelectorTests : BaseTest
    {
        private DefaultSelector _selector;

        [SetUp]
        public void SetUp()
        {
            _selector = Fixture.Create<DefaultSelector>();
        }

        [Test]
        public void ChooseOption_GivenNoDefault_ReturnsDefaultOption()
        {
            var selection = _selector.ChooseOption<ByteEnum>();

            Assert.That(selection, Is.EqualTo(default(ByteEnum)));
        }

        [Test]
        public void ChooseOption_GivenDefault_ReturnsSpecifiedDefault()
        {
            var defaultOption = Fixture.Create<ByteEnum>();

            var selection = _selector.ChooseOption(defaultOption);

            Assert.That(selection, Is.EqualTo(defaultOption));
        }
    }
}
