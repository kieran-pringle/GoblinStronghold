using System;
using GoblinStronghold.Graphics.Drawer;
using SadConsole;
using SadRogue.Primitives;
using Console = SadConsole.Console;

namespace GoblinStronghold.Screen
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
            SubConsole.Cursor.Move(1, 1).Print("Inventory");
            GradientDrawer.Draw(SubConsole, _testGradient);
        }
    }
}

