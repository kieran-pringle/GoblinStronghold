using System;
using GoblinStronghold.Graphics;
using GoblinStronghold.Maps;
using GoblinStronghold.Components;
using SadConsole;

namespace GoblinStronghold.Entities
{
    public class Wall : Entity
    {
        private ColoredGlyph _setAppearance;
        private String[] _appearancePool =
        {
            "brick-wall-1",
            "brick-wall-2",
            "brick-wall-3",
            "brick-wall-4"
        };

        public Wall()
        {
            AddComponent(CollisionComponent.Impassable);
        }

        public override ColoredGlyph Appearance()
        {
            if (_setAppearance == null)
            {
                int i = GameManager.Random.Next(0, _appearancePool.Length - 1);
                string name = _appearancePool[i];
                ColoredGlyph toSet = TileSet.ColoredGlyph(name);
                _setAppearance = toSet;
            }
           
            // pick one the first time then stick to it
            return _setAppearance;
        }
    }
}

