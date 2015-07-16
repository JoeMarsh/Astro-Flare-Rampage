using Microsoft.Xna.Framework;

namespace AstroFlare
{
    class PowerupMissiles : Powerup
    {
        public PowerupMissiles(SpriteSheet spriteSheet) : base(spriteSheet) { }

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

                applyShip.FireAction = new FireAction(ProjectileMissile.FireMissile);

                applyShip.HomingMissileTimer.Start(5);

                if (applyShip.HomingMissileBar != null)
                {
                    applyShip.HomingMissileBar.Percent = 0f;
                    applyShip.HomingMissileBar = null;
                }
                applyShip.HomingMissileBar = new Bar(100, 20, new Color(255, 165, 0, 5));
                applyShip.HomingMissileBar.Position = new Vector2(5, 260);

                //Player.NotFiring = true;
                applyShip.Weapon.StartFire();
            }

            if (ship == Player.EnemyPlayer)
            {
                applyAIShip = ship.GetRoot() as EnemyPlayerShip;

                applyAIShip.Weapon.StopFire();

                applyAIShip.FireAction = new FireAction(AIProjectileMissile.FireMissile);

                applyAIShip.HomingMissileTimer.Start(5);

                //if (applyAIShip.HomingMissileBar != null)
                //{
                //    applyAIShip.HomingMissileBar.Percent = 0f;
                //    applyAIShip.HomingMissileBar = null;
                //}
                //applyAIShip.HomingMissileBar = new Bar(100, 20, new Color(255, 165, 0, 5));
                //applyAIShip.HomingMissileBar.Position = new Vector2(5, 260);

                applyAIShip.Weapon.StartFire();
            }
        }

        public override void Collide(GameNode node)
        {
            GameplayScreen.FloatingPowerupText.Score = ("+Missile");
            GameplayScreen.FloatingPowerupText.StartPosition = this.Position;
            GameplayScreen.FloatingPowerupText.Alive = true;
            GameplayScreen.FloatingPowerupText.LifeSpan = 1000;
            base.Collide(node);
        }
    }
}
