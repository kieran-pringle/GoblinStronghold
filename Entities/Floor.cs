using System;
using GoblinStronghold.Graphics;
using GoblinStronghold.Maps;
using SadConsole;

namespace GoblinStronghold.Entities
{
    public class Floor : Entity
    {
        public override ColoredGlyph Appearance()
        {
            return TileSet.ColoredGlyph("floor-basic");
        }
    }
}

