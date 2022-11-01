using System;
using SadConsole;
using SadConsole.Input;
using GoblinStronghold.Graphics;
using GoblinStronghold.Messaging;

namespace GoblinStronghold.Entities
{
    public class Player : Entity, ISubscriber<Keyboard>
    {
        public Player()
        {
            GameManager.MessageBus.Register(this);
        }

        public override ColoredGlyph Appearance()
        {
            return TileSet.ColoredGlyph("@");
        }

        void ISubscriber<Keyboard>.Handle(Keyboard keyboard)
        {
            if (keyboard.IsKeyPressed(Keys.Up))
            {
                this.Cell.North().MoveHere(this);
            }
            if (keyboard.IsKeyPressed(Keys.Down))
            {
                this.Cell.South().MoveHere(this);
            }
            if (keyboard.IsKeyPressed(Keys.Left))
            {
                this.Cell.West().MoveHere(this);
            }
            if (keyboard.IsKeyPressed(Keys.Right))
            {
                this.Cell.East().MoveHere(this);
            }
        }
    }
}

