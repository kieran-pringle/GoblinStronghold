using System;
using GoblinStronghold.ECS;
using GoblinStronghold.Graphics.Components;
using GoblinStronghold.Graphics.Util;
using GoblinStronghold.Maps.Components;

namespace GoblinStronghold.Maps.Entities
{
    public class Wall
    {
        private static string[] _glyphPool = new[]
                {
            "wall-rock-shaded-1",
            "wall-rock-shaded-2",
            "wall-rock-shaded-3",
            "wall-rock-shaded-4",
            "wall-rock-shaded-5",
            "wall-rock-shaded-6",
            "wall-rock-shaded-7",
            "wall-rock-shaded-8",
        };

        public static Entity NewIn(IContext context)
        {
            var glyph = new RandomGlyph(_glyphPool);
            glyph.Background = Palette.BlackBright;
            glyph.Foreground = Palette.White;

            return context
                .CreateEntity()
                .With(Tile.Instance)
                .With(new HasGlyph(glyph));
        }
    }
}

