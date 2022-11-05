using System;
using Range = System.Range;
using GoblinStronghold.Messaging;

namespace GoblinStrongholdTests.Messaging
{
    [TestFixture]
    public class MessagingBusTest
    {
        private MessageBus _bus;
        private int message = 0;

        [SetUp]
        public void SetUp()
        {
            _bus = new MessageBus();
        }

        [Test]
        public void MessageBusTest_AllRegisteredSubscribersShouldReceiveMessage()
        {
            // create three mocked subscribers and register them
            List<Mock<ISubscriber<int>>> mocks = new List<Mock<ISubscriber<int>>>();
            List<ISubscriber<int>> subscribers = new List<ISubscriber<int>>();
            foreach (int _ in Enumerable.Range(1,3))
            {
                var mock = new Mock<ISubscriber<int>>();
                mocks.Add(mock);
                var unwrapped = mock.Object;
                subscribers.Add(unwrapped);
                _bus.Register(unwrapped);
            }

            // send a message on the bus
            var msg = 0;
            _bus.Send(msg);

            // check that each subscriber received the message

            foreach (var mock in mocks)
            {
                mock.Verify(
                    s => s.Handle(msg),
                    Times.AtLeast(3));
            }
        }
    }
}

