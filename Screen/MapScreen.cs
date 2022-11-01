using System;
using GoblinStronghold.Graphics;
using GoblinStronghold.Graphics.Drawer;
using GoblinStronghold.Maps;
using SadConsole;
using SadRogue.Primitives;
using Console = SadConsole.Console;

namespace GoblinStronghold.Screen
{
    public class MapScreen : SubScreen
    {
        private readonly Map _map;
        private readonly Camera _camera;

        public MapScreen(int width, int height) : base(width, height)
        {
            _map = new Map(3, 3);
            _camera = new Camera(this.SubConsole, _map);
            _camera.Draw();
        }
    }
}

