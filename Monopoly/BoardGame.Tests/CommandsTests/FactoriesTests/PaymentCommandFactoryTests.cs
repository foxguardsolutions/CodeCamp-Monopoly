using System.Linq;

using BoardGame.Commands.Factories;

using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;

using Tests.Support;
using Tests.Support.Extensions;

namespace BoardGame.Tests.CommandsTests.FactoriesTests
{
    public class PaymentCommandFactoryTests : BaseTest
    {
        private IPlayer _sender;
        private IPlayer _recipient;
        private uint _amount;

        private Mock<ITransactionCommandFactory> _mockWithdrawalFactory;
        private Mock<ITransactionCommandFactory> _mockDepositFactory;

        private PaymentCommandFactory _paymentCommandFactory;

        [SetUp]
        public void SetUp()
        {
            _sender = Fixture.Create<IPlayer>();
            _recipient = Fixture.Create<IPlayer>();
            _amount = Fixture.Create<uint>();

            _mockWithdrawalFactory = Fixture.Mock<ITransactionCommandFactory>();
            _mockDepositFactory = Fixture.Mock<ITransactionCommandFactory>();

            _paymentCommandFactory = new PaymentCommandFactory(
                _mockWithdrawalFactory.Object,
                _mockDepositFactory.Object);
        }

        [Test]
        public void CreatePaymentCommands_GivenAmount_CreatesCommandsUsingWithdrawalAndDepositCommandFactories()
        {
            _paymentCommandFactory.CreatePaymentCommands(_sender, _recipient, _amount).ToList();

            _mockWithdrawalFactory.Verify(w => w.Create(_sender, _amount));
            _mockDepositFactory.Verify(w => w.Create(_recipient, _amount));
        }

        [Test]
        public void CreateWithdrawalCommand_GivenAmount_CreatesCommandUsingWithdrawalCommandFactory()
        {
            _paymentCommandFactory.CreateWithdrawalCommand(_sender, _amount);

            _mockWithdrawalFactory.Verify(w => w.Create(_sender, _amount));
        }
    }
}
