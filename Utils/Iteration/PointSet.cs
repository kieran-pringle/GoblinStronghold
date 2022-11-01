using System;
using System.Collections.Generic;
using SadRogue.Primitives;

namespace GoblinStronghold.Utils.Iteration
{
    public static class PointSet
    {

        public static HashSet<Point> RectangleFilled(Point start, Point end)
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

        public static HashSet<Point> RectangleBorder(Point start, Point end)
        {
            var result = new HashSet<Point>();
            // top border
            for (int x = start.X; x <= end.X; x++)
            {
                result.Add(new Point(x, start.Y));
                result.Add(new Point(x, end.Y));
            }
            // bottom border
            for (int y = start.Y; y <= end.Y; y++)
            {
                result.Add(new Point(start.X, y));
                result.Add(new Point(end.X, y));
            }
            return result;
        }
    }
}

