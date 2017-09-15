using System;
using System.Collections.Generic;
using System.Linq;

using Autofac;
using Autofac.Core;

using BoardGame;
using BoardGame.Boards;
using BoardGame.Commands;
using BoardGame.Commands.Decorators;
using BoardGame.Commands.Factories;
using BoardGame.Construction;
using BoardGame.Dice;
using BoardGame.Locations;
using BoardGame.Money;
using BoardGame.Play;
using BoardGame.RealEstate;
using BoardGame.RealEstate.Rent;
using Monopoly.Commands.Factories;
using Monopoly.Construction;
using Monopoly.RealEstate;
using Monopoly.RealEstate.Factories;
using Shuffler;
using UserInterface;
using UserInterface.Choices;
using UserInterface.IO;

namespace Monopoly
{
    public class MonopolyModule : Module
    {
        public const int StartingSpaceIndex = 0;
        private const uint MinimumPlayerCount = 2;
        private const uint MaximumPlayerCount = 8;
        private const int TotalRoundsInAGame = 20;
        private const int StandardInitialBalance = 200;
        private const int RewardForPassingGo = 200;
        private const string PrimaryTurnActionKey = nameof(PrimaryTurnActionKey);
        private const string PrimaryTurnActionCommandQueueKey = nameof(PrimaryTurnActionCommandQueueKey);
        private const string MortgageOptionKey = nameof(MortgageOptionKey);
        private const string MortgageOptionCommandQueueKey = nameof(MortgageOptionCommandQueueKey);

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule<DefaultUiModule>();

            LoadBoard(builder);
            LoadCommands(builder);
            LoadConstructionServices(builder);
            LoadDice(builder);
            LoadLocationServices(builder);
            LoadAccountServices(builder);
            LoadPlayServices(builder);
            LoadProperties(builder);
            LoadSpaces(builder);

            builder.RegisterType<Runner>().AsSelf();
        }

        private static void LoadBoard(ContainerBuilder builder)
        {
            builder.RegisterType<DirectedCycleBoard>().As<IBoardWithEnd>().InstancePerLifetimeScope();
            builder.Register(context => context.Resolve<IBoardWithEnd>()).As<IBoard>();
        }

        private static void LoadCommands(ContainerBuilder builder)
        {
            LoadMonopolySpecificCommands(builder);
            LoadGeneralCommands(builder);
            LoadCommandQueues(builder);
        }

        private static void LoadMonopolySpecificCommands(ContainerBuilder builder)
        {
            LoadSpaceStrategies(builder);
            LoadPrimaryTurnActionCommandFactory(builder);
            LoadMortgageOptionCommandFactory(builder);
        }

        private static void LoadSpaceStrategies(ContainerBuilder builder)
        {
            builder.RegisterType<IncomeTaxCommandFactory>().AsSelf();
            var goToJailParameter = new ResolvedParameter(
                (parameters, context) => parameters.Name == "space",
                (parameters, context) => context.Resolve<IEnumerable<ISpace>>()
                    .Skip(GoToJailCommandFactory.JustVisitingSpaceIndex).First());
            builder.RegisterType<GoToJailCommandFactory>().AsSelf()
                .WithParameter(goToJailParameter);
            builder.RegisterType<LuxuryTaxCommandFactory>().AsSelf();
            builder.RegisterType<LandOnPropertyCommandFactory>();
        }

        private static void LoadPrimaryTurnActionCommandFactory(ContainerBuilder builder)
        {
            var rollAndMoveParameter = new ResolvedParameter(
                (parameters, context) => parameters.Name == "decoratedCommandFactory",
                (parameters, context) => context.Resolve<MonadicCommandFactory<RollAndMoveCommand>>());

            var rewardValueParameter = new NamedParameter("balanceModificationValue", RewardForPassingGo);
            var rewardParameter = new ResolvedParameter(
                (parameters, context) => parameters.ParameterType == typeof(IBalanceModification),
                (parameters, context) => context.Resolve<FixedBalanceModification>(rewardValueParameter));
            var rewardCommandFactoryParameter = new ResolvedParameter(
                (parameters, context) => parameters.Name == "lapRewardCommandFactory",
                (parameters, context) => context.Resolve<DyadicCommandFactory<IBalanceModification, UpdatePlayerBalanceCommand>>(rewardParameter));

            var completedLapsRewardingCommandFactoryParameter = new ResolvedParameter(
                (parameters, context) => parameters.ParameterType == typeof(ICommandFactory),
                (parameters, context) => context.Resolve<CompletedLapsRewardingCommandFactoryDecorator>(
                    rollAndMoveParameter, rewardCommandFactoryParameter));
            var textWriterParameter = new ResolvedParameter(
                (parameters, context) => parameters.ParameterType == typeof(ITextWriter),
                (parameters, context) => context.Resolve<ITextWriter>());

            builder.RegisterType<MonadicCommandFactoryDecorator<VerboseCommandDecorator>>()
                .As<ICommandFactory>()
                .WithParameter(completedLapsRewardingCommandFactoryParameter)
                .WithParameter(textWriterParameter)
                .Keyed<ICommandFactory>(PrimaryTurnActionKey);
        }

        private static void LoadMortgageOptionCommandFactory(ContainerBuilder builder)
        {
            var offerMortgageOptionCommandFactoryParameter = new ResolvedParameter(
                (parameters, context) => parameters.ParameterType == typeof(ICommandFactory),
                (parameters, context) => context.Resolve<MonadicCommandFactory<OfferMortgageOptionCommand>>());
            var textWriterParameter = new ResolvedParameter(
                (parameters, context) => parameters.ParameterType == typeof(ITextWriter),
                (parameters, context) => context.Resolve<ITextWriter>());

            builder.RegisterType<MonadicCommandFactoryDecorator<VerboseCommandDecorator>>()
                .As<ICommandFactory>()
                .WithParameter(offerMortgageOptionCommandFactoryParameter)
                .WithParameter(textWriterParameter)
                .Keyed<ICommandFactory>(MortgageOptionKey);
        }

        private static void LoadGeneralCommands(ContainerBuilder builder)
        {
            builder.RegisterType<CommandLogger>().As<ICommandLogger>();

            builder.RegisterGeneric(typeof(MonadicCommandFactoryDecorator<>)).AsSelf();
            builder.RegisterGeneric(typeof(MonadicCommandFactory<>)).AsSelf();
            builder.RegisterGeneric(typeof(DyadicCommandFactory<,>)).AsSelf();

            builder.RegisterType<CompletedLapsRewardingCommandFactoryDecorator>().AsSelf();
            builder.RegisterType<CompletedLapsRewardingCommandDecorator>();
            builder.RegisterType<VerboseCommandDecorator>();

            builder.RegisterType<UpdatePlayerBalanceCommand>();
            builder.RegisterType<RollAndMoveCommand>();
            builder.RegisterType<MoveDirectlyToSpaceCommand>();

            builder.RegisterType<AssessRentCommand>();
            builder.RegisterType<PurchasePropertyCommand>();

            builder.RegisterType<OfferMortgageOptionCommand>();
            builder.RegisterType<MortgageOptionCommand>();
            builder.RegisterType<UnmortgageOptionCommand>();
        }

        private static void LoadCommandQueues(ContainerBuilder builder)
        {
            builder.RegisterType<SelfExtendingCommandQueue>().As<ICommandQueue>()
                .WithParameter(
                    new ResolvedParameter(
                        (parameters, context) => parameters.ParameterType == typeof(ICommandFactory),
                        (parameters, context) => context.ResolveKeyed<ICommandFactory>(MortgageOptionKey)))
                .Keyed<ICommandQueue>(MortgageOptionCommandQueueKey);

            builder.RegisterType<SelfExtendingCommandQueue>().As<ICommandQueue>()
                .WithParameter(
                    new ResolvedParameter(
                        (parameters, context) => parameters.ParameterType == typeof(ICommandFactory),
                        (parameters, context) => context.ResolveKeyed<ICommandFactory>(PrimaryTurnActionKey)))
                .Keyed<ICommandQueue>(PrimaryTurnActionCommandQueueKey);

            builder.Register(context => new[]
                {
                    context.ResolveKeyed<ICommandQueue>(MortgageOptionCommandQueueKey),
                    context.ResolveKeyed<ICommandQueue>(PrimaryTurnActionCommandQueueKey),
                    context.ResolveKeyed<ICommandQueue>(MortgageOptionCommandQueueKey)
                })
                .As<ICommandQueue[]>();
        }

        private static void LoadConstructionServices(ContainerBuilder builder)
        {
            builder.RegisterType<GameStateConfigurationInitializer>().As<IGameStateConfigurationInitializer>();

            var initialSpaceParameter = new ResolvedParameter(
                (parameters, context) => parameters.ParameterType == typeof(ISpace),
                (parameters, context) => context.Resolve<IEnumerable<ISpace>>().Skip(StartingSpaceIndex).First());
            builder.RegisterType<SingleSpaceInitialPlacementHandler>().As<IInitialPlacementHandler>()
                .WithParameter(initialSpaceParameter);

            builder.RegisterType<PlayerCountConstraint>().As<IPlayerCountConstraint>()
                .WithParameters(PlayerCountConstraintParameters());

            builder.RegisterType<PlayCoordinatorFactory>().As<IPlayCoordinatorFactory>();
            builder.RegisterType<PlayerFactory>().As<IPlayerFactory>();

            builder.RegisterType<MonopolySpaceCommandFactoryBinder>().As<ISpaceCommandFactoryBinder>();
        }

        private static IEnumerable<Parameter> PlayerCountConstraintParameters()
        {
            yield return new NamedParameter("minimumPlayerCount", MinimumPlayerCount);
            yield return new NamedParameter("maximumPlayerCount", MaximumPlayerCount);
        }

        private static void LoadDice(ContainerBuilder builder)
        {
            builder.RegisterType<Random>().AsSelf();
            builder.RegisterType<FisherYatesShuffler>().As<IShuffler>();

            builder.RegisterType<PairOfSixSidedDice>()
                .Named<IDice>(nameof(PairOfSixSidedDice));

            builder.RegisterDecorator<IDice>(
                (context, innerDice) => new DiceWithCacheDecorator(innerDice),
                fromKey: nameof(PairOfSixSidedDice))
                .As<IDiceWithCache>()
                .As<IDice>()
                .InstancePerLifetimeScope();
        }

        private static void LoadLocationServices(ContainerBuilder builder)
        {
            builder.RegisterType<LapCounter>().As<ILapCounter>();
            builder.RegisterType<PlayerLocationMap>().As<IPlayerLocationMap>();
            builder.RegisterType<PlayerMover>().As<IPlayerMover>().InstancePerLifetimeScope();
        }

        private static void LoadAccountServices(ContainerBuilder builder)
        {
            builder.Register<IAccountFactory>(context => new AccountFactory(StandardInitialBalance));
            builder.RegisterType<AccountRegistry>().As<IAccountRegistry>().InstancePerLifetimeScope();
            builder.RegisterType<FixedBalanceModification>().AsSelf();
            builder.RegisterType<WithdrawalCommandFactory>();
            builder.RegisterType<DepositCommandFactory>();
            builder.RegisterType<FixedAmountPaymentFactory>().As<IPaymentFactory>();
            builder.RegisterType<PaymentCommandFactory>().As<IPaymentCommandFactory>().WithParameters(PaymentCommandFactoryParameters);
        }

        public static IEnumerable<Parameter> PaymentCommandFactoryParameters
        {
            get
            {
                yield return new ResolvedParameter(
                    (parameters, context) => parameters.Name == "withdrawalCommandFactory",
                    (parameters, context) => context.Resolve<WithdrawalCommandFactory>());
                yield return new ResolvedParameter(
                    (parameters, context) => parameters.Name == "depositCommandFactory",
                    (parameters, context) => context.Resolve<DepositCommandFactory>());
            }
        }

        private static void LoadPlayServices(ContainerBuilder builder)
        {
            builder.RegisterType<TurnFactory>().As<ITurnFactory>();

            var totalRoundsParameter = new NamedParameter("totalRoundsInAGame", TotalRoundsInAGame);
            builder.RegisterType<RoundBasedEndConditionDetector>().As<IEndConditionDetector>()
                .WithParameter(totalRoundsParameter);
        }

        private static void LoadProperties(ContainerBuilder builder)
        {
            builder.RegisterType<Property>().As<IProperty>();

            LoadPropertyGroupFactories(builder);
            builder.RegisterType<MonopolyProperties>()
                .AsSelf()
                .As<IEnumerable<IPropertyGroup>>()
                .InstancePerLifetimeScope();

            LoadRentServices(builder);
            builder.RegisterType<MortgageOptionCommandFactory>().As<IMortgageOptionCommandFactory>();
            builder.RegisterType<MonopolyPropertyCommandFactories>();
        }

        private static void LoadPropertyGroupFactories(ContainerBuilder builder)
        {
            builder.RegisterType<RailroadGroupFactory>().As<MonopolyPropertyGroupFactory>();
            builder.RegisterType<UtilityGroupFactory>().As<MonopolyPropertyGroupFactory>();
            builder.RegisterType<StreetGroupFactory>();

            foreach (var group in new MonopolyStreetGroups())
                RegisterStreetGroup(builder, group.Value);
        }

        private static void RegisterStreetGroup(ContainerBuilder builder, IEnumerable<Street> group)
        {
            var groupParameter = new TypedParameter(typeof(IEnumerable<Street>), group);
            builder.Register(
                context => context.Resolve<StreetGroupFactory>(groupParameter))
                .As<MonopolyPropertyGroupFactory>();
        }

        private static void LoadRentServices(ContainerBuilder builder)
        {
            LoadRentStrategies(builder);
            builder.RegisterType<RentCalculator>().As<IRentCalculator>();
        }

        private static void LoadRentStrategies(ContainerBuilder builder)
        {
            builder.RegisterType<RailroadRentStrategy>();
            builder.RegisterType<StreetRentStrategy>();
            builder.RegisterType<UtilityRentStrategy>();
        }

        private static void LoadSpaces(ContainerBuilder builder)
        {
            builder.Register(context => new Monopoly.Spaces().ToList())
                .As<IEnumerable<ISpace>>()
                .InstancePerLifetimeScope();
        }
    }
}
