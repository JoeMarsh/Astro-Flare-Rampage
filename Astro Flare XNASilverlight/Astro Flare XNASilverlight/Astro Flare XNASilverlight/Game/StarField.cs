using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AstroFlare
{
    class StarField
    {
        private List<SpriteOld> stars = new List<SpriteOld>();
        private int screenWidth = 1600;
        private int screenHeight = 800;
        private Random rand = new Random();
        private Color[] colors = { Color.White, Color.Yellow, Color.Wheat, Color.WhiteSmoke, Color.SlateGray };

        public StarField(int screenWidth, int screenHeight, int starCount, Vector2 starVelocity, Texture2D texture, Rectangle frameRectangle)
        {
            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;
            for (int i = 0; i < starCount; i++)
            {
                if (i < (starCount / 3))
                {
                    stars.Add(new SpriteOld(new Vector2(rand.Next(0, screenWidth), rand.Next(0, screenHeight)), texture, frameRectangle, starVelocity / 3));
                }
                else if (i >= (starCount / 3) && i < ((starCount / 3) * 2))
                {
                    stars.Add(new SpriteOld(new Vector2(rand.Next(0, screenWidth), rand.Next(0, screenHeight)), texture, frameRectangle, starVelocity / 2));
                }
                else
                {
                    stars.Add(new SpriteOld(new Vector2(rand.Next(0, screenWidth), rand.Next(0, screenHeight)), texture, frameRectangle, starVelocity));
                }

                Color starColor = colors[rand.Next(0, colors.Count())];
                starColor *= (float)(rand.Next(30, 80) / 100f);
                stars[stars.Count() - 1].TintColor = starColor;
            }
        }

        public void Update(GameTime gameTime)
        {
            int count = stars.Count;
            for (int i = 0; i < count; i++)
            {
                stars[i].Update(gameTime);
                if (stars[i].Location.Y > screenHeight)
                {
                    stars[i].Location = new Vector2(rand.Next(0, screenWidth), 0);
                }
            }
            //foreach (SpriteOld star in stars)
            //{
            //    star.Update(gameTime);
            //    if (star.Location.Y > screenHeight)
            //    {
            //        star.Location = new Vector2(rand.Next(0, screenWidth), 0);
            //    }
            //}
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            int count = stars.Count;
            for (int i = 0; i < count; i++)
            {
                stars[i].Draw(spriteBatch);
            }

            //foreach(SpriteOld star in stars)
            //{
            //    star.Draw(spriteBatch);
            //}
        }
    }
}
