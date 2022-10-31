using System;
using SadConsole;
using SadRogue.Primitives;
using Console = SadConsole.Console;

namespace sadconsoletut.Screen
{
    public class InventoryScreen : SubScreen
    {
        private Color[] _testGradient = new[]{
            Color.LightSalmon,
            Color.Pink,
            Color.MediumPurple,
            Color.Violet
        }; 

        public InventoryScreen(int width, int height) : base(width, height)
        {
            FillBackground(SubConsole, _testGradient);
        }
    }
}

