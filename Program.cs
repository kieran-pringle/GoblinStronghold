using System;
using SadConsole;
using SadRogue.Primitives;
using Console = SadConsole.Console;

namespace sadconsoletut
{
    public static class Program
    {

        static void Main()
        {
            var baseRes = 6;
            // Setup the engine and create the main window.
            // compensate for 8*16 default font
            Game.Create(baseRes * 16, baseRes * 9, "res/1-bit.font");

            // Hook the start event so we can add consoles to the system.
            Game.Instance.OnStart = Init;

            // Start the game.
            Game.Instance.Run();
            Game.Instance.Dispose();
        }

        static void Init()
        {
            Game.Instance.Screen = new RootScreen();
            Game.Instance.Screen.IsFocused = true;

            // needed because we have replaced the original screen object
            Game.Instance.DestroyDefaultStartingConsole();
        }
    }
}