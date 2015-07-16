using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;


namespace AstroFlare
{
    class GameNode : Node
    {
        public int Health;
        public List<GameNode> CollisionList;
        public List<GameNode> CollisionList2;

        Timer hitTimer;
        //public SpriteSheet ExplosionSpriteSheet;

        Rectangle bounds;

        //TODO: supply seed
        static Random random = new Random();

        //Event to recursively call explode on ships/attachments
        static EventHandler Action_Explode = new EventHandler(Explode);

        public GameNode(SpriteSheet spriteSheet) : base(spriteSheet) 
        {
            bounds = new Rectangle(0, 0, Config.WorldBoundsX, Config.WorldBoundsY);
            bounds.Inflate((int)(this.Sprite.Origin.X * 3), (int)(this.Sprite.Origin.Y * 3));
            hitTimer = new Timer();
            hitTimer.Fire += new NotifyHandler(hitTimer_Fire);
        }

        void hitTimer_Fire()
        {
            //this.Sprite.TextureIndex = 0;
            Sprite.ColorLerp(this.Sprite.Color, new Color(this.Sprite.Color.R, this.Sprite.Color.G, this.Sprite.Color.B, 255), 1f);
            this.hitTimer.Stop();
        }

        public override void Update(GameTime gameTime)
        {
            if (this.CollisionList != null)
            {
                for (int i = this.CollisionList.Count - 1; i >= 0; i--)
                {
                    //// getting argumentoutofrangeexception when using exploding projectile;
                    //if (this.CollisionList[i] != null)
                    //{
                    if (this != CollisionList[i])
                    {
                        if (Node.CheckCollision(this, CollisionList[i]))
                        {
                            //this.Bounce(this, this.CollisionList[i]);
                            this.Collide(this.CollisionList[i]);
                        }
                    }
                    //}
                }
                //if (this == Player.EnemyPlayer)
                //{
                //    for (int i = Projectile.Projectiles.Count - 1; i >= 0; i--)
                //    {
                //        //// getting argumentoutofrangeexception when using exploding projectile;
                //        //if (this.CollisionList[i] != null)
                //        //{

                //        if (Node.CheckCollision(this, Projectile.Projectiles[i]))
                //            this.Collide(Projectile.Projectiles[i]);
                //        //}
                //    }
                //}
            }

            if (this.CollisionList2 != null)
            {
                for (int i = this.CollisionList2.Count - 1; i >= 0; i--)
                {
                    if (this != CollisionList2[i])
                    {
                        if (Node.CheckCollision(this, CollisionList2[i]))
                        {
                            //this.Bounce(this, this.CollisionList[i]);
                            this.Collide(this.CollisionList2[i]);
                        }

                    }

                }
            }

            base.Update(gameTime);
        }

        public virtual void Collide(GameNode node) { }

        public virtual void TakeDamage(int amount, GameNode node)
        {
            if (amount > 0)
                this.Health -= amount;

            if (this.Health <= 0)
            {
                //this.Perform(Action_Explode, EventArgs.Empty);
                this.Explode();
                this.Remove();
            }
            else
            {
                //this.Sprite.TextureIndex = 1;
                Sprite.ColorLerp(this.Sprite.Color, new Color(this.Sprite.Color.R, this.Sprite.Color.G, this.Sprite.Color.B, 0), 1f);
                this.hitTimer.Start(Config.DamageEffectTimeout);
            }
        }

        protected virtual void Explode()
        {
            //if (this.ExplosionSpriteSheet != null)
            //{
            //    //move this into ship class


            //    //new EffectNode(this.ExplosionSpriteSheet, this.Position);
            //}           
        }

        static void Explode(object sender, EventArgs e)
        {
            GameNode node = sender as GameNode;
            if (node != null)
                node.Explode();
        }

        public virtual void RemoveOffScreen()
        {
            this.RemoveOutOfBounds(bounds);
        }

        public void RemoveOutOfBounds(Rectangle bounds)
        {
            if (!bounds.Contains((int)this.Position.X, (int)this.Position.Y))
                this.Remove();
        }

        public static GameNode PickRandomNode(List<GameNode> targetList)
        {
            if (targetList.Count > 0)
                return targetList[random.Next(0, targetList.Count)];
            //else if (Player.EnemyPlayer != null)
            //    return Player.EnemyPlayer;
            //else if (Player.Ship != null)
            //    return Player.Ship;

            return null;
        }

    }
}
