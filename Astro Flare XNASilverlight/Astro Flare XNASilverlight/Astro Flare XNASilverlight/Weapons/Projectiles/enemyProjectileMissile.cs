using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AstroFlare
{
    class enemyProjectileMissile : enemyProjectile
    {
        GameNode target;

        public enemyProjectileMissile(SpriteSheet spriteSheet) : base(spriteSheet) { }

        public override void Update(TimeSpan gameTime)
        {
            if (this.target == null || this.target.Dead)
                this.AquireTarget();

            if (this.target != null)
            {
                if (!this.target.Dead)
                {
                    Vector2 targetDirection = Vector2.Normalize(target.Position - this.Position);
                    this.Direction += targetDirection * Config.MissileTurnIncrement;
                    this.Direction.Normalize();
                }
            }
            ParticleEffects.TriggerMissileSmokeTrail(this.Position);
            base.Update(gameTime);
        }

        public override void RemoveOffScreen()
        {
            Rectangle bounds = new Rectangle(0, 0, Config.WorldBoundsX, Config.WorldBoundsY);
            bounds.Inflate(Config.WorldBoundsX / 2, Config.WorldBoundsY / 2);
            this.RemoveOutOfBounds(bounds);
        }

        //TODO: update to use rotation in Node Draw method
        public override void Draw(SpriteBatch spriteBatch)
        {
            float angle = (float)Math.Atan2(this.Direction.Y, this.Direction.X) + MathHelper.PiOver2;
            this.Sprite.Draw(spriteBatch, this.Position, angle);
        }

        void AquireTarget()
        {
            if (this.CollisionList != null)
                this.target = GameNode.PickRandomNode(this.CollisionList);
        }

        public static void enemyFireMissile(Vector2 direction, Vector2 position)
        {
            if (direction.Length() > 0 && position.Length() > 0)
            {
                enemyProjectile p = new enemyProjectileMissile(Config.EnemyBulletSheet);
                p.Position = position;
                p.Direction = direction;
                p.Speed = Config.MissileSpeed;
                p.Damage = Config.MissileDamage;
                p.Health = Config.MissileDamage;
                p.CollisionList = PlayerShip.PlayerShips;
                p.CollisionList2 = EnemyPlayerShip.EnemyPlayerShips;
                //p.ExplosionSpriteSheet = Config.ProjectileExplosion;
            }
        }
    }
}
