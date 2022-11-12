using System;
using System.Diagnostics;
using System.Linq;
using SadConsole;
using SadRogue.Primitives;
using GoblinStronghold.ECS;
using GoblinStronghold.Graphics.Util;
using GoblinStronghold.Graphics.Messages;

namespace GoblinStronghold.Graphics.Components
{
    public class AnimatedGlyph : IGlyphProvider
    {
        private ColoredGlyph[] _frames;
        private int _frame = 0;

        private Stopwatch _stopwatch = new Stopwatch();

        public AnimatedGlyph(string[] frames, Color foreground, Color background)
        {
            _stopwatch.Start();
            _frames = frames.Select(s => TileSet.ColoredGlyph(s, foreground, background))
                .ToArray();
        }

        public ColoredGlyph Glyph()
        {
            return _frames[_frame];
        }

        public void IncrementFrame()
        {
            _frame = (_frame + 1) % _frames.Length;
        }

        // register self for receiving animation framerate updates
        void IGlyphProvider.OnRegister(Entity e)
        {
            e.With(this);
        }
    }
}
