using System;
using Microsoft.Xna.Framework;

namespace AstroFlare
{
    class boss1 : Enemy
    {
        GameNode target;
        Timer fireTimer;
        float orientation;
        Vector2 wanderDirection;
        Sprite baseTexture;

        Random rand = new Random();

        public boss1(SpriteSheet spriteSheet)
            : base(spriteSheet)
        {
            this.Health = Config.BossHealth;
            //this.ExplosionSpriteSheet = Config.SmallExplosionSpriteSheet;
            this.Speed = Config.BossSpeed;
            this.FireAction = new FireAction(bossProjectileBullet.enemyFireBullet);

            //this.Weapon = new WeaponSingle(this);
            
            //WeaponBurstWave burst = new WeaponBurstWave(this, 0, 8, 0.8f);
            //WeaponBurstWave burst = new WeaponBurstWave(this, 0, 6, 2.0f);
            this.Weapon = new WeaponBurstWave(this, 0, 6, 2.0f);
            //this.Weapon = new WeaponAutoBurst(this, Config.BossFireInterval, burst);

            this.Weapon.Direction = new Vector2(0, 1);
            //this.Weapon.Offset = new Vector2(0, this.Sprite.Origin.Y);

            this.fireTimer = new Timer();
            this.fireTimer.Fire += new NotifyHandler(fireTimer_Fire);
            this.fireTimer.Start(Config.BossFireInterval);
            this.baseTexture = new Sprite(Config.BossSpriteSheetBase);
        }

        void fireTimer_Fire()
        {
            if (this.Dead)
                return;

            this.Weapon.StopFire();

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

            //for (int i = 0; i < 10; i++)
            //{
            //    GameNode node = null;
            //    switch (Config.Rand.Next(1, 5))
            //    {
            //        case 1:
            //            node = new EnemyInterceptor(Config.EnemyInterceptorSpriteSheet);
            //            break;
            //        case 2:
            //            node = new EnemyShooter(Config.EnemyInterceptorSpriteSheet);
            //            break;
            //        case 3:
            //            node = new EnemyChaser(Config.EnemyInterceptorSpriteSheet);
            //            break;
            //        case 4:
            //            node = new EnemyAvoider(Config.EnemyInterceptorSpriteSheet);
            //            break;
            //    }
            //    node.Position = this.Position;
            //}

            this.Weapon.StartFire();
        }

        public override void Remove()
        {
            if (this.fireTimer != null)
                this.fireTimer.Stop();

            this.fireTimer = null;
            base.Remove();
        }

        public override void Update(TimeSpan gameTime)
        {
            this.baseTexture.ColorLerp(Config.DamageColor, Config.EnemyColor, ((float)this.Health / Config.EnemyHealth));

            if (PlayerShip.PlayerShips.Count == 1)
            {
                this.Rotation = Steering.TurnToFace(this.Position, PlayerShip.PlayerShips[0].Position, this.Rotation, 0.10f);
                this.Direction = new Vector2((float)Math.Cos(this.Rotation), (float)Math.Sin(this.Rotation));
                this.Direction.Normalize();
                //this.Position += Direction * this.Speed;
            }
            else
            {
                Steering.Wander(this.Position, ref this.wanderDirection, ref orientation, 0.2f);
                Vector2 heading = new Vector2((float)Math.Cos(orientation), (float)Math.Sin(orientation));
                //this.Position += heading * this.Speed;
                this.Rotation = orientation;
            }

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
            base.Update(gameTime);
        }


        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            this.baseTexture.Draw(spriteBatch, this.Position, this.Rotation);
            base.Draw(spriteBatch);            
        }

        protected override void Explode()
        {
            //if (this.ExplosionSpriteSheet != null)
            //{
                //ParticleEffects.TriggerExplosionSquaresLarge(this.Position);
                //ParticleEffects.TriggerExplosionSquaresLarge(this.Position + new Vector2(10, 0));
                //ParticleEffects.TriggerExplosionSquaresLarge(this.Position + new Vector2(10, 10));
                //ParticleEffects.TriggerExplosionSquaresLarge(this.Position + new Vector2(0, 10));
                //ParticleEffects.TriggerExplosionSquaresLarge(this.Position + new Vector2(10, -10));
                //ParticleEffects.TriggerExplosionSquaresLarge(this.Position + new Vector2(-10, -10));
                //ParticleEffects.TriggerExplosionSquaresLarge(this.Position + new Vector2(-10, 0));
                //ParticleEffects.TriggerExplosionSquaresLarge(this.Position + new Vector2(-10, 10));
                //ParticleEffects.TriggerExplosionSquaresLarge(this.Position + new Vector2(0, -10));
                ParticleEffects.TriggerExplosionSquaresLarge(this.Position);
                ParticleEffects.TriggerExplosionSquaresLarge(this.Position + new Vector2(Config.Rand.Next(0, 100), 0));
                ParticleEffects.TriggerExplosionSquaresLarge(this.Position + new Vector2(Config.Rand.Next(0, 100), Config.Rand.Next(0, 100)));
                ParticleEffects.TriggerExplosionSquaresLarge(this.Position + new Vector2(0, Config.Rand.Next(0, 100)));
                ParticleEffects.TriggerExplosionSquaresLarge(this.Position + new Vector2(Config.Rand.Next(0, 100), Config.Rand.Next(100, 200) - 200));
                ParticleEffects.TriggerExplosionSquaresLarge(this.Position + new Vector2(Config.Rand.Next(100, 200) - 200, Config.Rand.Next(100, 200) - 200));
                ParticleEffects.TriggerExplosionSquaresLarge(this.Position + new Vector2(Config.Rand.Next(100, 200) - 200, 0));
                ParticleEffects.TriggerExplosionSquaresLarge(this.Position + new Vector2(Config.Rand.Next(100, 200) - 200, Config.Rand.Next(0, 100)));
                ParticleEffects.TriggerExplosionSquaresLarge(this.Position + new Vector2(0, Config.Rand.Next(100, 200) - 200));

            //}

            for (int i = 0; i < 100; i++)
            {
                Coin coin = new Coin(Config.CoinSpriteSheet);
                coin.Position = this.Position + new Vector2(rand.Next(0, 200) - 100, rand.Next(0, 200) - 100);
            }
            //GameStateManagementGame.Instance.soundManager.PlaySound("ShipExplode");
            base.Explode();
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


