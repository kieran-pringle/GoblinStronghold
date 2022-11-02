using System;
namespace GoblinStronghold.Components
{
    // represents what happens when another entity tries to move into the same
    // space as the entity with this component
    abstract public class CollisionComponent : IComponent
    {
        // TODO: flywheel pattern, only need one instance
        // unowned components should all be flywheelable? how to manage?
        public static CollisionComponent Impassable = new ImpassableCollision();

        // returns true if space can be entered
        // returns false if the owners space can not be entered
        abstract public bool CollideWith(IHasComponents hasComponents);

        // TODO: is this not just a function type? are components basically functions?
        // maybe only non-owned components?
        private class ImpassableCollision : CollisionComponent
        {
            public override bool CollideWith(IHasComponents hasComponents)
            {
                return false;
            }
        }
    }
}

