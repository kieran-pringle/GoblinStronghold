using System;
using SadConsole.Input;
using GoblinStronghold.ECS;
using GoblinStronghold.Input.Components;
using GoblinStronghold.Physics.Components;
using GoblinStronghold.Physics.Messages;

namespace GoblinStronghold.Input.Systems
{
    public class KeyboardControlSystem : System<Keyboard>
    {
        // TODO: stack instead of iteration for when we have submenus and things
        public override void Handle(Keyboard message)
        {
            if (message.HasKeysPressed)
            {
                foreach (var keyControl in Context().AllComponents<KeyboardControllable>())
                {
                    keyControl.Content.Handle(keyControl.Owner, message);
                }
            }
        }
    }
}
