using System;
using Microsoft.Xna.Framework;
using Astro_Flare_XNASilverlight;


namespace AstroFlare
{
    class PowerupAddBullet : Powerup
    {
        public PowerupAddBullet(SpriteSheet spriteSheet) : base(spriteSheet) { }

        protected override void ApplyPowerup(Ship ship)
        {
            PlayerShip applyShip;
            EnemyPlayerShip applyAIShip;

            if (ship == Player.Ship)
            {
                //if (Config.SoundFXOn)
                //    GameStateManagementGame.Instance.soundManager.PlaySound("FX1", 0.7f);
                Sounds.PlaySound("powerup");

                applyShip = ship.GetRoot() as PlayerShip;

                ship.Weapon.RemoveWeapon();
                applyShip.weaponBurstTotal += 1;

                //switch(Config.Rand.Next(1, 6))
                //{
                //    case 1:
                //        Config.CurrentProjectile = Config.BulletSheetBlueLaserDouble;
                //        break;
                //    case 2:
                //        Config.CurrentProjectile = Config.BulletSheetEnergyPurple;
                //         break;
                //    case 3:
                //        Config.CurrentProjectile = Config.BulletSheetMelter;
                //        break;
                //    case 4:
                //        Config.CurrentProjectile = Config.BulletSheetWave;
                //        break;
                //    case 5:
                //        Config.CurrentProjectile = Config.BulletSheetGreenLaser;
                //        break;
                //}

                float spread = 0.14f;
                if (applyShip.weaponBurstTotal > 1)
                    spread = 0.28f;

                //ship.Weapon = null;

                //WeaponBurst burstWeapon = new WeaponBurstWave(applyShip, 0, Math.Min(applyShip.weaponBurstTotal, 5), spread);
                WeaponBurst burstWeapon = new WeaponBurstWave(applyShip, 0, Math.Min(applyShip.weaponBurstTotal, 2), spread);
                //WeaponBurst burstWeapon = new WeaponBurstWave(applyShip, 0, applyShip.weaponBurstTotal, spread);
                applyShip.Weapon = new WeaponAutoBurst(applyShip, applyShip.FireInterval, burstWeapon);

                //ship.FireAction = new FireAction(ProjectileBulletGreenBeam.FireBullet);

                applyShip.AddProjectileTimer.Start(15);

                if (applyShip.AddProjectileBar != null)
                {
                    applyShip.AddProjectileBar.Percent = 0f;
                    applyShip.AddProjectileBar = null;
                }
                applyShip.AddProjectileBar = new Bar(100, 20, new Color(106, 90, 205, 5));
                applyShip.AddProjectileBar.Position = new Vector2(5, 230);

                //Player.NotFiring = true;
                applyShip.Weapon.StartFire();
            }

            if (ship == Player.EnemyPlayer)
            {
                applyAIShip = ship.GetRoot() as EnemyPlayerShip;

                applyAIShip.Weapon.RemoveWeapon();
                applyAIShip.weaponBurstTotal += 1;

                float spread = 0.14f;
                if (applyAIShip.weaponBurstTotal > 1)
                    spread = 0.28f;

                //ship.Weapon = null;

                WeaponBurst burstWeapon = new WeaponBurstWave(applyAIShip, 0, Math.Min(applyAIShip.weaponBurstTotal, 2), spread);
                //WeaponBurst burstWeapon = new WeaponBurstWave(applyAIShip, 0, applyAIShip.weaponBurstTotal, spread);
                applyAIShip.Weapon = new WeaponAutoBurst(applyAIShip, applyAIShip.FireInterval, burstWeapon);

                //ship.FireAction = new FireAction(ProjectileBulletGreenBeam.FireBullet);

                applyAIShip.AddProjectileTimer.Start(15);

                //if (applyAIShip.AddProjectileBar != null)
                //{
                //    applyAIShip.AddProjectileBar.Percent = 0f;
                //    applyAIShip.AddProjectileBar = null;
                //}
                //applyAIShip.AddProjectileBar = new Bar(100, 20, new Color(106, 90, 205, 5));
                //applyAIShip.AddProjectileBar.Position = new Vector2(5, 230);

                applyAIShip.Weapon.StartFire();
            }
        }

        public override void Collide(GameNode node)
        {
            GamePage.FloatingPowerupText.Score = ("+Projectile");
            GamePage.FloatingPowerupText.StartPosition = this.Position;
            GamePage.FloatingPowerupText.Alive = true;
            GamePage.FloatingPowerupText.LifeSpan = 1000;
            base.Collide(node);
        }
    }
}