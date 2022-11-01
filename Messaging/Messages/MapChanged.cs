using GoblinStronghold.Maps;
using System;

namespace GoblinStronghold.Messaging.Messages
{
    public struct MapChanged
    {
        public Map Map;

        public MapChanged(Map map)
        {
            Map = map;
        }
    }
}

