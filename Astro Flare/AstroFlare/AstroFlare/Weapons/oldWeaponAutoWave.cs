using System;
using Microsoft.Xna.Framework;

namespace AstroFlare
{
    class WeaponAutoWave : WeaponAuto
    {
        int waveTotal;
        float waveSpread;

        int fireCount;

        public WeaponAutoWave(Ship ship, double fireInterval, int waveTotal, float waveSpread)
            : base(ship, fireInterval)
        {
            this.waveTotal = waveTotal;
            this.waveSpread = waveSpread;
        }

        public override void StartFire()
        {
            this.fireCount = 0;
            base.StartFire();
        }

        Vector2 RotateVector(Vector2 direction, float radians)
        {
            float angle = (float)Math.Atan2(this.Direction.Y, this.Direction.X);
            angle += radians;
            return new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
        }

        protected override void Fire()
        {
            if (this.fireCount == 0)
            {
                base.Fire();
            }
            else
            {
                float percent = (float)this.fireCount / (this.waveTotal +1);
                //float percent = (float)this.fireCount / (this.waveTotal -1);
                float angle = this.waveSpread * percent;

                Vector2 direction = RotateVector(this.Direction, angle);
                this.Ship.FireAction(direction, this.Position);

                direction = RotateVector(this.Direction, -angle);
                this.Ship.FireAction(direction, this.Position);
            }

            this.fireCount++;
            this.fireCount %= this.waveTotal;
        }
    }
}
