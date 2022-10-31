using SadConsole;
using SadConsole.Input;
using SadRogue.Primitives;
using System.Collections.Generic;
using sadconsoletut.Screen;

namespace sadconsoletut
{
    public class RootScreen : ScreenObject
    {
        private readonly StatusScreen _status;
        private readonly MapScreen _map;
        private readonly InventoryScreen _inventory;
        private readonly LogScreen _logs;

        public RootScreen()
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

            _status.SubConsole.Cursor.Move(1, 1).Print("Stats");
            _map.SubConsole.Cursor.Move(1, 1).Print("Map");
            _inventory.SubConsole.Cursor.Move(1, 1).Print("Inventory");
            _logs.SubConsole.Cursor.Move(1, 1).Print("Logs");

            Children.Add(_status);
            Children.Add(_map);
            Children.Add(_inventory);
            Children.Add(_logs);

            // example of getting named glyph
            //var decorator = _map.Font.GetDecorator("border-top-right-diagonal", Color.White);
            //_map.SubConsole.Surface[8, 8].CopyAppearanceFrom(new ColoredGlyph(Color.AnsiMagentaBright, Color.DarkViolet, decorator.Glyph));
        }
    }
}