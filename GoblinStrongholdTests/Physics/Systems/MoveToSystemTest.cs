using System;
using Moq;
using GoblinStronghold.ECS;
using GoblinStronghold.Physics.Components;
using GoblinStronghold.Physics.Messages;
using GoblinStronghold.Physics.Systems;

namespace GoblinStronghold.Tests.Physics.Systems
{
    [TestFixture]
    public class MoveToSystemTest
    {
        private IContext _ctx;
        private MoveToSystem _moveTo;

        [SetUp]
        public void SetUp()
        {
            _moveTo = new MoveToSystem();
            _ctx = new Context();

            _ctx.Register(_moveTo);
        }

        [TestCase]
        public void MoveToSystemTest_EntityMovesIfPositionEmpty()
        {
            var originalPosition = new Position(0, 0);
            var movingEntity = _ctx.CreateEntity().With(originalPosition);
            var newPosition = new Position(1,1);

            var msg = new MoveTo(
                movingEntity,
                newPosition
            );

            _ctx.Send(msg);

            var positionAfterMsg = movingEntity.Component<Position>().Content();

            Assert.That(
                positionAfterMsg,
                Is.EqualTo(newPosition));
        }

        [TestCase]
        public void MoveToSystemTest_EntityMovesIfPositionHasEntityButMoveAllowed()
        {
            // arrange
            var originalPosition = new Position(0, 0);
            var movingEntity = _ctx.CreateEntity().With(originalPosition);
            var newPosition = new Position(1, 1);

            var collisionHandlerMock = new Mock<ICollisionHandler>();
            // allow moving into this space
            collisionHandlerMock
                .Setup(c => c.Handle(It.IsAny<Entity>()))
                .Returns(true);

            var collidingEntity = _ctx.CreateEntity()
                .With(newPosition)
                .With(new Collision(collisionHandlerMock.Object));

            // act
            var msg = new MoveTo(
                movingEntity,
                newPosition
            );
            _ctx.Send(msg);

            // assert
            collisionHandlerMock.Verify(
                ch => ch.Handle(movingEntity),
                Times.Once()
            );

            var positionAfterMsg = movingEntity.Component<Position>().Content();
            Assert.That(
                positionAfterMsg,
                Is.EqualTo(newPosition)
            );
        }

        [TestCase]
        public void MoveToSystemTest_EntityStaysPutIfCollisionFailed()
        {
            // arrange
            var originalPosition = new Position(0, 0);
            var movingEntity = _ctx.CreateEntity().With(originalPosition);
            var newPosition = new Position(1, 1);

            // don't allow moving into this space
            var rejectCollisionHandler = new Mock<ICollisionHandler>();
            rejectCollisionHandler
                .Setup(c => c.Handle(It.IsAny<Entity>()))
                .Returns(false);
            _ctx.CreateEntity()
                .With(newPosition)
                .With(new Collision(rejectCollisionHandler.Object));

            // act
            var msg = new MoveTo(
                movingEntity,
                newPosition
            );
            _ctx.Send(msg);

            // assert
            rejectCollisionHandler.Verify(
                ch => ch.Handle(movingEntity),
                Times.Once()
            );

            var positionAfterMsg = movingEntity.Component<Position>().Content();
            Assert.That(
                positionAfterMsg,
                Is.EqualTo(originalPosition)
            );
        }

        [TestCase]
        public void MoveToSystemTest_EntityMovesIfPositionHasMultipleEntitesButMoveAllowed()
        {
            // arrange
            var originalPosition = new Position(0, 0);
            var movingEntity = _ctx.CreateEntity().With(originalPosition);
            var newPosition = new Position(1, 1);

            // allow moving into this space
            var allowCollisionHandler = new Mock<ICollisionHandler>();
            allowCollisionHandler
                .Setup(c => c.Handle(It.IsAny<Entity>()))
                .Returns(true);
            for (int i = 0; i < 3; i++)
            {
                _ctx.CreateEntity()
                    .With(newPosition)
                    .With(new Collision(allowCollisionHandler.Object));
            } 

            // act
            var msg = new MoveTo(
                movingEntity,
                newPosition
            );
            _ctx.Send(msg);

            // assert
            allowCollisionHandler.Verify(
                ch => ch.Handle(movingEntity),
                Times.Exactly(3)
            );

            var positionAfterMsg = movingEntity.Component<Position>().Content();
            Assert.That(
                positionAfterMsg,
                Is.EqualTo(newPosition)
            );
        }


        [TestCase]
        public void MoveToSystemTest_EntityStaysPutIfPositionHasMultipleEntitesAndOneDoesNotAllow()
        {
            // arrange
            var originalPosition = new Position(0, 0);
            var movingEntity = _ctx.CreateEntity().With(originalPosition);
            var newPosition = new Position(1, 1);

            // allow moving into this space
            var allowCollisionHandler = new Mock<ICollisionHandler>();
            allowCollisionHandler
                .Setup(c => c.Handle(It.IsAny<Entity>()))
                .Returns(true);
            for (int i = 0; i < 3; i++)
            {
                _ctx.CreateEntity()
                    .With(newPosition)
                    .With(new Collision(allowCollisionHandler.Object));
            }
            // don't allow moving into this space
            var rejectCollisionHandler = new Mock<ICollisionHandler>();
            rejectCollisionHandler
                .Setup(c => c.Handle(It.IsAny<Entity>()))
                .Returns(false);
            _ctx.CreateEntity()
                .With(newPosition)
                .With(new Collision(rejectCollisionHandler.Object));


            // act
            var msg = new MoveTo(
                movingEntity,
                newPosition
            );
            _ctx.Send(msg);

            // assert
            allowCollisionHandler.Verify(
                ch => ch.Handle(movingEntity),
                Times.Exactly(3)
            );
            rejectCollisionHandler.Verify(
                ch => ch.Handle(movingEntity),
                Times.Once()
            );

            var positionAfterMsg = movingEntity.Component<Position>().Content();
            Assert.That(
                positionAfterMsg,
                Is.EqualTo(originalPosition)
            );
        }

        [TestCase]
        public void MoveToSystemTest_EntityMovesAndSideEffect()
        {
            // arrange
            var sideEffect = "I was attached by the handler";

            var originalPosition = new Position(0, 0);
            var movingEntity = _ctx.CreateEntity().With(originalPosition);
            var newPosition = new Position(1, 1);

            var allowCollisionHandler = new Mock<ICollisionHandler>();
            // allow moving into this space
            allowCollisionHandler
                .Setup(c => c.Handle(It.IsAny<Entity>()))
                .Callback<Entity>(e => e.With(sideEffect))
                .Returns(true); 

            var collidingEntity = _ctx.CreateEntity()
                .With(newPosition)
                .With(new Collision(allowCollisionHandler.Object));

            // act
            var msg = new MoveTo(
                movingEntity,
                newPosition
            );
            _ctx.Send(msg);

            // assert
            Assert.That(
                movingEntity.Component<string>().Content(),
                Is.EqualTo(sideEffect)
            );
        }

        [TestCase]
        public void MoveToSystemTest_EntityStaysAndSideEffect()
        {
            // arrange
            var sideEffect = "I was attached by the handler";

            var originalPosition = new Position(0, 0);
            var movingEntity = _ctx.CreateEntity().With(originalPosition);
            var newPosition = new Position(1, 1);

            // don't allow moving into this space
            var rejectCollisionHandler = new Mock<ICollisionHandler>();
            rejectCollisionHandler
                .Setup(c => c.Handle(It.IsAny<Entity>()))
                .Callback<Entity>(e => e.With(sideEffect))
                .Returns(false);
            _ctx.CreateEntity()
                .With(newPosition)
                .With(new Collision(rejectCollisionHandler.Object));

            // act
            var msg = new MoveTo(
                movingEntity,
                newPosition
            );
            _ctx.Send(msg);

            // assert
            Assert.That(
                movingEntity.Component<string>().Content(),
                Is.EqualTo(sideEffect)
            );
        }
    }
}

