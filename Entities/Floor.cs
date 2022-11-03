using System;
using GoblinStronghold.Components;
using GoblinStronghold.Graphics;
using GoblinStronghold.Maps;
using SadConsole;

namespace GoblinStronghold.Entities
{
    public class Floor : Entity
    {
        public Floor()
        {
            AddComponent(new RandomisedGlyph(
                    new string[]
                    {
                        "blank",
                        "floor-dithered-1",
                        "floor-dithered-2",
                        "floor-dithered-3"
                    }
                )
            );
        }
    }
}

