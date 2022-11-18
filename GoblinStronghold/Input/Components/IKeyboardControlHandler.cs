using System;
using SadConsole.Input;
using GoblinStronghold.ECS;
using GoblinStronghold.Input.Messages;

namespace GoblinStronghold.Input.Components
{
    public interface IKeyboardControlHandler
    {
        /**
         * return a list of things to do on the context
         */
        public void Handle(Entity entity, KeysPressed keyboard);
    }
}

