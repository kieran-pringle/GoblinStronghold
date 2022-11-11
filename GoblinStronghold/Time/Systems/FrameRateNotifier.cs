using System;
using GoblinStronghold.ECS;
using GoblinStronghold.Time.Messages;
using GoblinStronghold.Messaging;
using GoblinStronghold.Graphics.Messages;

namespace GoblinStronghold.Time.Systems
{
    public class FrameRateNotifier : System<UpdateTimePassed>
    {
        private int _framesPerSecond;
        private TimeSpan _fpsMillis;
        private TimeSpan _currentSpan;

        public FrameRateNotifier(int framesPerSecond)
        {
            _framesPerSecond = framesPerSecond;
            _fpsMillis = TimeSpan.FromMilliseconds((double)1000 / (double)framesPerSecond);
            _currentSpan = TimeSpan.Zero;
        }

        public override void Handle(UpdateTimePassed message)
        {
            _currentSpan = _currentSpan.Add(message.UpdateFrameDelta);
            if (_currentSpan > _fpsMillis)
            {
                _currentSpan = TimeSpan.Zero;
                Context().Send(new RenderFrame(_framesPerSecond));
            }
        }
    }
}
