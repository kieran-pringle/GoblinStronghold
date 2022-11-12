using System;
using System.Diagnostics;
using GoblinStronghold.Debug;
using GoblinStronghold.ECS;
using GoblinStronghold.Graphics;
using GoblinStronghold.Graphics.Messages;
using GoblinStronghold.Graphics.Util;
using GoblinStronghold.Graphics.Systems;
using GoblinStronghold.Messaging;
using GoblinStronghold.Screen;
using GoblinStronghold.Time.Systems;
using GoblinStronghold.Time.Messages;
using SadConsole;

namespace GoblinStronghold
{
    public static class GameManager
    {
        public static Random Random = new Random();
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
            Game.Instance.FrameRender += GameManager.RenderGame;
        }

        private static void LoadGame()
        {
            TestBench.Load(Context, Screen);

            // later we can have the other constant class inits
        }

        private static void UpdateGame(object sender, GameHost args)
        {
            // message about time passed
            Context.Send(new UpdateTimePassed(args.UpdateFrameDelta));

            // get inputs


            // emit events on start of turn


            // process events
        }

        private static void RenderGame(object sender, GameHost args)
        {
            // draw results
            Context.Send(Render.Instance);
        }
    }
}
