using System;
using SadConsole.Input;
using GoblinStronghold.ECS;

namespace GoblinStronghold.Input.Components
{
    public class KeyboardControllable
    {
        public bool IsCurrentlyControlled;
        private readonly IKeyboardControlHandler _handler;

        public KeyboardControllable(IKeyboardControlHandler handler)
        {
            _handler = handler;
            IsCurrentlyControlled = false;
        }

        public void Handle(Entity entity, Keyboard keyboard)
        {
            if (IsCurrentlyControlled)
            {
                _handler.Handle(entity, keyboard);
            }
        }
    }
}
