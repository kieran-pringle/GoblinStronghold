using System;
using System.Linq;
using GoblinStronghold.ECS;

namespace GoblinStronghold.Tests.ECS
{
    [TestFixture]
    public class ContextTest
    {
        private Context _ctx;
        private Entity _e;

        [SetUp]
        public void SetUp()
        {
            _ctx = new Context();
            _e = _ctx.CreateEntity();
        }

        [TestCase]
        public void ContextTest_CreateEntity()
        {
            var e = _ctx.CreateEntity();

            Assert.That(e, Is.InstanceOf(typeof(Entity)));
        }

        [TestCase]
        public void ContextTest_AllComponentsReturnsComponentsOnDIfferentEntities()
        {
            var str = "a component that we should see three times";

            var e1 = _ctx.CreateEntity().With(str);
            var e2 = _ctx.CreateEntity().With(str);
            var e3 = _ctx.CreateEntity().With(str);

            var allStrings = _ctx.AllComponents<string>();

            Assert.That(allStrings.Count(), Is.EqualTo(3));
            var owners = new List<Entity>();

            foreach (var s in allStrings)
            {
                owners.Add(s.Owner);
                Assert.That(s.Content, Is.EqualTo(str));
            }
          
            foreach (var e in new Entity[] {e1, e2, e3})
            {
                Assert.That(owners, Has.One.EqualTo(e));
            }
        }

        [TestCase]
        public void ContextTest_DestroyEntityRemovesComponentsFromEntity()
        {
            _e.With("a").With(12);

            _e.Destroy();

            Assert.Throws(
                typeof(KeyNotFoundException),
                () => _e.AllComponents());
        }

        [TestCase]
        public void ContextTest_DestroyEntityRemovesComponentsFromAllComponents()
        {
            _e.With("a").With(12);

            _e.Destroy();

            Assert.That(_ctx.AllComponents<string>().Count(), Is.EqualTo(0));
            Assert.That(_ctx.AllComponents<int>().Count(), Is.EqualTo(0));
        }

        [TestCase]
        public void ContextTest_DestroyComponentRemovesComponentFromAllComponents()
        {
            String str = "component";
            _e.With(str);
            var component = _e.Component<string>();

            _ctx.Destroy(component.Value);

            Assert.That(
                _ctx.AllComponents<string>(),
                Is.Empty,
                "there should be no components of that type");
        }

        [TestCase]
        public void ContextTest_DestroyComponentRemovesComponentFromEntity()
        {
            String str = "component";
            _e.With(str);
            var component = _e.Component<string>();

            _ctx.Destroy(component.Value);

            Assert.That(
                _e.AllComponents().Get<string>().HasValue,
                Is.False,
                "The component store should not be able to return a component");
            Assert.That(
                _e.Component<string>().HasValue,
                Is.False,
                "The entity should not be able to return a component");
        }
    }
}

