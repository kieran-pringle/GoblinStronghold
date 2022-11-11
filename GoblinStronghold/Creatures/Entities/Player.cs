using System;
using GoblinStronghold.Creatures.Components;
using GoblinStronghold.ECS;
using GoblinStronghold.Graphics.Components;
using GoblinStronghold.Graphics.Util;

namespace GoblinStronghold.Creatures.Entities
{
    public class Player
    {
        private static string[] _frames = new[]{
            "player-basic-1",
            "player-basic-2"
        };

        public static Entity NewIn(IContext context)
        {
            var glyph = new AnimatedGlyph(_frames,
                Palette.WhiteBright,
                Palette.Black
            );
            context.Register(glyph);

            return context
                .CreateEntity()
                .With(Creature.Instance)
                .With(new HasGlyph(glyph));
        }
    }
}
