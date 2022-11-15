namespace GoblinStronghold.ECS
{
    /**
     *  Marks a class intended as a component as having a callback for when it
     *  is registered.
     *  
     *  TODO: Consider whether this needs to require a destruction callback?
     */
    public interface IOnComponentRegister
    {
        public void OnRegisterTo(Entity entity);
    }
}

