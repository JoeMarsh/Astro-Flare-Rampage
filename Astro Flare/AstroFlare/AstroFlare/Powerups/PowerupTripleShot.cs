
namespace AstroFlare
{
    class PowerupTripleShot : Powerup
    {
        public PowerupTripleShot(SpriteSheet spriteSheet) : base(spriteSheet) { }

        //protected override void ApplyPowerup(PlayerShip ship)
        //{
        //    ship.Weapon.RemoveWeapon();
        //    WeaponBurst burstWeapon = new WeaponBurstWave(ship, 0, 2, 0.28f);
        //    ship.Weapon = new WeaponAutoBurst(ship, ship.Weapon.FireInterval, burstWeapon);
        //    ship.FireAction = new FireAction(ProjectileBulletGreenBeam.FireBullet);
        //    ship.ProjectileSpeedTimer.Start(20);
        //    ship.StartFire();
        //}
    }
}
