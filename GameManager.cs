using System;
using GoblinStronghold.Maps;
using GoblinStronghold.Messaging;
using GoblinStronghold.Screen;

namespace GoblinStronghold
{
    public class GameManager
    {
        private readonly MesssageBus _messsageBus = new MesssageBus();
        private Map _map;
        public readonly RootScreen Screen;

        public GameManager()
        {
            _map = new Map(20, 20);
            Screen = new RootScreen(_map);
        }
    }
}

