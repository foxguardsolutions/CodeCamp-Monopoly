using BoardGame.Commands;

namespace BoardGame.Play
{
    public class TurnFactory : ITurnFactory
    {
        private readonly ICommandQueue[] _commandQueues;

        public TurnFactory(params ICommandQueue[] commandQueues)
        {
            _commandQueues = commandQueues;
        }

        public Turn CreateFor(IPlayer player)
        {
            return new Turn(player, _commandQueues);
        }
    }
}
