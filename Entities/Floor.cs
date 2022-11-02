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
            var toReturn = TileSet.ColoredGlyph("floor-basic");
            return toReturn;
        }
    }
}

