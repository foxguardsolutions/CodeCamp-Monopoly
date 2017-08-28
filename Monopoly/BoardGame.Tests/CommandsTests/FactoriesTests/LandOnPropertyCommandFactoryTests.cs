using System;

using BoardGame.Commands;
using BoardGame.Commands.Factories;
using BoardGame.RealEstate;

using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;

using Tests.Support;
using Tests.Support.Extensions;

namespace BoardGame.Tests.CommandsTests.FactoriesTests
{
    public class LandOnPropertyCommandFactoryTests : BaseTest
    {
        private IPlayer _player;
        private Mock<IProperty> _mockProperty;
        private Mock<Func<IPlayer, IProperty, PurchasePropertyCommand>> _mockInnerPurchaseFactory;
        private Mock<Func<IPlayer, IProperty, AssessRentCommand>> _mockInnerRentFactory;

        private LandOnPropertyCommandFactory _factory;

        [SetUp]
        public void SetUp()
        {
            _player = Fixture.Create<IPlayer>();
            _mockProperty = Fixture.Mock<IProperty>();
            _mockInnerPurchaseFactory = Given_MockCommandFactory<PurchasePropertyCommand>();
            _mockInnerRentFactory = Given_MockCommandFactory<AssessRentCommand>();

            _factory = Fixture.Create<LandOnPropertyCommandFactory>();
        }

        [Test]
        public void CreateFor_GivenPropertyHasAnOwner_ReturnsCommandFromInnerRentFactory()
        {
            GivenPropertyHasAnOwner();

            _factory.CreateFor(_player);

            _mockInnerRentFactory.Verify();
        }

        [Test]
        public void CreateFor_GivenPropertyHasNoOwner_ReturnsCommandFromInnerPurchaseFactory()
        {
            GivenPropertyHasNoOwner();

            _factory.CreateFor(_player);

            _mockInnerPurchaseFactory.Verify();
        }

        private Mock<Func<IPlayer, IProperty, TCommand>> Given_MockCommandFactory<TCommand>()
        {
            var mockCommandFactory = Fixture.Mock<Func<IPlayer, IProperty, TCommand>>();
            mockCommandFactory.Setup(cf => cf(_player, _mockProperty.Object)).ReturnsUsingFixture(Fixture).Verifiable();
            return mockCommandFactory;
        }

        private void GivenPropertyHasAnOwner()
        {
            _mockProperty.Setup(p => p.Owner).ReturnsUsingFixture(Fixture);
        }

        private void GivenPropertyHasNoOwner()
        {
            _mockProperty.Setup(p => p.Owner).Returns(default(IPlayer));
        }
    }
}
