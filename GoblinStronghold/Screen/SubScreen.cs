using System;
using SadConsole;
using SadRogue.Primitives;
using GoblinStronghold.Graphics.Util.Drawer;
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

        protected void DrawBorder()
        {
                BorderDrawer.Draw(this, BorderDrawer.Default);
        }
    }
}

