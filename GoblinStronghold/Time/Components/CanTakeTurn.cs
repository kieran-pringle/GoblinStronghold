using System;
using GoblinStronghold.ECS;
using GoblinStronghold.Time.Messages.Turns;

namespace GoblinStronghold.Time.Components
{
	public class CanTakeTurn : IOnComponentRegister
	{
		private ITurnHandler _handler;

		public CanTakeTurn(ITurnHandler handler)
		{
			_handler = handler;
		}

        public void OnRegisterTo(Entity entity)
        {
            // notify turn system
            // TODO: get a wait value from the entity
            var msg = new CanTakeTurns(
				this,
				0 
			);

            entity.Context().Send(msg);
        }

        public void TakeTurn()
		{
			// probably need to pass a reference to entity?
            _handler.TakeTurn();
		}
	}
}
