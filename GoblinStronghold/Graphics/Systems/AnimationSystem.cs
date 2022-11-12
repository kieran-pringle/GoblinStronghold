using System;
using System.Linq;
using GoblinStronghold.ECS;
using GoblinStronghold.Graphics.Components;
using GoblinStronghold.Graphics.Messages;

namespace GoblinStronghold.Graphics.Systems
{
    public class AnimationSystem : System<RenderFrame>
    {
        public AnimationSystem()
        {
        }

        public override void Handle(RenderFrame message)
        {
            if (message.FPS == Graphics.Constants.ANIMATION_FPS)
            {
                foreach (var animation in Context().AllComponents<AnimatedGlyph>())
                {
                    animation.Content.IncrementFrame();
                }
            }
        }
    }
}

