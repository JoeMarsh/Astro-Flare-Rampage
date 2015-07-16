
namespace AstroFlare
{
    class PowerupDoubleShot : Powerup
    {
        public PowerupDoubleShot(SpriteSheet spriteSheet) : base(spriteSheet) { }

        //protected override void ApplyPowerup(PlayerShip ship)
        //{
        //    ship.Weapon.RemoveWeapon();
        //    WeaponBurst burstWeapon = new WeaponBurstWave(ship, 0, 1, 0.14f);
        //    ship.Weapon = new WeaponAutoBurst(ship, ship.Weapon.FireInterval, burstWeapon);
        //    ship.FireAction = new FireAction(ProjectileBulletGreenBeam.FireBullet);
        //    ship.ProjectileSpeedTimer.Start(20);
        //    ship.StartFire();
        //}
    }
}