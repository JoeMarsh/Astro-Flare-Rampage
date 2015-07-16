using Astro_Flare_XNASilverlight;

namespace AstroFlare
{
    class PowerupAddBulletEnemy : PowerupEnemy
    {
        public PowerupAddBulletEnemy(SpriteSheet spriteSheet) : base(spriteSheet) { }

        protected override void ApplyPowerup(Enemy ship)
        {
            ship.Weapon.RemoveWeapon();

            float spread = 0.28f;


            //ship.Weapon = null;

            WeaponBurst burstWeapon = new WeaponBurstWave(ship, 0, 2, spread);
            ship.Weapon = new WeaponAutoBurst(ship, 0.5f, burstWeapon);
            ship.FireAction = new FireAction(enemyProjectileBullet.enemyFireBullet);

            if (ship.Weapon != null)
                ship.Weapon.StartFire();
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