using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace AstroFlare
{
    enum SpriteSheetMode
    {
        Normal = 0,
        NoDamageTexture = 1,
    }

    class SpriteSheet
    {
        public static List<SpriteSheet> SpriteSheets = new List<SpriteSheet>();

        string assetName;
        int tilesX;
        int tilesY;
        int tileWidth;
        int tileHeight;
        int frameCount;
        double frameInterval;
        SpriteSheetMode mode;
        Texture2D[] textures;
        Color color;
        float layerDepth = 0.5f;

        public int TilesX { get { return this.tilesX; } }
        public int TileWidth { get { return this.tileWidth; } }
        public int TileHeight { get { return this.tileHeight; } }
        public int FrameCount { get { return this.frameCount; } }
        public double FrameInterval { get { return this.frameInterval; } }
        public Texture2D[] Textures { get { return this.textures; } }
        public Color Color { get { return this.color; } }
        public float LayerDepth { get { return this.layerDepth; } }

        public SpriteSheet(string assetName, int tilesX, int tilesY, double frameRate)
            : this(assetName, tilesX, tilesY, frameRate, tilesX * tilesY, Color.White, 0.5f, SpriteSheetMode.Normal) { }

        public SpriteSheet(string assetName, int tilesX, int tilesY, double frameRate, SpriteSheetMode mode)
            : this(assetName, tilesX, tilesY, frameRate, tilesX * tilesY, Color.White, 0.5f, mode) { }

        public SpriteSheet(string assetName, int tilesX, int tilesY, double frameRate, Color color, SpriteSheetMode mode)
            : this(assetName, tilesX, tilesY, frameRate, tilesX * tilesY, color, 0.5f, mode) { }

        public SpriteSheet(string assetName, int tilesX, int tilesY, double frameRate, Color color, float layerDepth, SpriteSheetMode mode)
            : this(assetName, tilesX, tilesY, frameRate, tilesX * tilesY, color, layerDepth, mode) { }

        public SpriteSheet(string assetName, int tilesX, int tilesY, double frameRate, int frameCount, SpriteSheetMode mode)
            : this(assetName, tilesX, tilesY, frameRate, frameCount, Color.White, 0.5f, mode) { }

        public SpriteSheet(string assetName, int tilesX, int tilesY, double frameRate, int frameCount, Color color, float layerDepth, SpriteSheetMode mode)
        {
            SpriteSheets.Add(this);
            this.textures = new Texture2D[2];
            this.assetName = assetName;
            this.tilesX = tilesX;
            this.tilesY = tilesY;
            this.frameCount = frameCount;
            this.frameInterval = 1 / frameRate;
            this.color = color;
            this.mode = mode;
            this.layerDepth = layerDepth;
        }

        void SetTexture(Texture2D texture, GraphicsDevice graphicsDevice)
        {
            this.textures[0] = texture;

            //if (mode == SpriteSheetMode.Normal)
            //    this.textures[1] = MakeDamageTexture(texture, graphicsDevice);
            //else
                this.textures[1] = null;

            this.tileWidth = texture.Width / tilesX;
            this.tileHeight = texture.Height / tilesY;
        }

        public static void LoadContent(ContentManager content, GraphicsDevice graphicsDevice)
        {
            foreach (SpriteSheet spriteSheet in SpriteSheets)
            {
                spriteSheet.SetTexture(
                    content.Load<Texture2D>(spriteSheet.assetName),
                    graphicsDevice);
            }
        }

        //static Texture2D MakeDamageTexture(Texture2D texture, GraphicsDevice graphicsDevice)
        //{
        //    int pixelCount = texture.Width * texture.Height;
        //    Color[] pixels = new Color[pixelCount];
        //    texture.GetData<Color>(pixels);

        //    for (int i = 0; i < pixels.Length; i++)
        //    {
        //        //int offset = 200;
        //        //byte r = (byte)Math.Min(pixels[i].R + offset, 255);
        //        //byte g = (byte)Math.Min(pixels[i].R + offset, 255);
        //        //byte b = (byte)Math.Min(pixels[i].R + offset, 255);
        //        //pixels[i] = new Color(r, g, b, pixels[i].A);
        //        pixels[i] = new Color(pixels[i].R, pixels[i].R, pixels[i].R, 0);
        //    }


        //    Texture2D outTexture = new Texture2D(graphicsDevice, texture.Width, texture.Height, false, SurfaceFormat.Color);
        //    outTexture.SetData<Color>(pixels);
        //    return outTexture;
        //}
    }
}
