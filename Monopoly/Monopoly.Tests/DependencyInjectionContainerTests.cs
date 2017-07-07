using System;
using System.Collections.Generic;

using Autofac;

using BoardGame;
using BoardGame.Boards;
using BoardGame.Commands;
using BoardGame.Commands.Factories;
using BoardGame.Construction;
using BoardGame.Dice;
using BoardGame.Locations;
using BoardGame.Money;
using BoardGame.Play;
using BoardGame.RealEstate;
using Monopoly.Commands.Factories;
using Monopoly.Construction;
using Monopoly.RealEstate;
using NUnit.Framework;

using Shuffler;

namespace Monopoly.Tests
{
    [TestFixture]
    public class DependencyInjectionContainerTests
    {
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

        [Test]
        public void ResolveDiceAndDiceWithCache_ReturnsSameInstance()
        {
            using (var container = ContainerFactory.Create())
            {
                var dice = container.Resolve<IDice>();
                var diceWithCache = container.Resolve<IDiceWithCache>();
                Assert.That(dice, Is.SameAs(diceWithCache).And.TypeOf<DiceWithCacheDecorator>());
            }
        }

        [Test]
        public void ResolveMonopolyPropertiesAndIEnumerableIPropertyGroup_ReturnsSameInstance()
        {
            using (var container = ContainerFactory.Create())
            {
                var monopolyProperties = container.Resolve<MonopolyProperties>();
                var propertyGroups = container.Resolve<IEnumerable<IPropertyGroup>>();
                Assert.That(monopolyProperties, Is.SameAs(propertyGroups));
            }
        }

        private static IEnumerable<TestCaseData> ComponentResolutionTestCases()
        {
            yield return new TestCaseData(typeof(IBoardWithEnd), typeof(DirectedCycleBoard));
            yield return new TestCaseData(typeof(IBoard), typeof(DirectedCycleBoard));

            yield return new TestCaseData(typeof(IncomeTaxCommandFactory), typeof(IncomeTaxCommandFactory));
            yield return new TestCaseData(typeof(GoToJailCommandFactory), typeof(GoToJailCommandFactory));
            yield return new TestCaseData(typeof(LuxuryTaxCommandFactory), typeof(LuxuryTaxCommandFactory));
            yield return new TestCaseData(typeof(RollAndMoveCommandFactory), typeof(RollAndMoveCommandFactory));
            yield return new TestCaseData(typeof(ICommandQueue), typeof(SelfExtendingCommandQueue));

            yield return new TestCaseData(typeof(IGameStateConfigurationInitializer), typeof(GameStateConfigurationInitializer));
            yield return new TestCaseData(typeof(IInitialPlacementHandler), typeof(SingleSpaceInitialPlacementHandler));
            yield return new TestCaseData(typeof(IPlayerCountConstraint), typeof(PlayerCountConstraint));
            yield return new TestCaseData(typeof(IPlayCoordinatorFactory), typeof(PlayCoordinatorFactory));
            yield return new TestCaseData(typeof(IPlayerFactory), typeof(PlayerFactory));
            yield return new TestCaseData(typeof(ISpaceCommandFactoryBinder), typeof(MonopolySpaceCommandFactoryBinder));

            yield return new TestCaseData(typeof(Random), typeof(Random));
            yield return new TestCaseData(typeof(IShuffler), typeof(FisherYatesShuffler));

            yield return new TestCaseData(typeof(ILapCounter), typeof(LapCounter));
            yield return new TestCaseData(typeof(IPlayerLocationMap), typeof(PlayerLocationMap));
            yield return new TestCaseData(typeof(IPlayerMover), typeof(PlayerMover));

            yield return new TestCaseData(typeof(IAccountFactory), typeof(AccountFactory));
            yield return new TestCaseData(typeof(IAccountRegistry), typeof(AccountRegistry));

            yield return new TestCaseData(typeof(ITurnFactory), typeof(TurnFactory));
            yield return new TestCaseData(typeof(IEndConditionDetector), typeof(RoundBasedEndConditionDetector));

            yield return new TestCaseData(typeof(Runner), typeof(Runner));

            yield return new TestCaseData(typeof(IPaymentCommandFactory), typeof(PaymentCommandFactory));
        }
    }
}
