using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace AstroFlare
{
    class queueBullet : Projectile
    {
            public static Queue<queueBullet> queue = new Queue<queueBullet>(100);

            bool visable = false;

            public queueBullet(SpriteSheet spriteSheet) : base(spriteSheet) { }

            //public static void DamageAll(Vector2 direction, Vector2 position)
            //{
            //    for (int i = Enemy.Enemies.Count - 1; i >= 0; i--)
            //    {
            //        Enemy.Enemies[i].TakeDamage(10);
            //    }
            //}

            public override void Update(TimeSpan gameTime)
            {
                if (visable)
                {
                    //ParticleEffects.UpdatePlayerSmokeTrail(this.Position);
                    base.Update(gameTime);
                }   
            }

            public override void Remove()
            {
                visable = false;

                queue.Enqueue(this);
            }

            public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
            {
                if (visable)
                {
                    float angle = (float)Math.Atan2(this.Direction.Y, this.Direction.X) + MathHelper.PiOver2;
                    this.Sprite.Draw(spriteBatch, this.Position, angle);
                }
            }

            public static void FireBullet(Vector2 direction, Vector2 position)
            {
                if (direction.Length() > 0 && position.Length() > 0)
                {
                    queueBullet bullet = queue.Dequeue();
                    bullet.visable = true;
                    bullet.Position = position;
                    bullet.Direction = direction;
                    //bullet.ExplosionSpriteSheet = Config.ProjectileExplosion;
                    bullet.CollisionList = Enemy.Enemies;
                    bullet.CollisionList2 = EnemyPlayerShip.EnemyPlayerShips;
                    bullet.Damage = Config.PlayerBulletDamage;
                    bullet.Health = Config.PlayerBulletDamage;
                    bullet.Speed = Config.PlayerBulletSpeed;
                }
            }
        
    }
}
