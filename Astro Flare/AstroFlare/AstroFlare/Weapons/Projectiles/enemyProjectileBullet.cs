using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AstroFlare
{
    class enemyProjectileBullet : enemyProjectile
    {

        public enemyProjectileBullet(SpriteSheet spriteSheet) : base(spriteSheet) 
        {
        }

        public override void Update(GameTime gameTime)
        {
            ParticleEffects.TriggerEnemySmokeTrail(this.Position);
            base.Update(gameTime);
        }

        public override void Remove()
        {
            base.Remove();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            float angle = (float)Math.Atan2(this.Direction.Y, this.Direction.X) + MathHelper.PiOver2;
            this.Sprite.Draw(spriteBatch, this.Position, angle);
        }

        public static void enemyFireBullet(Vector2 direction, Vector2 position)
        {
            if (direction.Length() > 0 && position.Length() > 0)
            {
                enemyProjectile bullet = new enemyProjectileBullet(Config.EnemyBulletSheet);
                bullet.CollisionList = PlayerShip.PlayerShips;
                bullet.CollisionList2 = EnemyPlayerShip.EnemyPlayerShips;
                bullet.Position = position;
                bullet.Direction = direction;
                bullet.Speed = Config.EnemyBulletSpeed;
                bullet.Damage = Config.EnemyBulletDamage;
                bullet.Health = Config.EnemyBulletDamage;
                //bullet.ExplosionSpriteSheet = Config.ProjectileExplosion;
            }
        }
    }
}
