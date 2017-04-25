using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;

using BoardGame;
using BoardGame.Boards;
using BoardGame.Construction;
using BoardGame.Dice;
using BoardGame.Locations;
using BoardGame.Play;

using NUnit.Framework;

using Shuffler;

namespace Monopoly.Tests
{
    [TestFixture]
    public class DependencyInjectionContainerTests
    {
        [Test]
        public void ResolveSpaces_GivenMonopolyContainer_ReturnsCorrectNumberOfSpacesAndFirstSpace()
        {
            using (var container = ContainerFactory.Create())
            {
                var spaces = container.Resolve<IEnumerable<Space>>();
                var actualSpace = container.Resolve<Space>();

                Assert.That(spaces.Count, Is.EqualTo(MonopolyModule.NumberOfSpaces));
                Assert.That(actualSpace, Is.EqualTo(spaces.First()));
            }
        }

        [TestCaseSource(nameof(ComponentResolutionTestCases))]
        public void Resolve_GivenMonopolyContainer_ReturnsInstanceOfCorrectComponentType(
            Type serviceToResolve, Type expectedComponentType)
        {
            using (var container = ContainerFactory.Create())
            {
                var componentInstance = container.Resolve(serviceToResolve);
                Assert.That(componentInstance, Is.TypeOf(expectedComponentType));
            }
        }

        private static IEnumerable<TestCaseData> ComponentResolutionTestCases()
        {
            yield return new TestCaseData(typeof(Random), typeof(Random));
            yield return new TestCaseData(typeof(IShuffler), typeof(FisherYatesShuffler));
            yield return new TestCaseData(typeof(IDice), typeof(PairOfSixSidedDice));
            yield return new TestCaseData(typeof(IBoard), typeof(DirectedCycleBoard));
            yield return new TestCaseData(typeof(IPlayerLocationMap), typeof(PlayerLocationMap));
            yield return new TestCaseData(typeof(IPlayerMover), typeof(PlayerMover));
            yield return new TestCaseData(typeof(ITurnFactory), typeof(TurnFactory));
            yield return new TestCaseData(typeof(IInitialPlacementHandler), typeof(SingleSpaceInitialPlacementHandler));
            yield return new TestCaseData(typeof(IEndConditionDetector), typeof(RoundBasedEndConditionDetector));
            yield return new TestCaseData(typeof(IPlayerCountConstraint), typeof(PlayerCountConstraint));
            yield return new TestCaseData(typeof(IPlayCoordinatorFactory), typeof(PlayCoordinatorFactory));
            yield return new TestCaseData(typeof(IPlayerFactory), typeof(PlayerFactory));
            yield return new TestCaseData(typeof(Runner), typeof(Runner));
        }
    }
}
