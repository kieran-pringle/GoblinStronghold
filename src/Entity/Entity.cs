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

        private ColoredGlyph _mapAppearance = new ColoredGlyph();

        public GameObject(
            ColoredGlyph appearance,
            Point position,
            IScreenSurface hostSurface)
        {
            Appearance = appearance;
            Position = position;

            hostSurface.Surface[position].CopyAppearanceTo(_mapAppearance);
            DrawGameObject(hostSurface);
        }

        public void Move(Point newPosition, IScreenSurface screenSurface)
        {
            // Restore the old cell
            _mapAppearance.CopyAppearanceTo(screenSurface.Surface[Position]);

            // Store the map cell of the new position
            screenSurface.Surface[newPosition].CopyAppearanceTo(_mapAppearance);

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