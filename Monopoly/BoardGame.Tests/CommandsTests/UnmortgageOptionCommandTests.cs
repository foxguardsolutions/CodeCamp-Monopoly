using BoardGame.Commands;
using BoardGame.Commands.Factories;
using BoardGame.RealEstate;
using BoardGame.RealEstate.Choices;

using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;

using Tests.Support;
using Tests.Support.Extensions;
using UserInterface.Choices;

using static BoardGame.RealEstate.Choices.UnmortgageProperty;

namespace BoardGame.Tests.CommandsTests
{
    public class UnmortgageOptionCommandTests : BaseTest
    {
        private IPlayer _player;
        private Mock<IProperty> _mockProperty;
        private Mock<ITransactionCommandFactory> _mockCommandFactory;
        private Mock<IOptionSelector> _mockOptionSelector;
        private ICommand _mortgagePaymentWithdrawalCommand;

        private UnmortgageOptionCommand _command;

        [SetUp]
        public void SetUp()
        {
            _player = Fixture.Freeze<IPlayer>();
            _mockProperty = Fixture.Mock<IProperty>();
            _mockProperty.SetupProperty(p => p.Owner);
            _mockCommandFactory = Fixture.Mock<ITransactionCommandFactory>();
            _mockOptionSelector = Fixture.Mock<IOptionSelector>();
            _mortgagePaymentWithdrawalCommand = Given_MortgagePaymentWithdrawalCommand();

            _command = Fixture.Create<UnmortgageOptionCommand>();
        }

        [Test]
        public void Execute_GivenPlayerIsNotPropertyOwner_DoesNotOfferOptionToUnmortgageProperty()
        {
            _command.Execute();

            _mockOptionSelector.Verify(
                os => os.ChooseOption(It.IsAny<UnmortgageProperty>(), It.IsAny<string>()),
                Times.Never);
        }

        [Test]
        public void Execute_GivenPlayerIsNotPropertyOwner_DoesNotUnmortgageProperty()
        {
            _command.Execute();

            _mockCommandFactory.Verify(cf => cf.Create(_player, It.IsAny<uint>()), Times.Never);
            _mockProperty.VerifySet(p => p.IsMortgaged = false, Times.Never);
            Assert.That(_command.GetSubsequentCommands(), Is.Empty);
        }

        [Test]
        public void Execute_GivenPlayerOwnsPropertyButPropertyIsNotMortgaged_DoesNotOfferOptionToUnmortgageProperty()
        {
            Given_PlayerOwnsProperty();
            Given_PropertyIsNotMortgaged();

            _command.Execute();

            _mockOptionSelector.Verify(
                os => os.ChooseOption(It.IsAny<UnmortgageProperty>(), It.IsAny<string>()),
                Times.Never);
        }

        [Test]
        public void Execute_GivenPlayerOwnsPropertyButPropertyIsNotMortgaged_DoesNotUnmortgageProperty()
        {
            Given_PlayerOwnsProperty();
            Given_PropertyIsNotMortgaged();

            _command.Execute();

            _mockCommandFactory.Verify(cf => cf.Create(_player, It.IsAny<uint>()), Times.Never);
            _mockProperty.VerifySet(p => p.IsMortgaged = false, Times.Never);
            Assert.That(_command.GetSubsequentCommands(), Is.Empty);
        }

        [Test]
        public void Execute_GivenPlayerOwnsPropertyAndPropertyIsMortgaged_OffersOptionToUnmortgageProperty()
        {
            Given_PlayerOwnsProperty();
            Given_PropertyIsMortgaged();

            _command.Execute();

            _mockOptionSelector.Verify(os => os.ChooseOption(Yes, UnmortgageOptionCommand.Message));
        }

        [Test]
        public void Execute_GivenPlayerChoosesNotToUnmortgageProperty_DoesNotUnmortgageProperty()
        {
            Given_PlayerChoosesNotToUnmortgageProperty();

            _command.Execute();

            _mockCommandFactory.Verify(cf => cf.Create(_player, It.IsAny<uint>()), Times.Never);
            _mockProperty.VerifySet(p => p.IsMortgaged = false, Times.Never);
            Assert.That(_command.GetSubsequentCommands(), Is.Empty);
        }

        [Test]
        public void Execute_GivenPlayerChoosesToUnmortgageProperty_UnmortgagesProperty()
        {
            Given_PlayerChoosesToUnmortgageProperty();

            _command.Execute();

            _mockProperty.VerifySet(p => p.IsMortgaged = false);
            Assert.That(_command.GetSubsequentCommands(), Contains.Item(_mortgagePaymentWithdrawalCommand));
        }

        private void Given_PlayerChoosesToUnmortgageProperty()
        {
            Given_PlayerOwnsProperty();
            Given_PropertyIsMortgaged();
            Given_OptionSelectorReturns(Yes);
        }

        private void Given_PlayerChoosesNotToUnmortgageProperty()
        {
            Given_PlayerOwnsProperty();
            Given_PropertyIsMortgaged();
            Given_OptionSelectorReturns(No);
        }

        private void Given_OptionSelectorReturns(UnmortgageProperty optionSelected)
        {
            _mockOptionSelector.Setup(os => os.ChooseOption(It.IsAny<UnmortgageProperty>(), It.IsAny<string>()))
                .Returns(optionSelected);
        }

        private void Given_PropertyIsMortgaged()
        {
            _mockProperty.SetupProperty(p => p.IsMortgaged, true);
        }

        private void Given_PropertyIsNotMortgaged()
        {
            _mockProperty.SetupProperty(p => p.IsMortgaged, false);
        }

        private void Given_PlayerOwnsProperty()
        {
            _mockProperty.Object.Owner = _player;
        }

        private ICommand Given_MortgagePaymentWithdrawalCommand()
        {
            var mortgagePaymentValue = Given_PropertyPurchasePrice();

            var mortgagePaymentWithdrawalCommand = Fixture.Create<ICommand>();
            _mockCommandFactory.Setup(cf => cf.Create(_player, mortgagePaymentValue))
                .Returns(mortgagePaymentWithdrawalCommand);
            return mortgagePaymentWithdrawalCommand;
        }

        private uint Given_PropertyPurchasePrice()
        {
            var purchasePrice = Fixture.Create<uint>();
            _mockProperty.Setup(p => p.PurchasePrice).Returns(purchasePrice);
            return purchasePrice;
        }
    }
}
