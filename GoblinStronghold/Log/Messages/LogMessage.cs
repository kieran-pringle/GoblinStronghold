using System;
using Functional.Option;

namespace GoblinStronghold.Log.Messages
{
	public readonly struct LogMessage
	{
		public readonly String Message;
		public readonly Option<DiceRoll> Roll;

		public LogMessage(String message) : this(message, Option.None) { }

        public LogMessage(String message, Option<DiceRoll> roll)
        {
            Message = message;
            Roll = roll;
        }
    }
}
