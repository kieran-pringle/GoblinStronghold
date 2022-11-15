using GoblinStronghold.ECS;
using GoblinStronghold.Time.Messages;
using GoblinStronghold.Time.Messages.Turns;
using GoblinStronghold.Time.Systems;
using GoblinStronghold.Time.Components;
using System;

namespace GoblinStronghold.Tests.Time.Systems
{
	[TestFixture]
	public class TurnSystemTest
	{
        // util for creating entites with everything we are interested in
        private class TurnTaker
        {
            internal Entity Entity;
            internal CanTakeTurn CanTakeTurn;
            internal Mock<ITurnHandler> TurnHandlerMock;
            internal int Wait;
            internal IContext Context;

            public TurnTaker(IContext context, int wait = 1)
            {
                Context = context;
                TurnHandlerMock = new Mock<ITurnHandler>();
                CanTakeTurn = new CanTakeTurn(TurnHandlerMock.Object);
                Entity = context.CreateEntity().With(CanTakeTurn);
                Wait = wait;

                TurnHandlerMock
                    .Setup(th => th.TakeTurn())
                    .Callback(() => EndTurn());
            }

            public void EndTurn()
            {
                Context.Send(new TurnTaken(Wait));
            }
        }

        private Context _ctx;
        private TurnSystem _turnSystem;
        private Mock<ISystem<RoundStart>> _roundStartSystemMock;
        private Mock<ISystem<RoundEnd>> _roundEndSystemMock;

        [SetUp]
        public void SetUp()
        {
            _ctx = new Context();
            _turnSystem = new TurnSystem();

            _ctx.Register<UpdateTimePassed>(_turnSystem);
            _ctx.Register<CanTakeTurns>(_turnSystem);
            _ctx.Register<CanNotTakeTurns>(_turnSystem);
            _ctx.Register<TurnTaken>(_turnSystem);

            _roundStartSystemMock = new Mock<ISystem<RoundStart>>();
            _ctx.Register(_roundStartSystemMock.Object);
            _roundEndSystemMock = new Mock<ISystem<RoundEnd>>();
            _ctx.Register(_roundEndSystemMock.Object);
        }

        [TestCase]
        public void TurnSystemTest_DoesNothingIfNoTurnsToTake()
        {
            UpdateTimePassed();

            _roundStartSystemMock
                .Verify(
                    m => m.Handle(It.IsAny<RoundStart>()),
                    Times.Never());
            _roundEndSystemMock
                .Verify(
                    m => m.Handle(It.IsAny<RoundEnd>()),
                    Times.Never());
        }

        [TestCase]
        public void TurnSystemTest_SendsRoundStartMessageBeforeTakingTurns()
        {
            var turnTaker = new TurnTaker(_ctx);

            UpdateTimePassed();

            _roundStartSystemMock
                .Verify(
                    m => m.Handle(It.Is<RoundStart>(msg => msg.Round == 0)),
                    Times.Once());

            _roundEndSystemMock
                .Verify(
                    m => m.Handle(It.IsAny<RoundEnd>()),
                    Times.Never());
        }

        [TestCase]
        public void TurnSystemTest_NoTurnsOnRoundZero()
        {
            // since we can't have everything go on the same round its
            // registered or the first round would have everything acting
            // we want nothing to act on this first round

            var turnTaker = new TurnTaker(_ctx);

            // round start
            UpdateTimePassed();
            // first round is empty, nobody allowed to register to it
            UpdateTimePassed();

            _roundEndSystemMock
                .Verify(
                    m => m.Handle(It.Is<RoundEnd>(msg => msg.Round == 0)),
                    Times.Once());

            turnTaker.TurnHandlerMock.Verify(
                th => th.TakeTurn(),
                Times.Never());
        }

        [TestCase]
        public void TurnSystemTest_SendsRoundOneMessage()
        {
            var turnTaker = new TurnTaker(_ctx);

            // round start
            UpdateTimePassed();
            // first round is empty, nobody allowed to register to it
            UpdateTimePassed();

            // start round 1 
            UpdateTimePassed();
            // take first turn
            UpdateTimePassed();

            _roundStartSystemMock
                .Verify(
                    m => m.Handle(It.Is<RoundStart>(msg => msg.Round == 1)),
                    Times.Once());
        }

        [TestCase]
        public void TurnSystemTest_CallsTakeTurnOnKnownComponentOnRoundOne()
        {
            var turnTaker = new TurnTaker(_ctx);

            // round start
            UpdateTimePassed();
            // first round is empty, nobody allowed to register to it
            UpdateTimePassed();

            // start round 1 
            UpdateTimePassed();
            // take first turn
            UpdateTimePassed();

            turnTaker.TurnHandlerMock.Verify(
                th => th.TakeTurn(),
                Times.Once());
        }

        [TestCase]
        public void TurnSystemTest_DoesNotCallTakeTurnOnDeregisteredComponent()
        {
            var turnTaker = new TurnTaker(_ctx);

            // round start
            UpdateTimePassed();
            // first round is empty, nobody allowed to register to it
            UpdateTimePassed();

            // deregister component before its turn
            _ctx.Send(new CanNotTakeTurns(turnTaker.CanTakeTurn));

            // start round 1 
            UpdateTimePassed();
            // take first turn
            UpdateTimePassed();

            turnTaker.TurnHandlerMock.Verify(
                th => th.TakeTurn(),
                Times.Never());
        }


        [TestCase]
        public void TurnSystemTest_SendsRoundEndMessageAfterTurnsTaken()
        {
            var turnTaker = new TurnTaker(_ctx);
            var turnTaker2 = new TurnTaker(_ctx);

            // round start
            UpdateTimePassed();
            // first round is empty, nobody allowed to register to it
            UpdateTimePassed();

            // start round 1 
            UpdateTimePassed();
            // take first turn
            UpdateTimePassed();
            // take second turn
            UpdateTimePassed();
            // end round
            UpdateTimePassed();

            _roundEndSystemMock
              .Verify(
                  m => m.Handle(It.Is<RoundEnd>(msg => msg.Round == 0)),
                  Times.Once());
            _roundEndSystemMock
              .Verify(
                  m => m.Handle(It.Is<RoundEnd>(msg => msg.Round == 1)),
                  Times.Once());
        }

        [TestCase]
        public void TurnSystemTest_DoesCallTakeTurnOnNotDeregisteredComponentWithSameHandler()
        {
            // crap test neame but: we might have handlers that are equal to one
            // another used by different entites and we don't want all entites
            // using whichever handler to stop working when one is deregistered

            var turnTaker = new TurnTaker(_ctx);
            var hanlerMock = turnTaker.TurnHandlerMock;
            // use same handler
            var turnTaker2 = _ctx
                .CreateEntity()
                .With(new CanTakeTurn(hanlerMock.Object));

            // round start
            UpdateTimePassed();
            // first round is empty, nobody allowed to register to it
            UpdateTimePassed();

            // deregister component before its turn
            _ctx.Send(new CanNotTakeTurns(turnTaker.CanTakeTurn));

            // start round 1 
            UpdateTimePassed();
            // take first turn
            UpdateTimePassed();
            // take what would be second turn but skips as entity dequeued
            UpdateTimePassed();

            hanlerMock.Verify(
                th => th.TakeTurn(),
                Times.Once());
        }

        [TestCase]
        public void TurnSystemTest_CallsTakeTurnOnAllComponentsInRoundInOrder()
        {
            var calls = new List<TurnTaker>();

            var turnTaker1 = new TurnTaker(_ctx);
            var turnTaker2 = new TurnTaker(_ctx);

            turnTaker1.TurnHandlerMock
                .Setup(th => th.TakeTurn())
                .Callback(() =>
                {
                    calls.Add(turnTaker1);
                    _ctx.Send(new TurnTaken(turnTaker1.Wait));
                });
            turnTaker2.TurnHandlerMock
                .Setup(th => th.TakeTurn())
                .Callback(() =>
                {
                    calls.Add(turnTaker2);
                    _ctx.Send(new TurnTaken(turnTaker2.Wait));
                });


            // start round 0
            UpdateTimePassed();
            // first round is empty, nobody allowed to register to it
            UpdateTimePassed();

            // start round 1 
            UpdateTimePassed();
            // take first turn
            UpdateTimePassed();
            // take second turn
            UpdateTimePassed();

            Assert.That(calls[0], Is.EqualTo(turnTaker1));
            Assert.That(calls[1], Is.EqualTo(turnTaker2));
        }

        [TestCase]
        public void TurnSystemTest_CallsTakeTurnOnAllComponentsOnMultipleRoundsInOrder()
        {
            // the two turn takers have different speeds
            // turn taker one goes every two rounds
            // turn taker two goes every three rounds
            // < = round start
            // > = round end
            // . = round with no turns (start and end msg)
            // notice that entity 2 goes first in round 7!
            // 0  |1   |2  |3  |4  |5  |6  |7   |8  |9  |10 |
            // <.>|<12>|<.>|<1>|<2>|<1>|<.>|<21>|<.>|<1>|<2>|
            // this should take a total of 40 update messages exactly
            // there should be 9 calls to turn takers
            var calls = new List<TurnTaker>();

            var turnTaker1 = new TurnTaker(_ctx, 2);
            var turnTaker2 = new TurnTaker(_ctx, 3);

            turnTaker1.TurnHandlerMock
                .Setup(th => th.TakeTurn())
                .Callback(() =>
                {
                    calls.Add(turnTaker1);
                    turnTaker1.EndTurn();
                });
            turnTaker2.TurnHandlerMock
                .Setup(th => th.TakeTurn())
                .Callback(() =>
                {
                    calls.Add(turnTaker2);
                    turnTaker2.EndTurn();
                });

            // 32 messages
            for (int i = 0; i < 32; i++)
            {
                UpdateTimePassed();
            }

            // 7 rounds
            for (int i = 0; i <= 7; i++)
            {
                _roundStartSystemMock
                    .Verify(
                        m => m.Handle(It.Is<RoundStart>(msg => msg.Round == i)),
                        Times.Once());
                _roundEndSystemMock
                  .Verify(
                      m => m.Handle(It.Is<RoundEnd>(msg => msg.Round == i)),
                      Times.Once());
            }

            // 9 turns taken
            // turn taker 1 goes twice before turn taker 2 goes at ***
            Assert.That(calls[0], Is.EqualTo(turnTaker1));
            Assert.That(calls[1], Is.EqualTo(turnTaker2));
            Assert.That(calls[2], Is.EqualTo(turnTaker1));
            Assert.That(calls[3], Is.EqualTo(turnTaker2));
            Assert.That(calls[4], Is.EqualTo(turnTaker1));
            Assert.That(calls[5], Is.EqualTo(turnTaker2));
            Assert.That(calls[6], Is.EqualTo(turnTaker1));
            Assert.That(calls[7], Is.EqualTo(turnTaker1)); // ***
            Assert.That(calls[8], Is.EqualTo(turnTaker2));
        }

        [TestCase]
        public void TurnSystemTest_NonBlocking()
        {
            // some turn takers (like the player) will not take their move
            // synchronously, make sure that we can keep receiving time updates
            // even if they don't send an end turn message

            var asyncTurnTaker = new TurnTaker(_ctx, 2);

            asyncTurnTaker.TurnHandlerMock
                .Setup(th => th.TakeTurn())
                .Callback(() =>
                {
                    // do nothing for now
                });

            // round start
            UpdateTimePassed();
            // first round is empty, nobody allowed to register to it
            UpdateTimePassed();

            // start round 1 
            UpdateTimePassed();
            // take first turn
            UpdateTimePassed();

            // discarded updates
            for (int i = 0; i < 50; i++)
            {
                UpdateTimePassed();
            }

            asyncTurnTaker.TurnHandlerMock.Verify(
                th => th.TakeTurn(),
                Times.Once());

            _roundStartSystemMock
                  .Verify(
                      m => m.Handle(It.Is<RoundStart>(msg => msg.Round == 1)),
                      Times.Once());
            _roundEndSystemMock
                  .Verify(
                      m => m.Handle(It.Is<RoundEnd>(msg => msg.Round == 1)),
                      Times.Never());
        }

        private void UpdateTimePassed()
        {
            _ctx.Send(new UpdateTimePassed(TimeSpan.Zero));
        }
    }
}
