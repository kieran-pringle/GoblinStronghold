using System;
using SadConsole;
using SadRogue.Primitives;
using Console = SadConsole.Console;

namespace sadconsoletut.Screen
{
    public class SubScreen : Console
    {
        static protected ColoredGlyph HorizontalBorderUnfocused = new ColoredGlyph(
            Color.White,
            Color.Black,
            128);
        static protected ColoredGlyph VerticalBorderUnfocused = new ColoredGlyph(
            Color.White,
            Color.Black,
            130);
        static protected ColoredGlyph TopRightBorderUnfocused = new ColoredGlyph(
            Color.White,
            Color.Black,
            132);
        static protected ColoredGlyph BottomRightBorderUnfocused = new ColoredGlyph(
            Color.White,
            Color.Black,
            134);
        static protected ColoredGlyph TopLeftBorderUnfocused = new ColoredGlyph(
            Color.White,
            Color.Black,
            136);
        static protected ColoredGlyph BottomLeftBorderUnfocused = new ColoredGlyph(
            Color.White,
            Color.Black,
            138);

        static protected ColoredGlyph HorizontalBorderFocused = new ColoredGlyph(
            Color.White,
            Color.Black,
            129);
        static protected ColoredGlyph VerticalBorderFocused = new ColoredGlyph(
            Color.White,
            Color.Black,
            131);
        static protected ColoredGlyph TopRightBorderFocused = new ColoredGlyph(
            Color.White,
            Color.Black,
            133);
        static protected ColoredGlyph BottomRightBorderFocused = new ColoredGlyph(
            Color.White,
            Color.Black,
            135);
        static protected ColoredGlyph TopLeftBorderFocused = new ColoredGlyph(
            Color.White,
            Color.Black,
            137);
        static protected ColoredGlyph BottomLefttBorderUnfocused = new ColoredGlyph(
            Color.White,
            Color.Black,
            139);

        public Console SubConsole;

        public SubScreen(int width, int height) : base(width, height)
        {
            SubConsole = new Console(width - 2, height - 2); // allow for border
            SubConsole.Position = new Point(1, 1);
            Children.Add(SubConsole);

            DrawBorder();
        }

        private void DrawBorder(bool focused = false)
        {
            // draw top and bottom
            for (int x = 1; x < base.Width - 1; x++)
            {
                base.Surface[x, 0].CopyAppearanceFrom(HorizontalBorderUnfocused);
                base.Surface[x, base.Height - 1].CopyAppearanceFrom(HorizontalBorderUnfocused);
            }
            // draw left and right
            for (int y = 1; y < base.Height - 1; y++)
            {
                base.Surface[0, y].CopyAppearanceFrom(VerticalBorderUnfocused);
                base.Surface[base.Width - 1, y].CopyAppearanceFrom(VerticalBorderUnfocused);
            }
            // corners
            base.Surface[0, 0].CopyAppearanceFrom(TopLeftBorderUnfocused);
            base.Surface[base.Width - 1, 0].CopyAppearanceFrom(TopRightBorderUnfocused);
            base.Surface[0, base.Height - 1].CopyAppearanceFrom(BottomLeftBorderUnfocused);
            base.Surface[base.Width - 1, base.Height - 1].CopyAppearanceFrom(BottomRightBorderUnfocused);
        }

        // TODO: Get rid of this when we don't need it for testing
        protected void FillBackground(ScreenSurface screenSurface, Color[] colors)
        {
            var colorStops = new[] { 0f, 0.35f, 0.75f, 1f };
            var gradient = new Gradient(colors, colorStops);

            Algorithms.GradientFill(
                screenSurface.FontSize,             // cell size
                screenSurface.Surface.Area.Center,  // midpoint
                screenSurface.Surface.Width,    // spread witdth
                90,                                  // angle degrees
                screenSurface.Surface.Area,         // coverage area
                gradient,                           // gradient colours
                (x, y, color) => {                  // callback for each point
                    screenSurface.Surface[x, y].Background = color;
                }
            );
        }
    }
}

