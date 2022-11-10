using System;
using SadConsole;
using GoblinStronghold.ECS;
using GoblinStronghold.Physics.Components;
using GoblinStronghold.Graphics.Components;
using GoblinStronghold.Maps.Components;

namespace GoblinStronghold.Graphics.Systems
{
    public class TileCamera : Camera
    {
        public TileCamera(ICellSurface surface) : base(surface)
        {
        }

        public override void Render(IContext context)
        {
            foreach (var tile in context.AllEntitiesWith<Tile>())
            {
                tile.Component<Position>().ForEach(p =>
                {
                    // TODO: check for position in camera bounds

                    tile.Component<HasGlyph>().ForEach(g =>
                    {
                        // if we have both components needed
                        Position position = p.Content;
                        HasGlyph hasGlyh = g.Content;
                        Surface.SetCellAppearance(
                                position.X,
                                position.Y,
                                hasGlyh.Glyph()
                            );
                    });
                });
            }
        }
    }
}
