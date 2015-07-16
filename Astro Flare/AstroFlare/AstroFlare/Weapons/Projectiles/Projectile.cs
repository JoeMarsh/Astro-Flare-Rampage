using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace AstroFlare
{
    class Projectile : GameNode
    {
        public static List<GameNode> Projectiles = new List<GameNode>();

        public int Damage = 0;
        internal Vector2 startPosition;

        public bool CanKillStreak = true;

        public Projectile(SpriteSheet spriteSheet)
            : base(spriteSheet)
        {
            this.Health = 0;
            Projectiles.Add(this);
        }

        public override void Collide(GameNode node)
        {
            ParticleEffects.TriggerExplosionSquaresSmall(this.Position);
            this.TakeDamage(node.Health, node);

            //if (Player.AIControlled)
            //    node.TakeDamage((int)(this.Damage / 2), this);
            //else
                node.TakeDamage(this.Damage, this);
        }

        public override void Update(GameTime gameTime)
        {
            this.RemoveOffScreen();

            //instead of using node and gamenode updates
            Velocity = Direction * Speed * ((float)gameTime.ElapsedGameTime.TotalMilliseconds / 30);
            Position += Velocity;

            this.Sprite.Update(gameTime);

            if (this.CollisionList != null)
            {
                for (int i = this.CollisionList.Count - 1; i >= 0; i--)
                {
                    if (this != CollisionList[i])
                    {
                        if (Node.CheckCollision(this, CollisionList[i]))
                            this.Collide(this.CollisionList[i]);
                    }

                }

            }

            if (this.CollisionList2 != null)
            {
                for (int i = this.CollisionList2.Count - 1; i >= 0; i--)
                {
                    if (this != CollisionList2[i])
                    {
                        if (Node.CheckCollision(this, CollisionList2[i]))
                            this.Collide(this.CollisionList2[i]);
                    }

                }
            }

            if (Vector2.Distance(this.Position, this.startPosition) > Config.ProjectileRange)
                this.Remove();

            //

            //base.Update(gameTime);
        }

        public override void Remove()
        {
            Projectiles.Remove(this);
            base.Remove();
        }
    }
}
