using System;
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
        internal Entity _owner;

        private T _data;

        internal Component(ContextID id, Entity owner, T data)
        {
            _id = id;
            _owner = owner;
            _data = data;
        }

        // as a method so we can query context instead later
        public Entity Owner()
        {
            return _owner;
        }

        public T Data()
        {
            return _data;
        }

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

