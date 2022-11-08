using System;
using GoblinStronghold.ECS;

namespace GoblinStronghold.Tests.ECS
{
    [TestFixture]
    public class ComponentTest
    {
        private Context _ctx;
        private Entity _entity;

        [SetUp]
        public void SetUp()
        {
            _ctx = new Context();
            _entity = _ctx.CreateEntity();
        }

        [Test]
        public void ComponentTest_ShouldBeAbleToGetOwner()
        {
            var strComponent = "data";
            _entity.With(strComponent);
            var component = _entity.Component<String>();

            Assert.That(Is.ReferenceEquals(component.Value.Owner, _entity));
        }

        [Test]
        public void ComponentTest_ShouldBeAbleToGetData()
        {
            var strComponent = "data";
            _entity.With(strComponent);
            var component = _entity.Component<String>();

            Assert.That(component.Value.Content, Is.EqualTo(strComponent));
        }

        [Test]
        public void ComponentTest_ContentIsMutable()
        {
            var strComponent = "data";
            _entity.With(strComponent);
            
            _entity.Component<String>().Value.Content = "new-data";

            Assert.That(
                _entity.Component<String>().Value.Content,
                Is.EqualTo("new-data"));
        }

        [Test]
        public void ComponentTest_DestroyRemovesOwner()
        {
            var strComponent = "data";
            _entity.With(strComponent);
            var component = _entity.Component<String>().Value;

            component.Destroy();

            Assert.That(component.Owner, Is.Null);
        }

        [Test]
        public void ComponentTest_DestroyRemovesFromOwner()
        {
            var strComponent = "data";
            _entity.With(strComponent);
            var component = _entity.Component<String>().Value;

            component.Destroy();

            Assert.That(_entity.Component<string>().HasValue, Is.False);
        }

        [Test]
        public void ComponentTest_DestroyRemovesFromAllComponents()
        {
            var strComponent = "data";
            _entity.With(strComponent);
            var component = _entity.Component<String>().Value;

            component.Destroy();

            Assert.That(_ctx.AllComponents<string>().Count(), Is.EqualTo(0));
        }
    }
}

