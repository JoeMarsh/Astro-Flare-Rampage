//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Graphics;

//namespace AstroFlare
//{
//    class CollisionManager
//    {
//        private AsteroidManager asteroidManager;
//        private PlayerManager playerManager;
//        private EnemyManager enemyManager;
//        private ExplosionManager explosionManager;
//        private ParticleEffects effects;
//        private Vector2 offScreen = new Vector2(-500, -500);
//        private Vector2 shotToAsteroidImpact = new Vector2(0, -20);
//        private int enemyPointValue = 100;
//        private int asteroidPointValue = 20;
//        GameTime gameTime;

//        SpriteFontFloatScore floatingScore;




//        public CollisionManager(AsteroidManager asteroidManager, PlayerManager playerManager, EnemyManager enemyManager, 
//            ExplosionManager explosionManager, ParticleEffects effects, SpriteFont font, SpriteBatch spriteBatch, SpriteFontFloatScore floatingScore)
//        {
//            this.asteroidManager = asteroidManager;
//            this.playerManager = playerManager;
//            this.enemyManager = enemyManager;
//            this.explosionManager = explosionManager;
//            this.effects = effects;
//            this.floatingScore = floatingScore;
//        }

//        private void checkShotToEnemyCollisions()
//        {
//            foreach (Sprite shot in playerManager.PlayerShotManager.Shots)
//            {
//                foreach (Enemy enemy in enemyManager.Enemies)
//                {
//                    if (shot.IsCircleColliding(enemy.EnemySprite.Center, enemy.EnemySprite.CollisionRadius))
//                    {
//                        shot.Location = offScreen;
//                        playerManager.PlayerScore += enemyPointValue;
//                        effects.TriggerEnemyExplosionEffect(enemy.EnemySprite.Center, gameTime);

//                        floatingScore.Score = ("+" + enemyPointValue.ToString());
//                        floatingScore.StartPosition = enemy.EnemySprite.Center;
//                        floatingScore.Alive = true;
//                        floatingScore.LifeSpan = 1000;
//                        enemy.Destroyed = true;

//                        //explosionManager.AddExplosion(enemy.EnemySprite.Center, enemy.EnemySprite.Velocity / 10);
//                    }
//                }
//            }
//        }

//        private void checkShotToAsteroidCollisions()
//        {
//            foreach (Sprite shot in playerManager.PlayerShotManager.Shots)
//            {
//                foreach (Sprite asteroid in asteroidManager.Asteroids)
//                {
//                    if (shot.IsCircleColliding(asteroid.Center, asteroid.CollisionRadius))
//                    {
//                        shot.Location = offScreen;
//                        //asteroid.Velocity += shotToAsteroidImpact;
//                        effects.TriggerEnemyExplosionEffect(asteroid.Center, gameTime);
//                        floatingScore.Score = ("+" + asteroidPointValue.ToString());
//                        floatingScore.StartPosition = asteroid.Center;
//                        floatingScore.Alive = true;
//                        floatingScore.LifeSpan = 1000;
//                        asteroid.Location = offScreen;
//                        playerManager.PlayerScore += asteroidPointValue;
//                    }
//                }
//            }
//        }

//        private void checkShotToPlayerCollisions()
//        {
//            foreach (Sprite shot in enemyManager.EnemyShotManager.Shots)
//            {
//                if (shot.IsCircleColliding(playerManager.playerSprite.Center, playerManager.playerSprite.CollisionRadius))
//                {
//                    shot.Location = offScreen;
//                    //playerManager.Destroyed = true;
//                    playerManager.PlayerHealth -= 20;
//                    //explosionManager.AddExplosion(playerManager.playerSprite.Center, Vector2.Zero);
//                    effects.TriggerEnemyExplosionEffect(playerManager.playerSprite.Center, gameTime);
//                }
//            }
//        }

//        private void checkEnemyToPlayerCollisions()
//        {
//            foreach (Enemy enemy in enemyManager.Enemies)
//            {
//                if (enemy.EnemySprite.IsCircleColliding(playerManager.playerSprite.Center, playerManager.playerSprite.CollisionRadius))
//                {
//                    enemy.Destroyed = true;
//                    explosionManager.AddExplosion(enemy.EnemySprite.Center, enemy.EnemySprite.Velocity / 10);

//                    playerManager.Destroyed = true;

//                    explosionManager.AddExplosion(playerManager.playerSprite.Center, Vector2.Zero);
//                }
//            }
//        }

//        private void checkAsteroidToPlayerCollision()
//        {
//            foreach (Sprite asteroid in asteroidManager.Asteroids)
//            {
//                if (asteroid.IsCircleColliding(playerManager.playerSprite.Center, playerManager.playerSprite.CollisionRadius))
//                {
//                    //explosionManager.AddExplosion(asteroid.Center, asteroid.Velocity / 10);
//                    effects.TriggerEnemyExplosionEffect(playerManager.playerSprite.Center, gameTime);
//                    asteroid.Location = offScreen;

//                    playerManager.Destroyed = true;
//                    //explosionManager.AddExplosion(playerManager.playerSprite.Center, Vector2.Zero);

//                }
//            }
//        }

//        public void CheckCollisions()
//        {
//            checkShotToEnemyCollisions();
//            checkShotToAsteroidCollisions();
//            if (!playerManager.Destroyed)
//            {
//                checkShotToPlayerCollisions();
//                checkEnemyToPlayerCollisions();
//                checkAsteroidToPlayerCollision();
//            }
//        }

//        public void GetGameTime(GameTime gameTime)
//        {
//            this.gameTime = gameTime;
//        }
//    }
//}
