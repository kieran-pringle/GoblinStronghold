using System;
using SadConsole;
using SadRogue.Primitives;

using GoblinStronghold.Graphics.Util;

namespace GoblinStronghold.Graphics.Components
{
    public class RandomGlyph : IGlyphProvider
    {
        private string[] _possible;
        private ColoredGlyph _chosen;

        public Color Foreground = Color.White;
        public Color Background = Color.Black;

        public RandomGlyph(string[] possibleGlyphs)
        {
            _possible = possibleGlyphs;
        }

        public ColoredGlyph Glyph()
        {
            if (_chosen == null)
            {
                // pick randomly from pool
                _chosen = TileSet.ColoredGlyph(
                    _possible[
                        GameManager.Random.Next(0, _possible.Length)
                    ],
                    Foreground,
                    Background
                );   
            }
            return _chosen;
        }
    }
}

