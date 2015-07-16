
namespace AstroFlare
{
    class WeaponSingle : Weapon
    {
        public WeaponSingle(Ship ship) : base(ship) { }

        public override void StartFire()
        {
            this.Fire();
        }
    }
}
