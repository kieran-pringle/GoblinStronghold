using System;
using SadConsole;
using SadRogue.Primitives;
using GoblinStronghold.Screen.Drawer;
using Console = SadConsole.Console;

namespace GoblinStronghold.Screen
{
    public class SubScreen : Console
    {

        public Console SubConsole;

        public SubScreen(int width, int height) : base(width, height)
        {
            SubConsole = new Console(width - 2, height - 2); // allow for border
            SubConsole.Position = new Point(1, 1);
            Children.Add(SubConsole);

            DrawBorder();
        }

        private void DrawBorder()
        {
            if (this.IsFocused)
                BorderDrawer.DrawBorder(this, BorderDrawer.Double);
            else
                BorderDrawer.DrawBorder(this, BorderDrawer.Default);
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

        public override void OnFocused()
        {
            // redraw border
            IsDirty = true;
            base.OnFocused();
        }

        public override void OnFocusLost()
        {
            // redraw border (and clear any selection indicators or whatever
            IsDirty = true;
            base.OnFocusLost();
        }
    }
}

