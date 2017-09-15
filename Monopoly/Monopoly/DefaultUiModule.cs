using Autofac;

using UserInterface;
using UserInterface.Choices;
using UserInterface.IO;

namespace Monopoly
{
    public class DefaultUiModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ConsoleReaderWriter>().As<ITextReaderWriter>().As<ITextWriter>();
            builder.RegisterType<DefaultSelector>().As<IOptionSelector>();
        }
    }
}
