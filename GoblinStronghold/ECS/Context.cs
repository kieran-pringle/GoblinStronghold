using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Collections;
using GoblinStronghold.Messaging;

namespace GoblinStronghold.ECS
{
    // the entry point into the ECS system
    // all components and entites must be created and registered here
    //
    // rules:
    // 1 - an entity has one component of each concrete type at most
    //      - but could have one of each subclass of a type
    // 2 - a component belongs to one entity at most

    // just do a naive nested dictionary implementation for now because it is
    // simplest to reason about

    // BUT: a single flat ContextID to object dictionary could be fastest
    // rather than intermediate lookups. Would need context ID to enclose
    // something about class, then something like an AVL tree

    // https://stackoverflow.com/questions/13177882/implementing-a-database-how-to-get-started

    public class Context : IContext
    {
        // link back to parent context
        internal readonly struct ContextID : IComparable<ContextID>
        {
            private readonly int _id;
            internal readonly Context _ctx;

            // only the Context can create thes
            internal ContextID(Context ctx, int id)
            {
                _ctx = ctx;
                _id = id;
            }

            public int CompareTo(ContextID other)
            {
                return this._id.CompareTo(other._id);
            }

            internal ContextID Next()
            {
                return new ContextID(_ctx, id: (this._id + 1));
            }
        }

        private static readonly Context s_instance = new Context();

        // keeps track of last used ID
        // since this is a non-nullable reference to a readonly struct this is
        // initialised as 0;
        // note that no two objects in the context, no matter type, should have
        // the same index
        private ContextID _currentId;

        private MessageBus _bus;

        // all components on an entity
        private IDictionary<
            Entity,
            IDictionary<Type, object>
        > _entityToComponentTypeToComponent;

        // all components of a type
        private IDictionary<
            Type,
            IEnumerable<object>
        > _componentTypeToComponents;

        // components contain a link back to entity internally

        public Context()
        {
            _entityToComponentTypeToComponent = new Dictionary<
               Entity,
               IDictionary<Type, object>>();
            _componentTypeToComponents = new Dictionary<
                Type,
                IEnumerable<object>>();

            _bus = new MessageBus();

            _currentId = new ContextID(this, 0);
        }

        // TODO: call destroy on everything to eliminate references so they
        // can get garbage collected
        public void Clear()
        {
            throw new NotImplementedException("We don't have a clear command yet");
        }

        public Entity CreateEntity()
        {
            var entity = new Entity(
                id: NextID()
            );

            // init join table
            _entityToComponentTypeToComponent[entity] =
                new Dictionary<Type, object>();

            return entity;
        }

        public void Destroy(Entity entity)
        {
            // destroy all components
            foreach (
                KeyValuePair<Type, object> typeToComponent
                in AllComponentsOnEntityMutable(entity))
            {
                ((IDestroyable) typeToComponent.Value).Destroy();
            }

            _entityToComponentTypeToComponent.Remove(entity);
        }

        public void Destroy<T>(Component<T> component)
        {
            AllComponentsOfTypeMutable<T>().Remove(component);
            AllComponentsOnEntityMutable(component.Owner).Remove(typeof(T));
            component.Owner = null;
        }

        // read-only access to all components
        public IEnumerable<Component<T>> AllComponents<T>()
        {
            return (IEnumerable<Component<T>>)_componentTypeToComponents[typeof(T)];
        }

        public IEnumerable<Entity> AllEntitiesWith<T>()
        {
            return _entityToComponentTypeToComponent
                .Where(kv => kv.Value.ContainsKey(typeof(T)))
                .Select(kv => kv.Key);
        }

        public IEnumerable<Entity> AllEntitiesWithMatching<T>(Func<T, bool> matcher)
        {
           return AllComponents<T>()
                .Where(c => matcher(c.Content))
                .Select(c => c.Owner);
        }

        // ********************************************************
        // internal
        // ********************************************************

        internal void AddComponentTo<T>(T component, Entity entity)
        {
            if (component == null)
            {
                return; // noop
            }

            // check if such a component exists yet
            var componentType = typeof(T);
            var currentComponents = AllComponentsOnEntityMutable(entity);
            if (currentComponents.ContainsKey(componentType))
            {
                Destroy(GetComponentFromEntity<T>(entity));
            }

            var newComponent = new Component<T>(
                NextID(),
                entity,
                component);

            AllComponentsOfTypeMutable<T>().Add(newComponent);
            currentComponents[componentType] = newComponent;
        }

        internal Component<T> GetComponentFromEntity<T>(Entity entity)
        {
            var components = AllComponentsOnEntityMutable(entity);
            var componentType = typeof(T);
            if (components.ContainsKey(componentType))
            {
                return (Component<T>)AllComponentsOnEntityMutable(entity)[typeof(T)];
            }
            else
            {
                return null;
            }
        }

        internal ComponentStore ComponentStoreFor(Entity entity)
        {
            if (_entityToComponentTypeToComponent.ContainsKey(entity))
            {
                return new ComponentStore(_entityToComponentTypeToComponent[entity]);
            }
            else
            {
                throw new KeyNotFoundException("There is no such entity in" +
                    "this context. It may have been destroyed");
            }
        }

        public void Register<T>(System<T> system)
        {
            _bus.Register(system);
            system._context = this;
        }

        public void Unregister<T>(System<T> system)
        {
            _bus.UnRegister(system);
            system._context = null;
        }

        public void Send<T>(T message)
        {
            _bus.Send(message);
        }

        // ********************************************************
        // private
        // ********************************************************

        private ContextID NextID()
        {
            var next = _currentId.Next();
            _currentId = next;
            return next;
        }

        // ------------------------------------------
        // get or create accessors to sub-collections
        // ------------------------------------------
        private IDictionary<Type, object> AllComponentsOnEntityMutable(Entity entity)
        {
            IDictionary<Type, object> entityTypeToComponent;

            if (_entityToComponentTypeToComponent.ContainsKey(entity))
            {
                entityTypeToComponent = _entityToComponentTypeToComponent[entity]; 
            }
            else
            {
                entityTypeToComponent = new Dictionary<Type, object>();
                _entityToComponentTypeToComponent[entity] = entityTypeToComponent;
            }

            return entityTypeToComponent;
        }

        private ISet<Component<T>> AllComponentsOfTypeMutable<T>()
        {
            ISet<Component<T>> components;
            Type componentType = typeof(T);

            if(_componentTypeToComponents.ContainsKey(componentType))
            {
                // safe cast
                components =
                    (ISet<Component<T>>)_componentTypeToComponents[componentType];
            }
            else
            {
                components = new HashSet<Component<T>>();
                _componentTypeToComponents[componentType] =
                    (IEnumerable<object>) components;
            }

            return components;
        }
    }
}
