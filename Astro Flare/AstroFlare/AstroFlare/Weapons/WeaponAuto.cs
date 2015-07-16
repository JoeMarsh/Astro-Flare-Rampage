
namespace AstroFlare
{
    class WeaponAuto : Weapon
    {
        Timer timer;

        public WeaponAuto(Ship ship, double fireInterval)
            : base(ship)
        {
            this.FireInterval = fireInterval;
            this.timer = new Timer();
            this.timer.Fire += new NotifyHandler(timer_Fire);
        }

        void timer_Fire()
        {
            if (Config.SoundFXOn && this.Ship == Player.Ship)
                GameStateManagementGame.Instance.soundManager.PlaySound("Shot", 1f);

            this.Fire();
        }

        public override void StartFire()
        {
            this.Fire();
            this.timer.Start(this.FireInterval);
        }

        public override void StopFire()
        {
            if (this.timer != null)
                this.timer.Stop();
        }

        public override void RemoveWeapon()
        {
            this.timer.Stop();
            this.timer = null;
            base.RemoveWeapon();
        }
    }
}
