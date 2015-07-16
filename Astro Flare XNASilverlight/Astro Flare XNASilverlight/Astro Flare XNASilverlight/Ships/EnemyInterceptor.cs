using System;
using Microsoft.Xna.Framework;

namespace AstroFlare
{
    class EnemyInterceptor : Enemy
    {
        GameNode target;
        Timer fireTimer;
        //float orientation;
        //Vector2 wanderDirection;
        Sprite baseTexture;
        Vector2 moveTo;

        //Sidearm sidearm1;
        //Sidearm sidearm2;
        //Sidearm sidearm3;
        //Sidearm sidearm4;


        public EnemyInterceptor(SpriteSheet spriteSheet)
            : base(spriteSheet)
        {
            this.Health = Config.EnemyHealth;
            this.Speed = Config.EnemySpeed;
            this.FireAction = new FireAction(enemyProjectileBullet.enemyFireBullet);
            this.Weapon = new WeaponSingle(this);
            
            this.Weapon.Direction = new Vector2(0, 1);

            this.fireTimer = new Timer();
            this.fireTimer.Fire += new NotifyHandler(fireTimer_Fire);

            this.baseTexture = new Sprite(Config.EnemyInterceptorSpriteSheetBase);
            this.baseTexture.Color = Color.Turquoise;

            moveTo = new Vector2(Config.Rand.Next(50, 1550), Config.Rand.Next(50, 910));

            //sidearm1 = new Sidearm(Config.EnemyChaserSpriteSheet, this, new Vector2(30, 0));
            //sidearm1.Position = new Vector2(Config.WorldBoundsX / 2, Config.WorldBoundsY / 2 - this.Sprite.Origin.Y);
            //sidearm1.SetParent(this);

            //sidearm2 = new Sidearm(Config.EnemyChaserSpriteSheet, this, new Vector2(30, 0));
            //sidearm2.Position = new Vector2(Config.WorldBoundsX / 2, Config.WorldBoundsY / 2 - this.Sprite.Origin.Y);
            //sidearm2.SetParent(this);

            //sidearm3 = new Sidearm(Config.EnemyChaserSpriteSheet, this, new Vector2(30, 0));
            //sidearm3.Position = new Vector2(Config.WorldBoundsX / 2, Config.WorldBoundsY / 2 - this.Sprite.Origin.Y);
            //sidearm3.SetParent(this);

            //sidearm4 = new Sidearm(Config.EnemyChaserSpriteSheet, this, new Vector2(30, 0));
            //sidearm4.Position = new Vector2(Config.WorldBoundsX / 2, Config.WorldBoundsY / 2 - this.Sprite.Origin.Y);
            //sidearm4.SetParent(this);
        }

        void fireTimer_Fire()
        {
            if (this.Dead)
                return;

            this.Weapon.StopFire();

            if (Player.Ship != null)
                this.target = Player.Ship;
            else
                this.target = null;

            if (this.target == null)
                return;

            this.Weapon.Direction = Vector2.Normalize(target.Position - this.Weapon.Position);

            this.Weapon.StartFire();
        }

        public override void Remove()
        {
            if (this.fireTimer != null)
                this.fireTimer.Stop();

            fireTimer = null;
            base.Remove();
        }

        public override void Update(TimeSpan gameTime)
        {
            if (this.moveTo.X < 5)
                moveTo = new Vector2(Config.Rand.Next(50, 1550), Config.Rand.Next(50, 910));
            else if (this.moveTo.X > Config.WorldBoundsX - 5)
                moveTo = new Vector2(Config.Rand.Next(50, 1550), Config.Rand.Next(50, 910));

            if (this.moveTo.Y < 5)
                moveTo = new Vector2(Config.Rand.Next(50, 1550), Config.Rand.Next(50, 910));
            else if (this.moveTo.Y > Config.WorldBoundsY - 5)
                moveTo = new Vector2(Config.Rand.Next(50, 1550), Config.Rand.Next(50, 910));

            if (Vector2.Distance(this.Position, this.moveTo) < 50f)
                moveTo = new Vector2(Config.Rand.Next(50, 1550), Config.Rand.Next(50, 910));

            this.Rotation = Steering.TurnToFace(this.Position, moveTo, this.Rotation, 0.1f);

            this.Direction = new Vector2((float)Math.Cos(this.Rotation), (float)Math.Sin(this.Rotation));
            this.Direction.Normalize();

            clampToViewport();

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
                this.Position = new Vector2(this.Sprite.Origin.X, this.Position.Y);
            else if (this.Position.X > Config.WorldBoundsX - this.Sprite.Origin.X)
                this.Position = new Vector2(Config.WorldBoundsX - this.Sprite.Origin.X, this.Position.Y);

            if (this.Position.Y < this.Sprite.Origin.Y)
                this.Position = new Vector2(this.Position.X, this.Sprite.Origin.Y);
            else if (this.Position.Y > Config.WorldBoundsY - this.Sprite.Origin.Y)
                this.Position = new Vector2(this.Position.X, Config.WorldBoundsY - this.Sprite.Origin.Y);
        }       
    }
}
