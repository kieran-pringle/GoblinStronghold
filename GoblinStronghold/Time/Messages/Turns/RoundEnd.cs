using System;
namespace GoblinStronghold.Time.Messages.Turns
{
	public readonly struct RoundEnd
	{
		public readonly int Round;

		public RoundEnd(int roundNunber)
		{
			Round = roundNunber;
		}
	}
}

