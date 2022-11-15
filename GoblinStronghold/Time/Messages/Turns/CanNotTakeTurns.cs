using System;
using GoblinStronghold.ECS;
using GoblinStronghold.Time.Components;

namespace GoblinStronghold.Time.Messages.Turns
{
    public readonly struct CanNotTakeTurns
    {
        public readonly CanTakeTurn TurnTaker;

        public CanNotTakeTurns(CanTakeTurn turnTaker)
        {
            TurnTaker = turnTaker;
        }
    }
}
