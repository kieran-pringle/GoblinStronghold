using System;
using System.Collections.Generic;
using SadConsole;
using GoblinStronghold.ECS;
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
        private List<Camera> _children = new List<Camera>();

        protected ICellSurface _surface;

        public CameraSystem(ICellSurface surface)
        {
            _children.Add(new TileCamera(surface));
            _children.Add(new CreatureCamera(surface));
        }

        public virtual void Render()
        {
            foreach (var camera in _children)
            {
                camera.Render(Context());
            }
        }

        public override void Handle(Render message)
        {
            this.Render();
        }
    }
}
