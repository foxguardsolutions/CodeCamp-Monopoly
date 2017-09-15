﻿using System;

using BoardGame.Dice;
using BoardGame.Locations;

namespace BoardGame.Commands
{
    public class RollAndMoveCommand : MoveCommand
    {
        private readonly IDice _dice;

        public RollAndMoveCommand(IPlayer player, IPlayerMover playerMover, IDice dice, ICommandLogger logger)
            : base(player, playerMover, logger)
        {
            _dice = dice;
        }

        public override void Execute()
        {
            var roll = _dice.Roll();
            var destination = PlayerMover.Move(Player, roll.Value);
            AddCommandFrom(destination);

            Logger.Log($"{Environment.NewLine}{Player.Name} rolls {roll.Value} and moves to {destination.Name}.");
        }
    }
}
