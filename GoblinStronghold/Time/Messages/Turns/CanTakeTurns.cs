using System;
using GoblinStronghold.ECS;
using GoblinStronghold.Time.Components;

namespace GoblinStronghold.Time.Messages.Turns
{
	public readonly struct CanTakeTurns
	{
		public readonly CanTakeTurn TurnTaker;
		public readonly int InHowManyRounds;

		public CanTakeTurns(CanTakeTurn turnTaker, int firstTurnIn)
		{
			TurnTaker = turnTaker;
			InHowManyRounds = firstTurnIn;
		}
	}
}
