using System;
using System.Collections.Generic;
using SadConsole;
using GoblinStronghold.Creatures.Components;
using GoblinStronghold.ECS;
using GoblinStronghold.Maps.Components;
using GoblinStronghold.Messaging;
using GoblinStronghold.Graphics.Messages;

namespace GoblinStronghold.Graphics.Systems
{
    /**
     * Main way of rendering a scene to the screen
     */ 
    public class CameraSystem : System<Render>
    {

        // child cameras to iterate over to control draw order.
        // first in first called
        private List<ICamera> _children = new List<ICamera>();

        protected ICellSurface _surface;

        public CameraSystem(ICellSurface surface)
        {
            _surface = surface;
            _children.Add(new Camera<Tile>(surface));
            _children.Add(new Camera<Creature>(surface));
        }

        public virtual void Render()
        {
            foreach (var camera in _children)
            {
                camera.Render(Context());
            }

            // TODO: set this by the cameras if they update anything
            _surface.IsDirty = true;
        }

        public override void Handle(Render message)
        {
            this.Render();
        }
    }
}
