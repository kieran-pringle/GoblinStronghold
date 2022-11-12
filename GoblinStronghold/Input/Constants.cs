using System;
using GoblinStronghold.ECS;
using GoblinStronghold.Input.Systems;

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
