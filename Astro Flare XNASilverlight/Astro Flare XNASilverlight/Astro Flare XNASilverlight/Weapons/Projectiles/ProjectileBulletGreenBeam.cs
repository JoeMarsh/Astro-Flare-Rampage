using System;
using Microsoft.Xna.Framework;

namespace AstroFlare
{
    class ProjectileBulletGreenBeam : Projectile
    {
                //double triggerTimer = 0;
        //float triggerTime = 0.05f;

        public ProjectileBulletGreenBeam(SpriteSheet spriteSheet)
            : base(spriteSheet) 
        {
        }

        //public static void DamageAll(Vector2 direction, Vector2 position)
        //{
        //    for (int i = Enemy.Enemies.Count - 1; i >= 0; i--)
        //    {
        //        Enemy.Enemies[i].TakeDamage(10);
        //    }
        //}

        public override void Update(TimeSpan gameTime)
        {
            //triggerTimer += gameTime.ElapsedGameTime.TotalSeconds;

            //if (triggerTimer >= triggerTime)
            //{
            //    ParticleEffects.UpdatePlayerSmokeTrail(this.Position);
            //    triggerTimer = 0;
            //}

            //ParticleEffects.UpdatePlayerSmokeTrail(this.Position);

            //this.Sprite.ColorLerp(Color.White, new Color(r, g, b, 255), (float)gameTime.TotalGameTime.TotalSeconds % 1f);
            base.Update(gameTime);
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
                float angle = (float)Math.Atan2(this.Direction.Y, this.Direction.X) + MathHelper.PiOver2;
                this.Sprite.Draw(spriteBatch, this.Position, angle);
        }

        public static void FireBullet(Vector2 direction, Vector2 position)
        {
            if (direction.Length() > 0 && position.Length() > 0)
            {
                Projectile bullet = new ProjectileBulletGreenBeam(Config.CurrentProjectile);
                bullet.Position = position;
                bullet.Direction = direction;
                //bullet.ExplosionSpriteSheet = Config.ProjectileExplosion;
                bullet.CollisionList = Enemy.Enemies;
                bullet.CollisionList2 = EnemyPlayerShip.EnemyPlayerShips;
                bullet.Damage = Config.PlayerBulletDamage;
                bullet.Health = Config.PlayerBulletDamage;
                bullet.Speed = Config.PlayerBulletSpeed;
                bullet.startPosition = position;
                //GameStateManagementGame.Instance.soundManager.PlaySound("Shot", 0.7f);
            }
        }
    }
}
