using System;
using Moq;
using GoblinStronghold.ECS;

namespace GoblinStronghold.Tests.ECS
{
    [TestFixture]
    public class EntityTest
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
        public void EntityTest_CreateAnEntityFromAContext()
        {
            Assert.That(_entity, Is.Not.Null);
        }

        [Test]
        public void EntityTest_AddComponentToEntityAndRetrieve()
        {
            String str = "data";
            _entity.With(str);

            var strComponent = _entity.Component<String>();

            Assert.That(strComponent, Is.Not.Null,
                "we should be able to retrieve the string added");
            Assert.That(strComponent.Data, Is.Not.Null,
                "the retrieved component should not have empty data");
            Assert.That(strComponent.Data, Is.EqualTo(str));
        }

        [Test]
        public void EntityTest_AddMultipleComponentsAndRetrieve()
        {
            (string, string) strStrTup = ("a", "tuple");
            (string, int) strIntTup = ("number is", 12);

            _entity.With(strStrTup).With(strIntTup);

            var tup1Component = _entity.Component<(string, string)>();
            var tup2Component = _entity.Component<(string, int)>();

            Assert.That(
                _entity.Component<(string, string)>().Data,
                Is.EqualTo(strStrTup));
            Assert.That(
                _entity.Component<(string, int)>().Data,
                Is.EqualTo(strIntTup));
        }

        [Test]
        public void EntityTest_AddingNewComponentOfSameTypeShouldOverrwrite()
        {
            (string, string) tup1 = ("number", "1");
            (string, string) tup2 = ("a", "replacement");

            _entity.With(tup1);

            Assert.That(
                _entity.Component<(string, string)>().Data,
                Is.EqualTo(tup1));

            _entity.With(tup2);

            Assert.That(
               _entity.Component<(string, string)>().Data,
               Is.EqualTo(tup2));
        }

        [Test]
        public void EntityTest_ReturnsNullIfNoMatchingComponent()
        {
            // add nothing, there are no components

            var component = _entity.Component<object>();

            Assert.That(component, Is.Null);
        }
    }
}
