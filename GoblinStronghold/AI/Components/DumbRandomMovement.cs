using System;
using GoblinStronghold.ECS;
using GoblinStronghold.Physics.Components;
using GoblinStronghold.Physics.Messages;
using GoblinStronghold.Input.Components;
using GoblinStronghold.Time.Components;
using GoblinStronghold.Time.Messages.Turns;

namespace GoblinStronghold.AI.Components
{
	public class DumbRandomMovement : IOnComponentRegister
	{
        private CanTakeTurn _canTakeTurn;
        private Entity _owner;
        private IContext _ctx;

        public DumbRandomMovement()
		{
            _canTakeTurn = new CanTakeTurn(new DumbRandomMove(this));
		}

        public void OnRegisterTo(Entity entity)
        {
            _owner = entity;
            _ctx = entity.Context();
            entity.With(_canTakeTurn);
        }

        private class DumbRandomMove : ITurnHandler
        {
            private DumbRandomMovement _parent;

            public DumbRandomMove(DumbRandomMovement parent)
            {
                _parent = parent;
            }

            public void TakeTurn()
            {
                var e = _parent._owner;
                var current_pos = e.Component<Position>().Content();
                int i = GameManager.Random.Next(0, 4);
                var possibleNewPositions = new Position[]
                {
                    current_pos.North(),
                    current_pos.South(),
                    current_pos.East(),
                    current_pos.West()
                };
                _parent._ctx.Send(new MoveTo(e, possibleNewPositions[i]));
                _parent._ctx.Send(new TurnTaken(2));
            }
        }
    }
}
