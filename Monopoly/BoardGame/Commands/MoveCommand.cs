using BoardGame.Locations;

namespace BoardGame.Commands
{
    public abstract class MoveCommand : Command
    {
        protected IPlayer Player { get; }
        protected IPlayerMover PlayerMover { get; }
        protected MoveCommand(IPlayer player, IPlayerMover playerMover)
        {
            Player = player;
            PlayerMover = playerMover;
        }

        protected void AddCommandFrom(ISpace space)
        {
            if (space.CommandFactory == null) return;

            var nextCommand = space.CommandFactory.CreateFor(Player);
            SubsequentCommands.Add(nextCommand);
        }
    }
}
