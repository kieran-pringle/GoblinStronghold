using System;
using GoblinStronghold.ECS;
using GoblinStronghold.Graphics.Systems;
using GoblinStronghold.Time.Systems;

namespace GoblinStronghold.Graphics
{
    public static class Constants
    {
        public static int ANIMATION_FPS = 2;

        public static void Init(IContext context, RootScreen screen)
        {
            context.Register(new CameraSystem(screen.MapConsole()));
            context.Register(new FrameRateNotifier(Graphics.Constants.ANIMATION_FPS));
            context.Register(new AnimationSystem());
        }
    }
}

