using SadConsole.Input;
using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;

namespace GoblinStronghold.Input
{
    public class KeyboardInputBuffer
    {

        private readonly Dictionary<Keys, TimeSpan> _buffer =
            new Dictionary<Keys, TimeSpan>();
        private readonly HashSet<Keys> _newThisFrame =
            new HashSet<Keys>();

        // how many frames to be held before included again
        private readonly int _frameDelay;
        // how many frames a second
        private readonly int _frameLength;
        // a little delay to avoid laggy input
        private readonly int _error = 100;

        public KeyboardInputBuffer(int frameDelay, int frameLength)
        {
            _frameDelay = frameDelay;
            _frameLength = frameLength;
        }

        public void Add(Keys[] pressedKeys, TimeSpan elapsed)
        {
            // get keys in dict keys but not in currently pressed
            var released = new HashSet<Keys>(_buffer.Keys);
            released.RemoveWhere(k => pressedKeys.Contains(k));

            // remove them
            foreach (Keys k in released)
            {
                if (!_newThisFrame.Contains(k))
                {
                    _buffer.Remove(k);
                }
            }

            // update and add times for other keys
            foreach (Keys k in pressedKeys)
            {
                if (_buffer.ContainsKey(k))
                {
                    _buffer[k] = _buffer[k].Add(elapsed);
                }
                else
                {
                    // treat it as just pressed
                    _newThisFrame.Add(k);
                    _buffer[k] = elapsed;
                }
            }
        }

        // return keys only pressed for less than one frame
        // or more than n frames
        public HashSet<Keys> Flush()
        {
            var result = _buffer
                .Where(kv => IsKeyHeldValid(kv.Key, kv.Value))
                .Select(kv => kv.Key)
                .ToHashSet();
            _newThisFrame.Clear();
            return result;
        }

        // filter out keys that may have been released, but include ones that were just pressed
        private bool IsKeyHeldValid(Keys key, TimeSpan holdTime)
        {
            return _newThisFrame.Contains(key)
                || (holdTime.TotalMilliseconds > (_frameLength * _frameDelay))
                || (holdTime.TotalMilliseconds > (holdTime.TotalMilliseconds + _error));
        }
    }
}

