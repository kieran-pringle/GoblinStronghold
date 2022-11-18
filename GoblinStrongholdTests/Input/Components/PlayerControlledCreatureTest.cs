using System;
using System.Collections.ObjectModel;
using SadConsole.Input;
using GoblinStronghold.Input.Components;
using GoblinStronghold.Input.Messages;
using GoblinStronghold.Input.Systems;
using GoblinStronghold.Physics.Components;
using GoblinStronghold.Physics.Systems;
using GoblinStronghold.Time.Messages;
using GoblinStronghold.Time.Messages.Turns;
using GoblinStronghold.Time.Systems;

namespace GoblinStronghold.Tests.Input.Components
{
	[TestFixture]
	public class PlayerControlledCreatureTest
	{
        private IContext _ctx;
        private Entity _entity;
        private KeyboardControlSystem _kcSystem;
        private TurnSystem _tSystem;
        private MoveToSystem _mSystem;
        private KeysPressed _keysPressed;

        [SetUp]
        public void SetUp()
        {
            _ctx = new Context();

            _kcSystem = new KeyboardControlSystem();
            _ctx.Register(_kcSystem);

            _mSystem = new MoveToSystem();
            _ctx.Register(_mSystem);

            _tSystem = new TurnSystem();
            _ctx.Register<UpdateTimePassed>(_tSystem);
            _ctx.Register<CanTakeTurns>(_tSystem);
            _ctx.Register<TurnTaken>(_tSystem);

            // have to create after systems registered
            // so we send off `CanTakeTurn` message
            _entity = _ctx.CreateEntity()
                .With(new Position(0,0))
                .With(new PlayerControlledCreature());

            SetUpKeyPressed();
        }

		[TestCase]
		public void PlayerControlledCreatureTest_EntityNotMovedByKeyboardInputIfNotItsTurn()
		{
            _ctx.Send(_keysPressed);

            var pos = _entity.Component<Position>().Content();

            Assert.That(pos, Is.EqualTo(new Position(0, 0)));
		}

        [TestCase]
        public void PlayerControlledCreatureTest_EntityMovedByKeyboardInputIfItsTurn()
        {
            StartPlayerTurn();

            // press up
            _ctx.Send(_keysPressed);

            var pos = _entity.Component<Position>().Content();

            Assert.That(pos, Is.EqualTo(new Position(0, -1)));
        }

        [TestCase]
        public void PlayerControlledCreatureTest_SendsTurnTakenAfterInput()
        {
            Mock<ISystem<TurnTaken>> mockTurnTakenSystem = new Mock<ISystem<TurnTaken>>();
            _ctx.Register(mockTurnTakenSystem.Object);

            StartPlayerTurn();

            // press up
            _ctx.Send(_keysPressed);

            mockTurnTakenSystem.Verify(
                s => s.Handle(It.IsAny<TurnTaken>()),
                Times.Once());
        }

        [TestCase]
        public void PlayerControlledCreatureTest_EntityNotMovedByKeyboardInputAfterItsTurn()
        {
            StartPlayerTurn();

            // press up three times
            _ctx.Send(_keysPressed);
            _ctx.Send(_keysPressed);
            _ctx.Send(_keysPressed);

            // should only have moved once
            var pos = _entity.Component<Position>().Content();
            Assert.That(pos, Is.EqualTo(new Position(0, -1)));
        }

        [TestCase, Timeout(250)]
        public void PlayerControlledCreatureTest_EntityWaitingForKeyboardInputIsNonBlocking()
        {
            Mock<ISystem<TurnTaken>> mockTurnTakenSystem = new Mock<ISystem<TurnTaken>>();
            _ctx.Register(mockTurnTakenSystem.Object);

            StartPlayerTurn();

            // no keyboard event

            for (int i = 0; i < 10; i++)
            {
                // we should be able to send these messages and not hit the timeout
                _ctx.Send(new UpdateTimePassed(TimeSpan.Zero));
            }

            mockTurnTakenSystem.Verify(
                s => s.Handle(It.IsAny<TurnTaken>()),
                Times.Never());
        }

        private void SetUpKeyPressed()
        {
            var keysPressedList = new List<AsciiKey>();
            var upKey = new AsciiKey();
            upKey.Key = Keys.Up;
            keysPressedList.Add(upKey);
            _keysPressed = new KeysPressed(keysPressedList);
        }

        private void StartPlayerTurn()
        {
            // round 0 start
            _ctx.Send(new UpdateTimePassed(TimeSpan.Zero));
            // round 0 end
            _ctx.Send(new UpdateTimePassed(TimeSpan.Zero));
            // round 1 start
            _ctx.Send(new UpdateTimePassed(TimeSpan.Zero));
            // player turn start
            _ctx.Send(new UpdateTimePassed(TimeSpan.Zero));
        }
    }
}
