using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace AstroFlare
{
    class Buddy : Ship
    {
        public static List<GameNode> Buddys = new List<GameNode>();

        public Buddy(SpriteSheet spriteSheet)
            : base(spriteSheet)
        {
            Buddys.Add(this);
            //this.CollisionList = PlayerShip.PlayerShips;
        }

        public override void Remove()
        {
            Buddys.Remove(this);
            base.Remove();
        }

        public override void Update(TimeSpan gameTime)
        {
            //this.RemoveOffScreen();
            this.Rotation = Steering.TurnToFace(this.Position, Player.Ship.Position, this.Rotation, Config.MissileTurnIncrement);
            this.Direction = new Vector2((float)Math.Cos(this.Rotation), (float)Math.Sin(this.Rotation));
            //asteroid.Velocity = new Vector2((float)Math.Cos(asteroid.Rotation), (float)Math.Sin(asteroid.Rotation));
            this.Direction.Normalize();
            this.Direction *= this.Speed;
            base.Update(gameTime);
        }

        public override void Collide(GameNode node)
        {
            //int nodeHealth = node.Health;

            //node.TakeDamage(this.Health);
            //this.TakeDamage(nodeHealth);
        }
    }
}
