using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AstroFlare
{
    enum BarAlignment
    {
        Left = 0,
        Center = 1,
        Right = 2
    }

    class Bar
    {
        private Vector2 position;
        private BarAlignment alignment;
        private int width;
        private int height;
        private Color color;
        private float percent;
        public bool DrawCentered = false;
        Rectangle barRect;

        public static Texture2D Texture;
        public static List<Bar> Bars;

        #region properties

        public Vector2 Position
        {
            get { return this.position; }
            set { this.position = value; }
        }

        public BarAlignment Alignment
        {
            get { return this.alignment; }
            set { this.alignment = value; }
        }

        public int Width
        {
            get { return this.width; }
            set { this.width = value; }
        }

        public int Height
        {
            get { return this.height; }
            set { this.height = value; }
        }

        public float Percent
        {
            get { return this.percent; }
            set { this.percent = value; }
        }

        #endregion

        static Bar()
        {
            Bars = new List<Bar>();
        }

        public Bar(int width, int height, Color color)
        {
            Bars.Add(this);
            this.width = width;
            this.height = height;
            this.color = color;
            this.percent = 1;
            this.alignment = BarAlignment.Left;
        }

        public static void DrawBars(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < Bars.Count; i++)
            {
                Bars[i].Draw(spriteBatch);
            }
        }

        public void Draw(SpriteBatch spritebatch)
        {
            Vector2 origin = new Vector2((float)this.alignment, 0f);
            
            if (DrawCentered == true)
                barRect = new Rectangle((int)this.position.X + (int)((this.width * (1 - this.percent)) / 2), (int)this.position.Y, (int)(this.width * this.percent), this.height);
            else
                barRect = new Rectangle((int)this.position.X, (int)this.position.Y, (int)(this.width * this.percent), this.height);

            spritebatch.Draw(Bar.Texture, barRect, null, this.color, 0f, origin, SpriteEffects.None, 0f);
        }
    }
}
