using Functional.Option;
using static GoblinStronghold.ECS.Context;

namespace GoblinStronghold.ECS
{
    /**
     *  The wrapper class containing the data in a component and its link back to its parent entity.
     */
    public sealed class Component<T> :  IDestroyable
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

        public Context Context()
        {
            return _id._ctx;
        }
    }

    // extension method for unwrapping nicely from an Option when we know it is populated
    public static class ComponentExtensions
    {
        public static T Content<T>(this Option<Component<T>> componentOption)
        {
            return componentOption.Value.Content;
        }
    }
}
