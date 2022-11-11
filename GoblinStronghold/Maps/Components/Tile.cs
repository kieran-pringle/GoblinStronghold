using System;
namespace GoblinStronghold.Maps.Components
{
    // Empty component marking an entity as a tile
    public class Tile
    {
        public static Tile Instance = new Tile();

        private Tile()
        {
        }
    }
}

