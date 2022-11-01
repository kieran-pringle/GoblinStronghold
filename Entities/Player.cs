using System;
using SadConsole;
using GoblinStronghold.Graphics;

namespace GoblinStronghold.Entities
{
    public class Player : Entity
    {
        public Player()
        {
        }

        public override ColoredGlyph Appearance()
        {
            return TileSet.ColoredGlyph("@");
        }
    }
}

