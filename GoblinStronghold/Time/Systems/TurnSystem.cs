using System;
using System.Collections.Generic;
using System.Linq;
using GoblinStronghold.ECS;
using GoblinStronghold.ECS.Messaging;
using GoblinStronghold.Time.Components;
using GoblinStronghold.Time.Messages;
using GoblinStronghold.Time.Messages.Turns;

namespace GoblinStronghold.Time.Systems
{
    /**
     * Handles notifying things that might want to take act on an entity taking 
     * a turn. There are two messages, one incoming and one outgoing.
     * -> RoundStart    : A new round has begun. For systems to sync to
     * -> RoundEnd      : The round of turns has finished
     * 
     * There are also messages to register and deregister from the system
     * <- CanTakeTurns      : Register new component to take turns
     * <- CanNotTakeTurns   : Deregister component to stop taking turns
     */
	public class TurnSystem :
        ISystem<UpdateTimePassed>,
        ISystem<CanTakeTurns>,
        ISystem<CanNotTakeTurns>,
        ISystem<TurnTaken>
    {
        private IContext _context;
        IContext ISystem<UpdateTimePassed>.Context
        {
            get => _context;
            set => _context = value;
        }
        IContext ISystem<CanTakeTurns>.Context
        {
            get => _context;
            set => _context = value;
        }
        IContext ISystem<CanNotTakeTurns>.Context
        {
            get => _context;
            set => _context = value;
        }
        IContext ISystem<TurnTaken>.Context
        {
            get => _context;
            set => _context = value;
        }

        private int _roundNumber = 0;
        private bool _isRoundInProgress = false;

        private bool _isTurnInProgress = false;
        private CanTakeTurn _takingTurn;

        // index 0 is current turn
        private Queue<Queue<CanTakeTurn>> _roundQueues
            = new Queue<Queue<CanTakeTurn>>();
        // items to remove from the system
        private HashSet<CanTakeTurn> _dequeued
            = new HashSet<CanTakeTurn>();

        public void Handle(UpdateTimePassed message)
        {
            if (!_isRoundInProgress)
            {
                StartRound();
            }
            else
            {
                if(!_isTurnInProgress)
                {
                    if (AnyTurnsLeftToTakeThisRound())
                    {
                        TakeNextTurn();
                    }
                    else
                    {
                        EndRound();
                    }
                }
            }
        }

        public void Handle(CanTakeTurns message)
        {
            QueueForNextTurnIn(message.TurnTaker, message.InHowManyRounds);
        }

        public void Handle(CanNotTakeTurns message)
        {
            _dequeued.Add(message.TurnTaker);
        }

        public void Handle(TurnTaken message)
        {
            EndTurn(message.RoundsUntilNextTurn);
        }

        private void StartRound()
        {
            if (_roundQueues.Count > 0)
            {
                _context.Send(new RoundStart(_roundNumber));
                _isRoundInProgress = true;
            }
        }

        private void TakeNextTurn()
        {
            var turnTaker = TurnsLeftInCurrentRound().Dequeue();
            if (_dequeued.Count >= 0)
            {
                if (_dequeued.Any(dq => SameInstance(dq, turnTaker)))
                {
                    // remove from dequeued
                    _dequeued.RemoveWhere(dq => SameInstance(dq, turnTaker));
                    // do not take turn
                    return;
                }
            }
            _isTurnInProgress = true;
            _takingTurn = turnTaker;
            _takingTurn.TakeTurn();
        }

        private void EndTurn(int roundsUntilNextTurn)
        {
            _isTurnInProgress = false;
            QueueForNextTurnIn(
                _takingTurn,
                roundsUntilNextTurn);
        }

        private void EndRound()
        {
            // set up next queue
            _roundQueues.Dequeue();

            // send message about round ending
            _context.Send(new RoundEnd(_roundNumber));
            _isRoundInProgress = false;
            _roundNumber += 1;
        }

        private void QueueForNextTurnIn(CanTakeTurn turnTaker, int rounds)
        {
            // can't go again on same round
            if (rounds == 0)
            {
                rounds = 1;
            }

            // create missing round queues as necessary here
            while (_roundQueues.Count < (rounds + 1))
            {
                _roundQueues.Enqueue(new Queue<CanTakeTurn>());
            }

            _roundQueues.ElementAt(rounds).Enqueue(turnTaker);
        }

        private Queue<CanTakeTurn> TurnsLeftInCurrentRound()
        {
            return _roundQueues.Peek();
        }

        private bool AnyTurnsLeftToTakeThisRound()
        {
            return TurnsLeftInCurrentRound().Count > 0;
        }

        private static bool SameInstance(CanTakeTurn a, CanTakeTurn b)
        {
            return Object.ReferenceEquals(a, b);
        }
    }
}
