using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Functional.Option;

namespace GoblinStronghold.ECS
{
    // type-safe read-only dictionary of components by type
    public class ComponentStore
    {
        private IDictionary<Type, object> _store;

        internal ComponentStore(IDictionary<Type, object> store)
        {
            _store = store;
        }

        [return: MaybeNull] // if component doesn't exist
        public Option<Component<T>> Get<T>()
        {
            var type = typeof(T);
            if (_store.ContainsKey(type))
            {
                return (Component<T>)_store[type];
            }
            else
            {
                return Option<Component<T>>.None;
            }
        }
    }
}

