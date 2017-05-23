using BoardGame.Commands;

using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;

using Tests.Support.Extensions;

namespace BoardGame.Tests.CommandsTests
{
    public class MoveDirectlyToSpaceCommandTests : MoveCommandTests
    {
        private Mock<ISpace> _mockSpace;

        [SetUp]
        public void SetUp()
        {
            _mockSpace = Fixture.Mock<ISpace>();

            Command = Fixture.Create<MoveDirectlyToSpaceCommand>();
        }

        [Test]
        public void Execute_MovesPlayerToSpecifiedSpace()
        {
            Command.Execute();

            MockPlayerMover.Verify(p => p.Place(Player, _mockSpace.Object));
        }

        protected override void GivenDestinationSpaceWithoutCommandFactory()
        {
            _mockSpace.SetupProperty(s => s.CommandFactory, null);
        }

        protected override void GivenDestinationSpaceWithCommandFactory()
        {
        }
    }
}
