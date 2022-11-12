using System;
namespace GoblinStronghold.Physics.Components
{
    public readonly struct Position
    {
        public readonly int X;
        public readonly int Y;

        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Position North()
        {
            return new Position(this.X, this.Y - 1);
        }

        public Position South()
        {
            return new Position(this.X, this.Y + 1);
        }

        public Position East()
        {
            return new Position(this.X + 1, this.Y);
        }

        public Position West()
        {
            return new Position(this.X - 1, this.Y);
        }
    }
}
