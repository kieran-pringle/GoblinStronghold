using System;
namespace GoblinStronghold.Time.Messages
{
    /**
     *  Message for describing how much time has passed since the last game 
     *  loop update call
     */
    public struct UpdateTimePassed
    {
        public TimeSpan UpdateFrameDelta;

        public UpdateTimePassed(TimeSpan updateFrameDelta)
        {
            UpdateFrameDelta = updateFrameDelta;
        }
    }
}

