using SadConsole.Input;
using GoblinStronghold.ECS;
using GoblinStronghold.Input.Components;
using GoblinStronghold.Input.Messages;

namespace GoblinStronghold.Input.Systems
{
    public class KeyboardControlSystem : ISystem<KeysPressed>
    {
        private IContext _context;
        IContext ISystem<KeysPressed>.Context
        {
            get => _context;
            set => _context = value;
        }

        // TODO: stack instead of iteration for when we have submenus and things
        public void Handle(KeysPressed message)
        {
            foreach (var keyControl in _context.AllComponents<KeyboardControllable>())
            {
                keyControl.Content.Handle(keyControl.Owner, message);
            }
        }
    }
}
