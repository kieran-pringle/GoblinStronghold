using System;
using System.Linq;
using System.Collections.Generic;
using GoblinStronghold.Graphics;
using SadConsole;

namespace GoblinStronghold.Components
{
    // Represents something that can be drawn to screen
    abstract public class GlyphComponent : IComponent
    {
        // Return the glyph to draw when drawing this
        abstract public ColoredGlyph Glyph();
    }

    public class FixedGlyph : GlyphComponent
    {
        private ColoredGlyph _appearance;

        public FixedGlyph(string glyphName)
        {
            _appearance = TileSet.ColoredGlyph(glyphName);
        }

        public override ColoredGlyph Glyph()
        {
            return _appearance;
        }
    }

    public class RandomisedGlyph : GlyphComponent
    {
        List<ColoredGlyph> _appearancePool;
        ColoredGlyph _fixedApperance;

        public RandomisedGlyph(string[] appearancePool)
        {
            _appearancePool = appearancePool.Select(s => TileSet.ColoredGlyph(s)).ToList();
        }

        public override ColoredGlyph Glyph()
        {
            if (_fixedApperance == null)
            {
                int i = GameManager.Random.Next(0, _appearancePool.Count - 1);
                _fixedApperance = _appearancePool[i];
            }

            return _fixedApperance;
        }
    }

    public class AnimatedGlyph : GlyphComponent
    {
        private List<ColoredGlyph> _frames;
        private int _currentFrame;

        public AnimatedGlyph(string[] frames)
        {
            _frames = frames.Select(s => TileSet.ColoredGlyph(s)).ToList();
            _currentFrame = GameManager.Random.Next(0, _frames.Count - 1);
        }

        public override ColoredGlyph Glyph()
        {
            _currentFrame = (_currentFrame + 1) % (_frames.Count);
            return _frames[_currentFrame];
        }
    }
}
