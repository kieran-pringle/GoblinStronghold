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
            Game.Create(baseRes * 16, baseRes * 9, "res/fonts/1-bit_16x16/1-bit_16x16.font");

            // Hook the start event so we can add consoles to the system.
            Game.Instance.OnStart = Init;

            // Start the game.
            Game.Instance.Run();
            Game.Instance.Dispose();
        }

        static void Init()
        {
            GameManager.Init();
        }
    }
}