using GoblinStronghold.Graphics.Util.Drawer;
using System;
using SadConsole;
using SadConsole.SerializedTypes;
using SadConsole.Effects;
using SadRogue.Primitives;
using Newtonsoft.Json;
using Console = SadConsole.Console;
using Palette = GoblinStronghold.Graphics.Util.Palette;

namespace GoblinStronghold.Screen
{
    public class LogScreen : SubScreen
    {
        public LogScreen(int width, int height) : base(width, height)
        { 
            SubConsole.Font = SadConsole.Game.Instance.LoadFont("res/fonts/ascii_8x16/ascii_8x16.font");
            SubConsole.Print(0, 0, "Log", Palette.WhiteBright);
        }

        // override so subscreen has right font
        protected override Console BuildSubConsole(int width, int height)
        {
            var surface = new ScreenSurface((width - 2)*2, height - 2);
            return new Console(
                surface.Surface,
                SadConsole.Game.Instance.LoadFont("res/fonts/ascii_8x16/ascii_8x16.font"),
                new Point(8,16)
            );
        }

        protected override Point SubScreenPosition()
        {
            // children get placed according to *their* font size, not the
            // parents so we need to account for half width: 
            return new Point(2, 1);
        }
    }
}
