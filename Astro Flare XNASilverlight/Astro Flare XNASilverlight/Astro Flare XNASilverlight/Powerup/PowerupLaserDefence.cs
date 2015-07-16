using Astro_Flare_XNASilverlight;

namespace AstroFlare
{
    class PowerupLaserDefence : Powerup
    {
        public PowerupLaserDefence(SpriteSheet spriteSheet) : base(spriteSheet) { }

        protected override void ApplyPowerup(Ship ship)
        {
            PlayerShip applyShip;
            //EnemyPlayerShip applyAIShip;

            if (ship == Player.Ship)
            {
                //if (Config.SoundFXOn)
                //    GameStateManagementGame.Instance.soundManager.PlaySound("FX1", 0.7f);   
                Sounds.PlaySound("powerup");

                applyShip = ship.GetRoot() as PlayerShip;

                applyShip.laserDefence += 5;
            }

            if (ship == Player.EnemyPlayer)
            {

            }
        }

        public override void Collide(GameNode node)
        {
            GamePage.FloatingPowerupText.Score = ("+Laser Defence");
            GamePage.FloatingPowerupText.StartPosition = this.Position;
            GamePage.FloatingPowerupText.Alive = true;
            GamePage.FloatingPowerupText.LifeSpan = 1000;
            base.Collide(node);
        }
    }
}