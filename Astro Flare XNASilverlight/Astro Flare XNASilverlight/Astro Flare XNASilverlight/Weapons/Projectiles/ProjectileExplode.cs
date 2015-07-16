using System;
using Microsoft.Xna.Framework;

namespace AstroFlare
{
    class ProjectileExplode : Projectile
    {

        Vector2 line;

        public ProjectileExplode(SpriteSheet spriteSheet)
            : base(spriteSheet)
        {
        }

        public override void Update(TimeSpan gameTime)
        {
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
                ProjectileExplode bullet = new ProjectileExplode(Config.BulletSheetGreenLaser);
                bullet.Position = position;
                bullet.Direction = direction;
                bullet.CollisionList = Enemy.Enemies;
                bullet.CollisionList2 = EnemyPlayerShip.EnemyPlayerShips;
                bullet.Damage = Config.PlayerBulletDamage;
                bullet.Health = Config.PlayerBulletDamage;
                bullet.Speed = Config.PlayerBulletSpeed;
            }
        }

        public override void Collide(GameNode node)
        {
            for (int i = Enemy.Enemies.Count - 1; i >= 0; i--)
            {
                line = this.Position - Enemy.Enemies[i].Position;

                if (line.LengthSquared() < (40000))
                {
                    Enemy.Enemies[i].TakeDamage(this.Damage, this);
                }
            }
            base.Collide(node);
        }
    }
}
