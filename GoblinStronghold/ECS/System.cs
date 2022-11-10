using System;
using GoblinStronghold.Messaging;

namespace GoblinStronghold.ECS
{
    public abstract class System<T> : ISubscriber<T>
    {
        internal IContext _context;

        public abstract void Handle(T message);

        protected IContext Context()
        {
            return _context;
        }
    }
}

