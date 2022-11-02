using System;
using SadConsole;
using SadRogue.Primitives;
using GoblinStronghold.Graphics;

namespace GoblinStronghold.Graphics.Drawer
{
    public class BorderDrawer
    {
        // only used by BorderDrawer
        public class BorderStyle
        {
            internal ColoredGlyph Top;
            internal ColoredGlyph Bottom;
            internal ColoredGlyph Left;
            internal ColoredGlyph Right;
            internal ColoredGlyph CornerTopLeft;
            internal ColoredGlyph CornerTopRight;
            internal ColoredGlyph CornerBottomLeft;
            internal ColoredGlyph CornerBottomRight;
            internal Color Foreground;
            internal Color Background;

            public BorderStyle(
                    string top = "border-horizontal",
                    string bottom = "border-horizontal",
                    string left = "border-vertical",
                    string right = "border-vertical",
                    string cornerTopLeft = "border-top-left-corner",
                    string cornerTopRight = "border-top-right-corner",
                    string cornerBottomLeft = "border-bottom-left-corner",
                    string cornerBottomRight = "border-bottom-right-corner",
                    Color? foreground = null,
                    Color? background = null
                )
            {
                if (foreground == null)
                {
                    foreground = Color.White;
                }
                if (background == null)
                {
                    background = Color.Black;
                }
                Foreground = (Color)foreground;
                Background = (Color)background;

                Top = ToGlyph(top);
                Bottom = ToGlyph(bottom);
                Right = ToGlyph(right);
                Left = ToGlyph(left);
                CornerTopLeft = ToGlyph(cornerTopLeft);
                CornerTopRight = ToGlyph(cornerTopRight);
                CornerBottomLeft = ToGlyph(cornerBottomLeft);
                CornerBottomRight = ToGlyph(cornerBottomRight);
        }

            private ColoredGlyph ToGlyph(string name)
            {
                return TileSet.ColoredGlyph(
                    name,
                    Foreground,
                    Background);
            }
        }

        public static BorderStyle Default = new BorderStyle();
        public static BorderStyle Double = new BorderStyle(
            top: "border-horizontal-double",
            bottom: "border-horizontal-double",
            left: "border-vertical-double",
            right: "border-vertical-double",
            cornerTopLeft: "border-top-left-double-corner",
            cornerTopRight: "border-top-right-double-corner",
            cornerBottomLeft: "border-bottom-left-double-corner",
            cornerBottomRight: "border-bottom-right-double-corner");
        public static BorderStyle OffsetSmall = new BorderStyle(
            top: "border-offset-bottom",
            bottom: "border-offset-top",
            left: "border-offset-right",
            right: "border-offset-left",
            cornerTopLeft: "border-offset-top-left-inside-corner",
            cornerTopRight: "border-offset-top-right-inside-corner",
            cornerBottomLeft: "border-offset-bottom-left-inside-corner",
            cornerBottomRight: "border-offset-bottom-right-inside-corner");
        public static BorderStyle OffsetLarge = new BorderStyle(
            top: "border-offset-top",
            bottom: "border-offset-bottom",
            left: "border-offset-left",
            right: "border-offset-right",
            cornerTopLeft: "border-offset-top-left-outside-corner",
            cornerTopRight: "border-offset-top-right-outside-corner",
            cornerBottomLeft: "border-offset-bottom-left-outside-corner",
            cornerBottomRight: "border-offset-bottom-right-outside-corner");
        public static BorderStyle Diagonal = new BorderStyle(
            cornerTopLeft: "border-top-left-diagonal-corner",
            cornerTopRight: "border-top-right-diagonal-corner",
            cornerBottomLeft: "border-bottom-left-diagonal-corner",
            cornerBottomRight: "border-bottom-right-diagonal-corner");
        public static BorderStyle Fancy = new BorderStyle(
            cornerTopLeft: "border-top-left-loop-corner",
            cornerTopRight: "border-top-right-loop-corner",
            cornerBottomLeft: "border-bottom-left-loop-corner",
            cornerBottomRight: "border-bottom-right-loop-corner");


        public static void Draw(ICellSurface surface, BorderStyle? border = null)
        {
            if (border == null)
            {
                border = Default;
            }

            // draw top and bottom
            for (int x = 1; x < surface.Width - 1; x++)
            {
                surface[x, 0].CopyAppearanceFrom(border.Top);
                surface[x, surface.Height - 1].CopyAppearanceFrom(border.Bottom);
            }
            // draw left and right
            for (int y = 1; y < surface.Height - 1; y++)
            {
                surface[0, y].CopyAppearanceFrom(border.Left);
                surface[surface.Width - 1, y].CopyAppearanceFrom(border.Right);
            }
            // corners
            surface[0, 0].CopyAppearanceFrom(border.CornerTopLeft);
            surface[surface.Width - 1, 0].CopyAppearanceFrom(border.CornerTopRight);
            surface[0, surface.Height - 1].CopyAppearanceFrom(border.CornerBottomLeft);
            surface[surface.Width - 1, surface.Height - 1].CopyAppearanceFrom(border.CornerBottomRight);
        }
    }
}

