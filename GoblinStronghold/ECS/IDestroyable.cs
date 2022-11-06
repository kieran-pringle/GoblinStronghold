using System;
namespace GoblinStronghold.ECS
{
    // really only serves to give us something to cast components to so we can
    // tear down when we don't know what their type is
    public interface IDestroyable
    {
        public void Destroy();
    }
}

