using System.Collections.Generic;

using BoardGame.Commands;
using BoardGame.Commands.Factories;
using BoardGame.RealEstate;
using BoardGame.RealEstate.Rent;

using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;

using Tests.Support;
using Tests.Support.Extensions;

namespace BoardGame.Tests.CommandsTests
{
    public class AssessRentCommandTests : BaseTest
    {
        private IEnumerable<ICommand> _paymentCommands;

        private Mock<IProperty> _mockProperty;

        private IPlayer _playerOnProperty;
        private IPlayer _otherPlayer;

        private AssessRentCommand _command;

        [SetUp]
        public void SetUp()
        {
            _paymentCommands = Fixture.CreateMany<ICommand>();
            _mockProperty = Fixture.Mock<IProperty>();

            _otherPlayer = Fixture.Create<IPlayer>();
            _playerOnProperty = Fixture.Freeze<IPlayer>();

            var rentValue = Fixture.Create<uint>();
            GivenRentCalculatorThatReturns(rentValue);

            GivenPaymentCommandFactoryThatReturns(_paymentCommands, rentValue);

            _command = Fixture.Create<AssessRentCommand>();
        }

        private void GivenRentCalculatorThatReturns(uint rentValue)
        {
            var mockRentCalculator = Fixture.Mock<IRentCalculator>();
            mockRentCalculator.Setup(r => r.GetRentFor(_mockProperty.Object))
                .Returns((int)rentValue);
        }

        private void GivenPaymentCommandFactoryThatReturns(IEnumerable<ICommand> commands, uint amount)
        {
            var mockPaymentCommandFactory = Fixture.Mock<IPaymentCommandFactory>();
            mockPaymentCommandFactory.Setup(
                    f => f.CreatePaymentCommands(
                        _playerOnProperty,
                        It.Is<IPlayer>(p => p == _mockProperty.Object.Owner),
                        amount))
                .Returns(commands);
        }

        [Test]
        public void GetSubsequentCommands_GivenPropertyOwnerIsPlayerOnProperty_YieldsNoCommands()
        {
            GivenPropertyOwnedBy(_playerOnProperty);
            _command.Execute();

            var subsequentCommands = _command.GetSubsequentCommands();

            Assert.That(subsequentCommands, Is.Empty);
        }

        [Test]
        public void GetSubsequentCommands_GivenPropertyOwnerIsNotPlayerOnProperty_YieldsPaymentCommands()
        {
            GivenPropertyOwnedBy(_otherPlayer);
            _command.Execute();

            var subsequentCommands = _command.GetSubsequentCommands();

            Assert.That(subsequentCommands, Is.EquivalentTo(_paymentCommands));
        }

        private void GivenPropertyOwnedBy(IPlayer owner)
        {
            _mockProperty.Setup(p => p.Owner).Returns(owner);
        }
    }
}
