using SadConsole;
using SadConsole.Input;
using SadRogue.Primitives;
using System.Collections.Generic;
using GoblinStronghold.Screen;
using System.Diagnostics;
using GoblinStronghold.Graphics.Util.Drawer;
using System;

using Console = SadConsole.Console;
using Palette = GoblinStronghold.Graphics.Util.Palette;

namespace GoblinStronghold
{
    public class RootScreen : ScreenObject
    {
        private readonly MapScreen _map;
        private readonly LogScreen _logs;

        // TODO: inject map into here, possibly in Program
        public RootScreen()
        {
            // compensate for default font size of 16 * 8
            int fullWidth = Game.Instance.ScreenCellsX;
            int fullHeight = Game.Instance.ScreenCellsY;

            // right one one quarter for logs
            int logScreenWidth = fullWidth / 4;
            int remainingWidth = fullWidth - logScreenWidth;

            _logs = new LogScreen(
              logScreenWidth,
              fullHeight
            );
            _map = new MapScreen(
                remainingWidth,
                fullHeight
            );

            // left
            _map.Position = new Point(0, 0);
            // right
            _logs.Position = new Point(remainingWidth, 0);

            Children.Add(_map);
            Children.Add(_logs);

            _map.DefaultBackground = Palette.Black;
            _logs.DefaultBackground = Palette.Black;
        }

        public Console MapConsole()
        {
            return _map.SubConsole;
        }

        public Console LogConsole()
        {
            return _logs.SubConsole;
        }
    }
}