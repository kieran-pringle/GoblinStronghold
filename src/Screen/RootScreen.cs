using SadConsole;
using SadConsole.Input;
using SadRogue.Primitives;
using System.Collections.Generic;

namespace SadConsoleGame
{
    public class RootScreen : ScreenObject
    {
        private readonly Console _stats;
        private readonly Console _map;
        private readonly Console _inventory;
        private readonly Console _logs;
        private readonly IFont _font;

        public RootScreen()
        {
            // compensate for default font size of 16 * 8
            int fullWidth = Game.Instance.ScreenCellsX / 2;
            int fullHeight = Game.Instance.ScreenCellsY;

            // right one one quarter for logs
            int logScreenWidth = fullWidth / 4;
            int remainingWidth = fullWidth - logScreenWidth;

            // top and bottom quarter for inventory and stats
            int inventoryAndStatHeight = fullHeight / 6;
            int mapHeight = fullHeight - (inventoryAndStatHeight * 2);

            _stats = new Console(
                remainingWidth,
                inventoryAndStatHeight
            );
            _map = new Console(
                remainingWidth,
                mapHeight 
            );
            _inventory = new Console(
                remainingWidth,
                inventoryAndStatHeight
            );
            _logs = new Console(
              logScreenWidth,
              fullHeight
            );

            // position screens
            // top left
            _stats.Position = new Point(0, 0);
            // middle
            _map.Position = new Point(0, inventoryAndStatHeight);
            // bottom
            _inventory.Position = new Point(0, inventoryAndStatHeight + mapHeight);
            // right
            _logs.Position = new Point(remainingWidth, 0);

            FillBackground(_stats,
                new[]{
                    Color.LightGreen,
                    Color.SeaGreen,
                    Color.CornflowerBlue,
                    Color.DarkSlateBlue
                }
            );
            _stats.Cursor.Move(1, 1).Print("Stats");

            FillBackground(_map,
                new[]{
                    Color.DarkRed,
                    Color.OrangeRed,
                    Color.Goldenrod,
                    Color.PaleGoldenrod
                }
            );
            _map.Cursor.Move(1, 1).Print("Map");

            FillBackground(_inventory,
                new[]{
                    Color.LightSalmon,
                    Color.Pink,
                    Color.MediumPurple,
                    Color.Violet
                }
            );
            _inventory.Cursor.Move(1, 1).Print("Inventory");

            FillBackground(_logs,
                new[]{
                    Color.Black,
                    Color.DarkGray,
                    Color.LightGray,
                    Color.White
                }
            );
            _logs.Cursor.Move(1, 1).Print("Logs");

            Children.Add(_stats);
            Children.Add(_map);
            Children.Add(_inventory);
            Children.Add(_logs);
        }

        private void FillBackground(ScreenSurface screenSurface, Color[] colors)
        {
            var colorStops = new[] { 0f, 0.35f, 0.75f, 1f };
            var gradient = new Gradient(colors, colorStops);

            Algorithms.GradientFill(
                screenSurface.FontSize,             // cell size
                screenSurface.Surface.Area.Center,  // midpoint
                screenSurface.Surface.Width / 3,    // spread witdth
                33,                                 // angle degrees
                screenSurface.Surface.Area,         // coverage area
                gradient,                           // gradient colours
                (x, y, color) => {                  // callback for each point
                    screenSurface.Surface[x, y].Background = color;
                }
            );
        }
    }
}