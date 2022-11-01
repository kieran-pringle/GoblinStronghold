using GoblinStronghold.Graphics.Drawer;
using System;
using SadConsole;
using SadRogue.Primitives;
using Console = SadConsole.Console;

namespace GoblinStronghold.Screen
{
    public class LogScreen : SubScreen
    {
        private Color[] _testGradient = new[]{
            Color.Violet,
            Color.BlueViolet,
            Color.MediumPurple,
            Color.Indigo
        };

        public LogScreen(int width, int height) : base(width, height)
        {
            SubConsole.Cursor.Move(1, 1).Print("Log");
            GradientDrawer.Draw(SubConsole, _testGradient);
        }
    }
}

