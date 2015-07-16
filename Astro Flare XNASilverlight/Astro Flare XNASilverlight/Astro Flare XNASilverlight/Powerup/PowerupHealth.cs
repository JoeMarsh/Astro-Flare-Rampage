using System;
using Astro_Flare_XNASilverlight;

namespace AstroFlare
{
    class PowerupHealth : Powerup
    {
        public PowerupHealth(SpriteSheet spriteSheet) : base(spriteSheet) { }

        protected override void ApplyPowerup(Ship ship)
        {
            if (ship == Player.Ship || ship == Player.EnemyPlayer)
            {
                if (ship.Health < Config.ShipHealth)
                {
                    ship.Health = Math.Min(ship.Health + Config.HealthPowerupValue, Config.ShipHealth);
                    //new EffectNode(Config.Heal, ship.Position);
                }
            }
        }

        public override void Collide(GameNode node)
        {
            GamePage.FloatingPowerupText.Score = ("+Health");
            GamePage.FloatingPowerupText.StartPosition = this.Position;
            GamePage.FloatingPowerupText.Alive = true;
            GamePage.FloatingPowerupText.LifeSpan = 1000;
            base.Collide(node);
        }
    }
}
