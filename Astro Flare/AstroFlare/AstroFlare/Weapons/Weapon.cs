using Microsoft.Xna.Framework;

namespace AstroFlare
{
    abstract class Weapon
    {
        protected Ship Ship;

        public double FireInterval;

        //Matrix m = new Matrix();

        public Vector2 Direction;
        //public Vector2 Rotation;
        Vector2 position;
        //public Vector2 Position { get { return this.Ship.Position + this.Offset; } }
        //public Vector2 Position
        //{
        //    get { return this.Ship.Position; }

        //}


        //public Vector2 Offset
        //{
        //    get { return this.Ship.WeaponOffset; }
        //}
        public Vector2 Offset = Vector2.Zero;

        public Vector2 Position
        {
            get 
            { 
                this.position = this.Ship.Position;
                if (Offset != Vector2.Zero)
                    this.position = this.position + Vector2.Transform(Offset, Matrix.CreateRotationZ(this.Ship.Rotation));
                return this.position;        
            }
        }

        public Weapon(Ship ship)
        {
            this.Ship = ship;
            //this.Offset = new Vector2(this.Ship.Position.X, this.Ship.Position.Y);
            this.Direction = new Vector2(0, -1);
        }

        public abstract void StartFire();
        public virtual void StopFire() { }

        protected virtual void Fire()
        {
            this.Ship.FireAction(this.Direction, this.Position);
        }

        public virtual void RemoveWeapon()
        {
            this.StopFire();
            this.Ship = null;
        }
    }
}
