﻿using System;
using System.Collections.Generic;
using GoblinStronghold.ECS;
using GoblinStronghold.Input.Messages;
using GoblinStronghold.Physics.Components;
using GoblinStronghold.Physics.Messages;
using GoblinStronghold.Time.Components;
using GoblinStronghold.Time.Messages.Turns;
using SadConsole.Input;

namespace GoblinStronghold.Input.Components
{
    /**
     *  Represents keyboard control of an actual creature (e.g. the player)
     */
    public class PlayerControlledCreature : IOnComponentRegister
    {
        private CanTakeTurn _canTakeTurn;
        private KeyboardControllable _keyboardControllable;

        public PlayerControlledCreature()
        {
            _canTakeTurn = new CanTakeTurn(
                new KeyboardControlOnTurn(this));
            _keyboardControllable = new KeyboardControllable(
                new MoveEntityOnKeyboardEvent(this));
        }

        public void OnRegisterTo(Entity entity)
        {
            entity
                .With(_canTakeTurn)
                .With(_keyboardControllable);
        }

        private class KeyboardControlOnTurn : ITurnHandler
        {
            private PlayerControlledCreature _parent;

            public KeyboardControlOnTurn(PlayerControlledCreature parent)
            {
                _parent = parent;
            }

            public void TakeTurn()
            {
                _parent._keyboardControllable.IsCurrentlyControlled = true;
            }
        }

        private class MoveEntityOnKeyboardEvent : IKeyboardControlHandler
        {
            private PlayerControlledCreature _parent;

            public MoveEntityOnKeyboardEvent(PlayerControlledCreature parent)
            {
                _parent = parent;
            }

            public void Handle(Entity entity, KeysPressed keyboard)
            {
                entity.Component<Position>().ForEach(c =>
                {
                    var pos = c.Content;
                    foreach (var key in keyboard.Pressed)
                    {
                        // better to switch statment over this probably
                        // or some way to move once diagonally
                        if (key.Key == Keys.Up)
                        {
                            pos = pos.North();
                        }
                        if (key.Key == Keys.Down)
                        {
                            pos = pos.South();
                        }
                        if (key.Key == Keys.Right)
                        {
                            pos = pos.East();
                        }
                        if (key.Key == Keys.Left)
                        {
                            pos = pos.West();
                        }
                        entity.Context().Send(
                            new MoveTo(entity, pos)
                        );
                    }
                });

                _parent._keyboardControllable.IsCurrentlyControlled = false;
                entity.Context().Send(new TurnTaken(3));
            }
        }
    }
}
