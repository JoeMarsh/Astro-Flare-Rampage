using System;
using Microsoft.Xna.Framework;

namespace AstroFlare
{
    class EnemyDasher : Enemy
    {
        Sprite baseTexture;

        public EnemyDasher(SpriteSheet spriteSheet)
            : base(spriteSheet)
        {
            this.Health = Config.EnemyHealth;
            this.Speed = Config.EnemyDasherSpeed;
            
            this.baseTexture = new Sprite(Config.EnemyDasherSpriteSheetBase);
            this.baseTexture.Color = Color.DarkOrange;
        }

        public override void Remove()
        {
            base.Remove();
        }

        public override void Update(GameTime gameTime)
        {
            //this.baseTexture.ColorLerp(Config.DamageColor, Config.EnemyColor, ((float)this.Health / Config.EnemyHealth));

            this.RemoveOffScreen();

            if (this.CollisionList != null)
            {
                for (int i = CollisionList.Count - 1; i >= 0; i--)
                {
                    if (Node.CheckCollision(this, CollisionList[i]))
                        this.Collide(this.CollisionList[i]);
                }
            }

            this.Rotation = (float)Math.Atan2(Direction.Y, Direction.X);

            clampToViewport();
            this.Sprite.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Explode()
        {
            for (int i = 0; i < Config.Rand.Next(1, 5); i++)
            {
                Coin coin = new Coin(Config.CoinSpriteSheet);
                coin.Position = this.Position + new Vector2(Config.Rand.Next(0, 20) - 10, Config.Rand.Next(0, 20) - 10);
                //GameStateManagementGame.Instance.soundManager.PlaySound("ShipExplode", 0.3f);
            }

            base.Explode();
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            this.baseTexture.Draw(spriteBatch, this.Position, this.Rotation);
            base.Draw(spriteBatch);            
        }

        private void clampToViewport()
        {
            if (this.Position.X < this.Sprite.Origin.X)
            {
                this.Position = new Vector2(this.Sprite.Origin.X, this.Position.Y);
                this.Direction = Vector2.Negate(this.Direction);
            }
            else if (this.Position.X > Config.WorldBoundsX - this.Sprite.Origin.X)
            {
                this.Position = new Vector2(Config.WorldBoundsX - this.Sprite.Origin.X, this.Position.Y);
                this.Direction = Vector2.Negate(this.Direction);
            }

            if (this.Position.Y < this.Sprite.Origin.Y)
            {
                this.Position = new Vector2(this.Position.X, this.Sprite.Origin.Y);
                this.Direction = Vector2.Negate(this.Direction);
            }
            else if (this.Position.Y > Config.WorldBoundsY - this.Sprite.Origin.Y)
            {
                this.Position = new Vector2(this.Position.X, Config.WorldBoundsY - this.Sprite.Origin.Y);
                this.Direction = Vector2.Negate(this.Direction);
            }
        }       
    }
}
