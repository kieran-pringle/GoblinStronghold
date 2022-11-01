using System;
using GoblinStronghold.Maps;
using GoblinStronghold.Messaging;
using GoblinStronghold.Screen;
using SadConsole;

namespace GoblinStronghold
{
    public static class GameManager
    {
        public static readonly MesssageBus MessageBus = new MesssageBus();

        public static Map Map;
        public static RootScreen Screen;

        public static void Init()
        {
            Map = new Map(16, 16);
            Screen = new RootScreen(Map);
        }

        public static void Update(object sender, GameHost args)
        {
            // we can maybe have more than one message bus?
            // and give each one a prod

            // first: accept input, if we are accepting
            // then: act
            // then: sim results
            // then: draw

            // maybe check a minimum time has passed?
        }
    }
}

