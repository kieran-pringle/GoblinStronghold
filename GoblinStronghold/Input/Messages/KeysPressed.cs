using System.Collections.Generic;
using SadConsole.Input;

namespace GoblinStronghold.Input.Messages
{
	public readonly struct KeysPressed
	{
		public readonly IEnumerable<AsciiKey> Pressed;

		public KeysPressed(IEnumerable<AsciiKey> pressed)
		{
			Pressed = pressed;
		}
	}
}

