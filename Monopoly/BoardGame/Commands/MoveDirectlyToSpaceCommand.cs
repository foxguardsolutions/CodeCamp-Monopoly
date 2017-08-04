using BoardGame.Locations;

namespace BoardGame.Commands
{
    public class MoveDirectlyToSpaceCommand : MoveCommand
    {
        private readonly ISpace _destination;

        public MoveDirectlyToSpaceCommand(IPlayer player, IPlayerMover playerMover, ISpace destination)
            : base(player, playerMover)
        {
            _destination = destination;
        }

        public override void Execute()
        {
            PlayerMover.Place(Player, _destination);
            AddCommandFrom(_destination);

            Summary = $"\t{Player.Name} moves directly to {_destination.Name}.";
        }
    }
}
