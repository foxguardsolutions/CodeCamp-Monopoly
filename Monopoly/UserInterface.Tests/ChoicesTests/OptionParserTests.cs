using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;

using Tests.Support;
using Tests.Support.Extensions;
using UserInterface.Choices;

namespace UserInterface.Tests.ChoicesTests
{
    public class OptionParserTests : BaseTest
    {
        private Mock<IEnum> _mockEnum;
        private string _value;
        private ByteEnum _byteEnum;

        private OptionParser _parser;

        [SetUp]
        public void Setup()
        {
            _mockEnum = Fixture.Mock<IEnum>();
            _value = Fixture.Create<string>();

            _parser = Fixture.Create<OptionParser>();
        }

        [Test]
        public void TryParse_GivenValueCanNotBeParsed_ReturnsFalse()
        {
            Given_ValueCanNotBeParsed();

            Assert.That(_parser.TryParse(_value, out _byteEnum), Is.False);
        }

        [Test]
        public void TryParse_GivenValueCanBeParsedAndIsNotDefined_ReturnsFalse()
        {
            Given_ValueCanBeParsed();
            Given_ParseResultIsNotDefined();

            Assert.That(_parser.TryParse(_value, out _byteEnum), Is.False);
        }

        [Test]
        public void TryParse_GivenValueCanBeParsedAndIsDefined_ReturnsTrue()
        {
            Given_ValueCanBeParsed();
            Given_ParseResultIsDefined();

            Assert.That(_parser.TryParse(_value, out _byteEnum), Is.True);
        }

        [Test]
        public void TryParse_GivenValueCanBeParsed_SetsResultToParsedValue()
        {
            var expectedParseResult = Given_ValueCanBeParsed();

            _parser.TryParse(_value, out _byteEnum);

            Assert.That(_byteEnum, Is.EqualTo(expectedParseResult));
        }

        private ByteEnum Given_ValueCanBeParsed()
        {
            var expectedResult = Fixture.Create<ByteEnum>();
            Given_MockEnumTryParse_Returns(true, expectedResult);
            return expectedResult;
        }

        private void Given_ValueCanNotBeParsed()
        {
            Given_MockEnumTryParse_Returns(false);
            Given_MockEnumIsDefined_Returns(Fixture.Create<bool>());
        }

        private void Given_MockEnumTryParse_Returns(bool returnValue, ByteEnum expectedResult = default(ByteEnum))
        {
            _mockEnum.Setup(e => e.TryParse(_value, out expectedResult))
                .Returns(returnValue);
        }

        private void Given_ParseResultIsDefined()
        {
            Given_MockEnumIsDefined_Returns(true);
        }

        private void Given_ParseResultIsNotDefined()
        {
            Given_MockEnumIsDefined_Returns(false);
        }

        private void Given_MockEnumIsDefined_Returns(bool returnValue)
        {
            _mockEnum.Setup(e => e.IsDefined(typeof(ByteEnum), It.IsAny<ByteEnum>())).Returns(returnValue);
        }
    }
}