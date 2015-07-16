using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace AstroFlare
{
    class AIProjectile : GameNode
    {
        public static List<GameNode> AIProjectiles = new List<GameNode>();

        public int Damage = 1;
        internal Vector2 startPosition;

        public AIProjectile(SpriteSheet spriteSheet)
            : base(spriteSheet)
        {
            this.Health = 1;
            AIProjectiles.Add(this);
        }

        public override void Collide(GameNode node)
        {
            ParticleEffects.TriggerExplosionSquaresSmall(this.Position);
            this.TakeDamage(node.Health, node);
            node.TakeDamage(this.Damage, this);
        }

        public override void Update(GameTime gameTime)
        {
            this.RemoveOffScreen();
            base.Update(gameTime);
        }

        public override void Remove()
        {
            AIProjectiles.Remove(this);
            base.Remove();
        }
    }
}
