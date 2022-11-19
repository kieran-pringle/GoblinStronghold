using System;
using System.Linq;
using System.Collections.Generic;

namespace GoblinStronghold.Log.Messages
{
	public readonly struct DiceRoll
	{
		public readonly IList<Die> Dice;
		public readonly int Modifier;
		public readonly int Total;

		public DiceRoll(IList<Die> dice, int modifier = 0)
		{
			Dice = dice;
			Modifier = modifier;
			// sum the die and add modifier
			var diceSum = dice.Aggregate(
				0,
				(memo, d) => memo + (d.Used ? d.Value : 0)
			);
			Total = Math.Clamp(diceSum + modifier, 0, 999);
		}

		public readonly struct Die
		{
			public readonly int Value;
			public readonly bool Used;

			public Die(int value, bool used)
			{
				if (value < 1 || value > 6)
				{
					throw new ArgumentException("A d6 can't roll " + value);
				}
				Value = value;
				Used = used;
			}
		}

		public class Builder
		{
			private IList<Die> _dice = new List<Die>();
			private int _modifier = 0;

			public Builder WithModifier(int mod)
			{
				_modifier += mod;
				return this;
			}

			public Builder WithDie(int value, bool used = true)
			{
				_dice.Add(new Die(value, used));
				return this;
			}

			public DiceRoll Build()
			{
				return new DiceRoll(_dice, _modifier);
			}
		}
	}
}

