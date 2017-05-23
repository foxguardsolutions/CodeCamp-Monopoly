using System.Collections.Generic;

using BoardGame.Commands.Factories;
using BoardGame.Construction;

using Moq;
using Moq.Protected;
using NUnit.Framework;
using Ploeh.AutoFixture;

using Tests.Support;
using Tests.Support.Extensions;

namespace BoardGame.Tests.ConstructionTests
{
    public class SpaceCommandFactoryBinderTests : BaseTest
    {
        private IEnumerable<Mock<ISpace>> _mockSpaces;

        private Mock<SpaceCommandFactoryBinder> _mockBinder;

        [SetUp]
        public void SetUp()
        {
            _mockSpaces = Fixture.MockMany<ISpace>();
            _mockBinder = GivenMockBinderWithCommandFactories();
        }

        [Test]
        public void BindCommandFactoriesToSpaces_GivenBinderWithCommandFactoriesSet_SetsCommandFactoriesForSpaces()
        {
            _mockBinder.Object.BindCommandFactoriesToSpaces();

            VerifyCommandFactoryWasSetForEach(_mockSpaces);
        }

        private static void VerifyCommandFactoryWasSetForEach(IEnumerable<Mock<ISpace>> mockSpaces)
        {
            foreach (var mockSpace in mockSpaces)
                mockSpace.VerifySet(s => s.CommandFactory = It.IsAny<ICommandFactory>());
        }

        private Mock<SpaceCommandFactoryBinder> GivenMockBinderWithCommandFactories()
        {
            var mockBinder = Fixture.Create<Mock<SpaceCommandFactoryBinder>>();
            mockBinder.Protected().SetupGet<IEnumerable<ICommandFactory>>("CommandFactories")
                .Returns(Fixture.CreateMany<ICommandFactory>());
            return mockBinder;
        }
    }
}
