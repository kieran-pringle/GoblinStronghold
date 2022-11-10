using System;
using SadConsole;
using GoblinStronghold.ECS;

namespace GoblinStronghold.Graphics.Systems
{
    public class CreatureCamera : Camera
    {

        public CreatureCamera(ICellSurface surface) : base(surface)
        {
        }

        public override void Render(IContext context)
        {
            // do nothing for now, we only have tiles to render
        }
    }
}
