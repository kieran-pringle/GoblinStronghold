using System;
using SadConsole;
using GoblinStronghold.ECS;
using GoblinStronghold.Graphics.Components;
using GoblinStronghold.Physics.Components;

namespace GoblinStronghold.Graphics.Systems
{
    // T is the type that will be looked for to iterate over
    public class Camera<T> : ICamera
    {
        private int _width;
        private int _height;
        private ICellSurface _surface;

        public Camera(ICellSurface surface)
        {
            _surface = surface;
            _width = surface.Width;
            _width = surface.Height;
        }

        public void Render(IContext context)
        {
            foreach (var tile in context.AllEntitiesWith<T>())
            {
                tile.Component<Position>().ForEach(p =>
                {
                    // TODO: check for position in camera bounds

                    tile.Component<HasGlyph>().ForEach(g =>
                    {
                        // if we have both components needed
                        Position position = p.Content;
                        HasGlyph hasGlyh = g.Content;
                        _surface.SetCellAppearance(
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
