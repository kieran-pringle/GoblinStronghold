using System;
using System.Diagnostics;
using GoblinStronghold.Debug;
using GoblinStronghold.ECS;
using GoblinStronghold.Graphics;
using GoblinStronghold.Graphics.Messages;
using GoblinStronghold.Graphics.Util;
using GoblinStronghold.Messaging;
using GoblinStronghold.Screen;
using SadConsole;

namespace GoblinStronghold
{
    public static class GameManager
    {

        public static Random Random
            = new Random();
        public static readonly IContext Context = new Context();

        public static RootScreen Screen;

        public static void Init()
        {
            LoadTileset();
            CreateScreen();
            AttachUpdateHandlers();
            LoadGame();
        }

        private static void LoadTileset()
        {
            TileSet.Load(Game.Instance.DefaultFont);
        }


        private static void CreateScreen()
        {
            Screen = new RootScreen();

            Game.Instance.Screen = GameManager.Screen;
            Game.Instance.Screen.IsFocused = true;

            Game.Instance.DestroyDefaultStartingConsole();
        }

        private static void AttachUpdateHandlers()
        {
            Game.Instance.FrameUpdate += GameManager.UpdateGame;
        }

        private static void LoadGame()
        {
            TestBench.Load(Context, Screen);
        }

        private static void UpdateGame(object sender, GameHost args)
        {
            // message about time passed


            // get inputs


            // emit events on start of turn


            // process events


            // draw results
            Context.Send(Render.Instance);
        }
    }
}
