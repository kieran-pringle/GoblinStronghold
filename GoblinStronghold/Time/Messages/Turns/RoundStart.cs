namespace GoblinStronghold.Time.Messages.Turns
{
	public readonly struct RoundStart
	{
		public readonly int Round;

		public RoundStart(int turn)
		{
            Round = turn;
		}
	}
}
