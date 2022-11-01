using System;
namespace GoblinStronghold.Messaging
{
    public interface ISubscriber<in Message>
    {
        void Handle(Message message);
    }
}

