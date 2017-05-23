using BoardGame.Commands.Factories;

namespace BoardGame
{
    public interface ISpace
    {
        ICommandFactory CommandFactory { get; set; }
        string Name { get; }
    }
}
