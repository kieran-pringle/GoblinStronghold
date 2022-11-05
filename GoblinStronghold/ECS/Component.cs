using System;
using GoblinStronghold.ECS;
using static GoblinStronghold.ECS.Context;

namespace GoblinStronghold.ECS
{
    public abstract class Component
    {
        // internally managed ID, if null, this object is not managed by
        // the context
        internal ContextID _id;
        // reference back to the context for queries
        internal Context _ctx;

        // link back to owning entity
        internal Entity _owner;

        // as a method so we can query context instead later
        public Entity Owner()
        {
            return _owner;
        }
    }
}

