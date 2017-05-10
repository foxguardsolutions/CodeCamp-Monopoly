using Autofac;

using BoardGame;

namespace Monopoly
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var playerNames = args;

            using (var container = ContainerFactory.Create())
            {
                var monopolyGameRunner = container.Resolve<Runner>();
                monopolyGameRunner.RunGame(playerNames);
            }
        }
    }
}
