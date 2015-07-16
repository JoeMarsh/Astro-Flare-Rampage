using System;
using Microsoft.Xna.Framework;

namespace AstroFlare
{
    class EnemyAvoider : Enemy
    {
        GameNode target;
        Timer fireTimer;
        //float orientation;
        //Vector2 wanderDirection;
        Sprite baseTexture;
        Vector2 moveTo;
        float rotation;

        public EnemyAvoider(SpriteSheet spriteSheet)
            : base(spriteSheet)
        {
            this.Health = Config.EnemyHealth;
            //this.ExplosionSpriteSheet = Config.SmallExplosionSpriteSheet;
            this.Speed = Config.EnemySpeed;
            this.FireAction = new FireAction(enemyProjectileBullet.enemyFireBullet);
            this.Weapon = new WeaponSingle(this);

            //WeaponBurstWave burst = new WeaponBurstWave(this, 0, 8, 0.8f);
            //this.Weapon = new WeaponAutoBurst(this, Config.EnemyFireInterval, burst);

            this.Weapon.Direction = new Vector2(0, 1);
            //this.WeaponOffset = new Vector2(0, this.Sprite.Origin.Y);

            this.fireTimer = new Timer();
            this.fireTimer.Fire += new NotifyHandler(fireTimer_Fire);
            this.fireTimer.Start(15);
            this.baseTexture = new Sprite(Config.EnemyAvoiderSpriteSheetBase);
            //this.baseTexture.Color = Color.DarkBlue;
            this.baseTexture.Color = Color.BlueViolet;

            moveTo = new Vector2(Config.Rand.Next(50, 1550), Config.Rand.Next(50, 910));
        }

        void fireTimer_Fire()
        {
            if (this.Dead)
                return;

            this.Weapon.StopFire();

            //Pick a random player as target
            //target = GameNode.PickRandomNode(PlayerShip.PlayerShips);

            //if (PlayerShip.PlayerShips.Count > 0)
            //    this.target = PlayerShip.PlayerShips[0];
            ////testing remove ****************
            //else if (Buddy.Buddys.Count > 0)
            //    this.target = Buddy.Buddys[0];
            ////********************************
            //else
            //    this.target = null;

            if (Player.Ship != null)
                this.target = Player.Ship;
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
            if (this.fireTimer != null)
                this.fireTimer.Stop();

            fireTimer = null;
            base.Remove();
        }

        public override void Update(GameTime gameTime)
        {


            ////this.baseTexture.ColorLerp(Config.DamageColor, Config.EnemyColor, ((float)this.Health / Config.EnemyHealth));
            //if (PlayerShip.PlayerShips.Count > 0)
            //    this.target = PlayerShip.PlayerShips[0];
            ////testing remove ****************
            //else if (Buddy.Buddys.Count > 0)
            //    this.target = Buddy.Buddys[0];
            ////********************************
            //else
            //    this.target = null;






            //if (Projectile.Projectiles.Count > 0)
            //{
            //    Vector2 targetPos = Projectile.Projectiles[0].Position;

            //    for (int i = Projectile.Projectiles.Count - 1; i >= 0; i--)
            //    {
            //        if (Vector2.Distance(this.Position, targetPos) > Vector2.Distance(this.Position, Projectile.Projectiles[i].Position))
            //        {
            //            targetPos = Projectile.Projectiles[i].Position;
                        
            //            //this.Rotation = Steering.TurnToFace(Projectile.Projectiles[i].Position, this.Position, this.Rotation, 0.4f);
            //        }
            //    }
            //    if (Vector2.Distance(this.Position, targetPos) < 200)
            //    {
            //        this.moveTo = ((this.Position - targetPos) * 200);
            //    }             
            //}



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

            //if (this.target != null)
            //{
            //    //Avoidance
            //    if (Vector2.Distance(this.Position, this.target.Position) < 100)
            //    {
            //        this.Rotation = Steering.TurnToFace(target.Position, this.Position, this.Rotation, 0.6f);
            //    }
            //    else
            //    {
            //        this.Rotation = Steering.TurnToFace(this.Position, moveTo, this.Rotation, 0.6f);
            //    }
            //}
            //else
            //    this.Rotation = Steering.TurnToFace(this.Position, moveTo, this.Rotation, 0.6f);

            this.rotation = Steering.TurnToFace(this.Position, moveTo, this.rotation, 0.1f);

            for (int i = Projectile.Projectiles.Count - 1; i >= 0; i--)
            {
                if (Vector2.Distance(this.Position, Projectile.Projectiles[i].Position) < 200)
                {
                    this.rotation = Steering.TurnToFace(Projectile.Projectiles[i].Position, this.Position, this.rotation, 0.4f);
                }
            }

            for (int i = AIProjectile.AIProjectiles.Count - 1; i >= 0; i--)
            {
                if (Vector2.Distance(this.Position, AIProjectile.AIProjectiles[i].Position) < 200)
                {
                    this.rotation = Steering.TurnToFace(AIProjectile.AIProjectiles[i].Position, this.Position, this.rotation, 0.4f);
                }
            }



            //this.Rotation = Steering.TurnToFace(this.Position, moveTo, this.Rotation, 0.10f);

            

            this.Direction = new Vector2((float)Math.Cos(this.Rotation), (float)Math.Sin(this.Rotation));
            this.Direction.Normalize();
            this.Rotation = rotation;
            //this.RemoveOffScreen();

            //if (this.CollisionList != null)
            //{
            //    for (int i = CollisionList.Count - 1; i >= 0; i--)
            //    {
            //        if (Node.CheckCollision(this, CollisionList[i]))
            //            this.Collide(this.CollisionList[i]);
            //    }
            //}
            clampToViewport();
            //this.Sprite.Update(gameTime);
            //ParticleEffects.UpdatePlayerSmokeTrail(this.Position);
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

            //EnemyChaser chaser = new EnemyChaser(Config.EnemySpriteSheet);
            //chaser.Speed = this.Speed;
            //chaser.Position = this.Position;

            //EnemyInterceptor interceptor = new EnemyInterceptor(Config.EnemySpikeSpriteSheet);
            //interceptor.Speed = this.Speed;
            //interceptor.Position = this.Position;

            //EnemyChaser chaser2 = new EnemyChaser(Config.EnemySpriteSheet);
            //chaser2.Position = this.Position;

            base.Explode();
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

