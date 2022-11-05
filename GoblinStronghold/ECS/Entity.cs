using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using GoblinStronghold.ECS;
using static GoblinStronghold.ECS.Context;

namespace GoblinStronghold.ECS
{
    // represents an entity in the context system
    // but also an entry point into registering and deregistering components
    public sealed class Entity
    {
        internal readonly ContextID _id;
        // reference back to the context
        private readonly Context _ctx;

        internal Entity(Context ctx, ContextID id)
        {
            _ctx = ctx;
            _id = id;
        }

        public Entity With<C>(C component) where C : Component
        {
            _ctx.AddComponentTo(component, this);
            return this;
        }

        [return: MaybeNull]
        public C Component<C>() where C : Component
        {
            throw new NotImplementedException("Yet to do");
        }
    }
}

