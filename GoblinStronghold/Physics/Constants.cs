using System;
using GoblinStronghold.ECS;
using GoblinStronghold.Physics.Systems;

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

