using System;
using System.Linq;
using SadConsole;
using SadRogue.Primitives;
using System.Collections.Generic;
using GoblinStronghold.Maps;
using GoblinStronghold.Messaging;
using GoblinStronghold.Messaging.Messages;
using GoblinStronghold.Entities;
using GoblinStronghold.Components;
using GoblinStronghold.Utils;

namespace GoblinStronghold.Graphics
{
    // shows part of a map
    public class Camera : ISubscriber<RenderTickMessage>
    {
        private readonly ScreenSurface _surface;
        private readonly Map _map;
        // private Point Position;  // centre point, redraw on set

        public Camera(ScreenSurface surface, Map map)
        {
            _surface = surface;
            _map = map;
            // default centre to player position somehow (pass arg)

            // register handler for rendering
            GameManager.MessageBus.Register<RenderTickMessage>(this);
        }

        public void Handle(RenderTickMessage _)
        {
            Render();
        }

        public void Render()
        {
            // TODO: Cut a slice of map according to position of camera with point set
            // TODO: Only rerender the bare minimum
            foreach (var item in _map)
            {
                Point point = item.Key;
                Cell cell = item.Value;
                IList<GlyphComponent> drawableComponents = cell
                    .AllComponentsHere<GlyphComponent>(typeof(GlyphComponent));

                if (drawableComponents.Count() == 0)
                {
                    continue;
                }
                else
                {
                    // top element, assuming it is the most appropriate
                    CopyGlyphTo(drawableComponents[^1], point);
                }
            }

            _surface.IsDirty = true;
        }

        private void CopyGlyphTo(GlyphComponent hasGlyph, Point point)
        {
            _surface.Surface[point.X, point.Y]
                .CopyAppearanceFrom(hasGlyph.Glyph());
        }
    }
}

