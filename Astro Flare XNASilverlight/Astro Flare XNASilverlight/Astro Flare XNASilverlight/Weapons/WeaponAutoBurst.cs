
namespace AstroFlare
{
    class WeaponAutoBurst : WeaponAuto
    {
        WeaponBurst weapon;

        public WeaponAutoBurst(Ship ship, double fireInterval, WeaponBurst weaponBurst)
            : base(ship, fireInterval)
        {
            this.weapon = weaponBurst;
        }

        protected override void Fire()
        {
            this.weapon.Direction = this.Ship.Weapon.Direction;
            //this.weapon.Position = this.Ship.Weapon.Position;
            this.weapon.StartFire();
        }

        public override void RemoveWeapon()
        {
            this.weapon.RemoveWeapon();
            base.RemoveWeapon();
        }
    }
}
