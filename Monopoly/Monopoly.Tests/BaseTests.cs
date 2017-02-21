using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;

namespace Monopoly.Tests
{
    [TestFixture]
    public class BaseTests
    {
        protected IFixture Fixture { get; private set; }

        [SetUp]
        public void SetUpFixture()
        {
            Fixture = new Fixture()
                .Customize(new AutoMoqCustomization());

            var mockRoll = Fixture.Create<Mock<IRoll>>();
            mockRoll.Setup(r => r.Value).Returns((ushort)Fixture.CreateInRange(2, 12));
            Fixture.Register(() => mockRoll.Object);

            Fixture.Register<IRandom>(() => new RandomNumberGenerator());
        }
    }
}
