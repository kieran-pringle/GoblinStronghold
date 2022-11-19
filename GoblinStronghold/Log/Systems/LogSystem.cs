using System;
using System.Collections.Generic;
using SadConsole;
using GoblinStronghold.ECS;
using GoblinStronghold.ECS.Messaging;
using GoblinStronghold.Log.Messages;
using GoblinStronghold.Log.Util;
using GoblinStronghold.Time.Messages;

namespace GoblinStronghold.Log.Systems
{
	public class LogSystem :
		ISystem<RenderTimePassed>,
		ISystem<LogMessage>
	{
        // total log history to save
        private static int s_capacity = 128;

        private IContext _ctx;
        private int _head = 0; // points to oldest message or empty
        private LogMessage[] _logMessages = new LogMessage[s_capacity];
        private bool _newLogMessages = false;

        private ICellSurface _surface;
        private int _bottomRow => _surface.Height - 1;

		public LogSystem(ICellSurface surface) 
		{
            _surface = surface;
		}

        IContext ISystem<RenderTimePassed>.Context {
            get => _ctx;
            set => _ctx = value;
        }
        IContext ISystem<LogMessage>.Context
        {
            get => _ctx;
            set => _ctx = value;
        }

        void ISubscriber<RenderTimePassed>.Handle(RenderTimePassed message)
        {
            if (_newLogMessages)
            {
                _surface.Clear();
                PrintLogHistory();
                _surface.IsDirty = true;
            }
        }

        void ISubscriber<LogMessage>.Handle(LogMessage message)
        {
            _newLogMessages = true;
            _logMessages[_head] = message;
            _head = NegSafeIndexModulo(_head + 1);
        }

        private void PrintLogHistory()
        {
            var currentRow = 0;
            var messagesPrinted = 0;
            var currentMsgIdx = NegSafeIndexModulo(_head - 1);
            while (StillMessagesThatCanFit(currentRow, messagesPrinted))
            {
                // dice roll
                var nextMsg = _logMessages[currentMsgIdx];
                if (nextMsg.Roll.HasValue)
                {
                    currentRow += PrintDiceRollGetLinesUsed(currentRow, nextMsg.Roll.Value);
                }
                // string message
                var nextMsgString = _logMessages[currentMsgIdx].Message;
                if (nextMsgString != null)
                {
                    currentRow += PrintMessageGetLinesUsed(currentRow, nextMsgString);
                    messagesPrinted += 1;
                    currentMsgIdx = NegSafeIndexModulo(currentMsgIdx - 1);
                }
                else
                {
                    break;
                }
            }
        }

        private int PrintDiceRollGetLinesUsed(int currentRow, DiceRoll value)
        {
            var rowOffset = LineOffset(currentRow);
            var col = 0;
            foreach (var die in value.Dice)
            {
                var glyphs = DieGlyphs(die.Value, die.Used);
                _surface.SetCellAppearance(col, rowOffset, glyphs.Item1);
                col += 1;
                _surface.SetCellAppearance(col, rowOffset, glyphs.Item2);
                col += 1;
            }
            var mod = value.Modifier;
            var modifierDetail = "";
            if (mod != 0)
            {
                var sign = mod > 0 ? "+" : "";
                modifierDetail = $"{sign}{mod}";
            }
            _surface.Print(col, rowOffset, $"{modifierDetail}={value.Total}");
            return 1;
        }

        private bool StillMessagesThatCanFit(int currentRow, int messagesPrinted)
        {
            return currentRow < _surface.Height
                && messagesPrinted < _logMessages.Length;
        }

        private int PrintMessageGetLinesUsed(int endAtRow, String text)
        {
            var lines = BuildLines(text);
            var linesUsed = lines.Count;

            for (int currentLine = 0; currentLine < linesUsed; currentLine++)
            {
                _surface.Print(
                    0,
                    LineOffset(endAtRow) - currentLine,
                    lines.Pop());
            }

            return linesUsed;
        }

        private Stack<String> BuildLines(String text)
        {
            var words = new Queue<String>(text.Split(" "));
            var lines = new Stack<String>();
            while (words.Count > 0)
            {
                var thisLine = "";
                var nextWord = "";
                var hasWords = words.TryPeek(out nextWord);
                // TODO: maybe use hyphenation to hanld case where word is too
                // long to fit on even a single line
                while (hasWords && NextWordCanFitInLine(thisLine, nextWord))
                {
                    var join = thisLine.Length > 0 ? " " : "";
                    thisLine += (join + words.Dequeue());
                    hasWords = words.TryPeek(out nextWord);
                }
                lines.Push(thisLine);
            }
            return lines;
        }

        private bool NextWordCanFitInLine(string currentLine, string nextWord)
        {
            return (currentLine.Length + nextWord.Length + 1) <= _surface.Width;
        }

        private (ColoredGlyph, ColoredGlyph) DieGlyphs(int val, bool bright)
        {
            var mod = bright ? "bright" : "dim";
            return (
                LogFont.ColoredGlyph($"dice-{mod}-{val}-left"),
                LogFont.ColoredGlyph($"dice-{mod}-{val}-right")
            );
        }

        private int NegSafeIndexModulo(int x)
        {
            return (x % s_capacity + s_capacity) % s_capacity;
        }

        private int LineOffset(int currentRow)
        {
            return _bottomRow - currentRow;
        }
    }
}
