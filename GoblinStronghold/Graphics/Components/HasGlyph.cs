using System;
using SadConsole;
using GoblinStronghold.ECS;

namespace GoblinStronghold.Graphics.Components
{
    /**
    *   <summary>
    *       A component to give us a way to fetch everything that we might be
    *       able to drow to the screen as we can't get subclasses from the
    *       ECS. Delegates call to its internal <c>GlyphProvider</c>. 
    *   </summary>
    */
    public class HasGlyph : IOnComponentRegister
    {
        private IGlyphProvider _glyphProvider;

        public HasGlyph(IGlyphProvider provider)
        {
            _glyphProvider = provider;
        }

        public ColoredGlyph Glyph()
        {
            return _glyphProvider.Glyph();
        }

        public void OnRegisterTo(Entity entity)
        {
            _glyphProvider.OnRegister(entity);
        }
    }
}
