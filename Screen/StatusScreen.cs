using System;
using GoblinStronghold.Screen.Drawer;
using SadConsole;
using SadRogue.Primitives;
using Console = SadConsole.Console;

namespace GoblinStronghold.Screen
{
    public class StatusScreen : SubScreen
    {
        private Color[] _testGradient = new[]{
            Color.LightGreen,
            Color.SeaGreen,
            Color.CornflowerBlue,
            Color.DarkSlateBlue
        };

        public StatusScreen(int width, int height) : base(width, height)
        {
            SubConsole.Cursor.Move(1, 1).Print("Status");
            GradientDrawer.Draw(SubConsole, _testGradient);
        }
    }
}

