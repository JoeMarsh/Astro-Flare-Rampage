using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace AstroFlare
{
    class Powerup : GameNode
    {
        Timer timer;
        double interval;
        bool flashing = false;
        float alpha;
        Vector2 line;

        public static List<GameNode> Powerups = new List<GameNode>();

        public Powerup(SpriteSheet spriteSheet)
            : base(spriteSheet)
        {
            this.CollisionList = PlayerShip.PlayerShips;
            this.CollisionList2 = EnemyPlayerShip.EnemyPlayerShips;

            timer = new Timer();
            interval = 5;
            timer.Fire += new NotifyHandler(timer_Fire);
            timer.Start(interval);

            this.Speed = 10f;

            Powerups.Add(this);
        }

        void timer_Fire()
        {
            if (flashing == false)
                flashing = true;
            else
                this.Remove();
        }

        public override void Collide(GameNode node)
        {
            //PlayerShip ship = node.GetRoot() as PlayerShip;
            Ship ship = node.GetRoot() as Ship;

            if (ship != null)
            {
                ApplyPowerup(ship);        
            }

            this.Remove();

            base.Collide(node);
        }

        public override void Remove()
        {
            Powerups.Remove(this);
            if (this.timer != null)
            {
                this.timer.Stop();
                this.timer = null;
            }
            base.Remove();
        }

        public override void Update(TimeSpan gameTime)
        {
            if (flashing == true)
            {
                alpha += (float)gameTime.Milliseconds / 30;

                this.Sprite.ColorLerp(new Color(255, 255, 255, 255), new Color(0, 0, 0, 0), (alpha % 1));
            }

            if (Player.Ship != null)
            {
                line = Player.Ship.Position - this.Position;
                // use length squared instead of more costly length(). 40000 = (200 * 200)
                if (Config.ship3Active)
                {
                    if (Player.Ship.megaMagnetActive)
                    {
                        this.Rotation = Steering.TurnToFace(this.Position, Player.Ship.Position, this.Rotation, 1f);
                        this.Direction = new Vector2((float)Math.Cos(this.Rotation), (float)Math.Sin(this.Rotation));
                        this.Direction.Normalize();
                    }
                    else if (line.LengthSquared() < (50000))
                    {
                        this.Rotation = Steering.TurnToFace(this.Position, Player.Ship.Position, this.Rotation, 1f);
                        this.Direction = new Vector2((float)Math.Cos(this.Rotation), (float)Math.Sin(this.Rotation));
                        this.Direction.Normalize();
                    }
                    else
                    {
                        this.Direction = Vector2.Zero;
                    }
                }
                else
                {
                    if (line.LengthSquared() < (40000))
                    {
                        this.Rotation = Steering.TurnToFace(this.Position, Player.Ship.Position, this.Rotation, 1f);
                        this.Direction = new Vector2((float)Math.Cos(this.Rotation), (float)Math.Sin(this.Rotation));
                        this.Direction.Normalize();
                        //this.Position += Direction * 10;
                    }
                    else
                    {
                        this.Direction = Vector2.Zero;
                    }
                }
            }

            if (Player.EnemyPlayer != null)
            {
                line = Player.EnemyPlayer.Position - this.Position;
                // use length squared instead of more costly length(). 40000 = (200 * 200)
                if (line.LengthSquared() < (40000))
                {
                    this.Rotation = Steering.TurnToFace(this.Position, Player.EnemyPlayer.Position, this.Rotation, 1f);
                    this.Direction = new Vector2((float)Math.Cos(this.Rotation), (float)Math.Sin(this.Rotation));
                    this.Direction.Normalize();
                    //this.Position += Direction * 10;
                }
            }



            this.RemoveOffScreen();
            base.Update(gameTime);
        }

        protected virtual void ApplyPowerup(Ship ship) { }
    }
}
