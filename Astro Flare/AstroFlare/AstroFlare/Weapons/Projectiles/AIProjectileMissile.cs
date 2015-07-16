using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AstroFlare
{
    class AIProjectileMissile : AIProjectile
    {
        GameNode target;

        public AIProjectileMissile(SpriteSheet spriteSheet) : base(spriteSheet) { }

        public override void Update(GameTime gameTime)
        {
            if (this.target == null || this.target.Dead || this.target == Player.EnemyPlayer)
                this.AquireTarget();

            if (this.target != null)
            {
                if (!this.target.Dead)
                {
                    Vector2 targetDirection = Vector2.Normalize(target.Position - this.Position);
                    if (this.target == Player.EnemyPlayer)
                        this.Direction += targetDirection * Config.MissileOnPlayerTurnIncrement;
                    else if (this.target == Player.Ship)
                        this.Direction += targetDirection * Config.MissileOnPlayerTurnIncrement;
                    else
                        this.Direction += targetDirection * Config.MissileTurnIncrement;
                    this.Direction.Normalize();
                }
            }
            ParticleEffects.TriggerMissileSmokeTrail(this.Position);

            if (Vector2.Distance(this.Position, this.startPosition) > 500)
                this.Remove();

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
            {
                this.target = GameNode.PickRandomNode(this.CollisionList);

                if (this.target != null)
                {
                    for (int i = Enemy.Enemies.Count - 1; i >= 0; i--)
                    {
                        if ((Enemy.Enemies[i].Position - this.Position).Length() < (this.target.Position - this.Position).Length())
                        {
                            this.target = Enemy.Enemies[i];
                        }
                    }
                    if (Player.Ship != null)
                        if ((Player.Ship.Position - this.Position).Length() < (this.target.Position - this.Position).Length())
                        {
                            this.target = Player.Ship;
                        }
                }
            }
            //if (Player.Ship != null && this.target != null)
            //if ((Player.Ship.Position - this.Position).Length() < (this.target.Position - this.Position).Length())
            //{
            //    this.target = Player.Ship;
            //}

            if (this.target == Player.EnemyPlayer && Player.Ship != null)
                  this.target = Player.Ship;

            if (this.target == null)
                if (Player.EnemyPlayer != null)
                    this.target = Player.EnemyPlayer;
        }

        public static void FireMissile(Vector2 direction, Vector2 position)
        {
            if (direction.Length() > 0 && position.Length() > 0)
            {
                AIProjectileMissile p = new AIProjectileMissile(Config.EnemyPlayerBulletSheet);
                p.Position = position + new Vector2(1, 0);
                p.Direction = direction;
                p.Speed = Config.MissileSpeed;
                p.Damage = Config.MissileDamage;
                p.Health = Config.MissileDamage;
                p.CollisionList = Enemy.Enemies;
                p.CollisionList2 = PlayerShip.PlayerShips;
                p.startPosition = position;
                
                //p.ExplosionSpriteSheet = Config.ProjectileExplosion;
            }
        }
    }
}
