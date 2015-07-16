
namespace AstroFlare
{
    class PowerupDamageAll : Powerup
    {
        public PowerupDamageAll(SpriteSheet spriteSheet) : base(spriteSheet) { }

        protected override void ApplyPowerup(Ship ship)
        {
            //new EffectNode(Config.LightningSpriteSheet, this.Position);
            PlayerShip applyShip;
            applyShip = ship.GetRoot() as PlayerShip;

            //for (int i = Enemy.Enemies.Count - 1; i >= 0; i--)
            //{
            //    ProjectileBulletGreenBeam.FireBullet(Vector2.Normalize(Enemy.Enemies[i].Position - applyShip.Position), applyShip.Position);
            //}
            //for (int i = Enemy.Enemies.Count - 1; i >= 0; i--)
            //{
            //    ProjectileBulletGreenBeam.FireBullet(Vector2.Normalize(Enemy.Enemies[i].Position - applyShip.Position), applyShip.Position);
            //}
            //for (int i = Enemy.Enemies.Count - 1; i >= 0; i--)
            //{
            //    ProjectileBulletGreenBeam.FireBullet(Vector2.Normalize(Enemy.Enemies[i].Position - applyShip.Position), applyShip.Position);
            //}
            //for (int i = Enemy.Enemies.Count - 1; i >= 0; i--)
            //{
            //    ProjectileBulletGreenBeam.FireBullet(Vector2.Normalize(Enemy.Enemies[i].Position - applyShip.Position), applyShip.Position);
            //}
            //for (int i = Enemy.Enemies.Count - 1; i >= 0; i--)
            //{
            //    ProjectileBulletGreenBeam.FireBullet(Vector2.Normalize(Enemy.Enemies[i].Position - applyShip.Position), applyShip.Position);
            //}

            //AoEWeaponBurstWave tempWeapon = new AoEWeaponBurstWave(applyShip, 0, 200, 50f);
            AoEWeaponBurstWave tempWeapon = new AoEWeaponBurstWave(applyShip, 0, 100, 50f);
            tempWeapon.StartFire();
        }
    }
}
