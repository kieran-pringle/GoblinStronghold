using GoblinStronghold.ECS;
using GoblinStronghold.Graphics.Systems;

/**
 *  This module handles everything to do with actually putting the map and 
 *  entites onto the screen
 */
namespace GoblinStronghold.Graphics
{
    public static class Constants
    {

        public static void Init(IContext context, RootScreen screen)
        {
            context.Register(new CameraSystem(screen.MapConsole()));
            context.Register(new AnimationSystem());
        }
    }
}

