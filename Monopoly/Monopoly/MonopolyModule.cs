using System;
using System.Collections.Generic;
using System.Linq;

using Autofac;
using Autofac.Core;

using BoardGame;
using BoardGame.Boards;
using BoardGame.Commands;
using BoardGame.Commands.Factories;
using BoardGame.Construction;
using BoardGame.Dice;
using BoardGame.Locations;
using BoardGame.Money;
using BoardGame.Play;
using Monopoly.Commands.Factories;
using Monopoly.Construction;
using Shuffler;

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
        private const string TurnInitializationCommandFactoryKey = "turn initialization command factory";

        protected override void Load(ContainerBuilder builder)
        {
            LoadBoard(builder);
            LoadCommands(builder);
            LoadConstructionServices(builder);
            LoadDice(builder);
            LoadLocationServices(builder);
            LoadAccountServices(builder);
            LoadPlayServices(builder);
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
            builder.RegisterType<SelfExtendingCommandQueue>().As<ICommandQueue>().WithParameter(
                new ResolvedParameter(
                    (parameters, context) => parameters.ParameterType == typeof(ICommandFactory),
                    (parameters, context) => context.ResolveKeyed<ICommandFactory>(TurnInitializationCommandFactoryKey)));
        }

        private static void LoadMonopolySpecificCommands(ContainerBuilder builder)
        {
            LoadSpaceStrategies(builder);
            LoadTurnInitializationCommandFactory(builder);
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
        }

        private static void LoadTurnInitializationCommandFactory(ContainerBuilder builder)
        {
            var rollAndMoveParameter = new ResolvedParameter(
                (parameters, context) => parameters.Name == "decoratedCommandFactory",
                (parameters, context) => context.Resolve<RollAndMoveCommandFactory>());

            var rewardValueParameter = new NamedParameter("balanceModificationValue", RewardForPassingGo);
            var rewardParameter = new ResolvedParameter(
                (parameters, context) => parameters.Name == "balanceModification",
                (parameters, context) => context.Resolve<FixedBalanceModification>(rewardValueParameter));
            var rewardCommandFactoryParameter = new ResolvedParameter(
                (parameters, context) => parameters.Name == "lapRewardCommandFactory",
                (parameters, context) => context.Resolve<BalanceModificationCommandFactory>(rewardParameter));

            builder.RegisterType<CompletedLapsRewardingCommandFactoryDecorator>()
                .As<ICommandFactory>()
                .WithParameter(rollAndMoveParameter)
                .WithParameter(rewardCommandFactoryParameter)
                .Keyed<ICommandFactory>(TurnInitializationCommandFactoryKey);
        }

        private static void LoadGeneralCommands(ContainerBuilder builder)
        {
            builder.RegisterType<BalanceModificationCommandFactory>().AsSelf();
            builder.RegisterType<CompletedLapsRewardingCommandFactoryDecorator>().AsSelf();
            builder.RegisterType<RollAndMoveCommandFactory>().AsSelf();
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

            builder.RegisterType<PairOfSixSidedDice>().As<IDice>();
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
        }

        private static void LoadPlayServices(ContainerBuilder builder)
        {
            builder.RegisterType<TurnFactory>().As<ITurnFactory>();

            var totalRoundsParameter = new NamedParameter("totalRoundsInAGame", TotalRoundsInAGame);
            builder.RegisterType<RoundBasedEndConditionDetector>().As<IEndConditionDetector>()
                .WithParameter(totalRoundsParameter);
        }

        private static void LoadSpaces(ContainerBuilder builder)
        {
            builder.Register(context => new Monopoly.Spaces().ToList())
                .As<IEnumerable<ISpace>>()
                .InstancePerLifetimeScope();
        }
    }
}
