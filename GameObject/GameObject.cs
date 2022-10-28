using SadConsole;
using SadRogue.Primitives;

namespace SadConsoleGame
{
    /// <summary>
    /// <c>GameObject</c> is a base class for all objects
    /// </summary>
    public class GameObject
    {
        public Point Position { get; set; }

        public ColoredGlyph Appearance { get; private set; }

        public GameObject(
            ColoredGlyph appearance,
            Point position,
            IScreenSurface hostSurface)
        {
            Appearance = appearance;
            Position = position;
            DrawGameObject(hostSurface);
        }

        public void Move(Point newPosition, IScreenSurface screenSurface)
        {
            Position = newPosition;
            DrawGameObject(screenSurface);
        }

        private void DrawGameObject(IScreenSurface screenSurface)
        {
            // get the glyph at a given position
            ColoredGlyph glyphAtPos = screenSurface.Surface[Position];
            // overwrite it
            Appearance.CopyAppearanceTo(glyphAtPos);
            // tell the screen surface it needs to redraw
            screenSurface.IsDirty = true;
        }
    }
}