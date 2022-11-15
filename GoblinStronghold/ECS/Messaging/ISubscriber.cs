using System;
namespace GoblinStronghold.ECS.Messaging
{
    public interface ISubscriber<in Message>
    {
        void Handle(Message message);
    }
}

