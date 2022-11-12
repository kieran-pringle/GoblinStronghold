using System;
using GoblinStronghold.ECS;
using GoblinStronghold.Graphics.Components;
using GoblinStronghold.Graphics.Util;
using GoblinStronghold.Maps.Components;
using SadRogue.Primitives;
using Palette = GoblinStronghold.Graphics.Util.Palette;

namespace GoblinStronghold.Maps.Entities
{
    public static class Floor
    {
        private static string[] _glyphPool = new[]
        {
            "floor-dirt-1",
            "floor-dirt-2",
            "floor-dirt-3",
            "floor-dirt-4",
            "floor-dirt-5",
            "floor-dirt-6",
            "floor-dirt-7",
            "floor-dirt-8",
        };

        public static Entity NewIn(IContext context)
        {
            var glyph = new RandomGlyph(_glyphPool);
            glyph.Background = Palette.Black;
            glyph.Foreground = Palette.BlackBright;

            return context
                .CreateEntity()
                .With(Tile.Instance)
                .With(new HasGlyph(glyph));
        }
    }
}
