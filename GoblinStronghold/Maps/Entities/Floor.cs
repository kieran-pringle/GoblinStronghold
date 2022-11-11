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
            "wall-rock-flat-1",
            "wall-rock-flat-2",
            "wall-rock-flat-3",
            "wall-rock-flat-4",
            "wall-rock-flat-5",
            "wall-rock-flat-6",
            "wall-rock-flat-7",
            "wall-rock-flat-8",
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
