using System.Collections.Generic;
using System.Linq;

using BoardGame.Commands;
using BoardGame.Commands.Factories;
using BoardGame.RealEstate;

using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;

using Tests.Support;
using Tests.Support.Extensions;

namespace BoardGame.Tests.CommandsTests
{
    public class OfferMortgageOptionCommandTests : BaseTest
    {
        private IPlayer _player;
        private IEnumerable<Mock<IPropertyGroup>> _mockPropertyGroups;
        private Mock<IMortgageOptionCommandFactory> _mockCommandFactory;

        private OfferMortgageOptionCommand _command;

        [SetUp]
        public void SetUp()
        {
            _player = Fixture.Freeze<IPlayer>();
            _mockPropertyGroups = Fixture.MockMany<IPropertyGroup>();
            _mockCommandFactory = Fixture.Mock<IMortgageOptionCommandFactory>();

            _command = Fixture.Create<OfferMortgageOptionCommand>();
        }

        [Test]
        public void Execute_GivenPropertyOwnedByPlayer_CreatesMortgageOptionCommandForTheProperty()
        {
            var property = Given_PropertyInAGroup();
            Given_PropertyOwnedBy(_player, property);

            _command.Execute();

            _mockCommandFactory.Verify(cf => cf.Create(_player, property));
            Assert.That(_command.GetSubsequentCommands(), Is.Not.Empty);
        }

        [Test]
        public void Execute_GivenPropertyNotOwnedByPlayer_DoesNotCreateMortgageOptionCommandForTheProperty()
        {
            var property = Given_PropertyInAGroup();

            _command.Execute();

            _mockCommandFactory.Verify(cf => cf.Create(_player, property), Times.Never);
            Assert.That(_command.GetSubsequentCommands(), Is.Empty);
        }

        private IProperty Given_PropertyInAGroup()
        {
            var mockProperty = Fixture.Mock<IProperty>();
            mockProperty.SetupProperty(p => p.Owner);
            _mockPropertyGroups.First()
                .Setup(pg => pg.GetEnumerator())
                .Returns(new[] { mockProperty.Object }.Select(p => p).GetEnumerator());
            return mockProperty.Object;
        }

        private static void Given_PropertyOwnedBy(IPlayer player, IProperty property)
        {
            property.Owner = player;
        }
    }
}
