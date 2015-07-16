using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System;

namespace AstroFlare
{
    class enemyProjectile : GameNode
    {
        public static List<GameNode> EnemyProjectiles = new List<GameNode>();

        public int Damage = 1;
        //internal Vector2 startPosition;

        public enemyProjectile(SpriteSheet spriteSheet)
            : base(spriteSheet)
        {
            this.Health = 1;
            EnemyProjectiles.Add(this);
        }

        public override void Collide(GameNode node)
        {
            ParticleEffects.TriggerExplosionSquaresSmall(this.Position);
            this.TakeDamage(node.Health, node);
            node.TakeDamage(this.Damage, this);
        }

        public override void Update(TimeSpan gameTime)
        {
            this.RemoveOffScreen();
            base.Update(gameTime);
        }

        public override void Remove()
        {
            EnemyProjectiles.Remove(this);
            base.Remove();
        }
    }
}
