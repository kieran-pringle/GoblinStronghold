using System;
using GoblinStronghold.ECS;

namespace GoblinStronghold.Graphics.Systems
{
    // Get away with using generics on Camera
    public interface ICamera
    {
        // pass the context to look for things to render in
        public void Render(IContext context);
    }
}

