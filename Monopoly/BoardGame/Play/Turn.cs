using BoardGame.Commands;

namespace BoardGame.Play
{
    public class Turn
    {
        private readonly IPlayer _player;
        private readonly ICommandQueue[] _turnPhases;

        public Turn(IPlayer player, params ICommandQueue[] turnPhaseCommandQueues)
        {
            _player = player;
            _turnPhases = turnPhaseCommandQueues;
        }

        public void Complete()
        {
            foreach (var phase in _turnPhases)
                Complete(phase);
        }

        private void Complete(ICommandQueue phase)
        {
            phase.InitializeFor(_player);
            phase.ExecuteCommands();
        }
    }
}
