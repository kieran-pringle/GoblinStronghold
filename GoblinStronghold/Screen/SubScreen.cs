using System;
using SadConsole;
using SadRogue.Primitives;
using GoblinStronghold.Graphics.Util.Drawer;
using Palette = GoblinStronghold.Graphics.Util.Palette;
using Console = SadConsole.Console;

namespace GoblinStronghold.Screen
{
    public class SubScreen : Console
    {
        public Console SubConsole;

        public SubScreen(int width, int height) : base(width, height)
        {
            SubConsole = BuildSubConsole(width, height); // allow for border
            SubConsole.Position = SubScreenPosition();
            Children.Add(SubConsole);

            base.DefaultBackground = Palette.Black;
            base.DefaultForeground = Palette.White;

            SubConsole.Cursor.SetPrintAppearance(Palette.White);
            base.Cursor.SetPrintAppearance(Palette.White);

            DrawBorder();
        }

        protected void DrawBorder()
        {
                BorderDrawer.Draw(this, BorderDrawer.Default);
        }

        protected virtual Console BuildSubConsole(int width, int height)
        {
            return new Console(width - 2, height - 2); // allow for border
        }

        protected virtual Point SubScreenPosition()
        {
            return new Point(1, 1);
        }
    }
}

