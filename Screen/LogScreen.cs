using System;
using SadConsole;
using SadRogue.Primitives;
using Console = SadConsole.Console;

namespace sadconsoletut.Screen
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
            FillBackground(SubConsole, _testGradient);
        }
    }
}

