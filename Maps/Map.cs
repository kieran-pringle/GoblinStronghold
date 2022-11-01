using System;
using System.Collections.Generic;
using SadRogue.Primitives;
using GoblinStronghold.Utils.Iteration;
using GoblinStronghold.Entities;

namespace GoblinStronghold.Maps
{
    public class Map
    {
        public readonly Dictionary<Point, Cell> Contents = new Dictionary<Point, Cell>();

        public readonly int Width;
        public readonly int Height;

        public Map(int width, int height)
        {
            if (width < 1 || height < 1)
            {
                throw new ArgumentException("Maps can not smalled than 1x1");
            }
            Width = width;
            Height = height;

            // fill with empty cells
            var points = PointSet.Between(
                new Point(0, 0),
                new Point(width - 1, height - 1));
            foreach (var p in points)
            {
                Contents[p] = new Cell(this, p);
            }

            var middle = new Point(1, 1);
            points.Remove(middle);
            foreach (var p in points)
            {
                var c = Contents[p];
                Contents[p].MoveHere(new Wall());
            }
            Contents[middle].MoveHere(new Floor());
        }

        // TODO: method to deal with returning only valid cells, clean out nulls
    }
}

