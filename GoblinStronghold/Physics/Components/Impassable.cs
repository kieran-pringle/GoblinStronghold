using System;
using GoblinStronghold.ECS;

namespace GoblinStronghold.Physics.Components
{
    public class Impassable : ICollisionHandler
    {
        public static Impassable Instance = new Impassable();

        private Impassable()
        {
        }

        public bool Handle(Entity e)
        {
            return false; // you shall not pass
        }
    }
}

