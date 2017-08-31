using BoardGame.Locations;

namespace BoardGame.Commands
{
    public class MoveDirectlyToSpaceCommand : MoveCommand
    {
        private readonly ISpace _destination;

        public MoveDirectlyToSpaceCommand(IPlayer player, IPlayerMover playerMover, ISpace destination, ICommandLogger logger)
            : base(player, playerMover, logger)
        {
            _destination = destination;
        }

        public override void Execute()
        {
            PlayerMover.Place(Player, _destination);
            AddCommandFrom(_destination);

            Logger.Log($"\t{Player.Name} moves directly to {_destination.Name}.");
        }
    }
}
