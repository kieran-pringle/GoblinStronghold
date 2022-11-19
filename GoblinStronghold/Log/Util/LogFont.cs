using System;
using SadConsole;
using SadRogue.Primitives;
using Palette = GoblinStronghold.Graphics.Util.Palette;

namespace GoblinStronghold.Log.Util
{
    // TODO: somehow abstract this out? we've used it twice now
	public static class LogFont
	{
        private static IFont _font;

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
                foreground = Palette.WhiteBright;
            }
            if (background == null)
            {
                background = Palette.Black;
            }
            if (cellDecorators == null)
            {
                cellDecorators = Array.Empty<CellDecorator>();
            }

            return new ColoredGlyph(
                (Color)foreground,
                (Color)background,
                GlyphIndex(name),
                mirror,
                isVisible,
                cellDecorators
            );
        }
    }
}
