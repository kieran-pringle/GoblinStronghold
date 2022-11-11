using System;
namespace GoblinStronghold.Graphics.Messages
{
    /**
     * Represents the start of a new frame at some fixed FPS
     */
    public struct RenderFrame
    {
        public int FPS;

        public RenderFrame(int fps)
        {
            FPS = fps;
        }
    }
}

