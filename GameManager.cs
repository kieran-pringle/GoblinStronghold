using System;
using System.Diagnostics;
using GoblinStronghold.Graphics;
using GoblinStronghold.Input;
using GoblinStronghold.Maps;
using GoblinStronghold.Messaging;
using GoblinStronghold.Messaging.Messages;
using GoblinStronghold.Screen;
using GoblinStronghold.Utils;
using SadConsole;

namespace GoblinStronghold
{
    public static class GameManager
    {
        public static readonly int FrameRate = 4;
        public static readonly int FrameLengthMs = 1000 / FrameRate; 
        private static readonly Stopwatch _rateLimit = new Stopwatch();

        private static readonly KeyboardInputBuffer _keyboardInputBuffer
            = new KeyboardInputBuffer(frameDelay: 3, frameLength: FrameLengthMs);
        public static readonly MesssageBus MessageBus
            = new MesssageBus();
        public static Random Random
            = new Random();

        public static Map Map;
        public static RootScreen Screen;

        public static void Init()
        {
            LoadTileset();
            CreateGameObjects();
            AttachUpdateHandlers();
            CreateUI();
        }


        private static void LoadTileset()
        {
            TileSet.Load(Game.Instance.DefaultFont);
        }

        private static void CreateGameObjects()
        {
            Map = new Map(16, 16);
        }

        private static void AttachUpdateHandlers()
        {
            Game.Instance.FrameUpdate += GameManager.BufferKeyboardInput;
            Game.Instance.FrameUpdate += GameManager.UpdateGame;
        }

        private static void CreateUI()
        {
            Screen = new RootScreen(Map);

            Game.Instance.Screen = GameManager.Screen;
            Game.Instance.Screen.IsFocused = true;

            Game.Instance.DestroyDefaultStartingConsole();

            _rateLimit.Start();
        }


        private static void UpdateGame(object sender, GameHost args)
        {
            // limit our simulation and rendering
            if (IsFramePassed())
            {
                // Pass on keyboard input
                MessageBus.Send<KeyEventsMessage>(KeyEventsMessage.From(_keyboardInputBuffer));
                // AI act?
                // MessageBus.Send<TakeActions>(TakeActions.Tick);
                // update the sim
                MessageBus.Send<UpdateTickMessage>(UpdateTickMessage.Tick);
                // Draw result
                MessageBus.Send<RenderTickMessage>(RenderTickMessage.Tick);
            }
        }

        private static void BufferKeyboardInput(object sender, GameHost args)
        {
            _keyboardInputBuffer.Add(
                args.GetKeyboardState().GetPressedKeys(),
                args.DrawFrameDelta);
        }

    private static bool IsFramePassed()
        {
            if(_rateLimit.ElapsedMilliseconds > 1000 / FrameRate)
            {
                _rateLimit.Restart();
                return true;
            }
            return false;
        }
    }
}

