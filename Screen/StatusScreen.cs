using System;
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
            FillBackground(SubConsole, _testGradient);
        }
    }
}

