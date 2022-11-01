using System;
using SadConsole;
using SadRogue.Primitives;
using System.Collections.Generic;
using GoblinStronghold.Maps;
using GoblinStronghold.Messaging;
using GoblinStronghold.Messaging.Messages;
using GoblinStronghold.Entities;


namespace GoblinStronghold.Graphics
{
    // shows part of a map
    public class Camera : ISubscriber<MapChanged>
    {
        private readonly ScreenSurface _surface;
        private readonly Map _map;
        // private Point Position;  // centre point, redraw on set

        public Camera(ScreenSurface surface, Map map)
        {
            _surface = surface;
            _map = map;
            // default centre to player position somehow (pass arg)

            // register handler
            GameManager.MessageBus.Register(this);
        }

        public void Draw()
        {
            // TODO: Cut a slice of map according to position of camera with point set
            foreach (var item in _map)
            {
                Point point = item.Key;
                Cell cell = item.Value;
                Entity[] entities = cell.Entities();

                if (entities.Length == 0)
                {
                    CopyGlyphTo(cell, point);
                    
                }
                else
                {
                    // top element, assuming it is the most appropriate
                    CopyGlyphTo(entities[^1], point);
                }
            }

            _surface.IsDirty = true;
        }

        public void Handle(MapChanged message)
        {
            if (message.Map.Equals(_map))
                Draw();
        }

        private void CopyGlyphTo(IHasAppearance drawable, Point point)
        {
            _surface.Surface[point.X, point.Y]
                .CopyAppearanceFrom(drawable.Appearance());
        }
    }
}

