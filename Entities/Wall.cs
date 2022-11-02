using System;
using GoblinStronghold.Graphics;
using GoblinStronghold.Maps;
using GoblinStronghold.Components;
using SadConsole;

namespace GoblinStronghold.Entities
{
    public class Wall : Entity
    {
        public Wall()
        {
            AddComponent(CollisionComponent.Impassable);
        }

        public override ColoredGlyph Appearance()
        {
            return TileSet.ColoredGlyph("wall-basic");
        }
    }
}

