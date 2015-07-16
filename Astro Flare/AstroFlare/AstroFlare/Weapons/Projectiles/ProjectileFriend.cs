using System;
using Microsoft.Xna.Framework;

namespace AstroFlare
{
    class ProjectileFriend : Projectile
    {
        //double triggerTimer = 0;
        //float triggerTime = 0.05f;

        public ProjectileFriend(SpriteSheet spriteSheet)
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
                ProjectileFriend bullet = new ProjectileFriend(Config.CurrentProjectile);
                bullet.Position = position;
                bullet.Direction = direction;
                //bullet.ExplosionSpriteSheet = Config.ProjectileExplosion;
                bullet.CollisionList = Enemy.Enemies;
                bullet.CollisionList2 = EnemyPlayerShip.EnemyPlayerShips;
                bullet.Damage = 0;
                bullet.Speed = Config.PlayerBulletSpeed;
                //GameStateManagementGame.Instance.soundManager.PlaySound("Shot3", 0.7f);
            }
        }

        public override void Collide(GameNode node)
        {
            // make enemies turn friendly.
            Enemy.Enemies.Remove(node);
            
            node.CollisionList = Enemy.Enemies;
            node.Sprite.Color = Color.Green;
            base.Collide(node);
        }
    }
}
