//-----------------------------------------------------------------------------
// ImageControl.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Microsoft.Xna.Framework.Input.Touch;


namespace AstroFlare
{
    /// <summary>
    /// ImageControl is a control that displays a single sprite. By default it displays an entire texture.
    ///
    /// If a null texture is given, this control will use DrawContext.BlankTexture. This allows it to be
    /// used to draw solid-colored rectangles.
    /// </summary>
    public class ButtonControl : Control
    {
        private Texture2D texture;

        // Position within the source texture, in texels. Default is (0,0) for the upper-left corner.
        public Vector2 origin;

        // Size in texels of source rectangle. If null (the default), size will be the same as the size of the control.
        // You only need to set this property if you want texels scaled at some other size than 1-to-1; normally
        // you can just set the size of both the source and destination rectangles with the Size property.
        public Vector2? SourceSize;

        // Color to modulate the texture with. The default is white, which displays the original unmodified texture.
        public Color Color;

        public bool Active = false;
        bool hasText = false;
        bool hasDoubleText = false;
        bool hasTripleText = false;
        //bool locked = true;

        Rectangle destinationRectangle;

        public String Text;
        private SpriteFont font;

        public String Text2;
        private SpriteFont font2;

        public ButtonControl()
            : this(null, Vector2.Zero)
        {
        }

        public ButtonControl(Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            this.Position = position;
            this.Color = Color.White;
        }

        public ButtonControl(Texture2D texture, Vector2 position, String text, SpriteFont font)
        {
            this.texture = texture;
            this.Position = position;
            this.Color = Color.White;
            this.Text = text;
            this.font = font;
            hasText = true;
        }

        public ButtonControl(Texture2D texture, Vector2 position, String text, SpriteFont font, String text2, SpriteFont font2, bool hasTripleText)
        {
            this.texture = texture;
            this.Position = position;
            this.Color = Color.White;
            this.Text = text;
            this.font = font;
            this.Text2 = text2;
            this.font2 = font2;
            hasDoubleText = true;
            this.hasTripleText = hasTripleText;
        }

        // Texture to draw
        public Texture2D Texture
        {
            get { return texture; }
            set
            {
                if (texture != value)
                {
                    texture = value;
                    InvalidateAutoSize();
                }
            }
        }

        /// <summary>
        /// Invoked when the button is tapped.
        /// </summary>
        public event EventHandler<EventArgs> Tapped;

        /// <summary>
        /// Invokes the Tapped event and allows subclasses to perform actions when tapped.
        /// </summary>
        protected virtual void OnTapped()
        {
            if (Tapped != null)
            {
                Tapped(this, EventArgs.Empty);
                //Active = !Active;
            }
        }

        /// <summary>
        /// Passes a tap location to the button for handling.
        /// </summary>
        /// <param name="tap">The location of the tap.</param>
        /// <returns>True if the button was tapped, false otherwise.</returns>
        public bool HandleTap(Vector2 tap)
        {
            if (tap.X >= destinationRectangle.X &&
                tap.Y >= destinationRectangle.Y &&
                tap.X <= destinationRectangle.X + destinationRectangle.Width &&
                tap.Y <= destinationRectangle.Y + destinationRectangle.Height)
            {
                OnTapped();
                return true;
            }

            return false;
        }

        public override void HandleInput(InputState input)
        {
            // Read in our gestures
            foreach (GestureSample gesture in input.Gestures)
            {
                // If we have a tap
                if (gesture.GestureType == GestureType.Tap)
                {
                    HandleTap(gesture.Position);                   
                }
            }
            base.HandleInput(input);
        }

        public override void Draw(DrawContext context)
        {
            base.Draw(context);
            Texture2D drawTexture = texture ?? context.BlankTexture;

            Vector2 actualSourceSize = SourceSize ?? Size;
            Rectangle sourceRectangle = new Rectangle
            {
                X = (int)origin.X,
                Y = (int)origin.Y,
                Width = (int)actualSourceSize.X,
                Height = (int)actualSourceSize.Y,
            };
            Rectangle destRectangle = new Rectangle
            {
                X = (int)context.DrawOffset.X,
                Y = (int)context.DrawOffset.Y,
                Width = (int)Size.X,
                Height = (int)Size.Y
            };
            destinationRectangle = destRectangle;
            context.SpriteBatch.Draw(drawTexture, destRectangle, sourceRectangle, Color);
            
            if (hasText)
            {
                context.SpriteBatch.DrawString(font, Text, new Vector2(destRectangle.X + destRectangle.Width / 2, destRectangle.Y + destRectangle.Height / 2) + new Vector2(2,2), Color.Black, 0f, 
                    new Vector2(font.MeasureString(Text).X / 2, font.MeasureString(Text).Y / 2), 1f, SpriteEffects.None, 1f);
                context.SpriteBatch.DrawString(font, Text, new Vector2(destRectangle.X + destRectangle.Width / 2, destRectangle.Y + destRectangle.Height / 2), Color.White, 0f,
                    new Vector2(font.MeasureString(Text).X / 2, font.MeasureString(Text).Y / 2), 1f, SpriteEffects.None, 1f);
            }

            if (hasTripleText)
            {
                context.SpriteBatch.DrawString(font, Text, new Vector2(destRectangle.X + destRectangle.Width / 2, destRectangle.Y + destRectangle.Height / 2 - (font.MeasureString(Text).Y / 4) - (font.MeasureString(Text).Y / 2)) + new Vector2(2, 2), Color.Black, 0f,
                    new Vector2(font.MeasureString(Text).X / 2, font.MeasureString(Text).Y / 2), 1f, SpriteEffects.None, 1f);
                context.SpriteBatch.DrawString(font, Text, new Vector2(destRectangle.X + destRectangle.Width / 2, destRectangle.Y + destRectangle.Height / 2 - (font.MeasureString(Text).Y / 4) - (font.MeasureString(Text).Y / 2)), Color.White, 0f,
                    new Vector2(font.MeasureString(Text).X / 2, font.MeasureString(Text).Y / 2), 1f, SpriteEffects.None, 1f);

                context.SpriteBatch.DrawString(font2, Text2, new Vector2(destRectangle.X + destRectangle.Width / 2, destRectangle.Y + destRectangle.Height / 2 + (font.MeasureString(Text2).Y / 4)) + new Vector2(2, 2), Color.Black, 0f,
                    new Vector2(font2.MeasureString(Text2).X / 2, font2.MeasureString(Text2).Y / 2), 1f, SpriteEffects.None, 1f);
                context.SpriteBatch.DrawString(font2, Text2, new Vector2(destRectangle.X + destRectangle.Width / 2, destRectangle.Y + destRectangle.Height / 2 + (font.MeasureString(Text2).Y / 4)), Color.White, 0f,
                    new Vector2(font2.MeasureString(Text2).X / 2, font2.MeasureString(Text2).Y / 2), 1f, SpriteEffects.None, 1f);
            }
            else if (hasDoubleText)
            {
                context.SpriteBatch.DrawString(font, Text, new Vector2(destRectangle.X + destRectangle.Width / 2, destRectangle.Y + destRectangle.Height / 2 - (font.MeasureString(Text).Y / 2)) + new Vector2(2, 2), Color.Black, 0f,
                    new Vector2(font.MeasureString(Text).X / 2, font.MeasureString(Text).Y / 2), 1f, SpriteEffects.None, 1f);
                context.SpriteBatch.DrawString(font, Text, new Vector2(destRectangle.X + destRectangle.Width / 2, destRectangle.Y + destRectangle.Height / 2 - (font.MeasureString(Text).Y / 2)), Color.White, 0f,
                    new Vector2(font.MeasureString(Text).X / 2, font.MeasureString(Text).Y / 2), 1f, SpriteEffects.None, 1f);

                context.SpriteBatch.DrawString(font2, Text2, new Vector2(destRectangle.X + destRectangle.Width / 2, destRectangle.Y + destRectangle.Height / 2 + (font.MeasureString(Text2).Y / 2)) + new Vector2(2, 2), Color.Black, 0f,
                    new Vector2(font2.MeasureString(Text2).X / 2, font2.MeasureString(Text2).Y / 2), 1f, SpriteEffects.None, 1f);
                context.SpriteBatch.DrawString(font2, Text2, new Vector2(destRectangle.X + destRectangle.Width / 2, destRectangle.Y + destRectangle.Height / 2 + (font.MeasureString(Text2).Y / 2)), Color.White, 0f,
                    new Vector2(font2.MeasureString(Text2).X / 2, font2.MeasureString(Text2).Y / 2), 1f, SpriteEffects.None, 1f);
            }
        }

        override public Vector2 ComputeSize()
        {
            if(texture!=null)
            {
                return new Vector2(texture.Width, texture.Height);
            }
            return Vector2.Zero;
        }
    }
}
