using System;
using System.Linq;
using System.Collections.Generic;
using SadConsole;
using GoblinStronghold.Components;
using GoblinStronghold.Graphics;
using GoblinStronghold.Maps;

namespace GoblinStronghold.Entities
{
    public abstract class Entity : IHasComponents
    {
        // position
        public Cell Cell;

        // has components
        private List<IComponent> Components = new List<IComponent>();

        public void AddComponent(IComponent component)
        {
            Components.Add(component);
        }

        public bool RemoveComponent(IComponent component)
        {
            return Components.Remove(component);
        }

        public IEnumerable<IComponent> AllComponents()
        {
            return Components;
        }

        // has control brain?
        // has subscriptions?
        // can be destroyed?
    }
}

