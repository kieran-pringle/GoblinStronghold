using GoblinStronghold.Input;
using SadConsole.Input;
using System;
using System.Collections.Generic;

namespace GoblinStronghold.Messaging.Messages
{
    public class KeyEventsMessage
    {
        public ISet<Keys> KeysPressed;

        // flush buffer and return it as a KeyEventsMessage
        public static KeyEventsMessage From(KeyboardInputBuffer buffer)
        {
            return new KeyEventsMessage(buffer.Flush());
        }

        private KeyEventsMessage(ISet<Keys> keysPressed)
        {
            KeysPressed = keysPressed;
        }

        public bool IsKeyPressed(Keys key)
        {
            return KeysPressed.Contains(key);
        }
    }
}

