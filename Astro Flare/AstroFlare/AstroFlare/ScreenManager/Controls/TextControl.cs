//-----------------------------------------------------------------------------
// TextControl.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace AstroFlare
{
    /// <summary>
    /// TextControl is a control that displays a single string of text. By default, the
    /// size is computed from the given text and spritefont.
    /// </summary>
    public class TextControl : Control
    {
        private SpriteFont font;
        private string text;
        bool dropShadow = false;

        public Color Color;

        // Actual text to draw
        public string Text
        {
            get { return text; }
            set
            {
                if (text != value)
                {
                    text = value;
                    InvalidateAutoSize();
                }
            }
        }

        // Font to use
        public SpriteFont Font
        {
            get { return Font; }
            set
            {
                if (font != value)
                {
                    font = value;
                    InvalidateAutoSize();
                }
            }
        }

        public TextControl()
            : this(string.Empty, null, Color.White, Vector2.Zero)
        {
        }

        public TextControl(string text, SpriteFont font)
            : this(text, font, Color.White, Vector2.Zero)
        {
        }

        public TextControl(string text, SpriteFont font, Color color)
            : this(text, font, color, Vector2.Zero)
        {
        }

        public TextControl(string text, SpriteFont font, Color color, Vector2 position)
        {
            this.text = text;
            this.font = font;
            this.Position = position;
            this.Color = color;
        }

        Vector2 centerPos;
        public TextControl(string text, SpriteFont font, Color color, Vector2 position, bool centered, bool dropShadow)
        {
            this.text = text;
            this.font = font;
            if (centered == true)
            {
                centerPos.X = 400 - font.MeasureString(text).X / 2;
                centerPos.Y = position.Y;
                this.Position = centerPos;
            }
            else
            {
                this.Position = position;
            }

            this.dropShadow = dropShadow;
            this.Color = color;
        }

        public override void Draw(DrawContext context)
        {
            base.Draw(context);

            if (dropShadow)
                context.SpriteBatch.DrawString(font, Text, context.DrawOffset + new Vector2(2,2), Color.Black);

            context.SpriteBatch.DrawString(font, Text, context.DrawOffset, Color);
        }

        override public Vector2 ComputeSize()
        {
            return font.MeasureString(Text);
        }
    }
}
