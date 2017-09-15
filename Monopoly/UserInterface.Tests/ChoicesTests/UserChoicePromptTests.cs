using System.Collections.Generic;
using System.Linq;

using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;

using Tests.Support;
using Tests.Support.Extensions;
using UserInterface.Choices;
using UserInterface.Extensions;

namespace UserInterface.Tests.ChoicesTests
{
    public class UserChoicePromptTests : BaseTest
    {
        private Mock<ITextReaderWriter> _mockReaderWriter;
        private Mock<IEnum> _mockEnum;
        private string _message;
        private IEnumerable<ByteEnum> _options;

        private UserChoicePrompt _userChoicePrompt;

        [SetUp]
        public void Setup()
        {
            _message = Fixture.Create<string>();
            _options = Fixture.CreateMany<ByteEnum>();
            _mockEnum = Fixture.Mock<IEnum>();
            _mockReaderWriter = Fixture.Mock<ITextReaderWriter>();

            _mockEnum.Setup(e => e.GetValues(typeof(ByteEnum)))
                .Returns(_options.ToArray())
                .Verifiable();
            _mockEnum.Setup(e => e.GetUnderlyingType(typeof(ByteEnum)))
                .Returns(typeof(byte))
                .Verifiable();

            _userChoicePrompt = Fixture.Create<UserChoicePrompt>();
        }

        [Test]
        public void GetInput_GivenMessage_WritesMessage()
        {
            _userChoicePrompt.GetInput<ByteEnum>(_message);

            _mockReaderWriter.Verify(rw => rw.WriteLine(_message));
            _mockEnum.Verify();
        }

        [Test]
        public void GetInput_WritesOptions()
        {
            _userChoicePrompt.GetInput<ByteEnum>(_message);

            foreach (var option in _options)
                _mockReaderWriter.Verify(rw => rw.WriteLine(It.IsRegex($"Enter \"\\d+\" for {option.GetDescription()}\\.")));
            _mockEnum.Verify();
        }
    }
}