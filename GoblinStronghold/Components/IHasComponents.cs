using System;
using System.Linq;
using System.Collections.Generic;

namespace GoblinStronghold.Components
{
    public interface IHasComponents
    {
        public IEnumerable<IComponent> AllComponents();

        public void AddComponent(IComponent component);

        public bool RemoveComponent(IComponent component);

        // passed type needs to be the same as T
        public IList<T> ComponentsMatching<T>(Type t) where T : IComponent
        {
            return AllComponents().Where(c => {
                var cType = c.GetType();
                return cType == t || cType.IsSubclassOf(t);
            })
           .Select(c => (T)c)
           .ToList<T>();
        }
    }
}

