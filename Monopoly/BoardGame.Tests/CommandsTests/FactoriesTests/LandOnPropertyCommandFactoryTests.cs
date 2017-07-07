using BoardGame.Commands;
using BoardGame.Commands.Factories;
using BoardGame.RealEstate;

using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;

using Tests.Support;
using Tests.Support.Extensions;

namespace BoardGame.Tests.CommandsTests.FactoriesTests
{
    public class LandOnPropertyCommandFactoryTests : BaseTest
    {
        private IPlayer _player;
        private Mock<IProperty> _mockProperty;

        private LandOnPropertyCommandFactory _factory;

        [SetUp]
        public void SetUp()
        {
            _player = Fixture.Create<IPlayer>();
            _mockProperty = Fixture.Mock<IProperty>();

            _factory = Fixture.Create<LandOnPropertyCommandFactory>();
        }

        [Test]
        public void CreateFor_GivenPropertyHasAnOwner_ReturnsAssessRentCommand()
        {
            GivenPropertyHasAnOwner();

            var command = _factory.CreateFor(_player);

            Assert.That(command, Is.TypeOf<AssessRentCommand>());
        }

        [Test]
        public void CreateFor_GivenPropertyHasNoOwner_ReturnsPurchasePropertyCommand()
        {
            GivenPropertyHasNoOwner();

            var command = _factory.CreateFor(_player);

            Assert.That(command, Is.TypeOf<PurchasePropertyCommand>());
        }

        private void GivenPropertyHasAnOwner()
        {
        }

        private void GivenPropertyHasNoOwner()
        {
            _mockProperty.Setup(p => p.Owner).Returns(default(IPlayer));
        }
    }
}
