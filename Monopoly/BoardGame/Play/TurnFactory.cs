using BoardGame.Commands;

namespace BoardGame.Play
{
    public class TurnFactory : ITurnFactory
    {
        private readonly ICommandQueue _commandQueue;

        public TurnFactory(ICommandQueue commandQueue)
        {
            _commandQueue = commandQueue;
        }

        public Turn CreateFor(IPlayer player)
        {
            return new Turn(player, _commandQueue);
        }
    }
}
