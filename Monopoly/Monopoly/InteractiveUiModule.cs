using Autofac;

using UserInterface;
using UserInterface.Choices;
using UserInterface.IO;

namespace Monopoly
{
    public class InteractiveUiModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ConsoleReaderWriter>().As<ITextReaderWriter>().As<ITextWriter>();
            builder.RegisterType<OptionSelector>().As<IOptionSelector>();
            builder.RegisterType<EnumWrapper>().As<IEnum>();
            builder.RegisterType<OptionParser>().As<IOptionParser>();
            builder.RegisterType<UserChoicePrompt>().As<IPrompt>();
        }
    }
}
