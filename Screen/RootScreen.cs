using SadConsole;
using SadConsole.Input;
using SadRogue.Primitives;
using System.Collections.Generic;
using GoblinStronghold.Screen;
using GoblinStronghold.Maps;
using System.Diagnostics;
using GoblinStronghold.Graphics.Drawer;

namespace GoblinStronghold
{
    public class RootScreen : ScreenObject
    {
        private readonly StatusScreen _status;
        private readonly MapScreen _map;
        private readonly InventoryScreen _inventory;
        private readonly LogScreen _logs;

        // TODO: inject map into here, possibly in Program
        public RootScreen(Map map)
        {
            // compensate for default font size of 16 * 8
            int fullWidth = Game.Instance.ScreenCellsX;
            int fullHeight = Game.Instance.ScreenCellsY;

            // right one one quarter for logs
            int logScreenWidth = fullWidth / 4;
            int remainingWidth = fullWidth - logScreenWidth;

            // top and bottom quarter for inventory and stats
            int inventoryAndStatHeight = fullHeight / 6;
            int mapHeight = fullHeight - (inventoryAndStatHeight * 2);

            _status = new StatusScreen(
                remainingWidth,
                inventoryAndStatHeight
            );
            _map = new MapScreen(
                map,
                remainingWidth,
                mapHeight
            );
            _inventory = new InventoryScreen(
                remainingWidth,
                inventoryAndStatHeight
            );
            _logs = new LogScreen(
              logScreenWidth,
              fullHeight
            );

            // position screens
            // top left
            _status.Position = new Point(0, 0);
            // middle
            _map.Position = new Point(0, inventoryAndStatHeight);
            // bottom
            _inventory.Position = new Point(0, inventoryAndStatHeight + mapHeight);
            // right
            _logs.Position = new Point(remainingWidth, 0);

            Children.Add(_status);
            Children.Add(_map);
            Children.Add(_inventory);
            Children.Add(_logs);

        }

        public override bool ProcessKeyboard(Keyboard keyboard)
        {
            int i = 4;
            var subConsole = _logs.SubConsole;
            foreach (AsciiKey k in keyboard.KeysDown)
            {
                subConsole.Cursor.Move(1, i).Print(k.Character.ToString());
                i++;
            }
            return true;
        }
    }
}