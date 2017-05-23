using BoardGame.Money;

using NUnit.Framework;
using Ploeh.AutoFixture;

using Tests.Support;

namespace BoardGame.Tests.MoneyTests
{
    public class AccountFactoryTests : BaseTest
    {
        [Test]
        public void Create_ReturnsNewAccountWithInitialBalance()
        {
            var factory = Fixture.Create<AccountFactory>();

            var account = factory.Create();

            Assert.That(account, Is.TypeOf<Account>());
        }
    }
}
