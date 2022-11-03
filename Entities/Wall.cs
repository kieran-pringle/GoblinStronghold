using System;
using GoblinStronghold.Graphics;
using GoblinStronghold.Maps;
using GoblinStronghold.Components;
using SadConsole;

namespace GoblinStronghold.Entities
{
    public class Wall : Entity
    {
        public Wall()
        {
            AddComponent(CollisionComponent.Impassable);
            AddComponent(new RandomisedGlyph(
                    new string[] {
                        "brick-wall-1",
                        "brick-wall-2",
                        "brick-wall-3",
                        "brick-wall-4",
                        "brick-wall-5",
                        "brick-wall-6",
                        "brick-wall-7"
                    }
                )
            );
        }
    }
}

