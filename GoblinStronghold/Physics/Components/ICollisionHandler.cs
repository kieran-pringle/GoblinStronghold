using System;
using GoblinStronghold.ECS;

namespace GoblinStronghold.Physics.Components
{
    public interface ICollisionHandler
    {
        /**
         *  return true if the entity can move to this position
         *  mutate components on entity as required as result of collision
         */
        public bool Handle(Entity e);
    }
}

