using System;
using Microsoft.Xna.Framework;

namespace AstroFlare
{
    class PlayerShield : Ship
    {
        //GameNode target;
        //Timer fireTimer;
        Timer shieldVisibleTimer;
        Timer shieldRegenTick;
        int shieldRegenAmount;
        int shieldRegenRate = 5;
        Ship parentShip;

        public PlayerShield(SpriteSheet spriteSheet, Ship parentShip)
            : base(spriteSheet)
        {
            this.Health = Config.ShieldHealth;

            this.CollisionList = Enemy.Enemies;
            //this.CollisionList2 = EnemyPlayerShip.EnemyPlayerShips;

            shieldVisibleTimer = new Timer();
            shieldVisibleTimer.Fire += new NotifyHandler(shieldVisibleTimer_Fire);

            shieldRegenTick = new Timer();
            shieldRegenTick.Fire += new NotifyHandler(shieldRegenTick_Fire);
            this.parentShip = parentShip;
        }

        void shieldVisibleTimer_Fire()
        {
            this.Sprite.Color = Config.ShieldColor;
            shieldVisibleTimer.Stop();
        }

        public override void Remove()
        {
            base.Remove();
        }

        public override void Update(TimeSpan gameTime)
        {
            //if (this.CollisionList != null)
            //{
            //    for (int i = this.CollisionList.Count - 1; i >= 0; i--)
            //    {
            //        if (!this.CollisionList[i].Invulnerable)
            //            if (Node.CheckCollision(this, CollisionList[i]))
            //                this.Collide(this.CollisionList[i]);
            //    }
            //}

            if (enemyProjectile.EnemyProjectiles.Count > 0)
            {
                for (int i = enemyProjectile.EnemyProjectiles.Count - 1; i >= 0; i--)
                {
                    if (Node.CheckCollision(this, enemyProjectile.EnemyProjectiles[i]))
                        // need to change so that all collide methods use damage instead of health. this is currently using projectile collide instead of own collide.
                        //enemyProjectile.EnemyProjectiles[i].Collide(this);
                        this.Collide(enemyProjectile.EnemyProjectiles[i]);
                }
            }

            if (AIProjectile.AIProjectiles.Count > 0)
            {
                for (int i = AIProjectile.AIProjectiles.Count - 1; i >= 0; i--)
                {
                    if (Node.CheckCollision(this, AIProjectile.AIProjectiles[i]))
                        // need to change so that all collide methods use damage instead of health. this is currently using projectile collide instead of own collide.
                        //AIProjectile.AIProjectiles[i].Collide(this);
                        this.Collide(AIProjectile.AIProjectiles[i]);
                }
            }

            //this.Sprite.ColorLerp(new Color(255, 0, 0, 0), new Color(0, 128, 0, 255), ((float)this.Health / Config.ShieldHealth));
            //this.Sprite.ColorLerp(new Color(0, 0, 255, 0), Color.Blue, ((float)this.Health / Config.ShieldHealth));
            this.Rotation = Steering.TurnToFace(this.Position, parentShip.Position, this.Rotation, 0.3f);
            //this.Rotation += (float)(Math.PI / 4d);

            //this.Sprite.Update(gameTime);
            base.Update(gameTime);
            this.Position = parentShip.Position;
        }

        public override void TakeDamage(int amount, GameNode node)
        {
            //if (this.Health <= (Config.ShieldHealth / 2))
            //    this.Sprite.Color = Color.DarkRed;
            if (Player.Ship != null)
            {
                if (!Player.Ship.isInvulnerable && !node.Invulnerable)
                {
                    if (node == Player.EnemyPlayer)
                        amount /= 2;

                    if (amount > 0)
                        this.Health -= amount;
                }

                this.Sprite.Color = Color.RoyalBlue;
                shieldVisibleTimer.Start(1);
            }
        }

        public override void Collide(GameNode node)
        {
            if (!this.parentShip.Invulnerable && !node.Invulnerable)
            {
                int nodeHealth = node.Health;

                if (this.Health < 0)
                    this.Health = 0;

                if (this.Health > 0)
                {
                    node.TakeDamage(this.Health, this);
                    this.TakeDamage(nodeHealth, node);

                    base.Collide(node);
                }
            }
        }

        public void ShieldRegen(int amount)
        {
            shieldRegenTick.Start(1);
            shieldRegenAmount = amount;
        }

        void shieldRegenTick_Fire()
        {
            if (Player.Ship != null)
            {
                if (Player.Ship.Shield.Health < Config.ShieldHealth)
                {
                    Player.Ship.Shield.Health += shieldRegenRate;
                    Player.Ship.Shield.Health = Math.Min(Player.Ship.Shield.Health, Config.ShieldHealth);
                }
                else
                    shieldRegenTick.Stop();
            }
            else
                shieldRegenTick.Stop();

            shieldRegenAmount -= shieldRegenRate;
            if (shieldRegenAmount <= 0)
                shieldRegenTick.Stop();
        }
    }
}
