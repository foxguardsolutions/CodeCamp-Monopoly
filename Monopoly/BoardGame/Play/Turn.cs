using BoardGame.Commands;

namespace BoardGame.Play
{
    public class Turn
    {
        private readonly IPlayer _player;
        private readonly ICommandQueue _commandQueue;

        public Turn(IPlayer player, ICommandQueue commandQueue)
        {
            _player = player;
            _commandQueue = commandQueue;
        }

        public void Complete()
        {
            _commandQueue.InitializeFor(_player);
            _commandQueue.ExecuteCommands();
        }
    }
}
