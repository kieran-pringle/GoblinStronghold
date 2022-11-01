using System;
using GoblinStronghold.Graphics.Drawer;
using SadConsole;
using SadRogue.Primitives;
using Console = SadConsole.Console;

namespace GoblinStronghold.Screen
{
    public class MapScreen : SubScreen
    {
        private Color[] _testGradient = new[]{
            Color.DarkRed,
            Color.OrangeRed,
            Color.Goldenrod,
            Color.PaleGoldenrod
        };

        public MapScreen(int width, int height) : base(width, height)
        {
            SubConsole.Cursor.Move(1, 1).Print("Map");
            GradientDrawer.Draw(SubConsole, _testGradient);
        }
    }
}

