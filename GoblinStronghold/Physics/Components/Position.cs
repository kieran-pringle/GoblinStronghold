using System;
namespace GoblinStronghold.Physics.Components
{
    public class Position
    {
        public int X;
        public int Y;
        public int Z;

        public int VelocityX;
        public int VelocityY;
        public int VelocityZ;

        public Position(int x, int y, int z = 0)
        {
            X = x;
            Y = y;
            Z = z;

            VelocityX = 0;
            VelocityY = 0;
            VelocityZ = 0;
        }
    }
}
