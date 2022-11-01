using System;
using GoblinStronghold.Graphics;
using GoblinStronghold.Maps;
using SadConsole;

namespace GoblinStronghold.Entities
{
    public class Wall : Entity
    {
        public override ColoredGlyph Appearance()
        {
            return TileSet.ColoredGlyph("wall-basic");
        }
    }
}

