using GoblinStronghold.ECS;
using GoblinStronghold.Graphics.Components;
using GoblinStronghold.Graphics.Messages;

namespace GoblinStronghold.Graphics.Systems
{
    public class AnimationSystem : ISystem<RenderFrame>
    {
        private IContext _context;
        IContext ISystem<RenderFrame>.Context
        {
            get => _context;
            set => _context = value;
        }

        public AnimationSystem()
        {
        }

        public void Handle(RenderFrame message)
        {
            if (message.FPS == Time.Constants.ANIMATION_FPS)
            {
                foreach (var animation in _context.AllComponents<AnimatedGlyph>())
                {
                    animation.Content.IncrementFrame();
                }
            }
        }
    }
}

