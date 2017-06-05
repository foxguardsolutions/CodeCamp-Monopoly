using BoardGame.Commands;
using BoardGame.Play;

using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;

using Tests.Support;
using Tests.Support.Extensions;

namespace BoardGame.Tests.PlayTests
{
    public class TurnTests : BaseTest
    {
        private IPlayer _player;
        private Mock<ICommandQueue> _mockCommandQueue;

        private Turn _turn;

        [SetUp]
        public void SetUp()
        {
            _player = Fixture.Freeze<IPlayer>();
            _mockCommandQueue = Fixture.Mock<ICommandQueue>();

            _turn = Fixture.Create<Turn>();
        }

        [Test]
        public void Complete_InitializesCommandQueueForPlayerAndExecutesCommands()
        {
            _turn.Complete();

            _mockCommandQueue.Verify(q => q.InitializeFor(_player));
            _mockCommandQueue.Verify(q => q.ExecuteCommands());
        }
    }
}
