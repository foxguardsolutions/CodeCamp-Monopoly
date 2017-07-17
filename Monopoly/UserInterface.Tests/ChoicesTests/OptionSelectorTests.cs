using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;

using Tests.Support;
using Tests.Support.Extensions;
using UserInterface.Choices;

namespace UserInterface.Tests.ChoicesTests
{
    public class OptionSelectorTests : BaseTest
    {
        private Mock<IPrompt> _mockPrompt;
        private Mock<IOptionParser> _mockParser;
        private ByteEnum _byteEnum;

        private OptionSelector _selector;

        [SetUp]
        public void Setup()
        {
            _mockPrompt = Fixture.Mock<IPrompt>();
            _mockParser = Fixture.Mock<IOptionParser>();
            GivenSuccessfulParseOnFirstTry();

            _selector = Fixture.Create<OptionSelector>();
        }

        [Test]
        public void ChooseOption_GivenNoDefaultAndNoMessage_SendsDefaultMessageToUserPrompt()
        {
            _selector.ChooseOption<ByteEnum>();

            _mockPrompt.Verify(p => p.GetInput<ByteEnum>("Select an option:"), Times.Once);
        }

        [Test]
        public void ChooseOption_GivenMessageAndNoDefault_SendsMessageToUserPrompt()
        {
            var message = Fixture.Create<string>();

            _selector.ChooseOption<ByteEnum>(message);

            _mockPrompt.Verify(p => p.GetInput<ByteEnum>(message), Times.Once);
        }

        [Test]
        public void ChooseOption_GivenDefaultAndNoMessage_SendsDefaultMessageToUserPrompt()
        {
            var defaultOption = Fixture.Create<ByteEnum>();

            _selector.ChooseOption(defaultOption);

            _mockPrompt.Verify(p => p.GetInput<ByteEnum>("Select an option:"), Times.Once);
        }

        [Test]
        public void ChooseOption_GivenMessageAndDefault_SendsMessageToUserPrompt()
        {
            var message = Fixture.Create<string>();
            var defaultOption = Fixture.Create<ByteEnum>();

            _selector.ChooseOption(defaultOption, message);

            _mockPrompt.Verify(p => p.GetInput<ByteEnum>(message), Times.Once);
        }

        [Test]
        public void ChooseOption_GivenNoDefaultAndMultipleTriesForInputParse_SendsMessageToUserPromptMultipleTimes()
        {
            GivenSuccessfulParseAfterMultipleTries();

            _selector.ChooseOption<ByteEnum>();

            _mockPrompt.Verify(p => p.GetInput<ByteEnum>("Select an option:"), Times.AtLeast(2));
        }

        [Test]
        public void ChooseOption_GivenDefault_ReturnsResultAfterOneTry()
        {
            var defaultOption = Fixture.Create<ByteEnum>();
            var expectedOption = Given_ParsedOrDefaultOption<ByteEnum>();

            var selectedOption = _selector.ChooseOption(defaultOption);

            _mockPrompt.Verify(p => p.GetInput<ByteEnum>("Select an option:"), Times.Once);
            Assert.That(selectedOption, Is.EqualTo(expectedOption));
        }

        private void GivenSuccessfulParseOnFirstTry()
        {
            _mockParser.SetupIgnoreArgs(
                    p => p.TryParse(It.IsAny<string>(), out _byteEnum))
                .Returns(true);
        }

        private void GivenSuccessfulParseAfterMultipleTries()
        {
            _mockParser.SetupSequenceIgnoreArgs(
                    p => p.TryParse(It.IsAny<string>(), out _byteEnum))
                .Returns(false)
                .Returns(false)
                .Returns(true);
        }

        private TEnum Given_ParsedOrDefaultOption<TEnum>()
            where TEnum : struct
        {
            var result = Fixture.Create<TEnum>();
            _mockParser.Setup(p => p.ParseOrDefault(It.IsAny<string>(), It.IsAny<TEnum>())).Returns(result);
            return result;
        }
    }
}
