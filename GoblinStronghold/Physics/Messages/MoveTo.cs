using System;

using GoblinStronghold.ECS;
using GoblinStronghold.Physics.Components;

namespace GoblinStronghold.Physics.Messages
{
    public struct MoveTo
    {
        public readonly Entity Entity;
        public readonly Position NewPosition;

        public MoveTo(Entity entity, Position newPosition)
        {
            Entity = entity;
            NewPosition = newPosition;
        }
    }
}

