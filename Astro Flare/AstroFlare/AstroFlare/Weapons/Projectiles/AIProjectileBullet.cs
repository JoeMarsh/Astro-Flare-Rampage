using System;
using Microsoft.Xna.Framework;

namespace AstroFlare
{
    class AIProjectileBullet : AIProjectile
    {
                //double triggerTimer = 0;
        //float triggerTime = 0.05f;

        public AIProjectileBullet(SpriteSheet spriteSheet)
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

        public override void Update(GameTime gameTime)
        {
            //triggerTimer += gameTime.ElapsedGameTime.TotalSeconds;

            //if (triggerTimer >= triggerTime)
            //{
            //    ParticleEffects.UpdatePlayerSmokeTrail(this.Position);
            //    triggerTimer = 0;
            //}

            //ParticleEffects.UpdatePlayerSmokeTrail(this.Position);
            if (Vector2.Distance(this.Position, this.startPosition) > 500)
                this.Remove();

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
                AIProjectileBullet bullet = new AIProjectileBullet(Config.EnemyPlayerBulletSheet);
                bullet.Position = position;
                bullet.Direction = direction;
                //bullet.ExplosionSpriteSheet = Config.ProjectileExplosion;
                bullet.CollisionList = Enemy.Enemies;
                bullet.CollisionList2 = PlayerShip.PlayerShips;
                bullet.Damage = Config.PlayerBulletDamage;
                bullet.Health = Config.PlayerBulletDamage;
                bullet.Speed = Config.PlayerBulletSpeed;
                //GameStateManagementGame.Instance.soundManager.PlaySound("Shot3", 0.7f);
                bullet.startPosition = position;
            }
        }

        public override void Collide(GameNode node)
        {
            if (!(node is EnemyPlayerShip))
                base.Collide(node);
        }
    }
}
