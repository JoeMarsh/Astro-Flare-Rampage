using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AstroFlare
{
    class Sprite
    {
        SpriteSheet sheet;
        public int Frame;
        double frameTimeRemaining;
        public Color Color;
        float layerDepth;
        public int Width;
        public int Height;
        public float Scale;

        public Vector2 Origin;
        public event NotifyHandler Loop;
        public Texture2D Texture;

        //public int Width { get { return this.sheet.TileWidth; } }
        //public int Height { get { return this.sheet.TileHeight; } }

        //public Texture2D Texture
        //{
        //    get { return this.sheet.Textures[textureIndex]; }
        //}

        //uint textureIndex;
        //public uint TextureIndex
        //{
        //    get { return this.textureIndex; }
        //    set
        //    {
        //        if (value < this.sheet.Textures.Length && this.sheet.Textures[value] != null)
        //            this.textureIndex = value;
        //    }
        //}

        public void ColorLerp(Color color1, Color color2, float amount)
        {
            byte r = (byte)MathHelper.Lerp(color1.R, color2.R, amount);
            byte g = (byte)MathHelper.Lerp(color1.G, color2.G, amount);
            byte b = (byte)MathHelper.Lerp(color1.B, color2.B, amount);
            byte a = (byte)MathHelper.Lerp(color1.A, color2.A, amount);

            this.Color = new Color(r, g, b, a);
        }

        public Rectangle FrameBounds
        {
            get
            {
                int x = Frame % sheet.TilesX * sheet.TileWidth;
                int y = Frame / sheet.TilesX * sheet.TileHeight;
                return new Rectangle(x, y, sheet.TileWidth, sheet.TileHeight);
            }
        }

        public Sprite(SpriteSheet spriteSheet)
        {
            this.sheet = spriteSheet;
            this.frameTimeRemaining = sheet.FrameInterval;
            this.Origin = new Vector2(spriteSheet.TileWidth / 2, spriteSheet.TileHeight / 2);
            this.Color = sheet.Color;
            this.layerDepth = sheet.LayerDepth;
            this.Width = this.sheet.TileWidth;
            this.Height = this.sheet.TileHeight;
            this.Texture = this.sheet.Textures[0];
            this.Scale = 1.0f;
        }

        public void Update(GameTime gameTime)
        {
            this.frameTimeRemaining -= gameTime.ElapsedGameTime.TotalSeconds;
            if (this.frameTimeRemaining <= 0)
            {
                this.Frame++;
                if (this.Frame >= this.sheet.FrameCount)
                {
                    this.Frame = 0;
                    if (this.Loop != null)
                        this.Loop();
                }

                this.frameTimeRemaining = this.sheet.FrameInterval;
            }
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, float rotation)
        {
            spriteBatch.Draw(this.Texture, position, this.FrameBounds, Color, rotation, this.Origin, Scale, SpriteEffects.None, layerDepth);
        }
    }
}
