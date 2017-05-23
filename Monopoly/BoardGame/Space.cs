using BoardGame.Commands.Factories;

namespace BoardGame
{
    public class Space : ISpace
    {
        public ICommandFactory CommandFactory { get; set; }
        public string Name { get; }

        public Space(string name)
        {
            Name = name;
        }
    }
}
