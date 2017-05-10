using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using Autofac.Core;

using BoardGame;
using BoardGame.Boards;
using BoardGame.Construction;
using BoardGame.Dice;
using BoardGame.Locations;
using BoardGame.Play;
using Shuffler;

namespace Monopoly
{
    public class MonopolyModule : Module
    {
        public const int NumberOfSpaces = 40;
        public const int StartingSpaceIndex = 0;
        private const uint MinimumPlayerCount = 2;
        private const uint MaximumPlayerCount = 2;
        private const int TotalRoundsInAGame = 20;

        protected override void Load(ContainerBuilder builder)
        {
            LoadGameComponents(builder);
            LoadGameConstructionUtilities(builder);
            builder.RegisterType<Runner>().AsSelf();
        }

        private static void LoadGameComponents(ContainerBuilder builder)
        {
            LoadGameInitializationComponents(builder);
            LoadPlayerMovementAndLocationComponents(builder);
            LoadEndConditionDetector(builder);
            LoadTurnTakingComponents(builder);
        }

        private static void LoadGameInitializationComponents(ContainerBuilder builder)
        {
            builder.RegisterType<PlayerCountConstraint>().As<IPlayerCountConstraint>()
                .WithParameters(PlayerCountConstraintParameters());

            var gameSpaces = Spaces(NumberOfSpaces).ToList();
            builder.Register(context => gameSpaces[StartingSpaceIndex]).As<Space>();
            builder.RegisterType<SingleSpaceInitialPlacementHandler>().As<IInitialPlacementHandler>();

            builder.Register(context => gameSpaces).As<IEnumerable<Space>>();
            builder.RegisterType<DirectedCycleBoard>().As<IBoard>();
        }

        private static IEnumerable<Parameter> PlayerCountConstraintParameters()
        {
            yield return new NamedParameter("minimumPlayerCount", MinimumPlayerCount);
            yield return new NamedParameter("maximumPlayerCount", MaximumPlayerCount);
        }

        private static IEnumerable<Space> Spaces(int count)
        {
            for (uint spaceNumber = 0; spaceNumber < count; spaceNumber++)
                yield return new Space(spaceNumber);
        }

        private static void LoadPlayerMovementAndLocationComponents(ContainerBuilder builder)
        {
            builder.RegisterType<PlayerLocationMap>().As<IPlayerLocationMap>();
            builder.RegisterType<PlayerMover>().As<IPlayerMover>();
        }

        private static void LoadEndConditionDetector(ContainerBuilder builder)
        {
            var totalRoundsParameter = new NamedParameter("totalRoundsInAGame", TotalRoundsInAGame);
            builder.RegisterType<RoundBasedEndConditionDetector>().As<IEndConditionDetector>()
                .WithParameter(totalRoundsParameter);
        }

        private static void LoadTurnTakingComponents(ContainerBuilder builder)
        {
            LoadDice(builder);
            builder.RegisterType<TurnFactory>().As<ITurnFactory>();
        }

        private static void LoadDice(ContainerBuilder builder)
        {
            builder.RegisterType<Random>().AsSelf();
            builder.RegisterType<FisherYatesShuffler>().As<IShuffler>();
            builder.RegisterType<PairOfSixSidedDice>().As<IDice>();
        }

        private static void LoadGameConstructionUtilities(ContainerBuilder builder)
        {
            builder.RegisterType<PlayCoordinatorFactory>().As<IPlayCoordinatorFactory>();
            builder.RegisterType<PlayerFactory>().As<IPlayerFactory>();
        }
    }
}
