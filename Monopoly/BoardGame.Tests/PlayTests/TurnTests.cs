using System.Collections.Generic;

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
        private IEnumerable<Mock<ICommandQueue>> _mockCommandQueues;

        private Turn _turn;

        [SetUp]
        public void SetUp()
        {
            _player = Fixture.Freeze<IPlayer>();
            _mockCommandQueues = Fixture.MockMany<ICommandQueue>();

            _turn = Fixture.Create<Turn>();
        }

        [Test]
        public void Complete_InitializesCommandQueueForPlayerAndExecutesCommands()
        {
            _turn.Complete();

            foreach (var mockCommandQueue in _mockCommandQueues)
            {
                mockCommandQueue.Verify(q => q.InitializeFor(_player));
                mockCommandQueue.Verify(q => q.ExecuteCommands());
            }
        }
    }
}
