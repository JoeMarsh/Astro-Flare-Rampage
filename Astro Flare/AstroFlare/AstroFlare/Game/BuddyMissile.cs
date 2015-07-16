using System;
using Microsoft.Xna.Framework;

namespace AstroFlare
{
    class BuddyMissile : Buddy
    {
        GameNode target;
        Timer fireTimer;

        public BuddyMissile(SpriteSheet spriteSheet)
            : base(spriteSheet)
        {
            this.Health = Config.EnemyHealth;
            //this.ExplosionSpriteSheet = Config.SmallExplosionSpriteSheet;

            this.FireAction = new FireAction(this.FireBullet);
            this.Weapon = new WeaponBurst(this, Config.EnemyBurstFireInterval, Config.EnemyBurstFireTotal);
            this.Weapon.Direction = new Vector2(0, 1);
            //this.Weapon.Offset = new Vector2(0, this.Sprite.Origin.Y);

            this.fireTimer = new Timer();
            this.fireTimer.Fire += new NotifyHandler(fireTimer_Fire);
            this.fireTimer.Start(Config.EnemyFireInterval);

            this.Speed = Config.BuddySpeed;

            this.CollisionList = null;
        }

        void fireTimer_Fire()
        {
            if (this.Dead)
                return;

            //Pick a random player as target
            target = GameNode.PickRandomNode(Enemy.Enemies);

            //if (PlayerShip.PlayerShips.Count > 0)
            //    this.target = PlayerShip.PlayerShips[0];
            //else
            //    this.target = null;

            //if (this.target == null)
            //    return;
            if (Enemy.Enemies.Count >= 1)
                this.Weapon.Direction = Vector2.Normalize(target.Position - this.Weapon.Position);

            //makes sure enemy only fires weapon if enemy is above player
            //if (this.Weapon.Direction.Y <= 0f)
            //    return;
            if (Enemy.Enemies.Count >= 1)
                this.Weapon.StartFire();
            else
                this.Weapon.StopFire();
        }

        public override void Remove()
        {
            this.fireTimer.Stop();
            base.Remove();
        }

        public override void Update(GameTime gameTime)
        {

                //TODO: rewrite for 360degree gamefield
                //Avoidance
                //if (Vector2.Distance(this.Position, this.target.Position) < Config.EnemyAvoidanceRadius)
                //{
                //    Vector2 down = new Vector2(0, 1);
                //    Vector2 awayFromTarget = Vector2.Normalize(this.Position - this.target.Position);
                //    this.Direction = Vector2.Lerp(down, awayFromTarget, Config.EnemyAvoidanceWeight);
                //    this.Direction.Normalize();
                //}
                //else
                //    this.Direction = new Vector2(0, 1);

                //if (Enemy.Enemies.Count >= 1)

            if (Player.Ship != null)
            {
                this.Rotation = Steering.TurnToFace(this.Position, Player.Ship.Position, this.Rotation, Config.MissileTurnIncrement);

                this.Direction = new Vector2((float)Math.Cos(this.Rotation), (float)Math.Sin(this.Rotation));
                this.Direction.Normalize();
            }
//testing remove **********************************************************************
            else
            {
                if (Enemy.Enemies.Count >= 1 && target != null)
                {
                    this.Rotation = Steering.TurnToFace(this.Position, this.target.Position, this.Rotation, 0.10f);
                    this.Direction = new Vector2((float)Math.Cos(this.Rotation), (float)Math.Sin(this.Rotation));
                    this.Direction.Normalize();
                    //this.Position += this.Direction * this.Speed;
                }
                
            }
//*********************************************************************************************
                ////asteroid.Velocity = new Vector2((float)Math.Cos(asteroid.Rotation), (float)Math.Sin(asteroid.Rotation));
                
                //this.Direction *= this.Speed;
            //this.Position += Direction * Speed;

            base.Update(gameTime);
        }

        //TODO: maybe replace with new class of enemy projectile/projectile bullet type
        void FireBullet(Vector2 direction, Vector2 position)
        {
            if (!this.Dead)
            {
                Projectile bullet = new ProjectileMissile(Config.EnemyPlayerBulletSheet);
                bullet.CollisionList = Enemy.Enemies;
                bullet.Position = position;
                bullet.Direction = direction;
                bullet.Speed = Config.EnemyBulletSpeed;
                bullet.Damage = Config.EnemyBulletDamage;
                //bullet.ExplosionSpriteSheet = Config.ProjectileExplosion;
            }
        }
    }
}
