using Microsoft.Xna.Framework;

namespace AstroFlare
{
    class PowerupEnemy : GameNode
    {
        Timer timer;
        double interval;
        bool flashing = false;
        float alpha;
        //Vector2 line;

        //public static List<GameNode> Powerups = new List<GameNode>();

        public PowerupEnemy(SpriteSheet spriteSheet)
            : base(spriteSheet)
        {
            this.CollisionList = Enemy.Enemies;

            timer = new Timer();
            interval = 5;
            timer.Fire += new NotifyHandler(timer_Fire);
            timer.Start(interval);

            //Powerups.Add(this);
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
            Enemy ship = node as Enemy;

            if (ship != null)
                ApplyPowerup(ship);

            this.Remove();

            base.Collide(node);
        }

        public override void Remove()
        {
            //Powerups.Remove(this);
            base.Remove();
        }

        public override void Update(GameTime gameTime)
        {
            if (flashing == true)
            {
                alpha += (float)gameTime.ElapsedGameTime.Milliseconds / 30;

                this.Sprite.ColorLerp(new Color(255, 255, 255, 255), new Color(0, 0, 0, 0), (alpha % 1));
            }

            //if (PlayerShip.PlayerShips.Count == 1)
            //{
            //    line = Player.Ship.Position - this.Position;
            //    // use length squared instead of more costly length(). 40000 = (200 * 200)
            //    if (line.LengthSquared() < (40000))
            //    {
            //        this.Rotation = Steering.TurnToFace(this.Position, Player.Ship.Position, this.Rotation, 1f);
            //        this.Direction = new Vector2((float)Math.Cos(this.Rotation), (float)Math.Sin(this.Rotation));
            //        this.Direction.Normalize();
            //        this.Position += Direction * 10;
            //    }
            //}

            this.RemoveOffScreen();
            base.Update(gameTime);
        }

        protected virtual void ApplyPowerup(Enemy ship) { }
    }
}
