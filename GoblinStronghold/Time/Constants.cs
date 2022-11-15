using GoblinStronghold.ECS;
using GoblinStronghold.Time.Messages;
using GoblinStronghold.Time.Messages.Turns;
using GoblinStronghold.Time.Systems;

namespace GoblinStronghold.Time
{
    /**
     *  Everything to do with generating events related the passage of time.
     */
    public static class Constants
	{
        public static int ANIMATION_FPS = 2;

        public static void Init(IContext context)
		{
            var turnSystem = new TurnSystem();
            context.Register<UpdateTimePassed>(turnSystem);
            context.Register<CanTakeTurns>(turnSystem);
            context.Register<CanNotTakeTurns>(turnSystem);
            context.Register<TurnTaken>(turnSystem);
            context.Register(new FrameRateNotifier(ANIMATION_FPS));
        }
	}
}
