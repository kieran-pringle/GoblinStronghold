using System;
using GoblinStronghold.ECS;

namespace GoblinStronghold.Tests.ECS
{
    // special tests for garbage collection
    [TestFixture]
    public class DestroyableTest
    {
        // this doesn't work atm
        // https://blog.jetbrains.com/dotnet/2020/06/09/memory-profiling-linux-macos-dotmemory-2020-2/
        // https://www.jetbrains.com/help/dotmemory/Profiling_Guidelines__Advanced_Profiling_Using_dotTrace_API.html

        // look at dotMemoryUnit

        [Test]
        public void DestroyableTest_Context_DestroyComponent()
        {
            throw new NotImplementedException("Destroy a component");
        }

        [Test]
        public void DestroyableTest_Context_DestroyEntity()
        {
            throw new NotImplementedException("Destroy a component");
        }

        [Test]
        public void Entity_Destroy()
        {
            var ctx = new Context();
            WeakReference? weakRefToEntity = null;

            new Action(() =>
            {
                weakRefToEntity = new WeakReference(ctx.CreateEntity(), false);
                weakRefToEntity.Target.Destroy();
            })();

            if (weakRefToEntity != null)
            {
                Assert.That(
                    weakRefToEntity.Target,
                    Is.Not.Null,
                    "We should have a reference to the entity");
            }
            else
            {
                Assert.Fail("Weak ref should not be null");
            }

            GC.Collect(2, GCCollectionMode.Forced, true);
            GC.WaitForPendingFinalizers();

            if (weakRefToEntity != null)
            {
                Assert.That(
                    weakRefToEntity.Target,
                    Is.Null,
                    "The entity should no loger be referenced");
            }
            else
            {
                Assert.Fail("Weak ref should not be null");
            }
        }

        [Test]
        public void DestroyTest_Component_Destroy()
        {

        }
    }
}

