using System;
namespace GoblinStronghold.Time.Messages
{
    // empty message used to tell the rendering system to go off and do its thing
    public readonly struct RenderTimePassed
    {
        public readonly TimeSpan RenderFrameDelta;

        public RenderTimePassed(TimeSpan renderFrameDelta)
        {
            RenderFrameDelta = renderFrameDelta;
        }
    }
}

