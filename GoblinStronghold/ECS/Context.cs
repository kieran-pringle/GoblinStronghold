using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.ComponentModel;

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

    public class Context
    {
        // no way to make this a path dependent type, so we treat context as
        // a singleton so we don't have to manage multiple sources
        internal readonly struct ContextID : IComparable<ContextID>
        {
            private readonly int _id;

            // only the Context can create thes
            private ContextID(int id)
            {
                _id = id;
            }

            public int CompareTo(ContextID other)
            {
                return this._id.CompareTo(other._id);
            }

            internal ContextID Next()
            {
                return new ContextID(id: (this._id + 1));
            }
        }

        private static readonly Context s_instance = new Context();

        // keeps track of last used ID
        // since this is a non-nullable reference to a readonly struct this is
        // initialised as 0;
        // note that no two objects in the context, no matter type, should have
        // the same index
        private ContextID _currentId;

        private IDictionary<Entity, IDictionary<Type, Component>> _entityTable;

        private IDictionary<Type, IDictionary<Component, Entity>> _componentTable;

        // Tables of all the systems
        // private Dictionary<Type, object> _onLoadSystems;

        public static Context Instance()
        {
            return s_instance;
        }

        public void Clear()
        {
            // wipe everything
        }

        public Entity CreateEntity()
        {
            var entity = new Entity(
                ctx: this,
                id: NextID()
            );

            throw new NotImplementedException("We need to insert entity into table");

            return entity;
        }

        public void Destroy(Entity e)
        {
            throw new NotImplementedException("Need to remove entity and destroy its components");
        }

        public void Destroy(Component c)
        {
            throw new NotImplementedException("Need to remove component from parent");
        }

        internal void AddComponentTo(Component c, Entity e)
        {
            throw new NotImplementedException();
        }

        // private constructor
        private Context()
        {
            _entityHasComponents = new SortedDictionary<
                ContextID,
                ISet<Type>
            >();
            _componentToEntityJoins = new SortedDictionary<
                Type,
                IDictionary<
                    ContextID,
                    IList<ContextID>
                >
            >();
            // remember, we can't sort over type!
            _componentTables = new Dictionary<
                Type,
                IDictionary<
                    ContextID,
                    Component>
            >();
            _entityTable = new SortedDictionary<
                ContextID,
                Entity
            >();
        }

        private ContextID NextID()
        {
            var next = _currentId.Next();
            _currentId = next;
            return next;
        }

        // ------------------------------------------
        // get or create accessors to sub-collections
        // ------------------------------------------

    }
}

