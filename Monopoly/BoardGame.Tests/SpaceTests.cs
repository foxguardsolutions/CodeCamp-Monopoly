using BoardGame.Commands.Factories;

using NUnit.Framework;
using Ploeh.AutoFixture;

using Tests.Support;

namespace BoardGame.Tests
{
    public class SpaceTests : BaseTest
    {
        private string _spaceName;
        private Space _space;
        private ICommandFactory _commandFactory;

        [SetUp]
        public void SetUp()
        {
            _spaceName = Fixture.Freeze<string>();
            _space = Fixture.Create<Space>();

            _commandFactory = Fixture.Create<ICommandFactory>();
        }

        [Test]
        public void Name_ReturnsSpaceName()
        {
            Assert.That(_space, Has.Property(nameof(_space.Name)).EqualTo(_spaceName));
        }

        [Test]
        public void GetCommandFactory_GivenCommandFactorySet_ReturnsCommandFactory()
        {
            _space.CommandFactory = _commandFactory;

            Assert.That(_space, Has.Property(nameof(_space.CommandFactory)).EqualTo(_commandFactory));
        }
    }
}
