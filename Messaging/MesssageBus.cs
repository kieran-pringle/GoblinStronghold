using System;
using System.Linq;
using System.Collections.Generic;

namespace GoblinStronghold.Messaging
{
    public class MesssageBus
    {
        private readonly Dictionary<
                Type,
                Dictionary<
                    object,
                    Action<object>>>
            _subscribers;

        public MesssageBus()
        {
            _subscribers = new Dictionary<Type, Dictionary<object, Action<object>>>();
        }

        public void Send<MessageType>(MessageType message)
        {
            var t = typeof(MessageType);
            if (_subscribers.ContainsKey(t))
            {
                foreach (KeyValuePair<object, Action<object>> item in _subscribers[t])
                {
                    item.Value(message);
                }
            }
        }

        public void Register<MessageType>(ISubscriber<MessageType> subscriber)
        {
            var t = typeof(MessageType);

            // if no list of subscribers, add one
            if (!_subscribers.ContainsKey(t))
            {
                _subscribers.Add(t, new Dictionary<object, Action<object>>());
            }

            _subscribers[t].Add(
                subscriber,
                (msg) => subscriber.Handle((MessageType)msg)
            );
        }

        // deregister, and destroy queue
        public void UnRegister<MessageType>(ISubscriber<MessageType> subscriber)
        {
            var t = typeof(MessageType);
            if(_subscribers.ContainsKey(t))
            {
                _subscribers[t].Remove(subscriber);
            }
            // remove if this was the last handler
            if (_subscribers[t].Count < 1)
            {
                _subscribers.Remove(t);
            }
        }
    }
}

