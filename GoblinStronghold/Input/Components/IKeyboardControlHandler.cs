using System;
using System.Collections.Generic;
using SadConsole.Input;
using GoblinStronghold.ECS;

namespace GoblinStronghold.Input.Components
{
    public interface IKeyboardControlHandler
    {
        /**
         * return a list of things to do on the context
         */
        public void Handle(Entity entity, Keyboard keyboard);
    }
}

