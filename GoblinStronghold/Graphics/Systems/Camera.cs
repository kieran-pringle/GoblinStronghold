using System;
using SadConsole;
using GoblinStronghold.ECS;

namespace GoblinStronghold.Graphics.Systems
{
    public abstract class Camera
    {
        protected int Width;
        protected int Height;

        protected ICellSurface Surface;

        public Camera(ICellSurface surface)
        {
            Surface = surface;
            Width = surface.Width;
            Height = surface.Height;
        }

        public abstract void Render(IContext context);
    }
}
