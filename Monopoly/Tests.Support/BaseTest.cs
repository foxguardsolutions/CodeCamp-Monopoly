using NUnit.Framework;
using Ploeh.AutoFixture;

namespace Tests.Support
{
    [TestFixture]
    public abstract class BaseTest
    {
        protected IFixture Fixture { get; private set; }
        [SetUp]
        public void AutoMoqSetup()
        {
            Fixture = new Fixture();
        }
    }
}
