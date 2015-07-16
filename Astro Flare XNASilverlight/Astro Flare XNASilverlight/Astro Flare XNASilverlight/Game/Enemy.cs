using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Astro_Flare_XNASilverlight;
using System;

namespace AstroFlare
{
    class Enemy : Ship
    {
        public static List<GameNode> Enemies = new List<GameNode>();

        public float baseScore;
        public int invulDuration = 2;
        Timer invulTimer;

        public Enemy(SpriteSheet spriteSheet)
            : base(spriteSheet)
        {
            Enemies.Add(this);
            
            this.baseScore = 25;

            this.Invulnerable = true;
            this.invulTimer = new Timer();
            this.invulTimer.Fire += new NotifyHandler(invulTimer_Fire);
            this.invulTimer.Start(invulDuration);
            this.Sprite.Color = new Color(100, 100, 100, 50);
        }

        public Enemy(SpriteSheet spriteSheet, int invulDuration)
            : base(spriteSheet)
        {
            Enemies.Add(this);

            this.baseScore = 25;

            this.Invulnerable = true;
            this.invulTimer = new Timer();
            this.invulTimer.Fire += new NotifyHandler(invulTimer_Fire);
            this.invulTimer.Start(invulDuration);
            this.Sprite.Color = new Color(100, 100, 100, 50);
        }

        void invulTimer_Fire()
        {
            this.Sprite.Color = new Color(255, 255, 255, 255);
            this.Invulnerable = false;
            this.CollisionList = PlayerShip.PlayerShips;
            this.CollisionList2 = EnemyPlayerShip.EnemyPlayerShips;
            invulTimer.Stop();
            invulTimer = null;
        }

        public override void Remove()
        {
            Enemies.Remove(this);
            base.Remove();
        }

        public override void Update(TimeSpan gameTime)
        {
            //foreach (Enemy enemy in Enemies)
            //    if (enemy != this)
            //        if (CheckCollision(this, enemy))
            //            Bounce(this, enemy);

            //if (Player.Ship != null)
            //    if (CheckCollision(this, Player.Ship))
            //        Bounce(this, Player.Ship);
            if (this.Weapon != null && Player.Ship != null)
                this.Weapon.Direction = Vector2.Normalize(Player.Ship.Position - this.Weapon.Position);
            this.RemoveOffScreen();
            base.Update(gameTime);
        }

        public override void Collide(GameNode node)
        {
            int nodeHealth = node.Health;
            node.TakeDamage(this.Health, this);
            this.TakeDamage(nodeHealth, node);
        }

        public override void TakeDamage(int amount, GameNode node)
        {
            base.TakeDamage(amount, node);
            if (this.Health <= 0)
            {
                if (node is AIProjectile)
                {
                    GamePage.AddFloatingScore(GamePage.FloatingScoreList, ((int)(25 * Config.AIMulti)), this.Position, Color.Red, 0f);
                    Config.AIScore += this.baseScore * Config.AIMulti;
                }
                else
                {
                    GamePage.AddFloatingScore(GamePage.FloatingScoreList, ((int)(25 * Config.Multi)), this.Position, Color.Green, 1f);
                    Config.Score += this.baseScore * Config.Multi;

                    Config.EmemiesKilled += 1;

                    Config.KillStreak += 1;

                    if (Config.Level == LevelSelect.Six)
                        Player.ComboTimer.Start(0.5);
                    else
                        Player.ComboTimer.Start(1);
                }
            }
        }
      
        protected override void Explode()
        {

            if (Config.Level == LevelSelect.Five)
            {
                int rand = Config.Rand.Next(1000);
                if (rand >= Config.timeDropChance)
                {
                    PowerupSlowAll slowAll = new PowerupSlowAll(Config.PowerupSlowAllSpriteSheet);
                    slowAll.Position = this.Position;
                }
            }

            int random = Config.Rand.Next(1000);

            //if (random == 1)
            //{
            //    PowerupHealth health = new PowerupHealth(Config.PowerupHealthSpriteSheet);
            //    health.Position = this.Position;
            //}

            if (random <= Config.shieldDropChance)
            {
                PowerupShields shields = new PowerupShields(Config.PowerupShieldsSpriteSheet);
                shields.Position = this.Position;
            }
            else if (random <= Config.projectileDropChance)
            {
                PowerupAddBullet addProjectile = new PowerupAddBullet(Config.PowerupAddProjectileSpriteSheet);
                addProjectile.Position = this.Position;
            }
            else if (random <= Config.missileDropChance)
            {
                PowerupMissiles Missiles = new PowerupMissiles(Config.PowerupMissileSpriteSheet);
                Missiles.Position = this.Position;
            }
            else if (random <= Config.firerateDropChance)
            {
                PowerupShotSpeed shotSpeed = new PowerupShotSpeed(Config.PowerupProjectileSpeedSpriteSheet);
                shotSpeed.Position = this.Position;
            }
            else if (random <= Config.laserDropChance)
            {
                PowerupLaserDefence laserDefence = new PowerupLaserDefence(Config.PowerupLaserDefenceSpriteSheet);
                laserDefence.Position = this.Position;
            }
            //else if (random >= 19 && random <= 28)

            //else if (random >= 29 && random <= 33)

            //if (random >= 29)
            //{
            //    //if (Config.Level == LevelSelect.Six)
            //    //{
            //        PowerupMissiles Missiles = new PowerupMissiles(Config.PowerupMissileSpriteSheet);
            //        Missiles.Position = this.Position;
            //    //}
            //}

            //else if (random > 20)
            //{
            //    PowerupAddBulletEnemy enemyPowerup = new PowerupAddBulletEnemy(Config.PowerupAddProjectileSpriteSheet);
            //    enemyPowerup.Position = this.Position;
            //}
            //else if (random == 6)
            //{
            //    PowerupDamageAll Lightning = new PowerupDamageAll(Config.PowerupLightningSpriteSheet);
            //    Lightning.Position = this.Position;
            //}

            //else if (random == 3)
            //{
            //    PowerupDoubleShot doubleShot = new PowerupDoubleShot(Config.MissilePowerupSheet);
            //    doubleShot.Position = this.Position;
            //}
            //else if (random == 4)
            //{
            //    PowerupTripleShot tripleShot = new PowerupTripleShot(Config.MissilePowerupSheet);
            //    tripleShot.Position = this.Position;
            //}
            //else if (random == 5)
            //{
            //    PowerupMissiles Missiles = new PowerupMissiles(Config.MissilePowerupSheet);
            //    Missiles.Position = this.Position;
            //}

            base.Explode();
        }
    }
}
