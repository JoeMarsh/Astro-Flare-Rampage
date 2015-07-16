using System;
using Microsoft.Xna.Framework;

namespace AstroFlare
{
    class Sidearm : Attachment
    {
        //Timer timer;
        //double interval;
        Ship ship;

        public Sidearm(SpriteSheet spriteSheet, Ship ship, Vector2 offset) 
            : base(spriteSheet) 
        {
            //add to playership list so that sidearm can collision and be shot at by enemies
            //PlayerShip.PlayerShips.Add(this);
            this.ship = ship;
            this.Speed = Config.BuddySpeed;
            this.WeaponOffset = offset;
            //timer = new Timer();
            //interval = 30;
            //timer.Start(interval);
            //timer.Fire += new NotifyHandler(timer_Fire);

            this.Sprite.Origin -= offset;
            //this.Rotation += (float)(Math.PI / 4d);
            //this.Position = Player.Ship.Position;
            this.Health = 500;
            //this.ExplosionSpriteSheet = Config.ProjectileExplosion;

            //this.FireAction = new FireAction(ProjectileBullet.FireBullet);
            //this.Weapon = new WeaponAuto(this, Config.SidearmFireInterval);
            this.FireAction = new FireAction(queueBullet.FireBullet);
            this.Weapon = new WeaponAuto(this, Config.PlayerFireInterval);
                        //this.Weapon.Position = this.ship.Position;
            //this.Weapon.Offset = offset;
        }

        void timer_Fire()
        {
            this.Remove();
        }

        public override void Remove()
        {
            //remove from ship list so that collision/ enemy target of sidearm is removed when it is destroyed
            //PlayerShip.PlayerShips.Remove(this);
            base.Remove();
        }
        //public Matrix _transform;
        public override void Update(TimeSpan gameTime)
        {
            //this.Rotation = Steering.TurnToFace(this.Position, Player.Ship.Position, this.Rotation, Config.MissileTurnIncrement);
            //this.Direction = new Vector2((float)Math.Cos(this.Rotation), (float)Math.Sin(this.Rotation));
            //asteroid.Velocity = new Vector2((float)Math.Cos(asteroid.Rotation), (float)Math.Sin(asteroid.Rotation));
            //this.Rotation = Steering.TurnToFace(this.Position, Player.Ship.Position, this.Rotation, 0.3f);4

            if (Player.Ship != null)
            {
                this.Weapon.Direction = Player.Ship.Weapon.Direction;
                this.Rotation = Player.Ship.Rotation;
                this.Position = Player.Ship.Position;
            }


            ////this.Weapon.Direction = ship.Weapon.Direction;
            ////this.Rotation = ship.Rotation;
            ////this.Position = ship.Position;


            //this.Weapon.Position = this.Position;

            //_transform = Matrix.CreateTranslation(this.ship.Position.X - offset.X, this.ship.Position.X - offset.Y, 0f) *
            //                 Matrix.CreateRotationZ(this.Rotation) *
            //                 Matrix.CreateTranslation(this.ship.Position.X + offset.X, this.ship.Position.X + offset.Y, 0f);

            //this.Weapon.Position = Vector2.Transform(this.Position, _transform);

            //this.Weapon.Position = RotateAboutOrigin(this.ship.Position, this.ship.Position - offset, this.ship.Rotation);

            //this.Weapon.Position = Vector2.Transform(this.ship.Position, Matrix.CreateRotationZ(this.ship.Rotation));
            //this.Weapon.Position -= offset;

                //this.Weapon.Position = this.ship.Position;
                //this.Weapon.Position = this.Weapon.Position + Vector2.Transform(offset, Matrix.CreateRotationZ(this.ship.Rotation));


            //this.Direction.Normalize();

            //Vector2 targetDirection = Vector2.Normalize(Player.Ship.Position - this.Position);
            //this.Direction += targetDirection * Config.MissileOnPlayerTurnIncrement;
            //this.Direction.Normalize();

            base.Update(gameTime);
            //if (ship != null)
            //{
            //    this.Rotation = Steering.TurnToFace(this.Position, Player.Ship.Position, this.Rotation, 0.3f);
            //    this.Position = ship.Position;
            //}
            //else
            //    this.Remove();

            //this.Sprite.Update(gameTime);
        }


        public Vector2 RotateAboutOrigin(Vector2 point, Vector2 origin, float rotation)
        {
            Vector2 u = point - origin; //point relative to origin  

            if (u == Vector2.Zero)
                return point;

            float a = (float)Math.Atan2(u.Y, u.X); //angle relative to origin  
            a += rotation; //rotate  

            //u is now the new point relative to origin  
            u = u.Length() * new Vector2((float)Math.Cos(a), (float)Math.Sin(a));
            return u + origin;
        } 

        //public override void Update(GameTime gameTime)
        //{
        //    if (ship != null)
        //        this.Position = ship.Position;
        //    else
        //        this.Remove();

        //    base.Update(gameTime);
        //}
    }
}
