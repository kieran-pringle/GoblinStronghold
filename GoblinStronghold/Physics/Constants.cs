using GoblinStronghold.ECS;
using GoblinStronghold.Physics.Systems;

/**
 *  Physics module. Movement, collisions and simulating fluids if we ever do
 *  anything like that.
 */
namespace GoblinStronghold.Physics
{
    public static class Constants
    {
        public static void Init(IContext context)
        {
            context.Register(new MoveToSystem());
        }
    }
}

