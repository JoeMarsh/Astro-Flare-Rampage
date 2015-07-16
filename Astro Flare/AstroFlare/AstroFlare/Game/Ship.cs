using Microsoft.Xna.Framework;
//using Microsoft.Devices;

namespace AstroFlare
{
    delegate void FireAction(Vector2 direction, Vector2 position);

    class Ship : GameNode
    {
        public FireAction FireAction;
        public Weapon Weapon;
        //static VibrateController vibrate = VibrateController.Default;

        public Ship(SpriteSheet spriteSheet) : base(spriteSheet) { }

        public virtual void StartFire()
        {
            if (this.Weapon != null)
                this.Weapon.StartFire();
        }

        public virtual void StopFire()
        {
            if (this.Weapon != null)
                this.Weapon.StopFire();
        }

        public override void Remove()
        {
            if (this.Weapon != null)
                this.Weapon.RemoveWeapon();
            this.Weapon = null;
            base.Remove();
        }

        protected override void Explode()
        {
            ParticleEffects.TriggerExplosionSquaresLarge(this.Position);
            //ParticleEffects.TriggerMegaExplosionEffect(this.Position);

            if (Config.SoundFXOn)
                GameStateManagementGame.Instance.soundManager.PlaySound("ShipExplode", 1.0f);

            //GameplayScreen.cam.Move(new Vector2(20, 0));
            //vibrate.Start(TimeSpan.FromSeconds(0.1));

            base.Explode();
        }

    }
}
