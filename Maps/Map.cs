using System;
using System.Collections.Generic;
using SadRogue.Primitives;
using GoblinStronghold.Utils.Iteration;
using GoblinStronghold.Entities;
using GoblinStronghold.Messaging.Messages;
using Newtonsoft.Json.Linq;
using System.Diagnostics.CodeAnalysis;
using System.Collections;
using GoblinStronghold.Graphics;

namespace GoblinStronghold.Maps
{
    public class Map : Dictionary<Point, Cell>
    {
        public readonly int Width;
        public readonly int Height;
        public bool Dirty { get; set; }

        public Map(int width, int height)
        {
            if (width < 1 || height < 1)
            {
                throw new ArgumentException("Maps can not smalled than 1x1");
            }
            Width = width;
            Height = height;

            // fill with empty cells
            var points = PointSet.RectangleFilled(
                new Point(0, 0),
                new Point(width - 1, height - 1));
            foreach (var p in points)
            {
                this[p] = new Cell(this, p);
            }

            var middle = PointSet.RectangleFilled(
                new Point(1, 1),
                new Point(Width - 2, Height - 2));
           
            foreach (var p in middle)
            {
                var c = this[p];
                this[p].MoveHere(new Floor());
            }

            var border = PointSet.RectangleBorder(
                new Point(0, 0),
                new Point(Width - 1, Height - 1));

            foreach (var p in border)
            {
                var c = this[p];
                this[p].MoveHere(new Wall());
            }

            var player = new Player();
            var cell = this[new Point(Width / 2, Height / 2)];
            cell.MoveHere(player);
        }

        // TODO: method to deal with returning only valid cells, clean out nulls

        // TODO: smarter version of this message that can bundle a set of cells to redraw
        public void CellChanged(Cell cell)
        {
            GameManager.MessageBus.Send(new MapChanged(this));
        }
    }
}

