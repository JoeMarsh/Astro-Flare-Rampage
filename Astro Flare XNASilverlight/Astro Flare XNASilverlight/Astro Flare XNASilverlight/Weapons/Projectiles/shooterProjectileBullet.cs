using Microsoft.Xna.Framework;
using System;

namespace AstroFlare
{
    class shooterProjectileBullet : enemyProjectile
    {
        public shooterProjectileBullet(SpriteSheet spriteSheet) : base(spriteSheet) { }

        public override void Update(TimeSpan gameTime)
        {
            ParticleEffects.TriggerEnemySmokeTrail(this.Position);
            base.Update(gameTime);
        }

        public static void enemyFireBullet(Vector2 direction, Vector2 position)
        {
            if (direction.Length() > 0 && position.Length() > 0)
            {
                enemyProjectile bullet = new bossProjectileBullet(Config.BossBulletSheet);
                bullet.CollisionList = PlayerShip.PlayerShips;
                bullet.CollisionList2 = EnemyPlayerShip.EnemyPlayerShips;
                bullet.Position = position;
                bullet.Direction = direction;
                bullet.Speed = 10f;
                bullet.Damage = Config.EnemyBulletDamage;
                bullet.Health = Config.EnemyBulletDamage;
               // bullet.ExplosionSpriteSheet = Config.ProjectileExplosion;
            }
        }
    }
}
