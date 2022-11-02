using System;
using SadConsole;
using System.Collections.Generic;
using System.Linq;
using SadRogue.Primitives;
using GoblinStronghold.Components;
using GoblinStronghold.Entities;
using GoblinStronghold.Graphics;

namespace GoblinStronghold.Maps
{
    // TODO: some component checker to iterate over everything in this area that
    // has some component or not? Collisions could be moved to that

    // one space in the game map
    public class Cell : IHasAppearance 
    {
        public Point Position { get; private set; }
        public Map Map { get; private set; }
        public ColoredGlyph EmptyAppearance { get; private set; } = TileSet.ColoredGlyph("floor-basic");

        private List<Entity> _entities = new List<Entity>();

        public Cell(Map map, Point position)
        {
            Map = map;
            Position = position;
        }

        // should only be used if cell is completely empty
        public ColoredGlyph Appearance()
        {
            return EmptyAppearance;
        }

        // Copy it so that it can't be modified
        public Entity[] Entities()
        {
            var array = _entities.ToArray();
            return array;
        }

        // Move an entity to this location
        public void MoveHere(Entity entity)
        {
            // some further logic to check if it can and what happens and to
            // place it correctly

            if(IsCollides(entity))
            {
                return; // can't move to this location
            }

            var oldLocation = entity.Cell;
            if (oldLocation != null)
            {
                oldLocation.LeaveHere(entity);
            }
            _entities.Add(entity);
            entity.Cell = this;

            Map.CellChanged(this);
        }

        public void LeaveHere(Entity entity)
        {
            // similarly whatever callbacks can go here
            _entities.Remove(entity);
            entity.Cell = null;

            Map.CellChanged(this);
        }

        public Cell North()
        {
            return Map[
                new Point(
                    this.Position.X,
                    this.Position.Y - 1
                )
            ];
        }

        public Cell South()
        {
            return Map[
                new Point(
                    this.Position.X,
                    this.Position.Y + 1
                )
            ];
        }

        public Cell East()
        {
            return Map[
                new Point(
                    this.Position.X + 1,
                    this.Position.Y
                )
            ];
        }

        public Cell West()
        {
            return Map[
                new Point(
                    this.Position.X - 1,
                    this.Position.Y
                )
            ];
        }

        // check the collision component for all entities,
        // if even one says no then return true
        private bool IsCollides(Entity collider)
        {
            return _entities
                // get a list of all collision components
                .SelectMany((IHasComponents e) => {
                        return e.ComponentsMatching<CollisionComponent>(
                            typeof(CollisionComponent));
                    })
                // iterate over all
                .Select(c => c.CollideWith(collider))
                // return true on first false (which means collision)
                .Any(b => !b); 
        }
    }
}
