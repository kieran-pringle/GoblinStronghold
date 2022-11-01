using System;
using SadConsole;
using SadRogue.Primitives;
using GoblinStronghold.Graphics;

namespace GoblinStronghold.Screen.Drawer
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
                    string cornerTopLeft = "border-top-left",
                    string cornerTopRight = "border-top-right",
                    string cornerBottomLeft = "border-bottom-left",
                    string cornerBottomRight = "border-bottom-right",
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
            cornerTopLeft: "border-top-left-double",
            cornerTopRight: "border-top-right-double",
            cornerBottomLeft: "border-bottom-left-double",
            cornerBottomRight: "border-bottom-right-double");
        public static BorderStyle RoundedSmall = new BorderStyle(
            top: "border-offset-bottom",
            bottom: "border-offset-top",
            left: "border-offset-right",
            right: "border-offset-left",
            cornerTopLeft: "border-top-left-inside",
            cornerTopRight: "border-top-right-inside",
            cornerBottomLeft: "border-bottom-left-inside",
            cornerBottomRight: "border-bottom-right-inside");
        public static BorderStyle RoundedLarge = new BorderStyle(
            top: "border-offset-top",
            bottom: "border-offset-bottom",
            left: "border-offset-left",
            right: "border-offset-right",
            cornerTopLeft: "border-top-left-outside",
            cornerTopRight: "border-top-right-outside",
            cornerBottomLeft: "border-bottom-left-outside",
            cornerBottomRight: "border-bottom-right-outside");
        public static BorderStyle Diagonal = new BorderStyle(
            cornerTopLeft: "border-top-left-diagonal",
            cornerTopRight: "border-top-right-diagonal",
            cornerBottomLeft: "border-bottom-left-diagonal",
            cornerBottomRight: "border-bottom-right-diagonal");
        public static BorderStyle Fancy = new BorderStyle(
            cornerTopLeft: "border-top-left-loop",
            cornerTopRight: "border-top-right-loop",
            cornerBottomLeft: "border-bottom-left-loop",
            cornerBottomRight: "border-bottom-right-loop");


        public static void DrawBorder(ICellSurface surface, BorderStyle? border = null)
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

