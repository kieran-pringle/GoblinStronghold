using System;
using System.Collections.Generic;
using SadRogue.Primitives;

namespace GoblinStronghold.Utils.Iteration
{
    public static class PointSet
    {

        public static HashSet<Point> Between(Point start, Point end)
        {
            var result = new HashSet<Point>();
            for (int x = start.X; x <= end.X; x++)
            {
                for (int y = start.Y; y <= end.Y; y++)
                {
                    result.Add(new Point(x, y));
                }
            }
            return result;
        }
    }
}

