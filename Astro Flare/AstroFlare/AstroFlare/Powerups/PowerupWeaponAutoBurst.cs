
namespace AstroFlare
{
    class PowerupWeaponAutoBurst : Powerup
    {
        public PowerupWeaponAutoBurst(SpriteSheet spriteSheet) : base(spriteSheet) { }

        //protected override void ApplyPowerup(PlayerShip ship)
        //{
        //    ship.Weapon.RemoveWeapon();
        //    WeaponBurst burstWeapon = new WeaponBurstWave(ship, Config.WaveFireInterval, Config.WaveFireTotal, Config.WaveFireSpread);
        //    ship.StopFire();
        //    ship.Weapon = new WeaponAutoBurst(ship, Config.PlayerFireInterval, burstWeapon);
        //    ship.FireAction = new FireAction(ProjectileBulletGreenBeam.FireBullet);
        //    ship.ProjectileSpeedTimer.Start(30);
        //}
    }
}