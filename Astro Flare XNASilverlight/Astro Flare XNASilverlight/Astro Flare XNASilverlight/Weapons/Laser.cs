using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AstroFlare
{
    class Laser
    {
        float laserDamageTime;
        Vector2 direction;
        float rotation;
        float width;
        List<GameNode> targets; 

        public Laser()
        {
            targets = new List<GameNode>(); 
        }

        private void LaserDamage(TimeSpan gameTime, GameNode target)
        {
            if (Player.Ship != null && target != null)
            {
                laserDamageTime += (float)gameTime.TotalSeconds;
                                                
                if (laserDamageTime >= 0.25)
                {
                    target.TakeDamage(5, Player.Ship);
                }

                if (target.Dead == true || target.Health <= 0)
                {
                    Player.Ship.laserDefence--;
                    ParticleEffects.TriggerExplosionSquaresSmall(target.Position);
                }
            }

            if (laserDamageTime >= 0.25)
            {
                laserDamageTime = 0;
            }
        }

        public void UpdateLaser(TimeSpan gameTime)
        {
            targets.Clear();

            if (Player.Ship != null)
            {
                if (Player.Ship.laserDefence > 0)
                {
                    for (int i = enemyProjectile.EnemyProjectiles.Count - 1; i >= 0; i--)
                    {
                        if (Vector2.Distance(enemyProjectile.EnemyProjectiles[i].Position, Player.Ship.Position) < 250)
                        {
                            if (enemyProjectile.EnemyProjectiles[i] != null)
                            {
                                targets.Add(enemyProjectile.EnemyProjectiles[i]);

                                LaserDamage(gameTime, enemyProjectile.EnemyProjectiles[i]);
                            }
                        }
                    }

                    if (Config.Level == LevelSelect.Six)
                    {
                        for (int i = Enemy.Enemies.Count - 1; i >= 0; i--)
                        {
                            if (Vector2.Distance(Enemy.Enemies[i].Position, Player.Ship.Position) < 250)
                            {
                                if (Enemy.Enemies[i] != null && Enemy.Enemies[i] is BugDagger3)
                                {
                                    targets.Add(Enemy.Enemies[i]);

                                    LaserDamage(gameTime, Enemy.Enemies[i]);
                                }
                            }
                        }
                    }
                }
            }
        }

        public void DrawLaser(Texture2D texture, float thickness, SpriteBatch spriteBatch)
        {
            if (Player.Ship != null)
            {
                //spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.Additive, null, null, null, null, cameraTransform);
                foreach (GameNode target in targets)
                {
                    if (target != null)
                    {
                        direction = target.Position - Player.Ship.Position;
                        rotation = (float)Math.Atan2(direction.Y, direction.X);

                        width = (int)Vector2.Distance(new Vector2(Player.Ship.Position.X, Player.Ship.Position.Y), new Vector2(target.Position.X, target.Position.Y));

                        spriteBatch.Draw(texture, new Rectangle((int)Player.Ship.Position.X, (int)Player.Ship.Position.Y, (int)width, (int)thickness),
                            null, Color.White, rotation, Vector2.Zero, SpriteEffects.None, 0);
                        //spriteBatch.Draw(texture, new Rectangle((int)Player.Ship.Position.X, (int)Player.Ship.Position.Y, (int)width, (int)thickness),
                        //    null, Color.White, rotation, Vector2.Zero, SpriteEffects.None, 0);

                        ////spritebatch.Draw(texture, new Rectangle((int)from.X, (int)from.Y, (int)width, (int)thickness + 2),
                        ////    null, Color.Green, rotation, Vector2.Zero, SpriteEffects.None, 0);
                        //spriteBatch.Draw(texture, new Rectangle((int)Player.Ship.Position.X, (int)Player.Ship.Position.Y, (int)width, (int)thickness + 4),
                        //    null, Color.White, rotation, Vector2.Zero, SpriteEffects.None, 0);
                        //spriteBatch.Draw(texture, new Rectangle((int)Player.Ship.Position.X, (int)Player.Ship.Position.Y, (int)width, (int)thickness - 4),
                        //    null, Color.White, rotation, Vector2.Zero, SpriteEffects.None, 0);
                    }
                }
                //spriteBatch.End();
            }
        }
    }
}
