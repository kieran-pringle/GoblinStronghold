using System;
namespace GoblinStronghold.Components
{
    // Used for when components need a reference to their owner
    public interface IOwnedComponent : IComponent
    {
        public abstract IHasComponents Owner();
    }
}

