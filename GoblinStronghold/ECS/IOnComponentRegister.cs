using System;
namespace GoblinStronghold.ECS
{
    /**
     *  Marks a class intended as a component as having a callback for when it
     *  is registered
     */
    public interface IOnComponentRegister
    {
        public void OnRegisterTo(Entity entity);
    }
}

