using System;
using SadConsole;
using SadConsole.Input;
using GoblinStronghold.Components;
using GoblinStronghold.Graphics;
using GoblinStronghold.Messaging;
using GoblinStronghold.Messaging.Messages;

namespace GoblinStronghold.Entities
{
    public class Player : Entity, ISubscriber<KeyEventsMessage>
    {
        public Player()
        {
            GameManager.MessageBus.Register(this);
            AddComponent(new FixedGlyph("player-basic"));
        }

        void ISubscriber<KeyEventsMessage>.Handle(KeyEventsMessage keyEvents)
        {
            if (keyEvents.IsKeyPressed(Keys.Up))
            {
                this.Cell.North().MoveHere(this);
            }
            if (keyEvents.IsKeyPressed(Keys.Down))
            {
                this.Cell.South().MoveHere(this);
            }
            if (keyEvents.IsKeyPressed(Keys.Left))
            {
                this.Cell.West().MoveHere(this);
            }
            if (keyEvents.IsKeyPressed(Keys.Right))
            {
                this.Cell.East().MoveHere(this);
            }
        }
    }
}

