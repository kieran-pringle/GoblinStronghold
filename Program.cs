using System;
using SadConsole;
using SadRogue.Primitives;
using GoblinStronghold.Graphics;
using Console = SadConsole.Console;

namespace GoblinStronghold
{
    public static class Program
    {

        static void Main()
        {
            var baseRes = 6;
            // Setup the engine and create the main window.
            // compensate for 8*16 default font
            Game.Create(baseRes * 16, baseRes * 9, "res/1-bit_16x16.font");

            // Hook the start event so we can add consoles to the system.
            Game.Instance.OnStart = Init;

            // Start the game.
            Game.Instance.Run();
            Game.Instance.Dispose();
        }

        static void Init()
        {
            TileSet.Load(Game.Instance.DefaultFont);
            GameManager.Init();

            Game.Instance.Screen = GameManager.Screen;
            Game.Instance.Screen.IsFocused = true;

            Game.Instance.FrameUpdate += GameManager.Update;

            // needed because we have replaced the original screen object
            Game.Instance.DestroyDefaultStartingConsole();
        }
    }
}