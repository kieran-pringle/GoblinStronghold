using System;
using SadConsole;

namespace GoblinStronghold.ECS.Components.Graphics
{
    /**
    *   <summary>
    *       A component to give us a way to fetch everything that we might be
    *       able to drow to the screen as we can't get subclasses from the
    *       ECS. Delegates call to its internal <c>GlyphProvider</c>. 
    *   </summary>
    */
    public class HasGlyph
    {
        public IGlyphProvider GlyphProvider;

        public ColoredGlyph Glyph()
        {
            return GlyphProvider.Glyph();
        }
    }
}

