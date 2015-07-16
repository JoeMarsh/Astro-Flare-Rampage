using Microsoft.Xna.Framework;
using Astro_Flare_XNASilverlight;

namespace AstroFlare
{
    class PowerupSlowAll : Powerup
    {
        public PowerupSlowAll(SpriteSheet spriteSheet) : base(spriteSheet) { }

        protected override void ApplyPowerup(Ship ship)
        {
            PlayerShip applyShip;

            if (ship == Player.Ship)
            {
                applyShip = ship.GetRoot() as PlayerShip;

                Node.Fps = 120;
                applyShip.SlowAllTimer.Start(1);
                GamePage.levelTime += 1;
                //Timer.TimerSpeedModifier = 0.25f;
            }
        }

        public override void Collide(GameNode node)
        {
            GamePage.FloatingPowerupText.Score = ("+Time Warp");
            GamePage.FloatingPowerupText.StartPosition = this.Position;
            GamePage.FloatingPowerupText.Alive = true;
            GamePage.FloatingPowerupText.LifeSpan = 1000;
            GamePage.levelTimeColor = Color.LightGreen;
            GamePage.levelTimeColorInterval.Start(0.5);
            base.Collide(node);
        }
    }
}
