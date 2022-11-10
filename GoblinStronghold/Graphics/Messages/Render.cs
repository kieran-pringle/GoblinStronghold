using System;
namespace GoblinStronghold.Graphics.Messages
{
    // empty message used to tell the rendering system to go off and do its thing
    public class Render
    {
        public static Render Instance = new Render();

        public Render()
        {
            // empty message
        }
    }
}

