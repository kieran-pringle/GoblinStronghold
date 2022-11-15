using GoblinStronghold.Creatures.Components;
using GoblinStronghold.ECS;
using GoblinStronghold.Input.Components;
using GoblinStronghold.Graphics.Components;
using GoblinStronghold.Graphics.Util;
using GoblinStronghold.Physics.Components;

namespace GoblinStronghold.Creatures.Entities
{
    public static class Player
    {
        private static string[] _frames = new[]{
            "gunslinger-1",
            "gunslinger-2"
        };

        public static Entity NewIn(IContext context)
        {
            var glyph = new AnimatedGlyph(_frames,
                Palette.WhiteBright,
                Palette.Black
            );

            var control = new PlayerControlledCreature();

            return context
                .CreateEntity()
                .With(Creature.Instance)
                .With(new HasGlyph(glyph))
                .With(new Collision(Impassable.Instance))
                .With(control);
        }
    }
}
