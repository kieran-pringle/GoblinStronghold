using System;
using SadConsole;
using System.Collections.Generic;
using SadRogue.Primitives;
using GoblinStronghold.Entities;
using GoblinStronghold.Graphics;

namespace GoblinStronghold.Maps
{
    // one space in the game map
    public class Cell : IHasAppearance
    {
        private List<Entity> _entities = new List<Entity>(); // last into space is first out for rendering
        public Point Position;
        public Map Map;

        public ColoredGlyph EmptyAppearance = TileSet.ColoredGlyph("floor-basic");

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
            var oldLocation = entity.Cell;
            if (oldLocation != null)
            {
                oldLocation.LeaveHere(entity);
            }
            _entities.Add(entity);
        }

        public void LeaveHere(Entity entity)
        {
            // similarly whatever callbacks can go here
            _entities.Remove(entity);
        }
    }
}
