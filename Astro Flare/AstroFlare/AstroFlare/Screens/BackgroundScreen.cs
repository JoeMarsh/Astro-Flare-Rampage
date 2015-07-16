#region File Description
//-----------------------------------------------------------------------------
// BackgroundScreen.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

#endregion

namespace AstroFlare
{
    /// <summary>
    /// The background screen sits behind all the other menu screens.
    /// It draws a background image that remains fixed in place regardless
    /// of whatever transitions the screens on top of it may be doing.
    /// </summary>
    class BackgroundScreen : GameScreen
    {
        #region Fields

        ContentManager content;
        //Texture2D backgroundTexture;
        Texture2D backgroundTexture1;
        Texture2D backgroundTexture2;
        Texture2D backgroundTexture3;
        Texture2D backgroundTexture4;
        Texture2D backgroundTexture5;
        Texture2D backgroundTexture6;
        //Texture2D backgroundTexture7;

        Demo demoLevel;

        public static bool InstructionBackground = false;

        #endregion

        #region Initialization


        /// <summary>
        /// Constructor.
        /// </summary>
        public BackgroundScreen()
        {
            TransitionOnTime = TimeSpan.FromSeconds(0.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);
        }


        /// <summary>
        /// Loads graphics content for this screen. The background texture is quite
        /// big, so we use our own local ContentManager to load it. This allows us
        /// to unload before going from the menus into the game itself, wheras if we
        /// used the shared ContentManager provided by the Game class, the content
        /// would remain loaded forever.
        /// </summary>
        public override void Activate(bool instancePreserved)
        {
            if (content == null)
                content = new ContentManager(ScreenManager.Game.Services, "Content");

            //ParticleEffects.Initialize(ScreenManager.GraphicsDeviceManager, ScreenManager.GraphicsDevice);
            //ParticleEngineEffect.LoadContent(content);
            //ParticleEffects.LoadContent(content);
            //Bar.Texture = content.Load<Texture2D>("Textures/Fill22");
            SpriteSheet.LoadContent(content, ScreenManager.GraphicsDevice);

            demoLevel = new Demo();
            //Player.SpawnEnemyShip();
            //backgroundTexture = ScreenManager.backgroundTexture;
            backgroundTexture1 = content.Load<Texture2D>("GameScreens\\MenuBackground1");
            backgroundTexture2 = content.Load<Texture2D>("GameScreens\\MenuBackground2");
            backgroundTexture3 = content.Load<Texture2D>("GameScreens\\MenuBackground3");
            backgroundTexture4 = content.Load<Texture2D>("GameScreens\\MenuBackground4");
            backgroundTexture5 = content.Load<Texture2D>("GameScreens\\MenuBackground5");
            backgroundTexture6 = content.Load<Texture2D>("GameScreens\\MenuBackground6");
            //backgroundTexture7 = content.Load<Texture2D>("GameScreens\\UIdescriptionBG");
        }


        /// <summary>
        /// Unloads graphics content for this screen.
        /// </summary>
        public override void Unload()
        {
            foreach (Node node in Node.Nodes)
            {
                node.Remove();
            }

            //GameStateManagementGame.Instance.soundManager.UnloadContent();

            demoLevel = null;

            Bar.Bars.Clear();
            PlayerShip.PlayerShips.Clear();
            Timer.Timers.Clear();
            SpriteSheet.SpriteSheets.Clear();
            Node.Nodes.Clear();
            Enemy.Enemies.Clear();
            Buddy.Buddys.Clear();
            Projectile.Projectiles.Clear();
            enemyProjectile.EnemyProjectiles.Clear();
            AIProjectile.AIProjectiles.Clear();
            queueBullet.queue.Clear();
            EnemyPlayerShip.EnemyPlayerShips.Clear();

            Player.Ship = null;
            Player.EnemyPlayer = null;

            //content.Unload();
            GC.Collect();
        }


        #endregion

        #region Update and Draw

        public static bool BackgroundTransition = false;
        /// <summary>
        /// Updates the background screen. Unlike most screens, this should not
        /// transition off even if it has been covered by another screen: it is
        /// supposed to be covered, after all! This overload forces the
        /// coveredByOtherScreen parameter to false in order to stop the base
        /// Update method wanting to transition off.
        /// </summary>
        public override void Update(GameTime gameTime, bool otherScreenHasFocus,
                                                       bool coveredByOtherScreen)
        {
            if (BackgroundTransition)
            {
                TransitionPosition = 1;
                TransitionOnTime = TimeSpan.FromSeconds(2);
                BackgroundTransition = false;
            }

            Node.UpdateNodes(gameTime);
            Node.RemoveDead();
            demoLevel.Update(gameTime);
            Timer.Update(gameTime);
            base.Update(gameTime, otherScreenHasFocus, false);
        }


        /// <summary>
        /// Draws the background screen.
        /// </summary>
        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
            Viewport viewport = ScreenManager.GraphicsDevice.Viewport;
            Rectangle fullscreen = new Rectangle(0, 0, viewport.Width, viewport.Height);

            spriteBatch.Begin();

            switch (Config.Level)
            {
                case LevelSelect.One:
                    spriteBatch.Draw(backgroundTexture1, fullscreen, new Color(TransitionAlpha, TransitionAlpha, TransitionAlpha));
                    ScreenManager.XLiveManager.Background = backgroundTexture1;
                    break;
                case LevelSelect.Two:
                    spriteBatch.Draw(backgroundTexture2, fullscreen, new Color(TransitionAlpha, TransitionAlpha, TransitionAlpha));
                    ScreenManager.XLiveManager.Background = backgroundTexture2;
                    break;
                case LevelSelect.Three:
                    spriteBatch.Draw(backgroundTexture3, fullscreen, new Color(TransitionAlpha, TransitionAlpha, TransitionAlpha));
                    ScreenManager.XLiveManager.Background = backgroundTexture3;
                    break;
                case LevelSelect.Four:
                    spriteBatch.Draw(backgroundTexture4, fullscreen, new Color(TransitionAlpha, TransitionAlpha, TransitionAlpha));
                    ScreenManager.XLiveManager.Background = backgroundTexture4;
                    break;
                case LevelSelect.Five:
                    spriteBatch.Draw(backgroundTexture5, fullscreen, new Color(TransitionAlpha, TransitionAlpha, TransitionAlpha));
                    ScreenManager.XLiveManager.Background = backgroundTexture5;
                    break;
                case LevelSelect.Six:
                    spriteBatch.Draw(backgroundTexture6, fullscreen, new Color(TransitionAlpha, TransitionAlpha, TransitionAlpha));
                    ScreenManager.XLiveManager.Background = backgroundTexture6;
                    break;
                default:
                    spriteBatch.Draw(backgroundTexture1, fullscreen, new Color(TransitionAlpha, TransitionAlpha, TransitionAlpha));
                    ScreenManager.XLiveManager.Background = backgroundTexture1;
                    break;
            }
            

            spriteBatch.End();
            //spriteBatch.Begin();
            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, null, null, null, null);
            int count = Node.Nodes.Count;
            for (int i = 0; i < count; i++)
            {
                //if(View.Intersects(Node.Nodes[i].Bounds))
                Node.Nodes[i].Draw(spriteBatch);
            }
            spriteBatch.End();
        }


        #endregion
    }
}
