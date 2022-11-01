using System;
using SadConsole;
using SadRogue.Primitives;

namespace GoblinStronghold.Graphics.Drawer
{
    public static class GradientDrawer
    {
        public static void Draw(ScreenSurface screenSurface, Color[] colors)
        {
            var colorStops = new[] { 0f, 0.35f, 0.75f, 1f };
            var gradient = new Gradient(colors, colorStops);

            Algorithms.GradientFill(
                screenSurface.FontSize,             // cell size
                screenSurface.Surface.Area.Center,  // midpoint
                screenSurface.Surface.Width,        // spread witdth
                90,                                 // angle degrees
                screenSurface.Surface.Area,         // coverage area
                gradient,                           // gradient colours
                (x, y, color) => {                  // callback for each point
                    screenSurface.Surface[x, y].Background = color;
                }
            );
        }
    }
}

