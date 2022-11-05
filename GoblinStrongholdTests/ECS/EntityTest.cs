using System;
using GoblinStronghold.ECS;

namespace GoblinStronghold.Tests.ECS
{
    [TestFixture]
    public class EntityTest
    {
        private Context _ctx;

        private class BasicComponent<T> : Component
        {
            internal T _data;

            internal BasicComponent(T data)
            {
                _data = data;
            }
        }

        [SetUp]
        public void SetUp()
        {
            _ctx = Context.Instance();
            _ctx.Clear();
        }

        [Test]
        public void EntityTest_CreateAnEntityFromAContext()
        {
            Entity e = _ctx.CreateEntity();

            Assert.That(e, Is.Not.Null);
        }

        [Test]
        public void EntityTest_AddComponentsToEntityAndRetrieve()
        {
            Entity e = _ctx.CreateEntity();

            BasicComponent<string> strComponent = new BasicComponent<string>("data");
            BasicComponent<int> intComponent = new BasicComponent<int>(0);

            e.With(strComponent).With(intComponent);

            var returnedStrComponent = e.Component<BasicComponent<string>>();
            var returnedIntComponent = e.Component<BasicComponent<int>>();

            Assert.That(returnedStrComponent, Is.EqualTo(strComponent));
            Assert.That(returnedIntComponent, Is.EqualTo(intComponent));
        }
    }
}

