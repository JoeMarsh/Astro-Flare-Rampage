
namespace AstroFlare
{
    class PowerupShields : Powerup
    {
        public PowerupShields(SpriteSheet spriteSheet) : base(spriteSheet) { }

        protected override void ApplyPowerup(Ship ship)
        {
            PlayerShip applyShip;
            EnemyPlayerShip applyAIShip;

            if (ship == Player.Ship)
            {
                if (Config.SoundFXOn)
                    GameStateManagementGame.Instance.soundManager.PlaySound("FX1", 0.7f);  
 
                applyShip = ship.GetRoot() as PlayerShip;

                if (applyShip.Shield.Health < Config.ShieldHealth)
                {
                    if (Config.ship1Active)
                        applyShip.Shield.ShieldRegen(Config.ShieldsPowerupValue + 5);
                    else
                        applyShip.Shield.ShieldRegen(Config.ShieldsPowerupValue);
                }
            }

            if (ship == Player.EnemyPlayer)
            {
                applyAIShip = ship.GetRoot() as EnemyPlayerShip;

                if (applyAIShip.Shield.Health < Config.ShieldHealth)
                    applyAIShip.Shield.ShieldRegen(Config.ShieldsPowerupValue);
            }
        }

        public override void Collide(GameNode node)
        {
            GameplayScreen.FloatingPowerupText.Score = ("+Shields");
            GameplayScreen.FloatingPowerupText.StartPosition = this.Position;
            GameplayScreen.FloatingPowerupText.Alive = true;
            GameplayScreen.FloatingPowerupText.LifeSpan = 1000;
            base.Collide(node);
        }
    }
}
