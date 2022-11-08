using System;
using Functional.Option;
using GoblinStronghold.ECS;
using static GoblinStronghold.ECS.Context;

namespace GoblinStronghold.ECS
{ 
    public sealed class Component<T> : IDestroyable
    {
        // internally managed ID, if null, this object is not managed by
        // the context
        internal ContextID _id;
        // link back to owning entity
        public Entity Owner { get; internal set; }

        public T Content { get; set; }

        internal Component(ContextID id, Entity owner, T data)
        {
            _id = id;
            Owner = owner;
            Content = data;
        }

        /**
        * <summary>
        *      Dereference this componet from the context and its owner
        * </summary>
        */
        public void Destroy()
        {
            _id._ctx.Destroy(this);
        }

        private Context ParentContext()
        {
            return _id._ctx;
        }
    }
}

