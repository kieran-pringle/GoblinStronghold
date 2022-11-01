using System;
using SadConsole;
using GoblinStronghold.Graphics;
using GoblinStronghold.Maps;

namespace GoblinStronghold.Entities
{
    public abstract class Entity : IHasAppearance
    {

        public Cell Cell;

        // has appearance

        public abstract ColoredGlyph Appearance();

        // has components

        // has control brain
    }
}

