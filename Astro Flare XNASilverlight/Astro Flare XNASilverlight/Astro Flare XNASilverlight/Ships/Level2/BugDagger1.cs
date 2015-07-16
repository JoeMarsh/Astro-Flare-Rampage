using System;
using Microsoft.Xna.Framework;

namespace AstroFlare
{
    class BugDagger1 : Enemy
    {
        GameNode target;
        Timer fireTimer;
        //float orientation;
        //Vector2 wanderDirection;
        Sprite baseTexture;
        Vector2 moveTo;
        Random rand = new Random();

        public BugDagger1(SpriteSheet spriteSheet)
            : base(spriteSheet, 1)
        {
            this.Health = Config.EnemyHealth;
            //this.ExplosionSpriteSheet = Config.SmallExplosionSpriteSheet;
            this.Speed = Config.EnemySpeed;
            this.FireAction = new FireAction(enemyProjectileBullet.enemyFireBullet);
            this.Weapon = new WeaponSingle(this);
            
            //WeaponBurstWave burst = new WeaponBurstWave(this, 0, 8, 0.8f);
            //this.Weapon = new WeaponAutoBurst(this, Config.EnemyFireInterval, burst);

            this.Weapon.Direction = new Vector2(0, 1);
            //this.Weapon.Offset = new Vector2(0, this.Sprite.Origin.Y);

            this.fireTimer = new Timer();
            this.fireTimer.Fire += new NotifyHandler(fireTimer_Fire);
            this.fireTimer.Start(Config.EnemyFireInterval);
            this.baseTexture = new Sprite(Config.BugDagger1SpriteSheetBase);
            moveTo = new Vector2(Config.Rand.Next(50, 1550), Config.Rand.Next(50, 910));
        }

        void fireTimer_Fire()
        {
            if (this.Dead)
                return;

            //Pick a random player as target
            //target = GameNode.PickRandomNode(PlayerShip.PlayerShips);

            if (PlayerShip.PlayerShips.Count > 0)
                this.target = PlayerShip.PlayerShips[0];
//testing remove ****************
            else if (Buddy.Buddys.Count > 0)
                this.target = Buddy.Buddys[0];
//********************************
            else
                this.target = null;

            if (this.target == null)
                return;

            this.Weapon.Direction = Vector2.Normalize(target.Position - this.Weapon.Position);

            //makes sure enemy only fires weapon if enemy is above player
            //if (this.Weapon.Direction.Y <= 0f)
            //    return;

            this.Weapon.StartFire();
        }

        public override void Remove()
        {
            this.fireTimer.Stop();
            base.Remove();
        }

        public override void Update(TimeSpan gameTime)
        {
            this.baseTexture.ColorLerp(Config.DamageColor, Config.EnemyColor, ((float)this.Health / Config.EnemyHealth));

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
            for (int i = 0; i < rand.Next(1, 5); i++)
            {
                Coin coin = new Coin(Config.CoinSpriteSheet);
                coin.Position = this.Position + new Vector2(rand.Next(0, 20) - 10, rand.Next(0, 20) - 10);
                //GameStateManagementGame.Instance.soundManager.PlaySound("ShipExplode");
            }

            for (int i = 0; i < 3; i++)
            {
                GameNode node = null;
                node = new BugDagger2(Config.BugDagger2SpriteSheet);
                node.Position = this.Position;
            }

            base.Explode();
        }

        public override void TakeDamage(int amount, GameNode node)
        {
            if (!Invulnerable)
            {
                base.TakeDamage(amount, node);
            }
        }

        public override void Collide(GameNode node)
        {
            if (!Invulnerable)
            {
                base.Collide(node);
            }
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            this.baseTexture.Draw(spriteBatch, this.Position, this.Rotation);
            base.Draw(spriteBatch);            
        }

        //TODO: maybe replace with new class of enemy projectile/projectile bullet type
        //void FireBullet(Vector2 direction, Vector2 position)
        //{
        //    if (!this.Dead)
        //    {
        //        Projectile bullet = new ProjectileBullet(Config.EnemyBulletSheet);
        //        bullet.CollisionList = PlayerShip.PlayerShips;
        //        bullet.Position = position;
        //        bullet.Direction = direction;
        //        bullet.Speed = Config.EnemyBulletSpeed;
        //        bullet.Damage = Config.EnemyBulletDamage;
        //        bullet.ExplosionSpriteSheet = Config.ProjectileExplosion;
        //    }
        //}

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
