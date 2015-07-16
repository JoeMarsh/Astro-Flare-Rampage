
namespace AstroFlare
{
    class WeaponBurst : Weapon
    {
        Timer timer;
        int queue;

        protected int BurstTotal;

        protected int FireCount
        {
            get { return this.BurstTotal -1 - (this.queue -1 % this.BurstTotal); }
        }

        public WeaponBurst(Ship ship, double fireInterval, int burstTotal)
            : base(ship)
        {
            this.BurstTotal = burstTotal;
            this.FireInterval = fireInterval;
            this.timer = new Timer();
            this.timer.Fire += new NotifyHandler(timer_Fire);
        }

        void timer_Fire()
        {
            this.queue--;
            if (queue > 0)
                Fire();
            else
                this.timer.Stop();
        }

        public override void StartFire()
        {
            if (this.queue == 0)
            {
                this.queue = BurstTotal;
                this.Fire();
                this.timer.Start(this.FireInterval);
            }
            else if (this.queue < this.BurstTotal)
            {
                this.queue += this.BurstTotal;
            }
        }

        public override void RemoveWeapon()
        {
            this.timer.Stop();
            this.timer = null;
            base.RemoveWeapon();
        }

    }
}
