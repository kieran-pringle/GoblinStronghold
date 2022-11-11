using System;
namespace GoblinStronghold.Creatures.Components
{
    // empty component marking an entity as a creature
    public class Creature
    {
        public static Creature Instance = new Creature();
       
        private Creature()
        {
        }
    }
}
