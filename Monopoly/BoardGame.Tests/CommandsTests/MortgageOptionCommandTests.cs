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

using static BoardGame.RealEstate.Choices.MortgageProperty;

namespace BoardGame.Tests.CommandsTests
{
    public class MortgageOptionCommandTests : BaseTest
    {
        private IPlayer _player;
        private Mock<IProperty> _mockProperty;
        private Mock<ITransactionCommandFactory> _mockCommandFactory;
        private Mock<IOptionSelector> _mockOptionSelector;
        private ICommand _mortgageDepositCommand;

        private MortgageOptionCommand _command;

        [SetUp]
        public void SetUp()
        {
            _player = Fixture.Freeze<IPlayer>();
            _mockProperty = Fixture.Mock<IProperty>();
            _mockProperty.SetupProperty(p => p.Owner);
            _mockCommandFactory = Fixture.Mock<ITransactionCommandFactory>();
            _mockOptionSelector = Fixture.Mock<IOptionSelector>();
            _mortgageDepositCommand = Given_MortgageDepositCommand();

            _command = Fixture.Create<MortgageOptionCommand>();
        }

        [Test]
        public void Execute_GivenPlayerIsNotPropertyOwner_DoesNotOfferOptionToMortgageProperty()
        {
            _command.Execute();

            _mockOptionSelector.Verify(
                os => os.ChooseOption(It.IsAny<MortgageProperty>(), It.IsAny<string>()),
                Times.Never);
        }

        [Test]
        public void Execute_GivenPlayerIsNotPropertyOwner_DoesNotMortgageProperty()
        {
            _command.Execute();

            _mockCommandFactory.Verify(cf => cf.Create(_player, It.IsAny<uint>()), Times.Never);
            _mockProperty.VerifySet(p => p.IsMortgaged = true, Times.Never);
            Assert.That(_command.GetSubsequentCommands(), Is.Empty);
        }

        [Test]
        public void Execute_GivenPlayerOwnsPropertyButPropertyIsAlreadyMortgaged_DoesNotOfferOptionToMortgageProperty()
        {
            Given_PlayerOwnsProperty();
            Given_PropertyIsAlreadyMortgaged();

            _command.Execute();

            _mockOptionSelector.Verify(
                os => os.ChooseOption(It.IsAny<MortgageProperty>(), It.IsAny<string>()),
                Times.Never);
        }

        [Test]
        public void Execute_GivenPlayerOwnsPropertyButPropertyIsAlreadyMortgaged_DoesNotMortgageProperty()
        {
            Given_PlayerOwnsProperty();
            Given_PropertyIsAlreadyMortgaged();

            _command.Execute();

            _mockCommandFactory.Verify(cf => cf.Create(_player, It.IsAny<uint>()), Times.Never);
            _mockProperty.VerifySet(p => p.IsMortgaged = true, Times.Never);
            Assert.That(_command.GetSubsequentCommands(), Is.Empty);
        }

        [Test]
        public void Execute_GivenPlayerOwnsPropertyAndPropertyIsUnmortgaged_OffersOptionToMortgageProperty()
        {
            Given_PlayerOwnsProperty();
            Given_PropertyIsUnmortgaged();

            _command.Execute();

            _mockOptionSelector.Verify(os => os.ChooseOption(No, MortgageOptionCommand.Message));
        }

        [Test]
        public void Execute_GivenPlayerChoosesNotToMortgageProperty_DoesNotMortgageProperty()
        {
            Given_PlayerChoosesNotToMortageProperty();

            _command.Execute();

            _mockCommandFactory.Verify(cf => cf.Create(_player, It.IsAny<uint>()), Times.Never);
            _mockProperty.VerifySet(p => p.IsMortgaged = true, Times.Never);
            Assert.That(_command.GetSubsequentCommands(), Is.Empty);
        }

        [Test]
        public void Execute_GivenPlayerChoosesToMortgageProperty_MortgagesProperty()
        {
            Given_PlayerChoosesToMortageProperty();

            _command.Execute();

            _mockProperty.VerifySet(p => p.IsMortgaged = true);
            Assert.That(_command.GetSubsequentCommands(), Contains.Item(_mortgageDepositCommand));
        }

        private void Given_PlayerChoosesToMortageProperty()
        {
            Given_PlayerOwnsProperty();
            Given_PropertyIsUnmortgaged();
            Given_OptionSelectorReturns(Yes);
        }

        private void Given_PlayerChoosesNotToMortageProperty()
        {
            Given_PlayerOwnsProperty();
            Given_PropertyIsUnmortgaged();
            Given_OptionSelectorReturns(No);
        }

        private void Given_OptionSelectorReturns(MortgageProperty optionSelected)
        {
            _mockOptionSelector.Setup(os => os.ChooseOption(It.IsAny<MortgageProperty>(), It.IsAny<string>()))
                .Returns(optionSelected);
        }

        private void Given_PropertyIsUnmortgaged()
        {
            _mockProperty.SetupProperty(p => p.IsMortgaged, false);
        }

        private void Given_PropertyIsAlreadyMortgaged()
        {
            _mockProperty.SetupProperty(p => p.IsMortgaged, true);
        }

        private void Given_PlayerOwnsProperty()
        {
            _mockProperty.Object.Owner = _player;
        }

        private ICommand Given_MortgageDepositCommand()
        {
            var mortgageValue = Given_MortgageValueOfProperty();

            var mortgageDepositCommand = Fixture.Create<ICommand>();
            _mockCommandFactory.Setup(cf => cf.Create(_player, mortgageValue))
                .Returns(mortgageDepositCommand);
            return mortgageDepositCommand;
        }

        private uint Given_MortgageValueOfProperty()
        {
            var purchasePrice = Fixture.Create<uint>();
            _mockProperty.Setup(p => p.PurchasePrice).Returns(purchasePrice);
            return purchasePrice * MortgageOptionCommand.MortgageValuePercentage / 100;
        }
    }
}
