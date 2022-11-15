using SadConsole.Input;
using GoblinStronghold.ECS;
using GoblinStronghold.Input.Components;

namespace GoblinStronghold.Input.Systems
{
    public class KeyboardControlSystem : ISystem<Keyboard>
    {
        private IContext _context;
        IContext ISystem<Keyboard>.Context
        {
            get => _context;
            set => _context = value;
        }

        // TODO: stack instead of iteration for when we have submenus and things
        public void Handle(Keyboard message)
        {
            if (message.HasKeysPressed)
            {
                foreach (var keyControl in _context.AllComponents<KeyboardControllable>())
                {
                    keyControl.Content.Handle(keyControl.Owner, message);
                }
            }
        }
    }
}
