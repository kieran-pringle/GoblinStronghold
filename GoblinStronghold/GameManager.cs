﻿using System;
using GoblinStronghold.Debug;
using GoblinStronghold.ECS;
using GoblinStronghold.Log.Util;
using GoblinStronghold.Graphics.Messages;
using GoblinStronghold.Graphics.Util;
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
            LoadTilesets();
            CreateScreen();
            AttachUpdateHandlers();
            LoadGame();
        }

        private static void LoadTilesets()
        {
            TileSet.Load(Game.Instance.DefaultFont);
            LogFont.Load(SadConsole.Game.Instance.LoadFont(
                    "res/fonts/ascii_8x16/ascii_8x16.font"));
        }

        private static void CreateScreen()
        {
            Screen = new RootScreen(Context);

            Game.Instance.Screen = Screen;
            Game.Instance.Screen.IsFocused = true;

            Game.Instance.DestroyDefaultStartingConsole();
        }

        private static void AttachUpdateHandlers()
        {
            Game.Instance.FrameUpdate += UpdateGame;
            Game.Instance.FrameRender += RenderGame;
        }

        private static void LoadGame()
        {
            // something that calls the constant class inits and populates a "map"
            LoggingBench.Load(Context, Screen);
        }

        // TODO: can I have systems register their own messages they want at
        // these cycles? try running everything off `UpdateTimePassed` until
        // clashes. Start and end of a turn should kick off synchronous stuff
        private static void UpdateGame(object sender, GameHost args)
        {
            // take turns and sim
            Context.Send(new UpdateTimePassed(args.UpdateFrameDelta));
        }

        private static void RenderGame(object sender, GameHost args)
        {
            // draw results
            Context.Send(new RenderTimePassed(args.DrawFrameDelta));

            SadConsole.Console logScreen = Screen.LogConsole();
        }
    }
}
