using Microsoft.Xna.Framework;

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
                GameplayScreen.levelTime += 1;
                //Timer.TimerSpeedModifier = 0.25f;
            }
        }

        public override void Collide(GameNode node)
        {
            GameplayScreen.FloatingPowerupText.Score = ("+Time Warp");
            GameplayScreen.FloatingPowerupText.StartPosition = this.Position;
            GameplayScreen.FloatingPowerupText.Alive = true;
            GameplayScreen.FloatingPowerupText.LifeSpan = 1000;
            GameplayScreen.levelTimeColor = Color.LightGreen;
            GameplayScreen.levelTimeColorInterval.Start(0.5);
            base.Collide(node);
        }
    }
}
