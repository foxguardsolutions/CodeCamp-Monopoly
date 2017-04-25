using Autofac;

namespace Monopoly
{
    public class ContainerFactory
    {
        public static IContainer Create()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<MonopolyModule>();
            return builder.Build();
        }
    }
}
