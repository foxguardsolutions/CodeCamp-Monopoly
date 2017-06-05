using BoardGame.Commands;
using BoardGame.Money;

using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;

using Tests.Support;
using Tests.Support.Extensions;

namespace BoardGame.Tests.CommandsTests
{
    public class UpdatePlayerBalanceCommandTests : BaseTest
    {
        private IBalanceModification _balanceModification;
        private Mock<IAccount> _mockAccount;
        private Mock<IAccountRegistry> _mockAccountRegistry;
        private IPlayer _player;

        private UpdatePlayerBalanceCommand _command;

        [SetUp]
        public void SetUp()
        {
            _mockAccount = Fixture.Mock<IAccount>();
            _player = Fixture.Freeze<IPlayer>();
            _mockAccountRegistry = GivenAccountRegisteredToPlayerInMockRegistry(_mockAccount.Object, _player);

            _balanceModification = Fixture.Freeze<IBalanceModification>();

            _command = Fixture.Create<UpdatePlayerBalanceCommand>();
        }

        private Mock<IAccountRegistry> GivenAccountRegisteredToPlayerInMockRegistry(
            IAccount account, IPlayer player)
        {
            var mockAccountRegistry = Fixture.Mock<IAccountRegistry>();
            mockAccountRegistry.Setup(a => a.GetAccount(player))
                .Returns(account);
            return mockAccountRegistry;
        }

        [Test]
        public void Execute_GivenBalanceChangeValue_ChangesPlayersBalance()
        {
            _command.Execute();

            _mockAccountRegistry.Verify(a => a.GetAccount(_player));
            _mockAccount.Verify(a => a.Assess(_balanceModification));
        }
    }
}
