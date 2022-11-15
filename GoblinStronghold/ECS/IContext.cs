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
         *      Get all <c>Entities</c> with a given <c>Component</c>
         * </summary>
         */
        public IReadOnlyDictionary<Entity, T> AllEntitiesWith<T>();

        /**
         * <summary>
         *      Get all <c>Entities</c> with a <c>Component</c> matching a 
         *      given choice function
         * </summary>
         */
        public IEnumerable<Entity> AllEntitiesWithMatching<T>(Func<T, bool> matcher);

        /**
         * <summary>
         *      Register a <c>System</c> to the context
         * </summary>
         */
        public void Register<T>(ISystem<T> system);

        /**
         * <summary>
         *      Unregister a <c>System</c> from the context
         * </summary>
         */
        public void Unregister<T>(ISystem<T> system);

        /**
         * Sends a message to all registered <c>System<c/> instances for that
         * message
         */
        public void Send<T>(T message);

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

        /**
         * Typesafe map access to all components
         */
        ComponentStore ComponentStoreFor(Entity entity);
    }
}
