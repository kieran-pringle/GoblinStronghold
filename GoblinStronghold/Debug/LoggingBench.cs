using System;
using System.Linq;
using System.Diagnostics;
using SadConsole;
using GoblinStronghold.ECS;
using GoblinStronghold.Graphics.Util;
using GoblinStronghold.Log.Messages;
using GoblinStronghold.Log.Systems;
using GoblinStronghold.Time.Messages;
using System.Runtime.InteropServices;


namespace GoblinStronghold.Debug
{
	public class LoggingBench
	{
        public static void Load(IContext context, RootScreen screen)
        {
			SadConsole.Console logScreen = screen.LogConsole();
            var logSystem = new LogSystem(logScreen);

            context.Register<LogMessage>(logSystem);
            context.Register<RenderTimePassed>(logSystem);

            context.Register(new RandomLogStream());
		}

        private class RandomLogStream : ISystem<UpdateTimePassed>
        {
            private static TimeSpan s_ms = TimeSpan.FromMilliseconds(2000);
            private static Random s_r = new Random();
            private static string s_chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            private IContext _ctx;
            private Stopwatch _stopwatch;

            private static string[] s_pangrams =
            {
                "Sphinx of black quartz, judge my vow!",
                "The quick brown fox jumps over the lazy dog.",
                "Waltz, bad nymph, for quick jigs vex.",
                "Glib jocks quiz nymph to vex dwarf.",
                "How quickly daft jumping zebras vex!",
                "The five boxing wizards jump quickly.",
                "Jackdaws love my big sphinx of quartz.",
                "Pack my box with five dozen liquor jugs."
            };

            private static string s_loremIpsum = @"Lorem ipsum dolor sit amet,
consectetur adipiscing elit. Duis vulputate congue faucibus. Morbi tellus sem,
fermentum vitae ante sed, mattis imperdiet dui.";

            public IContext Context
            {
                get => _ctx;
                set => _ctx = value;
            }

            public RandomLogStream()
            {
                _stopwatch = new Stopwatch();
                _stopwatch.Start();
            }

            public void Handle(UpdateTimePassed message)
            {
                if (_stopwatch.Elapsed > s_ms)
                {
                    var len = s_r.Next(0, 32);
                    // log some random text
                    var msg = new LogMessage(
                        s_loremIpsum,
                        RandomDice());
                    _ctx.Send(msg);
                    _stopwatch.Restart();
                }
            }

            private string RandomString(int length)
            {
                return new string(Enumerable.Repeat(s_chars, length)
                    .Select(s => s[s_r.Next(s.Length)]).ToArray());
            }

            private string RandomPangram()
            {
                var pangram = s_pangrams[s_r.Next(0, s_pangrams.Length)];
                if (s_r.Next(0,2) > 0)
                {
                    pangram = pangram.ToUpper();
                }
                return pangram;
            }

            private DiceRoll RandomDice()
            {
                var diceToRoll = s_r.Next(1, 13);
                var roll = new DiceRoll.Builder();
                for (int i = 0; i < diceToRoll; i++)
                {
                    roll.WithDie(
                        s_r.Next(1, 7),
                        s_r.Next(0, 2) > 0);
                }
                roll.WithModifier(s_r.Next(-10, 11));
                return roll.Build();
            }
        }
    }
}
