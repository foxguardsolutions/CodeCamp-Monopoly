using NUnit.Framework;
using Ploeh.AutoFixture;

using Tests.Support;

namespace BoardGame.Tests
{
    public class PlayerTests : BaseTest
    {
        private string _name;
        private Player _player;

        [SetUp]
        public void SetUp()
        {
            _name = Fixture.Freeze<string>();
            _player = Fixture.Create<Player>();
        }

        [Test]
        public void PlayerNameProperty_EqualsNamePassedToConstructor()
        {
            Assert.That(_player, Has.Property(nameof(Player.Name)).EqualTo(_name));
        }
    }
}
