//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net;
//using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Documents;
//using System.Windows.Input;
//using System.Windows.Navigation;
//using System.Windows.Shapes;
//using Microsoft.Phone.Controls;
//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Content;
//using Microsoft.Xna.Framework.Graphics;

using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Phone.Controls;
using AstroFlare;
using System.Windows;
using System.Windows.Navigation;
using Microsoft.Devices.Sensors;
using Microsoft.Phone.Applications.Common;
using Microsoft.Phone.Shell;
using Microsoft.Xna.Framework.Audio;
using SkillerSDK.Listeners;
using SkillerSDK.Operations;
using SkillerSDK.Listeners.Responses;


namespace Astro_Flare_XNASilverlight
{
    public partial class GamePage : PhoneApplicationPage
    {
        //public Accelerometer _theAccelerometer = new Accelerometer();

        ContentManager contentManager;
        GameTimer timer;
        SpriteBatch spriteBatch;

        #region Fields

        //FPS Counter
        bool fpscounterOn = false;
        int numOfFrames = 0;
        int FPS = 0;
        TimeSpan elapsedTime = TimeSpan.Zero;

        //ContentManager content;
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
        //InputAction pauseAction;

        //boosts
        bool boostShieldsUpActive = false;
        bool boostLastStandActive = false;

        enum GameStates { IntroScreen, Playing, PlayerDead, GameOver, Exiting };
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

        public GamePage()
        {
            InitializeComponent();

            // Get the content manager from the application
            contentManager = (Application.Current as App).Content;

            // Create a timer for this page
            timer = new GameTimer();
            timer.UpdateInterval = TimeSpan.FromTicks(333333);
            timer.Update += OnUpdate;
            timer.Draw += OnDraw;

            AccelerometerHelper.Instance.ReadingChanged += AccelerometerReadingChanged;
            //PhoneApplicationService.Current.ApplicationIdleDetectionMode = IdleDetectionMode.Disabled;

            //_theAccelerometer.CurrentValueChanged += _theAccelerometer_CurrentValueChanged;

            //_theAccelerometer.TimeBetweenUpdates = TimeSpan.FromMilliseconds(100);

            //if (Config.ControlOption == 3)
            //    _theAccelerometer.Start();

            Sounds.Initialize();
        }

        void AccelerometerReadingChanged(object sender, AccelerometerHelperReadingEventArgs e)
        {
            Player.AccelerometerInput(e.OptimalyFilteredAcceleration.X, e.OptimalyFilteredAcceleration.Y, e.OptimalyFilteredAcceleration.Magnitude);     
        }



        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // Set the sharing mode of the graphics device to turn on XNA rendering
            SharedGraphicsDeviceManager.Current.GraphicsDevice.SetSharingMode(true);

            //MainPage.myGame.GameManager.SinglePlayerTools.StartPractice(null);
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(SharedGraphicsDeviceManager.Current.GraphicsDevice);

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

            Config.CoinsCollected = 0;
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
            if (Config.boostPrepared > 0)
            {
                playerAbilityUses++;
                Config.boostPrepared--;
            }

            if (Config.boostShieldsUp > 0)
            {
                boostShieldsUpActive = true;
            }

            if (Config.boostLastStand > 0)
            {
                boostLastStandActive = true;
            }

            KillStreakBarBackground = new Bar(200, 20, new Color(0, 0, 0, 128));
            KillStreakBarBackground.Position = new Vector2(300, 455);
            KillStreakBarBackground.Percent = 1f;

            KillStreakBar = new Bar(200, 20, new Color(0, 128, 0, 128));
            KillStreakBar.Position = new Vector2(300, 455);


            laserCharges = "0";

            thumbstick = contentManager.Load<Texture2D>("thumbstick2");

            //thumbstick = content.Load<Texture2D>("thumbstick2");
            //introExtermination = content.Load<Texture2D>(@"Gamescreens\AlterEgo");
            phaserTexture = contentManager.Load<Texture2D>(@"phaser");

            levelTimeColorInterval = new Timer();
            levelTimeColorInterval.Fire += new NotifyHandler(levelTimeColorInterval_Fire);

            switch (Config.Level)
            {
                case LevelSelect.One:
                    stars = contentManager.Load<Texture2D>(@"Background\Stars3");
                    backgroundClouds2 = contentManager.Load<Texture2D>(@"Background\BackgroundClouds2");
                    //musicLevel1 = contentManager.Load<Song>(@"Music\throughthestars");
                    //musicLevel1 = contentManager.Load<Song>(@"Music\throughthestars");
                    levelTime = 300;
                    (Application.Current as App).TryPlayBackgroundMusic(1);
                    break;
                case LevelSelect.Two:
                    stars = contentManager.Load<Texture2D>(@"Background\Stars2");
                    //backgroundClouds2 = content.Load<Texture2D>(@"Background\cloudlayer2");
                    backgroundClouds2 = contentManager.Load<Texture2D>(@"Background\BackgroundClouds2");
                    //musicLevel1 = contentManager.Load<Song>(@"Music\galacticstruggle");
                    levelTime = 300;
                    levelTimeColor = Color.Green;
                    (Application.Current as App).TryPlayBackgroundMusic(2);
                    break;
                case LevelSelect.Three:
                    //stars = content.Load<Texture2D>(@"Background\stars");
                    //backgroundClouds2 = content.Load<Texture2D>(@"Background\nova2");
                    //backgroundClouds1 = content.Load<Texture2D>(@"Background\stars4");
                    stars = contentManager.Load<Texture2D>(@"Background\Stars8");
                    backgroundClouds2 = contentManager.Load<Texture2D>(@"Background\stars7");
                    backgroundClouds1 = contentManager.Load<Texture2D>(@"Background\cloudlayer2");
                    //musicLevel1 = contentManager.Load<Song>(@"Music\survival");
                    levelTime = 300;
                    playerLives = 3;
                    respawnTime = TimeSpan.FromSeconds(5);
                    (Application.Current as App).TryPlayBackgroundMusic(3);
                    break;
                case LevelSelect.Four:
                    stars = contentManager.Load<Texture2D>(@"Background\stars");
                    backgroundClouds2 = contentManager.Load<Texture2D>(@"Background\stars10");
                    backgroundClouds1 = contentManager.Load<Texture2D>(@"Background\stars7");
                    //musicLevel1 = contentManager.Load<Song>(@"Music\holo");
                    levelTime = 300;
                    playerLives = 3;
                    respawnTime = TimeSpan.FromSeconds(5);
                    (Application.Current as App).TryPlayBackgroundMusic(4);
                    break;
                case LevelSelect.Five:
                    //stars = content.Load<Texture2D>(@"Background\Stars5");
                    //backgroundClouds2 = content.Load<Texture2D>(@"Background\Stars4");
                    //backgroundClouds1 = content.Load<Texture2D>(@"Background\cloudlayer2");
                    stars = contentManager.Load<Texture2D>(@"Background\Stars5");
                    backgroundClouds2 = contentManager.Load<Texture2D>(@"Background\Stars4");
                    backgroundClouds1 = contentManager.Load<Texture2D>(@"Background\stars7");
                    //musicLevel1 = contentManager.Load<Song>(@"Music\giantrobotsfighting");
                    levelTime = 60;
                    (Application.Current as App).TryPlayBackgroundMusic(5);
                    break;
                case LevelSelect.Six:
                    stars = contentManager.Load<Texture2D>(@"Background\stars");
                    backgroundClouds2 = contentManager.Load<Texture2D>(@"Background\nova2");
                    backgroundClouds1 = contentManager.Load<Texture2D>(@"Background\stars4");
                    //musicLevel1 = contentManager.Load<Song>(@"Music\AgainstTheOdds");
                    levelTime = 300;
                    (Application.Current as App).TryPlayBackgroundMusic(6);
                    break;
                case LevelSelect.Practise:
                    //UIdescription = content.Load<Texture2D>(@"Gamescreens\UIdescription");
                    stars = contentManager.Load<Texture2D>(@"Background\Stars3");
                    backgroundClouds2 = contentManager.Load<Texture2D>(@"Background\BackgroundClouds2");
                    //musicLevel1 = contentManager.Load<Song>(@"Music\throughthestars");
                    levelTime = 300;
                    (Application.Current as App).TryPlayBackgroundMusic(1);
                    break;
            }

            font = contentManager.Load<SpriteFont>(@"Fonts\Tahoma14");
            floatingScoreFont = contentManager.Load<SpriteFont>(@"Fonts\Andy22");
            floatingPowerupFont = contentManager.Load<SpriteFont>(@"Fonts\Andy22");
            floatingMultiplierMsgFont = contentManager.Load<SpriteFont>(@"Fonts\Andy48");
            andy14 = contentManager.Load<SpriteFont>(@"Fonts\Andy14");
            andy18 = contentManager.Load<SpriteFont>(@"Fonts\Andy18");
            andy22 = contentManager.Load<SpriteFont>(@"Fonts\Andy22");
            andy48 = contentManager.Load<SpriteFont>(@"Fonts\Andy48");

            SpriteSheet.LoadContent(contentManager, SharedGraphicsDeviceManager.Current.GraphicsDevice);

            cam = new Camera2D(SharedGraphicsDeviceManager.Current.GraphicsDevice, worldBounds.Right, worldBounds.Bottom);

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
            ParticleEffects.Initialize(SharedGraphicsDeviceManager.Current, SharedGraphicsDeviceManager.Current.GraphicsDevice);
            //ParticleEngineEffect.LoadContent(content);
            ParticleEffects.LoadContent(contentManager);

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

            FloatingMultiplierText.Color = Color.Green;
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
            //if (Config.MusicOn && MediaPlayer.GameHasControl)
            //    GameStateManagementGame.Instance.musicManager.Play(musicLevel1);

            //GameStateManagementGame.Instance.musicManager.Stop();

            Bar.Texture = contentManager.Load<Texture2D>("Textures/Fill22");

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
            //ScreenManager.Game.ResetElapsedTime();

            // Start the timer
            timer.Start();

            if (Config.ControlOption == 3)
                AccelerometerHelper.Instance.Active = true;

            PhoneApplicationService.Current.UserIdleDetectionMode = IdleDetectionMode.Disabled;

            base.OnNavigatedTo(e);
        }

        void levelTimeColorInterval_Fire()
        {
            levelTimeColor = Color.Green;
            levelTimeColorInterval.Stop();
        }


        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            // Stop the timer
            timer.Stop();

            // Set the sharing mode of the graphics device to turn off XNA rendering
            SharedGraphicsDeviceManager.Current.GraphicsDevice.SetSharingMode(false);

            //for (int i = 0; i < 100; i++)
            //{
            //    queueBullet.queue.Clear();
            //}

            //if (Guide.IsTrialMode)
            //{
            //    if (Config.Coins > 5000)
            //        Config.Coins = 5000;
            //}
            //else
            //{
                //if (Config.Coins > 10000)
                //    Config.Coins = 10000;
            //}

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
            //Guide.IsScreenSaverEnabled = true;
            AccelerometerHelper.Instance.Active = false;
            PhoneApplicationService.Current.UserIdleDetectionMode = IdleDetectionMode.Enabled;
            GC.Collect();

            (Application.Current as App).TryPlayBackgroundMusic(0);

            base.OnNavigatedFrom(e);
        }

        public void PlaySound(string soundName)
        {

        }


        /// <summary>
        /// Allows the page to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        private void OnUpdate(object sender, GameTimerEventArgs e)
        {
            if (fpscounterOn)
            {
                elapsedTime += e.ElapsedTime;

                if (elapsedTime > TimeSpan.FromSeconds(1))
                {
                    elapsedTime -= TimeSpan.FromSeconds(1);
                    FPS = numOfFrames;
                    numOfFrames = 0;
                }
            }

            VirtualThumbsticks.Update();
            //Player.ProcessInput(_theAccelerometer);
            Player.ProcessInput();

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
                    Node.UpdateNodes(e.ElapsedTime);
                    Node.RemoveDead();

                    if (Player.Ship != null)
                    {
                        if (boostShieldsUpActive && Player.Ship.Health <= (Config.ShipHealth / 2))
                        {
                            Config.boostShieldsUp--;
                            boostShieldsUpActive = false;

                            PowerupShields shields = new PowerupShields(Config.PowerupShieldsSpriteSheet);
                            shields.Position = Player.Ship.Position;
                        }
                        if (boostLastStandActive && Player.Ship.Health <= (Config.ShipHealth / 5))
                        {
                            Config.boostLastStand--;
                            boostLastStandActive = false;

                            PowerupAddBullet addProjectile = new PowerupAddBullet(Config.PowerupAddProjectileSpriteSheet);
                            addProjectile.Position = Player.Ship.Position;

                            PowerupShotSpeed shotSpeed = new PowerupShotSpeed(Config.PowerupProjectileSpeedSpriteSheet);
                            shotSpeed.Position = Player.Ship.Position;

                            PowerupLaserDefence laserDefence = new PowerupLaserDefence(Config.PowerupLaserDefenceSpriteSheet);
                            laserDefence.Position = Player.Ship.Position;
                        }
                    }

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
                            levelTime -=  e.ElapsedTime.TotalSeconds;
                            levelTimeSpan = TimeSpan.FromSeconds(levelTime);
                            levelTimeText = string.Format("{0:D2}m:{1:D2}s", levelTimeSpan.Minutes, levelTimeSpan.Seconds);
                            if (levelTime <= 0d)
                                gameState = GameStates.GameOver;
                            break;
                        case LevelSelect.Three:
                            break;
                        case LevelSelect.Four:
                            levelTime -= e.ElapsedTime.TotalSeconds;
                            levelTimeSpan = TimeSpan.FromSeconds(levelTime);
                            levelTimeText = string.Format("{0:D2}m:{1:D2}s", levelTimeSpan.Minutes, levelTimeSpan.Seconds);
                            if (levelTime <= 0d)
                                gameState = GameStates.GameOver;
                            break;
                        case LevelSelect.Five:
                            levelTime -= e.ElapsedTime.TotalSeconds;
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
                            level1.Update(e.ElapsedTime);
                            break;
                        case LevelSelect.Two:
                            level2.Update(e.ElapsedTime);
                            break;
                        case LevelSelect.Three:
                            level3.Update(e.ElapsedTime);
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
                            level4.Update(e.ElapsedTime);
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
                            level5.Update(e.ElapsedTime);
                            break;
                        case LevelSelect.Six:
                            level6.Update(e.ElapsedTime);
                            break;
                        case LevelSelect.Practise:
                            practise.Update(e.ElapsedTime);
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
                                    Node.UpdateNodes(e.ElapsedTime);
                                    Node.RemoveDead();
                                    level3.Update(e.ElapsedTime);
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
                                    Node.UpdateNodes(e.ElapsedTime);
                                    Node.RemoveDead();
                                    level4.Update(e.ElapsedTime);
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

                            respawnTime -= TimeSpan.FromSeconds(e.ElapsedTime.TotalSeconds);
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

                    gameState = GameStates.Exiting;

                    Config.Coins += Config.CoinsCollected;

                    switch (Config.Level)
                    {
                        case LevelSelect.One:
                            Config.RampageScores.Add((float)(Math.Round(Config.Score - Config.AIScore)));
                            Config.RampageScores.Sort();
                            Config.RampageScores.Reverse();
                            while (Config.RampageScores.Count > 6) Config.RampageScores.RemoveAt(Config.RampageScores.Count - 1);
                            Config.RampageScores.Capacity = Config.RampageScores.Count;
                            Config.RampageScores.TrimExcess();


                            //ScreenManager.SubmitScore((float)(Math.Round(Config.Score - Config.AIScore)), 1);
                            //SKApplication.Instance.GameManager.SinglePlayerTools.EndPractice((int)(Math.Round(Config.Score - Config.AIScore)), 0, null);

                            SKApplication.Instance.GameManager.ReportScore("Rampage", (float)(Math.Round(Config.Score - Config.AIScore)), new SKListener<SKReportScoreResponse>(OnSuccess, OnFailure));
                            if (MainPage.tournamentId != null && MainPage.tournamentGameId != null)
                            {
                                //MainPage.myGame.GameManager.SinglePlayerTools.EndTournamentGame(MainPage.tourId, MainPage.tourGameId, (int)(Math.Round(Config.Score - Config.AIScore)), 0, null);
                                SKApplication.Instance.GameManager.SinglePlayerTools.EndTournamentGame(MainPage.tournamentId, MainPage.tournamentGameId, (int)(Math.Round(Config.Score - Config.AIScore)), 0, new SKListener<SKEndTournamentGameResponse>(OnTournamentEndSuccess, OnTournamentEndFailure));
                                MainPage.tournamentId = null;
                                MainPage.tournamentGameId = null;
                            }

                            //SKApplication.Instance.GameManager.UnlockAchievement("10211", new SKListener<SKStatusResponse>(OnAchievementUnlockSuccess, OnAchievementUnlockFailure));
                            if ((int)(Math.Round(Config.Score - Config.AIScore)) >= 5000)
                            {
                                //MainPage.myGame.GameManager.UnlockAchievement(10139, null);
                                SKApplication.Instance.GameManager.UnlockAchievement("10139", new SKListener<SKStatusResponse>(OnAchievementUnlockSuccess, OnAchievementUnlockFailure));
                            }
                            break;
                        case LevelSelect.Two:
                            //timeBonus = 1 + ((300 - levelTime) / 300);
                            //tempScore = (Config.Score - Config.AIScore);
                            //tempScore = tempScore * timeBonus;

                            Config.RampageTimedScores.Add((float)(Math.Round(Config.Score - Config.AIScore)));
                            Config.RampageTimedScores.Sort();
                            Config.RampageTimedScores.Reverse();
                            while (Config.RampageTimedScores.Count > 6) Config.RampageTimedScores.RemoveAt(Config.RampageTimedScores.Count - 1);
                            Config.RampageTimedScores.Capacity = Config.RampageTimedScores.Count;
                            Config.RampageTimedScores.TrimExcess();
                            //ScreenManager.SubmitScore((float)(Math.Round(Config.Score - Config.AIScore)), 2);

                            SKApplication.Instance.GameManager.ReportScore("RampageTimed", (float)(Math.Round(Config.Score - Config.AIScore)), new SKListener<SKReportScoreResponse>(OnSuccess, OnFailure));
                            break;
                        case LevelSelect.Three:
                            Config.AlterEgoScores.Add((float)(Math.Round(Config.Score - Config.AIScore)));
                            Config.AlterEgoScores.Sort();
                            Config.AlterEgoScores.Reverse();
                            while (Config.AlterEgoScores.Count > 6) Config.AlterEgoScores.RemoveAt(Config.AlterEgoScores.Count - 1);
                            Config.AlterEgoScores.Capacity = Config.AlterEgoScores.Count;
                            Config.AlterEgoScores.TrimExcess();
                            //ScreenManager.SubmitScore((float)(Math.Round(Config.Score - Config.AIScore)), 3);
                            SKApplication.Instance.GameManager.ReportScore("AlterEgo", (float)(Math.Round(Config.Score - Config.AIScore)), new SKListener<SKReportScoreResponse>(OnSuccess, OnFailure));
                            break;
                        case LevelSelect.Four:
                            //timeBonus = 1 + ((300 - levelTime) / 300);
                            //tempScore = (Config.Score - Config.AIScore);
                            //tempScore = tempScore * timeBonus;
                            Config.AlterEgoTimedScores.Add((float)(Math.Round(Config.Score - Config.AIScore)));
                            Config.AlterEgoTimedScores.Sort();
                            Config.AlterEgoTimedScores.Reverse();
                            while (Config.AlterEgoTimedScores.Count > 6) Config.AlterEgoTimedScores.RemoveAt(Config.AlterEgoTimedScores.Count - 1);
                            Config.AlterEgoTimedScores.Capacity = Config.AlterEgoTimedScores.Count;
                            Config.AlterEgoTimedScores.TrimExcess();
                            //ScreenManager.SubmitScore((float)(Math.Round(Config.Score - Config.AIScore)), 4);
                            SKApplication.Instance.GameManager.ReportScore("AlterEgoTimed", (float)(Math.Round(Config.Score - Config.AIScore)), new SKListener<SKReportScoreResponse>(OnSuccess, OnFailure));
                            break;
                        case LevelSelect.Five:
                            Config.TimeBanditScores.Add((float)(Math.Round(Config.Score - Config.AIScore)));
                            Config.TimeBanditScores.Sort();
                            Config.TimeBanditScores.Reverse();
                            while (Config.TimeBanditScores.Count > 6) Config.TimeBanditScores.RemoveAt(Config.TimeBanditScores.Count - 1);
                            Config.TimeBanditScores.Capacity = Config.TimeBanditScores.Count;
                            Config.TimeBanditScores.TrimExcess();
                            //ScreenManager.SubmitScore((float)(Math.Round(Config.Score - Config.AIScore)), 5);
                            SKApplication.Instance.GameManager.ReportScore("TimeBandit", (float)(Math.Round(Config.Score - Config.AIScore)), new SKListener<SKReportScoreResponse>(OnSuccess, OnFailure));
                            break;
                        case LevelSelect.Six:
                            Config.ExterminationScores.Add((float)(Math.Round(Config.Score - Config.AIScore)));
                            Config.ExterminationScores.Sort();
                            Config.ExterminationScores.Reverse();
                            while (Config.ExterminationScores.Count > 6) Config.ExterminationScores.RemoveAt(Config.ExterminationScores.Count - 1);
                            Config.ExterminationScores.Capacity = Config.ExterminationScores.Count;
                            Config.ExterminationScores.TrimExcess();
                            //ScreenManager.SubmitScore((float)(Math.Round(Config.Score - Config.AIScore)), 6);
                            SKApplication.Instance.GameManager.ReportScore("Extermination", (float)(Math.Round(Config.Score - Config.AIScore)), new SKListener<SKReportScoreResponse>(OnSuccess, OnFailure));
                            break;
                        case LevelSelect.Practise:
                            break;
                    }



                    //ExitScreen();
                    //LoadingScreen.Load(ScreenManager, true, ControllingPlayer, new BackgroundScreen(), new PhoneScoreScreen());

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

                case GameStates.Exiting:
                    NavigationService.Navigate(new Uri("/ScoreScreen.xaml", UriKind.Relative));
                    break;
            }



            cameraTransform = cam.get_transformation(SharedGraphicsDeviceManager.Current.GraphicsDevice, 1f);

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

            ParticleEffects.Update(e.ElapsedTime, cam, ref cameraTransform);
            Timer.Update(e.ElapsedTime);
            //Player.ProcessInput();
            //VirtualThumbsticks.Update();

            //bud.Update(gameTime);

            //MemoryMonitor.Update();

            FloatingScoreList.UpdateAllAnimatedPositions(e.ElapsedTime);
            //FloatingScore.UpdateAnimatedPosition(gameTime);
            FloatingPowerupText.UpdateAnimatedPosition(e.ElapsedTime);
            FloatingMultiplierText.UpdateAnimatedPosition(e.ElapsedTime);


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
        }

        private void OnTournamentEndSuccess(SKEndTournamentGameResponse response)
        {
            //your code
            bool leader = response.IsLeader;
        }

        private void OnTournamentEndFailure(SKStatusResponse response)
        {
            //your code
        }

        private void OnSuccess(SKReportScoreResponse response)
        {
            //your code
        }

        private void OnFailure(SKStatusResponse response)
        {
            //your code
        }

        private void OnAchievementUnlockSuccess(SKStatusResponse response)
        {
            //your code
        }

        private void OnAchievementUnlockFailure(SKStatusResponse response)
        {
            //your code
        }

        /// <summary>
        /// Allows the page to draw itself.
        /// </summary>
        private void OnDraw(object sender, GameTimerEventArgs e)
        {
            SharedGraphicsDeviceManager.Current.GraphicsDevice.Clear(Color.Black);


            if (Config.Level == LevelSelect.Six || Config.Level == LevelSelect.Five || Config.Level == LevelSelect.Four || Config.Level == LevelSelect.Three)
            {
                //Game background
                spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, null, null, null, null, cam.get_transformation(SharedGraphicsDeviceManager.Current.GraphicsDevice, 0.1f));
                spriteBatch.Draw(stars, new Rectangle(-360, -216, 880, 528), null, new Color(255, 255, 255, 0), 0f, Vector2.Zero, SpriteEffects.None, 0.1f);
                spriteBatch.End();

                //spriteBatch.Draw(backgroundClouds1, new Rectangle(0, 0, graphics.GraphicsDevice.Viewport.Width, graphics.GraphicsDevice.Viewport.Height), new Color(255, 255, 255, 0));
                spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, null, null, null, null, cam.get_transformation(SharedGraphicsDeviceManager.Current.GraphicsDevice, 0.15f));
                spriteBatch.Draw(backgroundClouds2, new Rectangle(120, 72, 920, 552), null, new Color(255, 255, 255, 0), 0f, new Vector2(backgroundClouds2.Width / 2, backgroundClouds2.Height / 2), SpriteEffects.None, 0.2f);
                spriteBatch.End();

                spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, null, null, null, null, cam.get_transformation(SharedGraphicsDeviceManager.Current.GraphicsDevice, 0.3f));
                spriteBatch.Draw(backgroundClouds1, new Rectangle(240, 144, 1040, 624), null, new Color(255, 255, 255, 0), 0f, new Vector2(backgroundClouds1.Width / 2, backgroundClouds1.Height / 2), SpriteEffects.None, 0.21f);
                spriteBatch.End();
            }
            else
            {
                //Game background
                spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, null, null, null, null, cam.get_transformation(SharedGraphicsDeviceManager.Current.GraphicsDevice, 0.1f));
                spriteBatch.Draw(stars, new Rectangle(-360, -216, 880, 528), null, new Color(255, 255, 255, 0), 0f, Vector2.Zero, SpriteEffects.None, 0.1f);
                spriteBatch.End();

                //spriteBatch.Draw(backgroundClouds1, new Rectangle(0, 0, graphics.GraphicsDevice.Viewport.Width, graphics.GraphicsDevice.Viewport.Height), new Color(255, 255, 255, 0));
                spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, null, null, null, null, cam.get_transformation(SharedGraphicsDeviceManager.Current.GraphicsDevice, 0.2f));
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

            if (fpscounterOn)
            {
                numOfFrames++;
                spriteBatch.DrawString(floatingPowerupFont, FPS.ToString(), new Vector2(100, 100), Color.White);
            }

            //MemoryMonitor.Draw(spriteBatch, font);

            if (Config.ThumbsticksOn)
            {
                if (Config.ControlOption == 2)
                {
                    spriteBatch.Draw(thumbstick, new Vector2(100 - thumbstick.Width / 2f, 380 - thumbstick.Height / 2f), Color.Green);
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
        }



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