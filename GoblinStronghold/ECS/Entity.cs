using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Functional.Option;
using GoblinStronghold.ECS;

using static GoblinStronghold.ECS.Context;

namespace GoblinStronghold.ECS
{
    // represents an entity in the context system
    // but also an entry point into registering and deregistering components
    public sealed class Entity : IDestroyable
    {
        internal readonly ContextID _id;

        internal Entity(ContextID id)
        {
            _id = id;
        }

        public Entity With<T>(T component)
        {
            ParentContext().AddComponentTo<T>(component, this);
            if (component is IOnComponentRegister)
            {
                ((IOnComponentRegister)component)
                    .OnRegisterTo(this);
            }
            return this;
        }

        // TODO: optional type for this? Might be much nicer way to handle the
        // frequent nullness - can pass a function to do if the value is there
        // would be nicer to not even expose component and then we could maybe
        // restrict access further
        [return: MaybeNull] // if component doesn't exist
        public Option<Component<T>> Component<T>()
        {
            return AllComponents().Get<T>();
        }

        public ComponentStore AllComponents()
        {
            return ParentContext().ComponentStoreFor(this);
        }

        public void Destroy()
        {
            ParentContext().Destroy(this);
        }

        private Context ParentContext()
        {
            return _id._ctx;
        }
    }
}
