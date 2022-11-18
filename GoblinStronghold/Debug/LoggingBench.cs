using System;
using SadConsole;
using GoblinStronghold.ECS;
using GoblinStronghold.Graphics.Util;

namespace GoblinStronghold.Debug
{
	public class LoggingBench
	{
        public static void Load(IContext context, RootScreen screen)
        {
			SadConsole.Console logScreen = screen.LogConsole();

			logScreen.SetBackground(5,5,Palette.Cyan);
			logScreen.Print(0, 2, "A very long message so that we can see if it automatically wraps or anything", Palette.WhiteBright);
			logScreen.IsDirty = true;

			// we will have to handwrite our own log writing
		}
	}
}

