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
            var baseRes = 7;
            // Setup the engine and create the main window.
            Game.Create(baseRes * 16, baseRes * 9, "res/font/1-bit_16x16.font");

            // Hook the start event so we can add consoles to the system.
            Game.Instance.OnStart = Init;

            // Start the game.
            Game.Instance.Run();
            Game.Instance.Dispose();
        }

        static void Init()
        {
            GameManager.Init();

            Game.Instance.Screen = GameManager.Screen;
            Game.Instance.Screen.IsFocused = true;

            Game.Instance.FrameUpdate += GameManager.Update;

            // needed because we have replaced the original screen object
            Game.Instance.DestroyDefaultStartingConsole();
        }
    }
}