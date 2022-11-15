using GoblinStronghold.ECS;
using GoblinStronghold.Input.Systems;

/**
 *  This module handles passing input along to anything that can accept it and
 *  making sure that only the thing that is supposed to receive the input does
 */
namespace GoblinStronghold.Input
{
    public static class Constants
    {
        public static void Init(IContext context)
        {
            context.Register(new KeyboardControlSystem());
        }
    }
}
