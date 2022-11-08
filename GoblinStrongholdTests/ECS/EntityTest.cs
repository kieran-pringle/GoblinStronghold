using System;
using Moq;
using GoblinStronghold.ECS;
using Newtonsoft.Json.Linq;

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

            Assert.That(strComponent.HasValue,
                "we should be able to retrieve the string added");
            Assert.That(strComponent.Value.Content, Is.EqualTo(str),
                "the retrieved component should not have empty data");
        }

        [Test]
        public void EntityTest_AddMultipleComponentsAndRetrieve()
        {
            (string, string) strStrTup = ("a", "tuple");
            (string, int) strIntTup = ("number is", 12);

            _entity.With(strStrTup).With(strIntTup);

            var tup1Component = _entity.Component<(string, string)>();
            var tup2Component = _entity.Component<(string, int)>();

            Assert.That(tup1Component.HasValue, "tup1Component should have a value");
            Assert.That(tup2Component.HasValue, "tup2Compoent should have a value");

            Assert.That(tup1Component.Value.Content, Is.EqualTo(strStrTup));
            Assert.That(tup2Component.Value.Content, Is.EqualTo(strIntTup));
        }

        [Test]
        public void EntityTest_AddingNewComponentOfSameTypeShouldOverrwrite()
        {
            (string, string) tup1 = ("number", "1");
            (string, string) tup2 = ("a", "replacement");

            _entity.With(tup1);

            Assert.That(
                _entity.Component<(string, string)>().Value.Content,
                Is.EqualTo(tup1));

            _entity.With(tup2);
            Assert.That(
               _entity.Component<(string, string)>().Value.Content,
               Is.EqualTo(tup2));
        }

        [Test]
        public void EntityTest_ReturnsNullIfNoMatchingComponent()
        {
            // add nothing, there are no components

            var component = _entity.Component<object>();

            Assert.That(component.HasValue, Is.False, "There should be no component matching");
        }
    }
}
