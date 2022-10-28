using SadConsole;
using SadConsole.Input;
using SadRogue.Primitives;
using System.Collections.Generic;

namespace SadConsoleGame
{
    public class RootScreen : ScreenObject
    {
        private ScreenSurface _map;
        private GameObject _player;

        public RootScreen()
        {
            _map = new ScreenSurface(
                Game.Instance.ScreenCellsX,
                Game.Instance.ScreenCellsY - 5);
            _map.UseMouse = false;

            Children.Add(_map);
            FillBackground(_map);

            _player = new GameObject(
                new ColoredGlyph(Color.AnsiWhiteBright, Color.AnsiBlack, 1),
                new Point(12, 12),
                _map);
        }

        private void FillBackground(ScreenSurface screenSurface)
        {
            var colors = new[] {
                Color.LightGreen,
                Color.SeaGreen,
                Color.CornflowerBlue,
                Color.DarkSlateBlue
            };
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