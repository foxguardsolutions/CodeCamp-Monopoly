using NUnit.Framework;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;

using Shuffler;

namespace Tests.Support
{
    [TestFixture]
    public abstract class BaseTest
    {
        protected IFixture Fixture { get; private set; }
        [SetUp]
        public void AutoMoqSetup()
        {
            Fixture = new Fixture()
                .Customize(new AutoMoqCustomization());

            Fixture.Register<IShuffler>(() => Fixture.Create<FisherYatesShuffler>());
        }
    }
}
