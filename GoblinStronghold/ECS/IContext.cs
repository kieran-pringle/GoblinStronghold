using System;
using System.Collections.Generic;

namespace GoblinStronghold.ECS
{
    /**
     * <summary>
     *      <c>IContext</c> represents the public API of an ECS context, which
     *      is the entry point into managing Entities, Components and Systems
     *      in the game's ECS.
     * </summary>
     */
    public interface IContext
    {
        /**
         * <summary>
         *      Destroy everything in the Context
         * </summary>
         */
        public void Clear();

        /**
         * <summary>
         *      Create an <c>Entity</c> managed by this context
         * </summary>
         */
        public Entity CreateEntity();

        /**
         *  <summary>
         *      Get all <c>Component</c> instances of a given type
         *  </summary>
         */
        public IEnumerable<Component<T>> AllComponents<T>();

        /**
        * <summary>
        *      Destroy an <c>Entity</c> managed by this context. This will also
        *      destroy all components owned by the entity.
        * </summary>
        */
        public void Destroy(Entity entity);

        /**
         * <summary>
         *      Destroy a <c>Component</c> and dereference it from an 
         *      <c>Entity</c>.
         * </summary>
         */
        public void Destroy<T>(Component<T> component);
    }
}

