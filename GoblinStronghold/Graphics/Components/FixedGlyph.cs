using System;
using SadConsole;
using GoblinStronghold.Graphics.Util;

namespace GoblinStronghold.Graphics.Components
{
    public class FixedGlyph : IGlyphProvider
    {
        private ColoredGlyph _glyph;

        public FixedGlyph(string name)
        {
            _glyph = TileSet.ColoredGlyph(name);
        }

        public ColoredGlyph Glyph()
        {
            return _glyph;
        }
    }
}

