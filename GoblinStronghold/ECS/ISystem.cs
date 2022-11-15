using GoblinStronghold.ECS.Messaging;

namespace GoblinStronghold.ECS
{
    public interface ISystem<T> : ISubscriber<T>
    {
        public IContext Context { get; set; }
    }
}
