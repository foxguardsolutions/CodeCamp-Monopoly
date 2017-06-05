using BoardGame.Money;

using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;

using Tests.Support;
using Tests.Support.Extensions;

namespace BoardGame.Tests.MoneyTests
{
    public class AccountRegistryTests : BaseTest
    {
        private IPlayer _player;
        private IAccount _expectedAccount;
        private Mock<IAccountFactory> _mockAccountFactory;
        private AccountRegistry _accountRegistry;

        [SetUp]
        public void SetUp()
        {
            _player = Fixture.Create<IPlayer>();
            _expectedAccount = Fixture.Create<IAccount>();
            _mockAccountFactory = GivenMockAccountFactoryThatCreates(_expectedAccount);
            _accountRegistry = Fixture.Create<AccountRegistry>();
        }

        private Mock<IAccountFactory> GivenMockAccountFactoryThatCreates(IAccount account)
        {
            var mockAccountFactory = Fixture.Mock<IAccountFactory>();
            mockAccountFactory.Setup(a => a.Create()).Returns(account);
            return mockAccountFactory;
        }

        [Test]
        public void RegisterAccount_GivenPlayer_CreatesAccountWithBalanceRegistered()
        {
            _accountRegistry.RegisterAccount(_player);

            _mockAccountFactory.Verify(a => a.Create());
        }

        [Test]
        public void GetAccount_GivenPlayer_ReturnsRegisteredAccount()
        {
            _accountRegistry.RegisterAccount(_player);
            var account = _accountRegistry.GetAccount(_player);

            Assert.That(account, Is.EqualTo(_expectedAccount));
        }
    }
}
