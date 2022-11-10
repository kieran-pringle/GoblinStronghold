using System;
using GoblinStronghold.ECS;
using GoblinStronghold.Graphics.Components;
using GoblinStronghold.Maps.Components;

namespace GoblinStronghold.Maps.Entities
{
    public static class Floor
    {
        public static Entity NewIn(IContext context)
        {
            return context
                .CreateEntity()
                .With(new Tile())
                .With(new HasGlyph(
                    new FixedGlyph("floor-basic"))
                );
        }
    }
}

