using System;
using Microsoft.Xna.Framework;

namespace AstroFlare
{
    class WeaponBurstWave : WeaponBurst
    {
        float waveSpread;

        public WeaponBurstWave(Ship ship, double fireInterval, int burstTotal, float waveSpread)
            : base(ship, fireInterval, burstTotal)
        {
            this.waveSpread = waveSpread;
        }

        Vector2 RotateVector(Vector2 direction, float radians)
        {
            float angle = (float)Math.Atan2(this.Direction.Y, this.Direction.X);
            angle += radians;
            return new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
        }

        protected override void Fire()
        {
            if (this.FireCount == 0)
            {
                base.Fire();
            }
            else
            {
                float percent = (float)this.FireCount / (this.BurstTotal + 1);
                //float percent = (float)this.fireCount / (this.waveTotal -1);
                float angle = this.waveSpread * percent;

                Vector2 direction = RotateVector(this.Direction, angle);
                this.Ship.FireAction(direction, this.Position);
                direction = RotateVector(this.Direction, -angle);
                this.Ship.FireAction(direction, this.Position);
            }
        }
    }
}
