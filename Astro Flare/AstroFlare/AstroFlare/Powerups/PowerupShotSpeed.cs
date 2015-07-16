using Microsoft.Xna.Framework;

namespace AstroFlare
{
    class PowerupShotSpeed : Powerup
    {
        public PowerupShotSpeed(SpriteSheet spriteSheet) : base(spriteSheet) { }

        protected override void ApplyPowerup(Ship ship)
        {
            PlayerShip applyShip;
            EnemyPlayerShip applyAIShip;

            if (ship == Player.Ship)
            {
                if (Config.SoundFXOn)
                    GameStateManagementGame.Instance.soundManager.PlaySound("FX1", 0.7f);  
 
                applyShip = ship.GetRoot() as PlayerShip;

                applyShip.Weapon.StopFire();
                applyShip.FireInterval = Config.PlayerFireInterval / 1.5f;
                applyShip.Weapon.FireInterval = Config.PlayerFireInterval / 1.5f;

                if (Config.ship2Active)
                    applyShip.ProjectileSpeedTimer.Start(18);
                else
                    applyShip.ProjectileSpeedTimer.Start(15);

                if (applyShip.ProjectileSpeedBar != null)
                {
                    applyShip.ProjectileSpeedBar.Percent = 0f;
                    applyShip.ProjectileSpeedBar = null;
                }
                applyShip.ProjectileSpeedBar = new Bar(100, 20, new Color(128, 0, 128, 5));
                applyShip.ProjectileSpeedBar.Position = new Vector2(5, 200);

                //Player.NotFiring = true;
                applyShip.Weapon.StartFire();
            }

            if (ship == Player.EnemyPlayer)
            {
                applyAIShip = ship.GetRoot() as EnemyPlayerShip;

                applyAIShip.Weapon.StopFire();
                applyAIShip.FireInterval = Config.PlayerFireInterval / 1.5f;
                applyAIShip.Weapon.FireInterval = Config.PlayerFireInterval / 1.5f;

                applyAIShip.ProjectileSpeedTimer.Start(15);

                //if (applyAIShip.ProjectileSpeedBar != null)
                //{
                //    applyAIShip.ProjectileSpeedBar.Percent = 0f;
                //    applyAIShip.ProjectileSpeedBar = null;
                //}
                //applyAIShip.ProjectileSpeedBar = new Bar(100, 20, new Color(128, 0, 128, 5));
                //applyAIShip.ProjectileSpeedBar.Position = new Vector2(5, 200);
                applyAIShip.Weapon.StartFire();
            }
        }

        public override void Collide(GameNode node)
        {
            GameplayScreen.FloatingPowerupText.Score = ("+Rapid Fire");
            GameplayScreen.FloatingPowerupText.StartPosition = this.Position;
            GameplayScreen.FloatingPowerupText.Alive = true;
            GameplayScreen.FloatingPowerupText.LifeSpan = 1000;
            base.Collide(node);
        }
    }
}
