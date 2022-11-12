using System;
using GoblinStronghold.ECS;

namespace GoblinStronghold.Physics.Components
{
    public class Collision
    {
        private readonly ICollisionHandler Handler;

        public Collision(ICollisionHandler handler)
        {
            Handler = handler;
        }

        public bool Collide(Entity e)
        {
            return Handler.Handle(e);
        }
    }
}

