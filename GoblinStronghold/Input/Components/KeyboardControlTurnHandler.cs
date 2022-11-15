using System;
using GoblinStronghold.Time.Components;

namespace GoblinStronghold.Input.Components
{
	public class KeyboardControlTurnHandler : ITurnHandler
	{
        private bool _isTakingTurn;

		public KeyboardControlTurnHandler()
		{
		}

        public void TakeTurn()
        {
            _isTakingTurn = true;
            // do not send turn end until we have handled a keyboard event
        }
    }
}
