using System;
using GoblinStronghold.Components;

namespace GoblinStronghold.Entities
{
    public class Bat : Entity
    {
        public Bat()
        {
            AddComponent(new AnimatedGlyph(
                    new string[]
                    {
                        "bat-1",
                        "bat-2",
                        "bat-1",
                        "bat-3"
                    }
                ));
            AddComponent(CollisionComponent.Impassable);
        }
    }
}

