using System;
using System.Linq;
using GoblinStronghold.ECS;
using GoblinStronghold.Physics.Components;
using GoblinStronghold.Physics.Messages;

namespace GoblinStronghold.Physics.Systems
{
    public class MoveToSystem : ISystem<MoveTo>
    {
        private IContext _context;
        IContext ISystem<MoveTo>.Context
        {
            get => _context;
            set => _context = value;
        }

        public MoveToSystem()
        {
        }

        public void Handle(MoveTo message)
        {
            // find all entites at position being moved to
            var allEntitesAtPosition = _context
                .AllEntitiesWithMatching<Position>(p =>
                {
                    return p.Equals(message.NewPosition);
                });

            // map each entity to the result of its collision
            // if there is one
            var collisionResults = allEntitesAtPosition
                .Select(e =>
                {
                    var canMoveTo = true;
                    e.Component<Collision>().ForEach(c =>
                    {
                        // collide with moving entity
                        canMoveTo = c.Content.Collide(message.Entity);
                    });
                    return canMoveTo;
                });

            // we are blocked if any handler returned false
            var blocked = collisionResults.Any(b => !b);

            // if no block happend, we update position
            if(!blocked)
            {
                // set new position on entity
                message.Entity.With(message.NewPosition);
            }
        }
    }
}

