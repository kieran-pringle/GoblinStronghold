using System;
using SadConsole;
using GoblinStronghold.ECS;

namespace GoblinStronghold.Graphics.Components
{
    public interface IGlyphProvider
    {
        public ColoredGlyph Glyph();

        public void OnRegister(Entity e)
        {
            // do nothing by default
        }
    }
}

