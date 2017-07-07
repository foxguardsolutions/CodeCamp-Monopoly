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
    public class PurchasePropertyCommandTests : BaseTest
    {
        private ICommand _paymentCommand;

        private Mock<IProperty> _mockProperty;
        private uint _purchasePrice;
        private IPlayer _player;

        private PurchasePropertyCommand _command;

        [SetUp]
        public void SetUp()
        {
            _mockProperty = Fixture.Mock<IProperty>();
            _purchasePrice = Fixture.Create<uint>();
            _mockProperty.Setup(p => p.PurchasePrice).Returns(_purchasePrice);

            _player = Fixture.Freeze<IPlayer>();

            var mockPaymentCommandFactory = Fixture.Mock<IPaymentCommandFactory>();
            _paymentCommand = Fixture.Create<ICommand>();
            mockPaymentCommandFactory.Setup(f => f.CreateWithdrawalCommand(_player, _purchasePrice))
                .Returns(_paymentCommand);

            _command = Fixture.Create<PurchasePropertyCommand>();
        }

        [Test]
        public void Execute_SetsPropertyOwnerToPlayer()
        {
            _command.Execute();

            _mockProperty.VerifySet(p => p.Owner = _player);
        }

        [Test]
        public void GetSubsequentCommands_YieldsCommandCreatedByPaymentCommandFactory()
        {
            _command.Execute();
            var subsequentCommands = _command.GetSubsequentCommands();

            Assert.That(subsequentCommands, Has.Exactly(1).Items.And.Exactly(1).EqualTo(_paymentCommand));
        }
    }
}
