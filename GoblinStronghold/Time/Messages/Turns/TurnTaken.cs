using System;
using GoblinStronghold.Time.Components;

namespace GoblinStronghold.Time.Messages.Turns
{
	public readonly struct TurnTaken
	{
		public readonly int RoundsUntilNextTurn;

		public TurnTaken(int roundsUntilNextTurn)
		{
			RoundsUntilNextTurn = roundsUntilNextTurn;
		}
	}
}
