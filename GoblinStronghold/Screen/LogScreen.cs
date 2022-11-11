using GoblinStronghold.Graphics.Util.Drawer;
using System;
using SadConsole;
using SadConsole.Effects;
using SadRogue.Primitives;
using Console = SadConsole.Console;
using Palette = GoblinStronghold.Graphics.Util.Palette;

namespace GoblinStronghold.Screen
{
    public class LogScreen : SubScreen
    {
        public LogScreen(int width, int height) : base(width, height)
        {
            SubConsole.Print(0, 0, "Log", Palette.WhiteBright);
        }
    }
}

