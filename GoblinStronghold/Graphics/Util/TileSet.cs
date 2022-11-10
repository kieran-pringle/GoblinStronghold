using System;
using SadConsole;
using SadRogue.Primitives;

namespace GoblinStronghold.Graphics.Util
{
    public static class TileSet
    {
        private static IFont _font;
        private static readonly Color _white = new Color(1f, 1f, 1f);

        public static void Load(IFont font)
        {
            _font = font;
        }

        public static int GlyphIndex(string name)
        {
            return _font.GetDecorator(name, Color.White).Glyph;
        }

        public static ColoredGlyph ColoredGlyph(
            string name,
            Color? foreground = null,
            Color? background = null,
            Mirror mirror = Mirror.None,
            bool isVisible = true,
            CellDecorator[] cellDecorators = null)
        {
            // workaround for default params needing to be compile time constants
            if (foreground == null)
            {
                foreground = Color.White;
            }
            if (background == null)
            {
                background = Color.Black;
            }
            if (cellDecorators == null)
            {
                cellDecorators = Array.Empty<CellDecorator>();
            }

            return new ColoredGlyph(
                (Color) foreground,
                (Color) background,
                GlyphIndex(name),
                mirror,
                isVisible,
                cellDecorators
            );
        }
    }
}

