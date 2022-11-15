using System.Diagnostics.CodeAnalysis;
using Functional.Option;

using static GoblinStronghold.ECS.Context;

namespace GoblinStronghold.ECS
{
    // represents an entity in the context system
    // but also an entry point into registering and deregistering components
    public sealed class Entity : IDestroyable
    {
        private ComponentStore _cache;
        private bool _isCacheValid;

        internal readonly ContextID _id;

        internal Entity(ContextID id)
        {
            _id = id;
        }

        public Entity With<T>(T component)
        {
            _id._ctx.AddComponentTo(component, this);
            // invalidate the cache
            _isCacheValid = false;
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
            if (_cache == null || !_isCacheValid)
            {
                _cache = Context().ComponentStoreFor(this);
            }
            return _cache;
        }

        public void Destroy()
        {
            Context().Destroy(this);
        }

        public IContext Context()
        {
            return _id._ctx;
        }

        public override bool Equals(object obj)
        {
            if (obj is Entity)
            {
                // checks internal ids for equality
                return ((Entity)obj)._id.Equals(_id);
            } else
            {
                return base.Equals(obj);
            }
        }
    }
}
