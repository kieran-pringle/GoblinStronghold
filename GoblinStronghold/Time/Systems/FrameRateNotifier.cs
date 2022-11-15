using System;
using GoblinStronghold.ECS;
using GoblinStronghold.Time.Messages;
using GoblinStronghold.Graphics.Messages;

namespace GoblinStronghold.Time.Systems
{
    public class FrameRateNotifier : ISystem<RenderTimePassed>
    {
        private int _framesPerSecond;
        private TimeSpan _fpsMillis;
        private TimeSpan _currentSpan;

        private IContext _context;
        IContext ISystem<RenderTimePassed>.Context { get => _context; set => _context = value; }

        public FrameRateNotifier(int framesPerSecond)
        {
            _framesPerSecond = framesPerSecond;
            _fpsMillis = TimeSpan.FromMilliseconds(1000 / (double)framesPerSecond);
            _currentSpan = TimeSpan.Zero;
        }

        public void Handle(RenderTimePassed message)
        {
            _currentSpan = _currentSpan.Add(message.RenderFrameDelta);
            if (_currentSpan > _fpsMillis)
            {
                _currentSpan = TimeSpan.Zero;
                _context.Send(new RenderFrame(_framesPerSecond));
            }
        }
    }
}
