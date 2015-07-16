#region File Description
//-----------------------------------------------------------------------------
// GameplayScreen.cs
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
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Media;
using OpenXLive.Forms;
using Microsoft.Xna.Framework.Input.Touch;

#endregion

namespace AstroFlare
{
    class GameplayScreen : GameScreen
    {
        #region Fields

        ContentManager content;
        SpriteFont font;
        static SpriteFont floatingScoreFont;
        SpriteFont floatingPowerupFont;
        SpriteFont floatingMultiplierMsgFont;
        SpriteFont andy14;
        SpriteFont andy18;
        SpriteFont andy22;
        SpriteFont andy48;

        Texture2D phaserTexture;

        float pauseAlpha;
        InputAction pauseAction;

        enum GameStates { IntroScreen, Playing, PlayerDead, GameOver };
        GameStates gameState = GameStates.IntroScreen;

        Sprite multiHud;
        String multi;
        int multiplierTracker = 2;

        Sprite healthHud;
        Sprite shieldHud;

        Sprite AImultiHud;
        String AImulti;

        Sprite AIHealthHud;
        Sprite AIShieldHud;

        Bar KillStreakBar;
        Bar KillStreakBarBackground;

        public static Camera2D cam;
        Matrix cameraTransform;
        //Rectangle View;
        public static Rectangle worldBounds = new Rectangle(0, 0, 1600, 960);
        string healthText;
        string shieldText;
        string enemyHealthText;
        string enemyShieldText;
        string AlterEgoScoreText = "";
        Color AlterEgoScoreColor = Color.Green;
        string laserCharges;

        //Player Player;
        //BuddyMissile bud;

        int playerLives;
        public static int playerAbilityUses;

        Level1 level1;
        Level2 level2;
        Level3 level3;
        Level4 level4;
        Level5 level5;
        Level6 level6;
        Practise practise;

        public static double levelTime;
        public static Color levelTimeColor = Color.Green;
        TimeSpan levelTimeSpan;
        string levelTimeText = "";
        public static Timer levelTimeColorInterval;
        //double timeBonus;
        //double tempScore;

        TimeSpan respawnTime;

        private Texture2D thumbstick;
        //Texture2D UIdescription;

        //Gane backgrounds
        Texture2D stars;
        Texture2D backgroundClouds1;
        Texture2D backgroundClouds2;

        //Texture2D cloudLayer1;
        //Texture2D cloudLayer2;

        public static SpriteFontFloatScores FloatingScoreList;
        //public static SpriteFontFloatScore FloatingScore;
        public static SpriteFontFloatScore FloatingPowerupText;
        public static SpriteFontFloatScore FloatingMultiplierText;

        //ContentManager levelContent;

        //Particle engine
        //ParticleEffects ParticleEngineEffect;

        Song musicLevel1;

        //bool runningslowly = false;
        
        #endregion

        #region Initialization

        /// <summary>
        /// Constructor.
        /// </summary>
        public GameplayScreen()
        {
            TransitionOnTime = TimeSpan.FromSeconds(1.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);

            EnabledGestures = GestureType.Tap;

            //ScreenManager.ScreenManagerInput = false;
            Guide.IsScreenSaverEnabled = false;

            pauseAction = new InputAction(
                new Buttons[] { Buttons.Start, Buttons.Back },
                new Keys[] { Keys.Escape },
                true);
                     
        }


        /// <summary>
        /// Load graphics content for the game.
        /// </summary>
        public override void Activate(bool instancePreserved)
        {
            if (!instancePreserved)
            {
                if (content == null)
                    content = new ContentManager(ScreenManager.Game.Services, "Content");

                //Example message box for error display
                //List<string> MBOPTIONS = new List<string>();
                //MBOPTIONS.Add("OK");
                //string msg = "Text that was typed on the keyboard will be displayed here.\nClick OK to continue...";
                //Guide.BeginShowMessageBox("Pause", msg, MBOPTIONS, 0, MessageBoxIcon.Alert, null, null);

                // if memory leak, try using a new content manager and unloading.
                //ContentManager levelContent = new ContentManager(services, "Content");

                //extensions.initialise_texture(ScreenManager.SpriteBatch);
                Config.timeDropChance = 200;

                Config.shieldDropChance = 8 + (int)Math.Round(((float)8 / 100) * Config.PowerupDropChance);

                Config.projectileDropChance = 38 + (int)Math.Round(((float)38 / 100) * Config.PowerupDropChance);
                Config.missileDropChance = 68 + (int)Math.Round(((float)68 / 100) * Config.PowerupDropChance);
                Config.firerateDropChance = 128 + (int)Math.Round(((float)128 / 100) * Config.PowerupDropChance);
                Config.laserDropChance = 188 + (int)Math.Round(((float)158 / 100) * Config.PowerupDropChance);

                //Config.projectileDropChance = 68 + (int)Math.Round(((float)38 / 100) * Config.PowerupDropChance);
                //Config.missileDropChance = 98 + (int)Math.Round(((float)68 / 100) * Config.PowerupDropChance);
                //Config.firerateDropChance = 158 + (int)Math.Round(((float)128 / 100) * Config.PowerupDropChance);
                //Config.laserDropChance = 218 + (int)Math.Round(((float)158 / 100) * Config.PowerupDropChance);

                multiplierTracker = 2;
                Config.KillStreak = 0;
                Config.KillStreakBuildpoints = 0;
                Config.Score = 0;
                Config.Multi = 1;
                Config.AIScore = 0;
                Config.AIMulti = 1;
                Config.EmemiesKilled = 0;
                Config.CoinsCollected = 0;
                Config.EnemySpeed = 6;
                playerLives = Config.PlayerLives;

                playerAbilityUses = Config.PlayerSpecialAbilityUses;

                KillStreakBarBackground = new Bar(200, 20, new Color(0, 0, 0, 128));
                KillStreakBarBackground.Position = new Vector2(300, 455);
                KillStreakBarBackground.Percent = 1f;

                KillStreakBar = new Bar(200, 20, new Color(0,128,0,128));
                KillStreakBar.Position = new Vector2(300, 455);


                laserCharges = "0";

                thumbstick = content.Load<Texture2D>("thumbstick2");
                //introExtermination = content.Load<Texture2D>(@"Gamescreens\AlterEgo");
                phaserTexture = content.Load<Texture2D>(@"phaser");

                levelTimeColorInterval = new Timer();
                levelTimeColorInterval.Fire += new NotifyHandler(levelTimeColorInterval_Fire);

                switch (Config.Level)
                {
                    case LevelSelect.One:
                        stars = content.Load<Texture2D>(@"Background\Stars3");
                        backgroundClouds2 = content.Load<Texture2D>(@"Background\BackgroundClouds2");
                        musicLevel1 = content.Load<Song>(@"Music\throughthestars");
                        musicLevel1 = content.Load<Song>(@"Music\throughthestars");
                        levelTime = 300;
                        break;
                    case LevelSelect.Two:
                        stars = content.Load<Texture2D>(@"Background\Stars2");
                        //backgroundClouds2 = content.Load<Texture2D>(@"Background\cloudlayer2");
                        backgroundClouds2 = content.Load<Texture2D>(@"Background\BackgroundClouds2");
                        musicLevel1 = content.Load<Song>(@"Music\galacticstruggle");
                        levelTime = 300;
                        levelTimeColor = Color.Green;
                        break;
                    case LevelSelect.Three:
                        //stars = content.Load<Texture2D>(@"Background\stars");
                        //backgroundClouds2 = content.Load<Texture2D>(@"Background\nova2");
                        //backgroundClouds1 = content.Load<Texture2D>(@"Background\stars4");
                        stars = content.Load<Texture2D>(@"Background\Stars8");
                        backgroundClouds2 = content.Load<Texture2D>(@"Background\stars7");
                        backgroundClouds1 = content.Load<Texture2D>(@"Background\cloudlayer2");
                        musicLevel1 = content.Load<Song>(@"Music\survival");
                        levelTime = 300;
                        playerLives = 3;
                        respawnTime = TimeSpan.FromSeconds(5);
                        break;
                    case LevelSelect.Four:
                        stars = content.Load<Texture2D>(@"Background\stars");
                        backgroundClouds2 = content.Load<Texture2D>(@"Background\stars10");
                        backgroundClouds1 = content.Load<Texture2D>(@"Background\stars7");
                        musicLevel1 = content.Load<Song>(@"Music\holo");
                        levelTime = 300;
                        playerLives = 3;
                        respawnTime = TimeSpan.FromSeconds(5);
                        break;
                    case LevelSelect.Five:
                        //stars = content.Load<Texture2D>(@"Background\Stars5");
                        //backgroundClouds2 = content.Load<Texture2D>(@"Background\Stars4");
                        //backgroundClouds1 = content.Load<Texture2D>(@"Background\cloudlayer2");
                        stars = content.Load<Texture2D>(@"Background\Stars5");
                        backgroundClouds2 = content.Load<Texture2D>(@"Background\Stars4");
                        backgroundClouds1 = content.Load<Texture2D>(@"Background\stars7");
                        musicLevel1 = content.Load<Song>(@"Music\giantrobotsfighting");
                        levelTime = 60;
                        break;
                    case LevelSelect.Six:
                        stars = content.Load<Texture2D>(@"Background\stars");
                        backgroundClouds2 = content.Load<Texture2D>(@"Background\nova2");
                        backgroundClouds1 = content.Load<Texture2D>(@"Background\stars4");
                        musicLevel1 = content.Load<Song>(@"Music\AgainstTheOdds");
                        levelTime = 300;
                        break;
                    case LevelSelect.Practise:
                        //UIdescription = content.Load<Texture2D>(@"Gamescreens\UIdescription");
                        stars = content.Load<Texture2D>(@"Background\Stars3");
                        backgroundClouds2 = content.Load<Texture2D>(@"Background\BackgroundClouds2");
                        musicLevel1 = content.Load<Song>(@"Music\throughthestars");
                        levelTime = 300;
                        break;
                }

                font = content.Load<SpriteFont>(@"Fonts\Tahoma14");
                floatingScoreFont = content.Load<SpriteFont>(@"Fonts\Andy22");
                floatingPowerupFont = content.Load<SpriteFont>(@"Fonts\Andy22");
                floatingMultiplierMsgFont = content.Load<SpriteFont>(@"Fonts\Andy48");
                andy14 = content.Load<SpriteFont>(@"Fonts\Andy14");
                andy18 = content.Load<SpriteFont>(@"Fonts\Andy18");
                andy22 = content.Load<SpriteFont>(@"Fonts\Andy22");
                andy48 = content.Load<SpriteFont>(@"Fonts\Andy48");

                SpriteSheet.LoadContent(content, ScreenManager.GraphicsDevice);

                cam = new Camera2D(ScreenManager.GraphicsDevice, worldBounds.Right, worldBounds.Bottom);

                //GameStateManagementGame.Instance.soundManager.LoadSound("Shot3", @"SoundEffects\Shot3");
                //GameStateManagementGame.Instance.soundManager.LoadSound("coin", @"SoundEffects\coin");
                //GameStateManagementGame.Instance.soundManager.LoadSound("ShipSpawn", @"SoundEffects\ShipSpawn");
                //GameStateManagementGame.Instance.soundManager.LoadSound("ShipExplode", @"SoundEffects\ShipExplode");

                Player.SpawnShip();

                //Player = new Player();
                switch (Config.Level)
                {
                    case LevelSelect.One:
                        level1 = new Level1();
                        break;
                    case LevelSelect.Two:
                        level2 = new Level2();
                        break;
                    case LevelSelect.Three:
                        level3 = new Level3();
                        Player.SpawnEnemyShip();
                        break;
                    case LevelSelect.Four:
                        level4 = new Level4();
                        Player.SpawnEnemyShip();
                        break;
                    case LevelSelect.Five:
                        level5 = new Level5();
                        break;
                    case LevelSelect.Six:
                        level6 = new Level6();
                        break;
                    case LevelSelect.Practise:
                        practise = new Practise();
                        break;
                }
                //bud = new BuddyMissile(Config.BuddySpriteSheet);
                //bud.Position = Player.Ship.Position;

                //if (Player.Ship == null)
                //{
                //    Player.SpawnShip();
                //}
                //ParticleEngineEffect = new ParticleEffects(graphicsDeviceManager, cam, GraphicsDevice);
                ParticleEffects.Initialize(ScreenManager.GraphicsDeviceManager, ScreenManager.GraphicsDevice);
                //ParticleEngineEffect.LoadContent(content);
                ParticleEffects.LoadContent(content);

                FloatingScoreList = new SpriteFontFloatScores();

                //FloatingScore = new SpriteFontFloatScore();

                //FloatingScore.Color = Color.Green;
                //FloatingScore.Score = "+";
                //FloatingScore.SpriteFont = floatingScoreFont;
                //FloatingScore.LifeSpan = 1000f;
                //FloatingScore.SizeTime = 500f;
                //FloatingScore.ShadowEffect = false;
                //FloatingScore.EndPosition = new Vector2(20, -20);

                FloatingPowerupText = new SpriteFontFloatScore();

                FloatingPowerupText.Color = Color.White;
                FloatingPowerupText.Score = "+";
                FloatingPowerupText.SpriteFont = floatingPowerupFont;
                FloatingPowerupText.LifeSpan = 1000f;
                FloatingPowerupText.SizeTime = 500f;
                FloatingPowerupText.ShadowEffect = false;
                FloatingPowerupText.EndPosition = new Vector2(20, -20);
                FloatingPowerupText.LayerDepth = 0.7f;

                FloatingMultiplierText = new SpriteFontFloatScore();

                FloatingMultiplierText.Color = Color.White;
                FloatingMultiplierText.Score = "Multiplyer";
                FloatingMultiplierText.SpriteFont = floatingMultiplierMsgFont;
                FloatingMultiplierText.LifeSpan = 1000f;
                FloatingMultiplierText.SizeTime = 500f;
                FloatingMultiplierText.ShadowEffect = true;
                FloatingMultiplierText.EndPosition = Vector2.Zero;
                FloatingPowerupText.LayerDepth = 1.0f;

                //if (queueBullet.queue.Count < 1)
                //{
                //    for (int i = 0; i < 100; i++)
                //    {

                //        queueBullet.queue.Enqueue(new queueBullet(Config.BulletSheetEnergyPurple));
                //    }
                //}
                if (Config.MusicOn && MediaPlayer.GameHasControl)
                    GameStateManagementGame.Instance.musicManager.Play(musicLevel1);

                //GameStateManagementGame.Instance.musicManager.Stop();

                Bar.Texture = content.Load<Texture2D>("Textures/Fill22");

                multiHud = new Sprite(Config.MultiHud);
                multiHud.Scale = 0.5f;
                multiHud.Color = Color.Green;
                multi = (Math.Round(Config.Multi, 2)).ToString() + "x";

                healthHud = new Sprite(Config.MultiHud);
                healthHud.Scale = 0.3f;
                healthHud.Color = Color.Red;

                shieldHud = new Sprite(Config.MultiHud);
                shieldHud.Scale = 0.5f;
                shieldHud.Color = Color.RoyalBlue;

                AImultiHud = new Sprite(Config.MultiHud);
                AImultiHud.Scale = 0.5f;
                AImultiHud.Color = Color.Green;
                AImulti = (Math.Round(Config.AIMulti, 2)).ToString() + "x";

                AIHealthHud = new Sprite(Config.MultiHud);
                AIHealthHud.Scale = 0.3f;
                AIHealthHud.Color = Color.Red;

                AIShieldHud = new Sprite(Config.MultiHud);
                AIShieldHud.Scale = 0.5f;
                AIShieldHud.Color = Color.RoyalBlue;



                GC.Collect();


                // once the load has finished, we use ResetElapsedTime to tell the game's
                // timing mechanism that we have just finished a very long frame, and that
                // it should not try to catch up.
                ScreenManager.Game.ResetElapsedTime();
            }

//#if WINDOWS_PHONE
//            if (Microsoft.Phone.Shell.PhoneApplicationService.Current.State.ContainsKey("PlayerPosition"))
//            {
//                playerPosition = (Vector2)Microsoft.Phone.Shell.PhoneApplicationService.Current.State["PlayerPosition"];
//                enemyPosition = (Vector2)Microsoft.Phone.Shell.PhoneApplicationService.Current.State["EnemyPosition"];
//            }
//#endif
        }

        void levelTimeColorInterval_Fire()
        {
            levelTimeColor = Color.Green;
            levelTimeColorInterval.Stop();
        }

        public override void Deactivate()
        {
//#if WINDOWS_PHONE
//            Microsoft.Phone.Shell.PhoneApplicationService.Current.State["PlayerPosition"] = playerPosition;
//            Microsoft.Phone.Shell.PhoneApplicationService.Current.State["EnemyPosition"] = enemyPosition;
//#endif

            base.Deactivate();
        }


        /// <summary>
        /// Unload graphics content used by the game.
        /// </summary>
        public override void Unload()
        {
            //for (int i = 0; i < 100; i++)
            //{
            //    queueBullet.queue.Clear();
            //}
                
            if (Guide.IsTrialMode)
            {
                if (Config.Coins > 5000)
                    Config.Coins = 5000;
            }
            else
            {
                if (Config.Coins > 10000)
                    Config.Coins = 10000;
            }

            foreach (Node node in Node.Nodes)
            {
                node.Remove();
            }

            //GameStateManagementGame.Instance.soundManager.UnloadContent();

            level1 = null;
            level2 = null;
            level3 = null;
            level4 = null;
            level5 = null;
            level6 = null;
            practise = null;

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
            levelTimeColorInterval.Stop();

            Player.Ship = null;
            Player.EnemyPlayer = null;
            //PlayerShip.PlayerShips = null;
            //EnemyPlayerShip.EnemyPlayerShips = null;

            //Node.RemoveDead();

            //levelContent.Unload();

            //content.Unload();
//#if WINDOWS_PHONE
//            Microsoft.Phone.Shell.PhoneApplicationService.Current.State.Remove("PlayerPosition");
//            Microsoft.Phone.Shell.PhoneApplicationService.Current.State.Remove("EnemyPosition");
//#endif
            Config.EnemySpeed = 6;
            //ScreenManager.ScreenManagerInput = true;
            Guide.IsScreenSaverEnabled = true;
            GC.Collect();
        }


        #endregion

        #region Update and Draw


        /// <summary>
        /// Updates the state of the game. This method checks the GameScreen.IsActive
        /// property, so the game will stop updating when the pause menu is active,
        /// or if you tab away to a different application.
        /// </summary>
        public override void Update(GameTime gameTime, bool otherScreenHasFocus,
                                                       bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
            
            // Gradually fade in or out depending on whether we are covered by the pause screen.
            if (coveredByOtherScreen)
                pauseAlpha = Math.Min(pauseAlpha + 1f / 32, 1);
            else
                pauseAlpha = Math.Max(pauseAlpha - 1f / 32, 0);

            if (IsActive)
            {
                switch (gameState)
                {
                    case GameStates.IntroScreen:
                        //if (Config.Level == LevelSelect.Practise)
                        //{
                        //    if (VirtualThumbsticks.RightThumbstickCenter.HasValue || VirtualThumbsticks.LeftThumbstickCenter.HasValue)
                        //        gameState = GameStates.Playing;
                        //}
                        //else
                            gameState = GameStates.Playing;
                        break;

                    case GameStates.Playing:
                        //FloatingScore.Score = "+" + ((int)(25 * Config.Multi)).ToString();
                        Node.UpdateNodes(gameTime);
                        Node.RemoveDead();

                        if (Config.Level == LevelSelect.Six)
                        {
                            if (Config.KillStreakBuildpoints >= 400)
                            {
                                playerAbilityUses += 1;
                                Config.KillStreakBuildpoints = Config.KillStreakBuildpoints - 400;
                            }
                            KillStreakBar.Percent = Config.KillStreakBuildpoints / 400;
                        }
                        else
                        {
                            if (Config.KillStreakBuildpoints >= 200)
                            {
                                playerAbilityUses += 1;
                                Config.KillStreakBuildpoints = Config.KillStreakBuildpoints - 200;
                            }
                            KillStreakBar.Percent = Config.KillStreakBuildpoints / 200;
                        }

                        switch (Config.Level)
                        {
                            case LevelSelect.One:
                                break;
                            case LevelSelect.Two:
                                levelTime -= gameTime.ElapsedGameTime.TotalSeconds;
                                levelTimeSpan = TimeSpan.FromSeconds(levelTime);
                                levelTimeText = string.Format("{0:D2}m:{1:D2}s", levelTimeSpan.Minutes, levelTimeSpan.Seconds);
                                if (levelTime <= 0d)
                                    gameState = GameStates.GameOver;
                                break;
                            case LevelSelect.Three:
                                break;
                            case LevelSelect.Four:                                                       
                                levelTime -= gameTime.ElapsedGameTime.TotalSeconds;
                                levelTimeSpan = TimeSpan.FromSeconds(levelTime);
                                levelTimeText = string.Format("{0:D2}m:{1:D2}s",levelTimeSpan.Minutes, levelTimeSpan.Seconds);
                                if (levelTime <= 0d)
                                    gameState = GameStates.GameOver;
                                break;
                            case LevelSelect.Five:
                                levelTime -= gameTime.ElapsedGameTime.TotalSeconds;
                                levelTimeSpan = TimeSpan.FromSeconds(levelTime);
                                levelTimeText = string.Format("{0:D2}m:{1:D2}s", levelTimeSpan.Minutes, levelTimeSpan.Seconds);
                                if (levelTime <= 0d)
                                    gameState = GameStates.GameOver;
                                break;
                            case LevelSelect.Six:
                                break;
                            case LevelSelect.Practise:
                                break;
                        }

                        switch (Config.Level)
                        {
                            case LevelSelect.One:
                                level1.Update(gameTime);
                                break;
                            case LevelSelect.Two:
                                level2.Update(gameTime);
                                break;
                            case LevelSelect.Three:
                                level3.Update(gameTime);
                                if ((int)Config.Score - (int)Config.AIScore >= 0)
                                {
                                    AlterEgoScoreColor = Color.Green;
                                    AlterEgoScoreText = "+" + ((int)Config.Score - (int)Config.AIScore).ToString();
                                }
                                else
                                {
                                    AlterEgoScoreColor = Color.Red;
                                    AlterEgoScoreText = ((int)Config.Score - (int)Config.AIScore).ToString();
                                }
                                break;
                            case LevelSelect.Four:
                                level4.Update(gameTime);
                                if ((int)Config.Score - (int)Config.AIScore >= 0)
                                {
                                    AlterEgoScoreColor = Color.Green;
                                    AlterEgoScoreText = "+" + ((int)Config.Score - (int)Config.AIScore).ToString();
                                }
                                else
                                {
                                    AlterEgoScoreColor = Color.Red;
                                    AlterEgoScoreText = ((int)Config.Score - (int)Config.AIScore).ToString();
                                }
                                break;
                            case LevelSelect.Five:
                                level5.Update(gameTime);
                                break;
                            case LevelSelect.Six:
                                level6.Update(gameTime);
                                break;
                            case LevelSelect.Practise:
                                practise.Update(gameTime);
                                break;
                        }

                        if (Player.Ship == null)
                            gameState = GameStates.PlayerDead;
                        break;

                    case GameStates.PlayerDead:
                        if (playerLives > 1)
                        {
                            if (Player.Ship == null)
                            {
                                //if (Player.EnemyPlayer != null)
                                //    cam.Pos = Player.EnemyPlayer.Position;

                                switch (Config.Level)
                                {
                                    case LevelSelect.Three:
                                        Node.UpdateNodes(gameTime);
                                        Node.RemoveDead();
                                        level3.Update(gameTime);
                                        if ((int)Config.Score - (int)Config.AIScore >= 0)
                                        {
                                            AlterEgoScoreColor = Color.Green;
                                            AlterEgoScoreText = "+" + ((int)Config.Score - (int)Config.AIScore).ToString();
                                        }
                                        else
                                        {
                                            AlterEgoScoreColor = Color.Red;
                                            AlterEgoScoreText = ((int)Config.Score - (int)Config.AIScore).ToString();
                                        }
                                        break;
                                    case LevelSelect.Four:
                                        Node.UpdateNodes(gameTime);
                                        Node.RemoveDead();
                                        level4.Update(gameTime);
                                        if ((int)Config.Score - (int)Config.AIScore >= 0)
                                        {
                                            AlterEgoScoreColor = Color.Green;
                                            AlterEgoScoreText = "+" + ((int)Config.Score - (int)Config.AIScore).ToString();
                                        }
                                        else
                                        {
                                            AlterEgoScoreColor = Color.Red;
                                            AlterEgoScoreText = ((int)Config.Score - (int)Config.AIScore).ToString();
                                        }
                                        break;
                                }

                                respawnTime -= TimeSpan.FromSeconds(gameTime.ElapsedGameTime.TotalSeconds);
                                if (respawnTime <= TimeSpan.FromSeconds(0))
                                {
                                    Player.SpawnShip();
                                    gameState = GameStates.Playing;
                                    playerLives--;
                                    respawnTime = TimeSpan.FromSeconds(5);
                                }
                                //if (VirtualThumbsticks.RightThumbstickCenter.HasValue)
                                //{
                                //    Player.SpawnShip();
                                //    gameState = GameStates.Playing;
                                //    playerLives--;
                                //    //gameState = GameStates.Playing;
                                //}
                            }
                        }
                        else
                            gameState = GameStates.GameOver;
                        break;

                    case GameStates.GameOver:
                        switch (Config.Level)
                        {
                            case LevelSelect.One:
                                Config.RampageScores.Add((float)(Math.Round(Config.Score - Config.AIScore)));
                                ScreenManager.SubmitScore((float)(Math.Round(Config.Score - Config.AIScore)), 1);
                                break;
                            case LevelSelect.Two:
                                //timeBonus = 1 + ((300 - levelTime) / 300);
                                //tempScore = (Config.Score - Config.AIScore);
                                //tempScore = tempScore * timeBonus;

                                Config.RampageTimedScores.Add((float)(Math.Round(Config.Score - Config.AIScore)));
                                ScreenManager.SubmitScore((float)(Math.Round(Config.Score - Config.AIScore)), 2);
                                break;
                            case LevelSelect.Three:
                                Config.AlterEgoScores.Add((float)(Math.Round(Config.Score - Config.AIScore)));
                                ScreenManager.SubmitScore((float)(Math.Round(Config.Score - Config.AIScore)), 3);
                                break;
                            case LevelSelect.Four:
                                //timeBonus = 1 + ((300 - levelTime) / 300);
                                //tempScore = (Config.Score - Config.AIScore);
                                //tempScore = tempScore * timeBonus;

                                Config.AlterEgoTimedScores.Add((float)(Math.Round(Config.Score - Config.AIScore)));
                                ScreenManager.SubmitScore((float)(Math.Round(Config.Score - Config.AIScore)), 4);
                                break;
                            case LevelSelect.Five:
                                Config.TimeBanditScores.Add((float)(Math.Round(Config.Score - Config.AIScore)));
                                ScreenManager.SubmitScore((float)(Math.Round(Config.Score - Config.AIScore)), 5);
                                break;
                            case LevelSelect.Six:
                                Config.ExterminationScores.Add((float)(Math.Round(Config.Score - Config.AIScore)));
                                ScreenManager.SubmitScore((float)(Math.Round(Config.Score - Config.AIScore)), 6);
                                break;
                            case LevelSelect.Practise:
                                break;
                        }

                        ExitScreen();
                        LoadingScreen.Load(ScreenManager, true, ControllingPlayer, new BackgroundScreen(), new PhoneScoreScreen());

                        //LoadingScreen.Load(ScreenManager, true, ControllingPlayer, new BackgroundScreen(), new MainMenuScreen());

                        //XLiveScoreForm form = new XLiveScoreForm(XLiveFormFactory.FormManager);
                        //form.FormResultEvent += new EventHandler<ScoreFormResultEventArgs>(form_FormResultEvent);

                        //switch (Config.Level)
                        //{
                        //    case LevelSelect.One:
                        //        form.Show("b6d0140f-fb1d-4cbc-bdb3-6540d5930093", (float)(Math.Round(Config.Score - Config.AIScore)));
                        //        break;
                        //    case LevelSelect.Two:
                        //        form.Show("847c1918-bef5-4394-8109-813678e6f320", (float)(Math.Round(Config.Score - Config.AIScore)));
                        //        break;
                        //    case LevelSelect.Three:
                        //        form.Show("75cc6984-c761-4fa3-9dba-98b9723dc81a", (float)(Math.Round(Config.Score - Config.AIScore)));
                        //        break;
                        //    case LevelSelect.Four:
                        //        form.Show("0ad81324-2712-4256-966e-8c6dfd1217ff", (float)(Math.Round(Config.Score - Config.AIScore)));
                        //        break;
                        //    case LevelSelect.Five:
                        //        form.Show("d4a9829a-277c-433d-8de4-2d846235cca6", (float)(Math.Round(Config.Score - Config.AIScore)));
                        //        break;
                        //    case LevelSelect.Six:
                        //        form.Show("27b9af22-239c-4613-9c37-069d23826981", (float)(Math.Round(Config.Score - Config.AIScore)));
                        //        break;
                        //    case LevelSelect.Practise:
                        //        break;
                        //}



                        break;
                }

                cameraTransform = cam.get_transformation(ScreenManager.GraphicsDevice, 1f);

                //if (Player.EnemyPlayer != null)
                //    cam.Pos = Player.EnemyPlayer.Position;
                if (Player.Ship != null)
                    cam.Pos = Player.Ship.Position;
                //else if (Player.EnemyPlayer != null)
                //    cam.Pos = Player.EnemyPlayer.Position;
                ////testing remove ***********************************************
                //else if (bud != null)
                //    cam.Pos = bud.Position;
                ////*************************************************************

                //View = new Rectangle((int)cam._pos.X - 400, (int)cam._pos.Y - 240, 800, 480);

                ParticleEffects.Update(gameTime, cam, ref cameraTransform);
                Timer.Update(gameTime);
                //Player.ProcessInput();
                //VirtualThumbsticks.Update();

                //bud.Update(gameTime);

                //MemoryMonitor.Update();

                FloatingScoreList.UpdateAllAnimatedPositions(gameTime);
                //FloatingScore.UpdateAnimatedPosition(gameTime);
                FloatingPowerupText.UpdateAnimatedPosition(gameTime);
                FloatingMultiplierText.UpdateAnimatedPosition(gameTime);


                multiHud.Frame = (int)((Config.Multi % 1) * 10);
                //multi = (Math.Round(Config.Multi, 2)).ToString() + "x";
                multi = ((int)Config.Multi).ToString() + "x";

                if ((int)Config.Multi == multiplierTracker)
                {
                    multiplierTracker++;
                    FloatingMultiplierText.Score = ("Multiplier: " + ((int)Config.Multi).ToString() + "x");
                    FloatingMultiplierText.StartPosition = new Vector2(400, 240);
                    FloatingMultiplierText.Alive = true;
                    FloatingMultiplierText.LifeSpan = 1500;
                }

                AImultiHud.Frame = (int)((Config.AIMulti % 1) * 10);
                //multi = (Math.Round(Config.Multi, 2)).ToString() + "x";
                AImulti = ((int)Config.AIMulti).ToString() + "x";

                if (Player.Ship != null)
                {
                    laserCharges = Player.Ship.laserDefence.ToString();
                }


                if (Player.Ship != null)
                {
                    healthHud.Frame = (int)Math.Round(((float)Player.Ship.Health / Config.ShipHealth) * 10) % 10;
                    shieldHud.Frame = (int)Math.Round(((float)Player.Ship.Shield.Health / Config.ShieldHealth) * 10) % 10;
                    if (Player.Ship.Health > 0)
                        healthHud.Color = Color.Red;
                    else
                        healthHud.Color = Color.Transparent;
                    if (Player.Ship.Shield.Health > 0)
                        shieldHud.Color = Color.RoyalBlue;
                    else
                        shieldHud.Color = Color.Transparent;
                }

                if (Player.EnemyPlayer != null)
                {
                    AIHealthHud.Frame = (int)Math.Round(((float)Player.EnemyPlayer.Health / Config.ShipHealth) * 10) % 10;
                    AIShieldHud.Frame = (int)Math.Round(((float)Player.EnemyPlayer.Shield.Health / Config.ShieldHealth) * 10) % 10;
                    if (Player.EnemyPlayer.Health > 0)
                        AIHealthHud.Color = Color.Red;
                    else
                        AIHealthHud.Color = Color.Transparent;
                    if (Player.EnemyPlayer.Shield.Health > 0)
                        AIShieldHud.Color = Color.RoyalBlue;
                    else
                        AIShieldHud.Color = Color.Transparent;
                }
                else
                {
                    AIShieldHud.Color = Color.Transparent;
                    AIHealthHud.Color = Color.Transparent;
                }

                if (Player.Ship != null)
                {
                    healthText = String.Format("{0}", Player.Ship.Health);
                    shieldText = String.Format("{0}", Player.Ship.Shield.Health);
                }
                else
                {
                    healthText = String.Format("<>");
                    shieldText = String.Format("<>");
                }

                if (Player.EnemyPlayer != null)
                {
                    enemyHealthText = String.Format("{0}", Player.EnemyPlayer.Health);
                    enemyShieldText = String.Format("{0}", Player.EnemyPlayer.Shield.Health);
                }
                else
                {
                    enemyHealthText = String.Format("<>");
                    enemyShieldText = String.Format("<>");
                }

                //if (gameTime.IsRunningSlowly == true)
                //    runningslowly = true;
                //else
                //    runningslowly = false;
            }
        }

        void form_FormResultEvent(object sender, ScoreFormResultEventArgs e)
        {
            switch (e.Result)
            {
                case ScoreFormResult.Retry:
                    LoadingScreen.Load(ScreenManager, true, null, new GameplayScreen());
                    break;
            }
        }



        /// <summary>
        /// Lets the game respond to player input. Unlike the Update method,
        /// this will only be called when the gameplay screen is active.
        /// </summary>
        public override void HandleInput(GameTime gameTime, InputState input)
        {
            //ScreenManager.ScreenManagerInput = false;


//            if (input == null)
//                throw new ArgumentNullException("input");

//            // Look up inputs for the active player profile.
//            int playerIndex = (int)ControllingPlayer.Value;

//            KeyboardState keyboardState = input.CurrentKeyboardStates[playerIndex];
//            GamePadState gamePadState = input.CurrentGamePadStates[playerIndex];

//            // The game pauses either if the user presses the pause button, or if
//            // they unplug the active gamepad. This requires us to keep track of
//            // whether a gamepad was ever plugged in, because we don't want to pause
//            // on PC if they are playing with a keyboard and have no gamepad at all!
//            bool gamePadDisconnected = !gamePadState.IsConnected &&
//                                       input.GamePadWasConnected[playerIndex];

//            PlayerIndex player;
//            if (pauseAction.Evaluate(input, ControllingPlayer, out player) || gamePadDisconnected)
//            {
//#if WINDOWS_PHONE
//                ScreenManager.AddScreen(new PhonePauseScreen(), ControllingPlayer);
//#else
//                ScreenManager.AddScreen(new PauseMenuScreen(), ControllingPlayer);
//#endif
//            }
//            else
//            {

//                if (playerAbilityUses > 0)
//                {
//                    foreach (var g in input.Gestures)
//                    {
//                        if (g.GestureType == GestureType.DoubleTap)
//                        {
//                            if (Config.ship1Active)
//                            {
//                                if (Player.Ship != null)
//                                {
//                                    Player.Ship.Health = (int)MathHelper.Min(Player.Ship.Health + 25, Player.Ship.MaxHealth);
//                                    playerAbilityUses--;
//                                }
//                            }
//                            else if (Config.ship2Active)
//                            {
//                                if (Player.Ship != null)
//                                {
//                                    PowerupDamageAll AoEDamage = new PowerupDamageAll(Config.PowerupLightningSpriteSheet);
//                                    AoEDamage.Position = Player.Ship.Position;
//                                    playerAbilityUses--;
//                                }
//                            }
//                            else if (Config.ship3Active)
//                            {
//                                if (Player.Ship != null)
//                                {
//                                    Player.Ship.InvulnerableTimer.Start(10);
//                                    Player.Ship.isInvulnerable = true;
//                                    playerAbilityUses--;
//                                }
//                            }
//                        }
//                    }
//                }

                VirtualThumbsticks.Update(input);
                Player.ProcessInput();

                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                {
                    ScreenManager.AddScreen(new PhonePauseScreen(), null);
                    //ScreenManager.ScreenManagerInput = true;
                }

                //if (playerAbilityUses > 0)
                //{
                //    EnabledGestures = GestureType.DoubleTap | GestureType.Tap;
                    
                //    if (TouchPanel.IsGestureAvailable)
                //    {
                //        GestureSample gesture = TouchPanel.ReadGesture();

                //        //if (gesture.GestureType == GestureType.Tap)
                //        //{
                //        //    if (gesture.Position.X > 300 && gesture.Position.X < 500 && gesture.Position.Y > 455 && gesture.Position.Y < 475)
                //        //    {
                //        //        if (Config.ship1Active)
                //        //        {
                //        //            if (Player.Ship != null)
                //        //            {
                //        //                //Player.Ship.Health = (int)MathHelper.Min(Player.Ship.Health + 25, Player.Ship.MaxHealth);
                //        //                Player.Ship.Health = (int)MathHelper.Min(Player.Ship.Health + 10, Player.Ship.MaxHealth);
                //        //                Player.Ship.Shield.ShieldRegen(20);
                //        //                playerAbilityUses--;
                //        //            }
                //        //        }
                //        //        else if (Config.ship2Active)
                //        //        {
                //        //            if (Player.Ship != null)
                //        //            {
                //        //                PowerupDamageAll AoEDamage = new PowerupDamageAll(Config.PowerupSlowAllSpriteSheet);
                //        //                AoEDamage.Position = Player.Ship.Position;
                //        //                playerAbilityUses--;
                //        //            }
                //        //        }
                //        //        else if (Config.ship3Active)
                //        //        {
                //        //            if (Player.Ship != null)
                //        //            {
                //        //                Player.Ship.InvulnerableTimer.Start(10);
                //        //                Player.Ship.isInvulnerable = true;
                //        //                Player.Ship.megaMagnetActive = true;
                //        //                playerAbilityUses--;
                //        //                Player.Ship.InvulnAbilityBar = new Bar(100, 20, new Color(255, 255, 255, 5));
                //        //                Player.Ship.InvulnAbilityBar.Position = new Vector2(5, 290);
                //        //            }
                //        //        }
                //        //    }
                //        //}

                //        if (gesture.GestureType == GestureType.DoubleTap)
                //        {
                //            if (Config.ship1Active)
                //            {
                //                if (Player.Ship != null)
                //                {
                //                    //Player.Ship.Health = (int)MathHelper.Min(Player.Ship.Health + 25, Player.Ship.MaxHealth);
                //                    Player.Ship.Health = (int)MathHelper.Min(Player.Ship.Health + 10, Player.Ship.MaxHealth);
                //                    Player.Ship.Shield.ShieldRegen(20);
                //                    playerAbilityUses--;
                //                }
                //            }
                //            else if (Config.ship2Active)
                //            {
                //                if (Player.Ship != null)
                //                {
                //                    PowerupDamageAll AoEDamage = new PowerupDamageAll(Config.PowerupSlowAllSpriteSheet);
                //                    AoEDamage.Position = Player.Ship.Position;
                //                    playerAbilityUses--;
                //                }
                //            }
                //            else if (Config.ship3Active)
                //            {
                //                if (Player.Ship != null)
                //                {
                //                    Player.Ship.InvulnerableTimer.Start(10);
                //                    Player.Ship.isInvulnerable = true;
                //                    Player.Ship.megaMagnetActive = true;
                //                    playerAbilityUses--;
                //                    Player.Ship.InvulnAbilityBar = new Bar(100, 20, new Color(255, 255, 255, 5));
                //                    Player.Ship.InvulnAbilityBar.Position = new Vector2(5, 290);
                //                }
                //            }
                //        }


                //    }

                //}
                //else
                //{
                //    EnabledGestures = GestureType.Tap;
                //}
            //}
        }

        /// <summary>
        /// Draws the gameplay screen.
        /// </summary>
        public override void Draw(GameTime gameTime)
        {
            ScreenManager.GraphicsDevice.Clear(ClearOptions.Target,
                                               Color.Black, 0, 0);

            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;


            if (Config.Level == LevelSelect.Six || Config.Level == LevelSelect.Five || Config.Level == LevelSelect.Four || Config.Level == LevelSelect.Three)
            {
                //Game background
                spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, null, null, null, null, cam.get_transformation(ScreenManager.GraphicsDevice, 0.1f));
                spriteBatch.Draw(stars, new Rectangle(-360, -216, 880, 528), null, new Color(255, 255, 255, 0), 0f, Vector2.Zero, SpriteEffects.None, 0.1f);
                spriteBatch.End();

                //spriteBatch.Draw(backgroundClouds1, new Rectangle(0, 0, graphics.GraphicsDevice.Viewport.Width, graphics.GraphicsDevice.Viewport.Height), new Color(255, 255, 255, 0));
                spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, null, null, null, null, cam.get_transformation(ScreenManager.GraphicsDevice, 0.15f));
                spriteBatch.Draw(backgroundClouds2, new Rectangle(120, 72, 920, 552), null, new Color(255, 255, 255, 0), 0f, new Vector2(backgroundClouds2.Width / 2, backgroundClouds2.Height / 2), SpriteEffects.None, 0.2f);
                spriteBatch.End();

                spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, null, null, null, null, cam.get_transformation(ScreenManager.GraphicsDevice, 0.3f));
                spriteBatch.Draw(backgroundClouds1, new Rectangle(240, 144, 1040, 624), null, new Color(255, 255, 255, 0), 0f, new Vector2(backgroundClouds1.Width / 2, backgroundClouds1.Height / 2), SpriteEffects.None, 0.21f);
                spriteBatch.End();
            }
            else
            {
                //Game background
                spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, null, null, null, null, cam.get_transformation(ScreenManager.GraphicsDevice, 0.1f));
                spriteBatch.Draw(stars, new Rectangle(-360, -216, 880, 528), null, new Color(255, 255, 255, 0), 0f, Vector2.Zero, SpriteEffects.None, 0.1f);
                spriteBatch.End();

                //spriteBatch.Draw(backgroundClouds1, new Rectangle(0, 0, graphics.GraphicsDevice.Viewport.Width, graphics.GraphicsDevice.Viewport.Height), new Color(255, 255, 255, 0));
                spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, null, null, null, null, cam.get_transformation(ScreenManager.GraphicsDevice, 0.2f));
                spriteBatch.Draw(backgroundClouds2, new Rectangle(160, 96, 960, 576), null, new Color(255, 255, 255, 0), 0f, new Vector2(backgroundClouds2.Width / 2, backgroundClouds2.Height / 2), SpriteEffects.None, 0.2f);
                spriteBatch.End();
            }



            //spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, null, null, null, null, cam.get_transformation(GraphicsDevice, 0.2f));
            //spriteBatch.Draw(cloudLayer1, new Rectangle(-560, -336, 1120, 672), null, new Color(255, 255, 255, 0), 0f, Vector2.Zero, SpriteEffects.None, 0.2f);
            //spriteBatch.End();

            //spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, null, null, null, null, cam.get_transformation(GraphicsDevice, 0.3f));
            //spriteBatch.Draw(cloudLayer2, new Rectangle(-640, -384, 1280, 768), null, new Color(255, 255, 255, 0), 0f, Vector2.Zero, SpriteEffects.None, 0.3f);
            //spriteBatch.End();

            //drawlaser here

            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, null, null, null, null, cameraTransform);

            FloatingScoreList.DrawAll(spriteBatch);
            //FloatingScore.Draw(spriteBatch, Vector2.Zero);
            FloatingPowerupText.Draw(spriteBatch, Vector2.Zero);

            int count = Node.Nodes.Count;
            for (int i = 0; i < count; i++)
            {
                //if (View.Intersects(Node.Nodes[i].Bounds))
                    Node.Nodes[i].Draw(spriteBatch);
            }
            //foreach (Node node in Node.Nodes)
            //{
            //    node.Draw(spriteBatch);
            //}
            ParticleEffects.Draw();

            spriteBatch.End();



            switch (gameState)
            {
                case GameStates.IntroScreen:
                    //if (Config.Level == LevelSelect.Practise)
                    //{
                    //    spriteBatch.Begin();
                    //    spriteBatch.Draw(UIdescription, Vector2.Zero, null, Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 1.0f);
                    //    spriteBatch.End();
                    //}
                    break;
                case GameStates.Playing:
                    spriteBatch.Begin();

                    if (Player.Ship != null)
                    {
                        //spriteBatch.DrawString(andy14, Player.Ship.laserDefence.ToString(), Player.Ship.Position, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
                        spriteBatch.DrawString(andy18, "Laser:", new Vector2(40, 160), Color.Green, 0f, new Vector2(font.MeasureString("Laser:").X / 2, font.MeasureString("Laser:").Y / 2), 0.7f, SpriteEffects.None, 1.0f);
                        spriteBatch.DrawString(andy18, laserCharges, new Vector2(40, 180), Color.Green, 0f, new Vector2(font.MeasureString(laserCharges).X / 2, font.MeasureString(laserCharges).Y / 2), 0.7f, SpriteEffects.None, 1.0f);
                    }
                    //spriteBatch.DrawString(font, String.Format("Nodes {0}", Node.Nodes.Count), new Vector2(10f, 140f), Color.Orange, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1.0f);

                    Bar.DrawBars(spriteBatch);
                    spriteBatch.DrawString(andy18, shieldText, new Vector2(40, 135), Color.RoyalBlue, 0f, new Vector2(font.MeasureString(shieldText).X / 2, font.MeasureString(shieldText).Y / 2), 0.7f, SpriteEffects.None, 1.0f);
                    spriteBatch.DrawString(andy18, healthText, new Vector2(40, 100), Color.Red, 0f, new Vector2(font.MeasureString(healthText).X / 2, font.MeasureString(healthText).Y / 2), 0.7f, SpriteEffects.None, 1.0f);
                    spriteBatch.DrawString(andy18, "Score: " + (Math.Round(Config.Score)).ToString(), new Vector2(80, 20), Color.Green);
                    //spriteBatch.DrawString(andy14, "Ability: " + playerAbilityUses.ToString(), new Vector2(80, 50), Color.Green);  
                    //spriteBatch.DrawString(andy14, "Ability Uses: " + playerAbilityUses.ToString(), new Vector2(300, 430), Color.Green);  
                    spriteBatch.DrawString(andy14, "Ability Uses: " + playerAbilityUses.ToString(), new Vector2(350, 458), Color.White);  
                    //spriteBatch.DrawString(font, "Lives: " + playerLives.ToString(), new Vector2(700, 10), Color.Green);
                    spriteBatch.DrawString(andy18, multi, new Vector2(40, 40), Color.Green, 0f, new Vector2(font.MeasureString(multi).X / 2, font.MeasureString(multi).Y / 2), 0.75f, SpriteEffects.None, 1.0f);              
                    multiHud.Draw(spriteBatch, new Vector2(40, 40), 0f);
                    shieldHud.Draw(spriteBatch, new Vector2(40, 100), 0f);
                    healthHud.Draw(spriteBatch, new Vector2(40, 100), 0f);      
                    FloatingMultiplierText.Draw(spriteBatch, Vector2.Zero);

                    if (Config.KillStreak > 2)
                    {
                        spriteBatch.DrawString(andy22, "Kill Streak: " + Config.KillStreak.ToString(), new Vector2(330, 425), Color.Black);
                        spriteBatch.DrawString(andy22, "Kill Streak: " + Config.KillStreak.ToString(), new Vector2(328, 423), Color.LimeGreen);

                        //spriteBatch.DrawString(andy22, "Kill Streak: ", new Vector2(330, 425), Color.Black);
                        //spriteBatch.DrawString(andy22, "Kill Streak: ", new Vector2(328, 423), Color.LimeGreen);

                        //spriteBatch.DrawString(andy22, Config.KillStreak.ToString(), new Vector2(450, 425), Color.Black);
                        //spriteBatch.DrawString(andy22, Config.KillStreak.ToString(), new Vector2(448, 423), Color.White);
                    }


                    switch (Config.Level)
                    {
                        case LevelSelect.One:
                            break;
                        case LevelSelect.Two:
                            spriteBatch.DrawString(andy18, levelTimeText, new Vector2(400, 20), levelTimeColor, 0f, new Vector2(font.MeasureString(levelTimeText).X / 2, 0), 1.0f, SpriteEffects.None, 1.0f);
                            break;
                        case LevelSelect.Three:
                            spriteBatch.DrawString(andy18, enemyShieldText, new Vector2(760, 135), Color.RoyalBlue, 0f, new Vector2(font.MeasureString(enemyShieldText).X / 2, font.MeasureString(enemyShieldText).Y / 2), 0.7f, SpriteEffects.None, 1.0f);
                            spriteBatch.DrawString(andy18, enemyHealthText, new Vector2(760, 100), Color.Red, 0f, new Vector2(font.MeasureString(enemyHealthText).X / 2, font.MeasureString(enemyHealthText).Y / 2), 0.7f, SpriteEffects.None, 1.0f);
                            spriteBatch.DrawString(andy18, "Score: " + (Math.Round(Config.AIScore)).ToString(), new Vector2(720 - (font.MeasureString("Score: " + ((Math.Round(Config.AIScore)).ToString())).X), 20), Color.Red);
                            spriteBatch.DrawString(andy14, "Lives: " + playerLives.ToString(), new Vector2(80, 70), Color.Green);  
                            spriteBatch.DrawString(andy18, AlterEgoScoreText, new Vector2(400, 20), AlterEgoScoreColor, 0f, new Vector2(font.MeasureString(AlterEgoScoreText).X / 2, 0), 1.0f, SpriteEffects.None, 1.0f);
                            spriteBatch.DrawString(andy18, AImulti, new Vector2(760, 40), Color.Green, 0f, new Vector2(font.MeasureString(multi).X / 2, font.MeasureString(multi).Y / 2), 0.75f, SpriteEffects.None, 1.0f);                        
                            AImultiHud.Draw(spriteBatch, new Vector2(760, 40), 0f);
                            AIShieldHud.Draw(spriteBatch, new Vector2(760, 100), 0f);
                            AIHealthHud.Draw(spriteBatch, new Vector2(760, 100), 0f);
                            break;
                        case LevelSelect.Four:
                            spriteBatch.DrawString(andy18, enemyShieldText, new Vector2(760, 135), Color.RoyalBlue, 0f, new Vector2(font.MeasureString(enemyShieldText).X / 2, font.MeasureString(enemyShieldText).Y / 2), 0.7f, SpriteEffects.None, 1.0f);
                            spriteBatch.DrawString(andy18, enemyHealthText, new Vector2(760, 100), Color.Red, 0f, new Vector2(font.MeasureString(enemyHealthText).X / 2, font.MeasureString(enemyHealthText).Y / 2), 0.7f, SpriteEffects.None, 1.0f);
                            spriteBatch.DrawString(andy18, "Score: " + (Math.Round(Config.AIScore)).ToString(), new Vector2(720 - (font.MeasureString("Score: " + ((Math.Round(Config.AIScore)).ToString())).X), 20), Color.Red);
                            spriteBatch.DrawString(andy14, "Lives: " + playerLives.ToString(), new Vector2(80, 70), Color.Green);  
                            spriteBatch.DrawString(andy18, levelTimeText, new Vector2(500, 20), Color.Green, 0f, new Vector2(font.MeasureString(levelTimeText).X / 2, 0), 1.0f, SpriteEffects.None, 1.0f);
                            spriteBatch.DrawString(andy18, AlterEgoScoreText, new Vector2(300, 20), AlterEgoScoreColor, 0f, new Vector2(font.MeasureString(AlterEgoScoreText).X / 2, 0), 1.0f, SpriteEffects.None, 1.0f);
                            spriteBatch.DrawString(andy18, AImulti, new Vector2(760, 40), Color.Green, 0f, new Vector2(font.MeasureString(multi).X / 2, font.MeasureString(multi).Y / 2), 0.75f, SpriteEffects.None, 1.0f);
                            AImultiHud.Draw(spriteBatch, new Vector2(760, 40), 0f);
                            AIShieldHud.Draw(spriteBatch, new Vector2(760, 100), 0f);
                            AIHealthHud.Draw(spriteBatch, new Vector2(760, 100), 0f);
                            break;
                        case LevelSelect.Five:
                            spriteBatch.DrawString(andy18, levelTimeText, new Vector2(400, 20), levelTimeColor, 0f, new Vector2(font.MeasureString(levelTimeText).X / 2, 0), 1.0f, SpriteEffects.None, 1.0f);
                            break;
                        case LevelSelect.Six:
                            break;
                        case LevelSelect.Practise:
                            break;
                    }

                    spriteBatch.End();
                    break;
                case GameStates.PlayerDead:
                    spriteBatch.Begin();
                    Bar.DrawBars(spriteBatch);
                    spriteBatch.DrawString(andy18, shieldText, new Vector2(40, 135), Color.RoyalBlue, 0f, new Vector2(font.MeasureString(shieldText).X / 2, font.MeasureString(shieldText).Y / 2), 0.6f, SpriteEffects.None, 1.0f);
                    spriteBatch.DrawString(andy18, healthText, new Vector2(40, 100), Color.Red, 0f, new Vector2(font.MeasureString(healthText).X / 2, font.MeasureString(healthText).Y / 2), 0.6f, SpriteEffects.None, 1.0f);
                    spriteBatch.DrawString(andy18, "Score: " + (Math.Round(Config.Score)).ToString(), new Vector2(80, 20), Color.Green);
                    //spriteBatch.DrawString(andy14, "Ability: " + playerAbilityUses.ToString(), new Vector2(80, 50), Color.Green);
                    //spriteBatch.DrawString(andy14, "Ability Uses: " + playerAbilityUses.ToString(), new Vector2(300, 430), Color.Green); 
                    spriteBatch.DrawString(andy14, "Ability Uses: " + playerAbilityUses.ToString(), new Vector2(350, 458), Color.White);  
                    //spriteBatch.DrawString(font, "Lives: " + playerLives.ToString(), new Vector2(700, 10), Color.Green);
                    multiHud.Draw(spriteBatch, new Vector2(40, 40), 0f);
                    shieldHud.Draw(spriteBatch, new Vector2(40, 100), 0f);
                    healthHud.Draw(spriteBatch, new Vector2(40, 100), 0f);
                    FloatingMultiplierText.Draw(spriteBatch, Vector2.Zero);

                    if (Player.Ship != null)
                    {
                        //spriteBatch.DrawString(andy14, Player.Ship.laserDefence.ToString(), Player.Ship.Position, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
                        spriteBatch.DrawString(andy18, "Laser:", new Vector2(40, 160), Color.Green, 0f, new Vector2(font.MeasureString("Laser:").X / 2, font.MeasureString("Laser:").Y / 2), 0.7f, SpriteEffects.None, 1.0f);
                        spriteBatch.DrawString(andy18, laserCharges, new Vector2(40, 180), Color.Green, 0f, new Vector2(font.MeasureString(laserCharges).X / 2, font.MeasureString(laserCharges).Y / 2), 0.7f, SpriteEffects.None, 1.0f);
                    }

                    switch (Config.Level)
                    {
                        case LevelSelect.One:
                            break;
                        case LevelSelect.Two:
                            spriteBatch.DrawString(andy18, levelTimeText, new Vector2(400, 20), levelTimeColor, 0f, new Vector2(font.MeasureString(levelTimeText).X / 2, 0), 1.0f, SpriteEffects.None, 1.0f);
                            break;
                        case LevelSelect.Three:
                            spriteBatch.DrawString(andy18, enemyShieldText, new Vector2(760, 135), Color.RoyalBlue, 0f, new Vector2(font.MeasureString(enemyShieldText).X / 2, font.MeasureString(enemyShieldText).Y / 2), 0.7f, SpriteEffects.None, 1.0f);
                            spriteBatch.DrawString(andy18, enemyHealthText, new Vector2(760, 100), Color.Red, 0f, new Vector2(font.MeasureString(enemyHealthText).X / 2, font.MeasureString(enemyHealthText).Y / 2), 0.7f, SpriteEffects.None, 1.0f);
                            spriteBatch.DrawString(andy18, "Score: " + (Math.Round(Config.AIScore)).ToString(), new Vector2(710 - (font.MeasureString("Score: " + ((Math.Round(Config.AIScore)).ToString())).X), 20), Color.Red);
                            spriteBatch.DrawString(andy18, AlterEgoScoreText, new Vector2(400, 20), AlterEgoScoreColor, 0f, new Vector2(font.MeasureString(AlterEgoScoreText).X / 2, 0), 1.0f, SpriteEffects.None, 1.0f);
                            spriteBatch.DrawString(andy18, AImulti, new Vector2(760, 40), Color.Green, 0f, new Vector2(font.MeasureString(multi).X / 2, font.MeasureString(multi).Y / 2), 0.75f, SpriteEffects.None, 1.0f);
                            AImultiHud.Draw(spriteBatch, new Vector2(760, 40), 0f);
                            AIShieldHud.Draw(spriteBatch, new Vector2(760, 100), 0f);
                            AIHealthHud.Draw(spriteBatch, new Vector2(760, 100), 0f);
                            if (playerLives > 1)
                            {
                                spriteBatch.DrawString(andy48, "Respawn in:", new Vector2(400, 200), Color.Green, 0f, new Vector2(andy48.MeasureString("Respawn in:").X / 2, andy48.MeasureString("Respawn in:").Y / 2), 1f,
                                    SpriteEffects.None, 1f);
                                spriteBatch.DrawString(andy48, respawnTime.Seconds.ToString(), new Vector2(400, 260), Color.Green, 0f, new Vector2(andy48.MeasureString(respawnTime.Seconds.ToString()).X / 2, andy48.MeasureString(respawnTime.Seconds.ToString()).Y / 2), 1f,
                                    SpriteEffects.None, 1f);
                            }
                            break;
                        case LevelSelect.Four:
                            spriteBatch.DrawString(andy18, enemyShieldText, new Vector2(760, 135), Color.RoyalBlue, 0f, new Vector2(font.MeasureString(enemyShieldText).X / 2, font.MeasureString(enemyShieldText).Y / 2), 0.7f, SpriteEffects.None, 1.0f);
                            spriteBatch.DrawString(andy18, enemyHealthText, new Vector2(760, 100), Color.Red, 0f, new Vector2(font.MeasureString(enemyHealthText).X / 2, font.MeasureString(enemyHealthText).Y / 2), 0.7f, SpriteEffects.None, 1.0f);
                            spriteBatch.DrawString(andy18, "Score: " + (Math.Round(Config.AIScore)).ToString(), new Vector2(710 - (font.MeasureString("Score: " + ((Math.Round(Config.AIScore)).ToString())).X), 20), Color.Red);
                            spriteBatch.DrawString(andy18, levelTimeText, new Vector2(500, 20), Color.Green, 0f, new Vector2(font.MeasureString(levelTimeText).X / 2, 0), 1.0f, SpriteEffects.None, 1.0f);
                            spriteBatch.DrawString(andy18, AlterEgoScoreText, new Vector2(300, 20), AlterEgoScoreColor, 0f, new Vector2(font.MeasureString(AlterEgoScoreText).X / 2, 0), 1.0f, SpriteEffects.None, 1.0f);
                            spriteBatch.DrawString(andy18, AImulti, new Vector2(760, 40), Color.Green, 0f, new Vector2(font.MeasureString(multi).X / 2, font.MeasureString(multi).Y / 2), 0.75f, SpriteEffects.None, 1.0f);
                            AImultiHud.Draw(spriteBatch, new Vector2(760, 40), 0f);
                            AIShieldHud.Draw(spriteBatch, new Vector2(760, 100), 0f);
                            AIHealthHud.Draw(spriteBatch, new Vector2(760, 100), 0f);
                            if (playerLives > 1)
                            {
                                spriteBatch.DrawString(andy48, "Respawn in:", new Vector2(400, 200), Color.Green, 0f, new Vector2(andy48.MeasureString("Respawn in:").X / 2, andy48.MeasureString("Respawn in:").Y / 2), 1f,
                                    SpriteEffects.None, 1f);
                                spriteBatch.DrawString(andy48, respawnTime.Seconds.ToString(), new Vector2(400, 260), Color.Green, 0f, new Vector2(andy48.MeasureString(respawnTime.Seconds.ToString()).X / 2, andy48.MeasureString(respawnTime.Seconds.ToString()).Y / 2), 1f,
                                    SpriteEffects.None, 1f);
                            }
                            break;
                        case LevelSelect.Five:
                            spriteBatch.DrawString(andy18, levelTimeText, new Vector2(400, 20), levelTimeColor, 0f, new Vector2(font.MeasureString(levelTimeText).X / 2, 0), 1.0f, SpriteEffects.None, 1.0f);
                            break;
                        case LevelSelect.Six:
                            break;
                        case LevelSelect.Practise:
                            break;
                    }

                    spriteBatch.End();
                    break;
                case GameStates.GameOver:
                    break;
            }

            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);

            //MemoryMonitor.Draw(spriteBatch, font);

            if (Config.ThumbsticksOn)
            {
                if (Config.ControlOption == 2)
                {
                    spriteBatch.Draw(thumbstick,  new Vector2(100 - thumbstick.Width / 2f, 380 - thumbstick.Height / 2f), Color.Green);
                }
                else if (VirtualThumbsticks.LeftThumbstickCenter.HasValue)
                {
                    spriteBatch.Draw(
                        thumbstick,
                        VirtualThumbsticks.LeftThumbstickCenter.Value - new Vector2(thumbstick.Width / 2f, thumbstick.Height / 2f),
                        Color.Green);
                }

                if (Config.ControlOption == 2)
                {
                    spriteBatch.Draw(thumbstick, new Vector2(700 - thumbstick.Width / 2f, 380 - thumbstick.Height / 2f), Color.Blue);
                }
                else if (VirtualThumbsticks.RightThumbstickCenter.HasValue)
                {
                    spriteBatch.Draw(
                        thumbstick,
                        VirtualThumbsticks.RightThumbstickCenter.Value - new Vector2(thumbstick.Width / 2f, thumbstick.Height / 2f),
                        Color.Blue);
                }
            }


            // spriteBatch.DrawString(font, String.Format("Nodes: {0}", Node.Nodes.Count), new Vector2(5f, font.LineSpacing * 3), Color.Orange, 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 1.0f);

            //if (runningslowly)
            //{
            //    spriteBatch.DrawString(font, "Running slowly!", new Vector2(400, 240), Color.White);
            //}
            spriteBatch.End();

            // If the game is transitioning on or off, fade it out to black.
            if (TransitionPosition > 0 || pauseAlpha > 0)
            {
                float alpha = MathHelper.Lerp(1f - TransitionAlpha, 1f, pauseAlpha / 2);

                ScreenManager.FadeBackBufferToBlack(alpha);
            }
        }
        #endregion

        public static void AddFloatingScore(SpriteFontFloatScores floatingScores, int Score, Vector2 Position, Color color, float colorChange)
        {
            //string strScore = "";

            SpriteFontFloatScore floatScore = new SpriteFontFloatScore();
            floatScore.SpriteFont = floatingScoreFont;
            floatScore.LifeSpan = 1500;
            floatScore.Color = color;
            floatScore.Alive = true;
            floatScore.ShadowEffect = false;
            floatScore.EndPosition = new Vector2(20, -20);
            floatScore.SizeTime = 500;
            floatScore.StartPosition = Position;
            floatScore.LayerDepth = 0.7f;
            floatScore.ColorChange = colorChange;

            //floatScore.Spawn3DCoordinates = Position;

            //if (Score > 0)
            //{
            //    strScore += "+ ";
            //    floatScore.Color = Color.Yellow;
            //}
            //else
            //{
            //    strScore += "- ";
            //    floatScore.Color = Color.Red;
            //}
            //strScore += Math.Abs(Score).ToString();

            //floatScore.Score = strScore;
            floatScore.Score = "+" + Score;

            floatingScores.Add(floatScore);
        }
        

    }
}

        



//LineBatch lineBatch;

//loadcontent:
//lineBatch = new LineBatch(ScreenManager.GraphicsDevice);
//// update the projection in the line-batch
//lineBatch.SetProjection(Matrix.CreateOrthographicOffCenter(0.0f,
//    ScreenManager.GraphicsDevice.Viewport.Width,
//    ScreenManager.GraphicsDevice.Viewport.Height, 0.0f, 0.0f, 1.0f));


//draw:
//lineBatch.Begin();
//lineBatch.DrawLine(new Vector2(0, 400), new Vector2(1600, 400), Color.Yellow);
//lineBatch.End();

//////how one might draw a laser with limited distance. Could draw as a polygon multple times with slight offset and alpha color to create glow effect
////const float lineLengthVelocityPercent = 0.01f;
////lineBatch.DrawLine(position, position - velocity * lineLengthVelocityPercent, Color.Yellow);


//unloadcontent:          
//if (lineBatch != null)
//{
//    lineBatch.Dispose();
//    lineBatch = null;
//}
