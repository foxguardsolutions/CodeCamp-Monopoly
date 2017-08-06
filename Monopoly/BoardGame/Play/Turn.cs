using BoardGame.Commands;

namespace BoardGame.Play
{
    public class Turn
    {
        private readonly IPlayer _player;
        private readonly ICommandQueue[] _turnStages;

        public Turn(IPlayer player, params ICommandQueue[] turnStageCommandQueues)
        {
            _player = player;
            _turnStages = turnStageCommandQueues;
        }

        public void Complete()
        {
            foreach (var stage in _turnStages)
                Complete(stage);
        }

        private void Complete(ICommandQueue stage)
        {
            stage.InitializeFor(_player);
            stage.ExecuteCommands();
        }
    }
}
