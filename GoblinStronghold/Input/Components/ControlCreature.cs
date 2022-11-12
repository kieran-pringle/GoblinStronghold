using System;
using System.Collections.Generic;
using GoblinStronghold.ECS;
using GoblinStronghold.Physics.Components;
using GoblinStronghold.Physics.Messages;
using SadConsole.Input;

namespace GoblinStronghold.Input.Components
{
    /**
     *  Represents keyboard control of an actual creature (e.g. the player)
     */
    public class ControlCreature : IKeyboardControlHandler
    {

        public void Handle(Entity entity, Keyboard keyboard)
        {
            var posComponent = entity.Component<Position>();
            Queue<Action<IContext>> actions = new Queue<Action<IContext>>();

            if (posComponent.HasValue)
            {
                var pos = posComponent.Value.Content;
                foreach (var key in keyboard.KeysPressed)
                {
                    // better to switch statment over this probably
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
            }
        }
    }
}
