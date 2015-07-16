using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Devices;

namespace AstroFlare
{
    class PlayerShip : Ship
    {
        /////////////AI
        GameNode target;
        Vector2 moveTo;
        //Timer fireTimer;
        float rotation;
        //////////////

        // used mainly as collision list for enemies, should refactor all calls to just use PlayerShip instead, unless we want to add dones to this list
        // in which case need to refactor to not look at PlayerShips[0] etc.
        public static List<GameNode> PlayerShips = new List<GameNode>();

        public Bar ProjectileSpeedBar;
        public Bar AddProjectileBar;
        public Bar HomingMissileBar;
        public Bar InvulnAbilityBar;
        //public Bar HealthBar;
        //public Bar ShieldBar;

        public Timer ProjectileSpeedTimer;
        public Timer AddProjectileTimer;
        public Timer HomingMissileTimer;
        public Timer SlowAllTimer;

        public Timer InvulnerableTimer;
        public bool isInvulnerable;
        public bool megaMagnetActive;
        float alpha;

        public int laserDefence = 0;

        //bool purple = false;
        //bool blue = false;

        public float FireInterval;

        static VibrateController vibrate = VibrateController.Default;

        public PlayerShield Shield;
        //Sidearm sidearm1;
        //Sidearm sidearm2;
        Sprite baseTexture;

        public int weaponBurstTotal = 0;
        public int MaxHealth = 0;

        Laser laser;

        public PlayerShip(SpriteSheet spriteSheet)
            : base(spriteSheet)
        {
            PlayerShips.Add(this);
            this.Speed = Config.ShipSpeed;
            this.Health = Config.ShipHealth;
            this.MaxHealth = Config.ShipHealth;
            this.FireInterval = Config.PlayerFireInterval;
            //this.ExplosionSpriteSheet = Config.SmallExplosionSpriteSheet;

            this.FireAction = new FireAction(ProjectileBulletGreenBeam.FireBullet);

            this.Weapon = new WeaponAuto(this, FireInterval);

            InvulnerableTimer = new Timer();
            InvulnerableTimer.Fire += new NotifyHandler(InvulnerableTimer_Fire);
            this.InvulnerableTimer.Start(3);
            isInvulnerable = true;
            megaMagnetActive = false;

            laser = new Laser();
            ////ShieldBar = new Bar(400, 2, Color.RoyalBlue);
            //ShieldBar = new Bar(400, 2, new Color(65, 105, 225, 0));
            //ShieldBar.Position = new Vector2(200, 440);
            //ShieldBar.DrawCentered = true;

            ////HealthBar = new Bar(400, 2, Color.Red);
            //HealthBar = new Bar(400, 2, new Color(255, 0, 0, 0));
            //HealthBar.Position = new Vector2(200, 460);
            //HealthBar.DrawCentered = true;

            //WeaponBurst burstWeapon = new WeaponBurstWave(this, 0, 1, 0.14f);
            //this.Weapon = new WeaponAutoBurst(this, Config.PlayerFireInterval, burstWeapon);

            //this.FireAction = new FireAction(ProjectileMissile.FireMissile);
            //this.Weapon = new WeaponBurstWave(this, Config.WaveFireInterval, Config.WaveFireTotal, Config.WaveFireSpread);

            //WeaponBurst burstWeapon = new WeaponBurstWave(this, Config.WaveFireInterval, Config.WaveFireTotal, Config.WaveFireSpread);
            //this.Weapon = new WeaponAutoBurst(this, Config.WaveFireInterval * Config.WaveFireTotal, burstWeapon);

            //spawn sidearms
            //sidearm1 = new Sidearm(Config.BuddySpriteSheet, this);
            //sidearm1.Position = this.Position;
            //sidearm1.SetParent(this);

            //sidearm1 = new Sidearm(Config.BulletSheetGreenLaser, this, new Vector2(30, 0));
            //sidearm1.Position = new Vector2(Config.WorldBoundsX / 2, Config.WorldBoundsY / 2 - this.Sprite.Origin.Y);
            //sidearm1.SetParent(this);

            //sidearm2 = new Sidearm(Config.BulletSheetGreenLaser, this, new Vector2(-30, 0));
            //sidearm2.Position = new Vector2(Config.WorldBoundsX / 2, Config.WorldBoundsY / 2 - this.Sprite.Origin.Y);
            //sidearm2.SetParent(this);

            this.Position = new Vector2(Config.WorldBoundsX / 2, Config.WorldBoundsY / 2 - this.Sprite.Origin.Y);
            this.baseTexture = new Sprite(Config.CurrentShipBase);
            this.baseTexture.Color = Config.CurrentShipColor;
            //GameStateManagementGame.Instance.soundManager.PlaySound("ShipSpawn", 0.5f);

            ProjectileSpeedTimer = new Timer();
            ProjectileSpeedTimer.Fire += new NotifyHandler(ProjectileSpeedTimer_Fire);

            AddProjectileTimer = new Timer();
            AddProjectileTimer.Fire += new NotifyHandler(AddProjectileTimer_Fire);

            HomingMissileTimer = new Timer();
            HomingMissileTimer.Fire += new NotifyHandler(HomingMissileTimer_Fire);

            SlowAllTimer = new Timer();
            SlowAllTimer.Fire += new NotifyHandler(SlowAllTimer_Fire);

            Shield = new PlayerShield(Config.ShipShield, this);
            //PlayerShips.Add(Shield);

            //////////AI
            if (Config.AIControlled)
            {
                //this.Speed = 8f;
                //moveTo = new Vector2(Config.Rand.Next(50, 1550), Config.Rand.Next(50, 910));
                this.Weapon.StartFire();
                ////this.fireTimer = new Timer();
                ////this.fireTimer.Fire += new NotifyHandler(fireTimer_Fire);
                ////this.fireTimer.Start(FireInterval);
            }
            ////////////

        }

        public override void Update(GameTime gameTime)
        {
            //this.baseTexture.ColorLerp(Config.DamageColor, Config.ShipColor, ((float)this.Health / Config.ShipHealth));
            //this.shield.ColorLerp(Color.Red, Color.Green, ((float)this.Health / Config.ShipHealth));

            //this.Weapon.Position = this.Position;
            laser.UpdateLaser(gameTime);

            if (isInvulnerable == true)
            {
                alpha += (float)gameTime.ElapsedGameTime.Milliseconds / 30;

                this.Sprite.ColorLerp(new Color(255, 255, 255, 255), new Color(0, 0, 0, 0), (alpha % 1));
            }

            if (this.Shield != null)
            {
                if (this.Shield.Health > 0 && this.Shield.Dead == true)
                {
                    //Config.ShieldHealth = this.Shield.Health;
                    int currentShieldHealth = this.Shield.Health;
                    Shield = new PlayerShield(Config.ShipShield, this);
                    Shield.Health = currentShieldHealth;
                }
            }

            //if (Config.Score > 10000 && purple == false)
            //{
            //    Config.CurrentProjectile = Config.BulletSheetEnergyPurple;
            //    purple = true;
            //}

            //if (Config.Score > 30000 && blue == false)
            //{
            //    Config.CurrentProjectile = Config.BulletSheetBlueLaserDouble;
            //    blue = true;
            //}

            /////////////AI
            if (Config.AIControlled)
                UpdateAI(gameTime);

            #region base.Update function
            //if (this.CollisionList != null)
            //{
            //    for (int i = this.CollisionList.Count - 1; i >= 0; i--)
            //    {
            //        if (this != CollisionList[i])
            //        {
            //            if (Node.CheckCollision(this, CollisionList[i]))
            //                this.Collide(this.CollisionList[i]);
            //        }
            //    }
            //}

            //if (this.CollisionList2 != null)
            //{
            //    for (int i = this.CollisionList2.Count - 1; i >= 0; i--)
            //    {
            //        if (this != CollisionList2[i])
            //        {
            //            if (Node.CheckCollision(this, CollisionList2[i]))
            //                this.Collide(this.CollisionList2[i]);
            //        }

            //    }
            //}

            //Velocity = Direction * Speed * ((float)gameTime.ElapsedGameTime.TotalMilliseconds / 30);
            //Position += Velocity;

            //this.Sprite.Update(gameTime); 
            #endregion
            base.Update(gameTime);

            if (this.Position.X < this.Sprite.Origin.X)
                this.Position = new Vector2(this.Sprite.Origin.X, this.Position.Y);
            else if (this.Position.X > Config.WorldBoundsX - this.Sprite.Origin.X)
                this.Position = new Vector2(Config.WorldBoundsX - this.Sprite.Origin.X, this.Position.Y);

            if (this.Position.Y < this.Sprite.Origin.Y)
                this.Position = new Vector2(this.Position.X, this.Sprite.Origin.Y);
            else if (this.Position.Y > Config.WorldBoundsY - this.Sprite.Origin.Y)
                this.Position = new Vector2(this.Position.X, Config.WorldBoundsY - this.Sprite.Origin.Y);

            ///////////AI

            ///////////////
            if (Config.ship1Active)
            {
                ParticleEffects.TriggerShip1Trail(this.Position);
            }
            else if (Config.ship2Active)
            {
                ParticleEffects.TriggerShip2Trail(this.Position);
            }
            else if (Config.ship3Active)
            {
                ParticleEffects.TriggerShip3Trail(this.Position);
            }

            if (this.ProjectileSpeedBar != null)
                this.ProjectileSpeedBar.Percent -= (float)(gameTime.ElapsedGameTime.TotalSeconds / 15);

            if (this.AddProjectileBar != null)
                this.AddProjectileBar.Percent -= (float)(gameTime.ElapsedGameTime.TotalSeconds / 15);

            if (this.HomingMissileBar != null)
                this.HomingMissileBar.Percent -= (float)(gameTime.ElapsedGameTime.TotalSeconds / 5);

            if (this.InvulnAbilityBar != null)
                this.InvulnAbilityBar.Percent -= (float)(gameTime.ElapsedGameTime.TotalSeconds / 10);

            //if (this.HealthBar != null)
            //    this.HealthBar.Percent = ((float)this.Health / Config.ShipHealth); 

            //if (this.ShieldBar != null)
            //    this.ShieldBar.Percent = ((float)Shield.Health / Config.ShieldHealth);
        }

        ////////////////AI
        //void fireTimer_Fire()
        //{
        //    if (this.Dead)
        //        return;

        //    this.Weapon.StartFire();
        //}
        ////////////////////////////////////

        void InvulnerableTimer_Fire()
        {
            isInvulnerable = false;
            megaMagnetActive = false;

            this.Sprite.Color = new Color(255, 255, 255, 255);
            InvulnerableTimer.Stop();
            //InvulnerableTimer = null;

            if (InvulnAbilityBar != null)
            {
                InvulnAbilityBar.Percent = 0f;
                InvulnAbilityBar = null;
            }
        }

        public override void TakeDamage(int amount, GameNode node)
        {
            if (isInvulnerable == true || Config.Level == LevelSelect.Practise)
            {

            }
            else
            {
                if (node == Player.EnemyPlayer)
                    amount /= 2;
                //if (this.Shield.Health > 0)
                //    this.Shield.TakeDamage(amount);
                //else
                    base.TakeDamage(amount, node);

                if (Config.Vibrate)
                    vibrate.Start(TimeSpan.FromSeconds(0.1));
            }
        }

        #region Timers

        void ProjectileSpeedTimer_Fire()
        {
            if (this.Weapon != null)
                this.Weapon.StopFire();

            this.FireInterval = Config.PlayerFireInterval;

            if (this.Weapon != null)
                this.Weapon.FireInterval = FireInterval;

            //this.Weapon = null;
            //this.Weapon = new WeaponAuto(this, FireInterval);

            if (ProjectileSpeedBar != null)
            {
                ProjectileSpeedBar.Percent = 0f;
                ProjectileSpeedBar = null;
            }

                //Player.NotFiring = true;

            if (this.Weapon != null)
                this.Weapon.StartFire();
            ProjectileSpeedTimer.Stop();
        }

        void AddProjectileTimer_Fire()
        {
            if (this.Weapon != null)
                this.Weapon.RemoveWeapon();

            //this.FireAction = null;
            //this.FireAction = new FireAction(ProjectileBulletGreenBeam.FireBullet);

            //this.Weapon = null;
            this.Weapon = new WeaponAuto(this, FireInterval);

            if (AddProjectileBar != null)
            {
                AddProjectileBar.Percent = 0f;
                AddProjectileBar = null;
            }

            if (this.Weapon != null)
                Player.NotFiring = true;

            if (this.Weapon != null)
                this.Weapon.StartFire();

            weaponBurstTotal = 0;
            AddProjectileTimer.Stop();
        }

        void HomingMissileTimer_Fire()
        {
            if (this.Weapon != null)
                this.Weapon.StopFire();

            //this.FireAction = null;
            this.FireAction = new FireAction(ProjectileBulletGreenBeam.FireBullet);
            //this.FireAction = new FireAction(ProjectileMissile.FireMissile);
            //this.Weapon = null;
            //this.Weapon = new WeaponAuto(this, FireInterval);

            if (this.Weapon != null)
                Player.NotFiring = true;

            if (this.Weapon != null)
                this.Weapon.StartFire();

            if (HomingMissileBar != null)
            {
                HomingMissileBar.Percent = 0f;
                HomingMissileBar = null;
            }
            HomingMissileTimer.Stop();
        }

        void SlowAllTimer_Fire()
        {
            Node.Fps = 30;
            //Timer.TimerSpeedModifier = 1f;
            SlowAllTimer.Stop();
        }

        #endregion

        public override void Remove()
        {
            ProjectileSpeedTimer.Stop();
            AddProjectileTimer.Stop();
            HomingMissileTimer.Stop();
            SlowAllTimer.Stop();
            Node.Fps = 30;
            //Timer.TimerSpeedModifier = 1f;
            //fireTimer.Stop();
            //fireTimer = null;
            Bar.Bars.Remove(ProjectileSpeedBar);
            Bar.Bars.Remove(AddProjectileBar);
            Bar.Bars.Remove(HomingMissileBar);
            Bar.Bars.Remove(InvulnAbilityBar);
            PlayerShips.Remove(this);
            Player.RemoveShip(this);
            this.Shield.Remove();
            this.Shield = null;
            //sidearm1.Remove();
            //sidearm2.Remove();
            base.Remove();
        }

        protected override void Explode()
        {
            //ParticleEffects.TriggerMegaExplosionEffect(this.Position);
            Config.Multi = Math.Max(Config.Multi / 2, 1d);

            if (ProjectileSpeedBar != null)
            {
                ProjectileSpeedBar.Percent = 0f;
                ProjectileSpeedBar = null;
            }
            
            base.Explode();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //this.shield.Draw(spriteBatch, this.Position, this.Rotation);
            base.Draw(spriteBatch);
            laser.DrawLaser(Config.BulletSheetGreenLaser.Textures[0], 10f, spriteBatch);
            this.baseTexture.Draw(spriteBatch, this.Position, this.Rotation);
        }

        float aimTime;
        public void UpdateAI(GameTime gameTime)
        {
            aimTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (aimTime > 1)
            {
                if (Enemy.Enemies.Count > 0)
                {
                    this.target = PickRandomNode(Enemy.Enemies);
                    if (this.target != null)
                    {
                        for (int i = Enemy.Enemies.Count - 1; i >= 0; i--)
                        {
                            if ((Vector2.Distance(this.Position, this.target.Position) > (Vector2.Distance(this.Position, Enemy.Enemies[i].Position))))
                            {
                                this.target = Enemy.Enemies[i];
                            }
                        }
                        if (Vector2.Distance(this.Position, this.target.Position) > 500)
                        {
                            target = null;
                        }
                        //if (Vector2.Distance(this.Position, this.target.Position) < 200)
                        //{
                        //    //this.moveTo = Vector2.Negate((Vector2.Normalize(target.Position - this.Position)) * 200);
                        //    this.moveTo = ((this.Position - target.Position) * 200);
                        //}
                    }
                }
                aimTime = 0;


                if (Player.EnemyPlayer != null)
                {
                    if (Vector2.Distance(this.Position, Player.EnemyPlayer.Position) < 350)
                    {
                        this.target = Player.EnemyPlayer;
                        //this.FireAction = new FireAction(shooterProjectileBullet.enemyFireBullet);
                    }
                    //else
                    //{
                    //    this.FireAction = new FireAction(EnemyPlayerProjectileBullet.FireBullet);
                    //}
                }
            }

            if (this.target != null && this.Weapon != null)
            {
                if (this.target == Player.EnemyPlayer)
                    this.Weapon.Direction = Vector2.Normalize((target.Position + new Vector2(Config.Rand.Next(0, 40) - 20, Config.Rand.Next(0, 40) - 20)) - this.Weapon.Position);
                else
                    this.Weapon.Direction = Vector2.Normalize(target.Position - this.Weapon.Position);
            }

            //aiMove();
        }

        void aiMove()
        {
            this.Speed = 8f;

            if (Vector2.Distance(this.Position, this.moveTo) < 50f)
                moveTo = new Vector2(Config.Rand.Next(50, 1550), Config.Rand.Next(50, 910));

            for (int i = Powerup.Powerups.Count - 1; i >= 0; i--)
            {
                if ((Vector2.Distance(this.Position, moveTo) > (Vector2.Distance(this.Position, Powerup.Powerups[i].Position))))
                {
                    moveTo = Powerup.Powerups[i].Position;
                }
            }

            this.rotation = Steering.TurnToFace(this.Position, moveTo, this.rotation, 0.2f);

            for (int i = enemyProjectile.EnemyProjectiles.Count - 1; i >= 0; i--)
            {
                if (Vector2.Distance(this.Position, enemyProjectile.EnemyProjectiles[i].Position) < 100)
                {
                    this.rotation = Steering.TurnToFace(enemyProjectile.EnemyProjectiles[i].Position, this.Position, this.rotation, 0.4f);
                }
            }

            for (int i = AIProjectile.AIProjectiles.Count - 1; i >= 0; i--)
            {
                if (Vector2.Distance(this.Position, AIProjectile.AIProjectiles[i].Position) < 100)
                {
                    this.rotation = Steering.TurnToFace(AIProjectile.AIProjectiles[i].Position, this.Position, this.rotation, 0.4f);
                }
            }

            //if (this.target != null)
            //    if (Vector2.Distance(this.Position, this.target.Position) < 200)
            //        this.rotation = Steering.TurnToFace(this.Position, moveTo, this.rotation, 0.4f);

            for (int i = Enemy.Enemies.Count - 1; i >= 0; i--)
            {
                if (Vector2.Distance(this.Position, Enemy.Enemies[i].Position) < 250)
                {
                    this.rotation = Steering.TurnToFace(Enemy.Enemies[i].Position, this.Position, this.rotation, 0.4f);
                }
            }

            this.Direction = new Vector2((float)Math.Cos(this.rotation), (float)Math.Sin(this.rotation));
            this.Direction.Normalize();
            this.Rotation = rotation + MathHelper.PiOver2;

            if (this.moveTo.X < 5)
                moveTo = new Vector2(Config.Rand.Next(50, 1550), Config.Rand.Next(50, 910));
            else if (this.moveTo.X > Config.WorldBoundsX - 5)
                moveTo = new Vector2(Config.Rand.Next(50, 1550), Config.Rand.Next(50, 910));

            if (this.moveTo.Y < 5)
                moveTo = new Vector2(Config.Rand.Next(50, 1550), Config.Rand.Next(50, 910));
            else if (this.moveTo.Y > Config.WorldBoundsY - 5)
                moveTo = new Vector2(Config.Rand.Next(50, 1550), Config.Rand.Next(50, 910));

            //ability usage
            if (GameplayScreen.playerAbilityUses > 0 && Enemy.Enemies.Count > 30)
            {
                if (Config.ship1Active)
                {
                    if (Player.Ship != null)
                    {
                        //Player.Ship.Health = (int)MathHelper.Min(Player.Ship.Health + 25, Player.Ship.MaxHealth);
                        Player.Ship.Health = (int)MathHelper.Min(Player.Ship.Health + 10, Player.Ship.MaxHealth);
                        Player.Ship.Shield.ShieldRegen(20);
                        GameplayScreen.playerAbilityUses--;
                    }
                }
                else if (Config.ship2Active)
                {
                    if (Player.Ship != null)
                    {
                        PowerupDamageAll AoEDamage = new PowerupDamageAll(Config.PowerupSlowAllSpriteSheet);
                        AoEDamage.Position = Player.Ship.Position;
                        GameplayScreen.playerAbilityUses--;
                    }
                }
                else if (Config.ship3Active)
                {
                    if (Player.Ship != null)
                    {
                        Player.Ship.InvulnerableTimer.Start(10);
                        Player.Ship.isInvulnerable = true;
                        GameplayScreen.playerAbilityUses--;
                        Player.Ship.InvulnAbilityBar = new Bar(100, 20, new Color(255, 255, 255, 5));
                        Player.Ship.InvulnAbilityBar.Position = new Vector2(5, 290);
                    }
                }
            }
        }

    }
}


