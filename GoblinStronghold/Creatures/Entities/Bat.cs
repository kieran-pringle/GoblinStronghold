using GoblinStronghold.Creatures.Components;
using GoblinStronghold.ECS;
using GoblinStronghold.Graphics.Components;
using GoblinStronghold.Graphics.Util;
using GoblinStronghold.Physics.Components;

namespace GoblinStronghold.Creatures.Entities
{
	public static class Bat
	{
        private static string[] _frames = new[]{
            "bat-1",
            "bat-2"
        };

        public static Entity NewIn(IContext context)
        {
            var glyph = new AnimatedGlyph(_frames,
                Palette.WhiteBright,
                Palette.Black
            );

            // somehow attach AI here
            // var control

            return context
                .CreateEntity()
                .With(Creature.Instance)
                .With(new HasGlyph(glyph))
                .With(new Collision(Impassable.Instance));
        }
    }
}

