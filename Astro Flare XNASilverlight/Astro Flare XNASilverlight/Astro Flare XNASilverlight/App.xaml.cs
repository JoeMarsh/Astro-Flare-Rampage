using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using AstroFlare;
using Microsoft.Phone.Applications.Common;
using Microsoft.Xna.Framework.Media;
using System.Windows.Media;

namespace Astro_Flare_XNASilverlight
{
    public partial class App : Application
    {

        /// <summary>
        /// Provides easy access to the root frame of the Phone Application.
        /// </summary>
        /// <returns>The root frame of the Phone Application.</returns>
        public PhoneApplicationFrame RootFrame { get; private set; }

        /// <summary>
        /// Provides access to a ContentManager for the application.
        /// </summary>
        public ContentManager Content { get; private set; }

        /// <summary>
        /// Provides access to a GameTimer that is set up to pump the FrameworkDispatcher.
        /// </summary>
        public GameTimer FrameworkDispatcherTimer { get; private set; }

        /// <summary>
        /// Provides access to the AppServiceProvider for the application.
        /// </summary>
        public AppServiceProvider Services { get; private set; }

        public static bool CanPlayMusic;

        /// <summary>
        /// Constructor for the Application object.
        /// </summary>
        public App()
        {
            // Global handler for uncaught exceptions. 
            UnhandledException += Application_UnhandledException;

            // Standard Silverlight initialization
            InitializeComponent();

            // Change default styles
            InitializeStyleChanges();

            // Phone-specific initialization
            InitializePhoneApplication();

            // XNA initialization
            InitializeXnaApplication();

            // Show graphics profiling information while debugging.
            if (System.Diagnostics.Debugger.IsAttached)
            {
                // Display the current frame rate counters.
                Application.Current.Host.Settings.EnableFrameRateCounter = false;

                // Show the areas of the app that are being redrawn in each frame.
                //Application.Current.Host.Settings.EnableRedrawRegions = true;

                // Enable non-production analysis visualization mode, 
                // which shows areas of a page that are handed off to GPU with a colored overlay.
                //Application.Current.Host.Settings.EnableCacheVisualization = true;

                // Disable the application idle detection by setting the UserIdleDetectionMode property of the
                // application's PhoneApplicationService object to Disabled.
                // Caution:- Use this under debug mode only. Applications that disable user idle detection will continue to run
                // and consume battery power when the user is not using the phone.
                PhoneApplicationService.Current.UserIdleDetectionMode = IdleDetectionMode.Disabled;
            }
        }

        // Code to execute when the application is launching (eg, from Start)
        // This code will not execute when the application is reactivated
        private void Application_Launching(object sender, LaunchingEventArgs e)
        {

            //GlobalSave.Initialize();
            Config.Initialize();
            //Config.loadSavedData();
            Config.Coins = ApplicationSettingHelper.TryGetValueWithDefault<double>("Coins", 0);

            //Config.RampageScores = IsolatedStorageSettings.ApplicationSettings["RampageScores"] as List<float>;

            Config.RampageScores = ApplicationSettingHelper.TryGetValueWithDefault<List<float>>("RampageScores", new List<float> { 0, 0, 0, 0, 0, 0 });
            Config.RampageTimedScores = ApplicationSettingHelper.TryGetValueWithDefault<List<float>>("RampageTimedScores", new List<float> { 0, 0, 0, 0, 0, 0 });
            Config.AlterEgoScores = ApplicationSettingHelper.TryGetValueWithDefault<List<float>>("AlterEgoScores", new List<float> { 0, 0, 0, 0, 0, 0 });
            Config.AlterEgoTimedScores = ApplicationSettingHelper.TryGetValueWithDefault<List<float>>("AlterEgoTimedScores", new List<float> { 0, 0, 0, 0, 0, 0 });
            Config.TimeBanditScores = ApplicationSettingHelper.TryGetValueWithDefault<List<float>>("TimeBanditScores", new List<float> { 0, 0, 0, 0, 0, 0 });
            Config.ExterminationScores = ApplicationSettingHelper.TryGetValueWithDefault<List<float>>("ExterminationScores", new List<float> { 0, 0, 0, 0, 0, 0 });

            //Game Options
            Config.Vibrate = ApplicationSettingHelper.TryGetValueWithDefault<bool>("Vibrate", true);
            Config.MusicOn = ApplicationSettingHelper.TryGetValueWithDefault<bool>("MusicOn", true);
            Config.SoundFXOn = ApplicationSettingHelper.TryGetValueWithDefault<bool>("SoundFXOn", true);
            Config.ControlOption = ApplicationSettingHelper.TryGetValueWithDefault<int>("ControlOption", 0);
            Config.ThumbsticksOn = ApplicationSettingHelper.TryGetValueWithDefault<bool>("ThumbsticksOn", true);
            Config.AIControlled = ApplicationSettingHelper.TryGetValueWithDefault<bool>("AIControlled", true);

            //Ships
            Config.ship1Active = ApplicationSettingHelper.TryGetValueWithDefault<bool>("ship1Active", true);
            Config.ship2Active = ApplicationSettingHelper.TryGetValueWithDefault<bool>("ship2Active", false);
            Config.ship3Active = ApplicationSettingHelper.TryGetValueWithDefault<bool>("ship3Active", false);
            Config.ship2Locked = ApplicationSettingHelper.TryGetValueWithDefault<bool>("ship2Locked", true);
            Config.ship3Locked = ApplicationSettingHelper.TryGetValueWithDefault<bool>("ship3Locked", true);

            //Boosts
            Config.boostPrepared = ApplicationSettingHelper.TryGetValueWithDefault<int>("boostPrepared", 0);
            Config.boostShieldsUp = ApplicationSettingHelper.TryGetValueWithDefault<int>("boostShieldsUp", 0);
            Config.boostLastStand = ApplicationSettingHelper.TryGetValueWithDefault<int>("boostLastStand", 0);

            //Powerups
            Config.activeUpgrades = ApplicationSettingHelper.TryGetValueWithDefault<int>("activeUpgrades", 0);

            Config.bHealth5Locked = ApplicationSettingHelper.TryGetValueWithDefault<bool>("bHealth5Locked", true);
            Config.bHealth10Locked = ApplicationSettingHelper.TryGetValueWithDefault<bool>("bHealth10Locked", true);
            Config.bHealth15Locked = ApplicationSettingHelper.TryGetValueWithDefault<bool>("bHealth15Locked", true);

            Config.bShields10Locked = ApplicationSettingHelper.TryGetValueWithDefault<bool>("bShields10Locked", true);
            Config.bShields20Locked = ApplicationSettingHelper.TryGetValueWithDefault<bool>("bShields20Locked", true);
            Config.bShields30Locked = ApplicationSettingHelper.TryGetValueWithDefault<bool>("bShields30Locked", true);

            Config.bDamage1Locked = ApplicationSettingHelper.TryGetValueWithDefault<bool>("bDamage1Locked", true);
            Config.bDamage2Locked = ApplicationSettingHelper.TryGetValueWithDefault<bool>("bDamage2Locked", true);
            Config.bDamage4Locked = ApplicationSettingHelper.TryGetValueWithDefault<bool>("bDamage4Locked", true);

            Config.bRange10Locked = ApplicationSettingHelper.TryGetValueWithDefault<bool>("bRange10Locked", true);
            Config.bRange20Locked = ApplicationSettingHelper.TryGetValueWithDefault<bool>("bRange20Locked", true);
            Config.bRange50Locked = ApplicationSettingHelper.TryGetValueWithDefault<bool>("bRange50Locked", true);

            Config.bPowerup5Locked = ApplicationSettingHelper.TryGetValueWithDefault<bool>("bPowerup5Locked", true);
            Config.bPowerup10Locked = ApplicationSettingHelper.TryGetValueWithDefault<bool>("bPowerup10Locked", true);
            Config.bPowerup20Locked = ApplicationSettingHelper.TryGetValueWithDefault<bool>("bPowerup20Locked", true);

            Config.bHealth5Active = ApplicationSettingHelper.TryGetValueWithDefault<bool>("bHealth5Active", false);
            Config.bHealth10Active = ApplicationSettingHelper.TryGetValueWithDefault<bool>("bHealth10Active", false);
            Config.bHealth15Active = ApplicationSettingHelper.TryGetValueWithDefault<bool>("bHealth15Active", false);

            Config.bShields10Active = ApplicationSettingHelper.TryGetValueWithDefault<bool>("bShields10Active", false);
            Config.bShields20Active = ApplicationSettingHelper.TryGetValueWithDefault<bool>("bShields20Active", false);
            Config.bShields30Active = ApplicationSettingHelper.TryGetValueWithDefault<bool>("bShields30Active", false);

            Config.bDamage1Active = ApplicationSettingHelper.TryGetValueWithDefault<bool>("bDamage1Active", false);
            Config.bDamage2Active = ApplicationSettingHelper.TryGetValueWithDefault<bool>("bDamage2Active", false);
            Config.bDamage4Active = ApplicationSettingHelper.TryGetValueWithDefault<bool>("bDamage4Active", false);

            Config.bRange10Active = ApplicationSettingHelper.TryGetValueWithDefault<bool>("bRange10Active", false);
            Config.bRange20Active = ApplicationSettingHelper.TryGetValueWithDefault<bool>("bRange20Active", false);
            Config.bRange50Active = ApplicationSettingHelper.TryGetValueWithDefault<bool>("bRange50Active", false);

            Config.bPowerup5Active = ApplicationSettingHelper.TryGetValueWithDefault<bool>("bPowerup5Active", false);
            Config.bPowerup10Active = ApplicationSettingHelper.TryGetValueWithDefault<bool>("bPowerup10Active", false);
            Config.bPowerup20Active = ApplicationSettingHelper.TryGetValueWithDefault<bool>("bPowerup20Active", false);

            Config.loadConfiguration();

            CanPlayMusic = true;
            if (!MediaPlayer.GameHasControl)
            {
                //ask user about background music
                MessageBoxResult mbr = MessageBox.Show("press ok if you'd like to use this app's background music (this will stop your current music playback)", "use app background music?", MessageBoxButton.OKCancel);
                if (mbr != MessageBoxResult.OK)
                {
                    CanPlayMusic = false;
                }
                else
                    CanPlayMusic = true;
            }


            (Application.Current as App).TryPlayBackgroundMusic(0);
        }

        // Code to execute when the application is activated (brought to foreground)
        // This code will not execute when the application is first launched
        private void Application_Activated(object sender, ActivatedEventArgs e)
        {

            //GlobalSave.Initialize();
            Config.Initialize();
            //Config.loadSavedData();

            //Config.RampageScores = IsolatedStorageSettings.ApplicationSettings["RampageScores"] as List<float>;
            Config.RampageScores = ApplicationSettingHelper.TryGetValueWithDefault<List<float>>("RampageScores", new List<float> { 0, 0, 0, 0, 0, 0 });
            Config.RampageTimedScores = ApplicationSettingHelper.TryGetValueWithDefault<List<float>>("RampageTimedScores", new List<float> { 0, 0, 0, 0, 0, 0 });
            Config.AlterEgoScores = ApplicationSettingHelper.TryGetValueWithDefault<List<float>>("AlterEgoScores", new List<float> { 0, 0, 0, 0, 0, 0 });
            Config.AlterEgoTimedScores = ApplicationSettingHelper.TryGetValueWithDefault<List<float>>("AlterEgoTimedScores", new List<float> { 0, 0, 0, 0, 0, 0 });
            Config.TimeBanditScores = ApplicationSettingHelper.TryGetValueWithDefault<List<float>>("TimeBanditScores", new List<float> { 0, 0, 0, 0, 0, 0 });
            Config.ExterminationScores = ApplicationSettingHelper.TryGetValueWithDefault<List<float>>("ExterminationScores", new List<float> { 0, 0, 0, 0, 0, 0 });

            //Coins
            Config.Coins = ApplicationSettingHelper.TryGetValueWithDefault<double>("Coins", 0);

            //Game Options
            Config.Vibrate = ApplicationSettingHelper.TryGetValueWithDefault<bool>("Vibrate", true);
            Config.MusicOn = ApplicationSettingHelper.TryGetValueWithDefault<bool>("MusicOn", true);
            Config.SoundFXOn = ApplicationSettingHelper.TryGetValueWithDefault<bool>("SoundFXOn", true);
            Config.ControlOption = ApplicationSettingHelper.TryGetValueWithDefault<int>("ControlOption", 0);
            Config.ThumbsticksOn = ApplicationSettingHelper.TryGetValueWithDefault<bool>("ThumbsticksOn", true);
            Config.AIControlled = ApplicationSettingHelper.TryGetValueWithDefault<bool>("AIControlled", true);

            //Ships
            Config.ship1Active = ApplicationSettingHelper.TryGetValueWithDefault<bool>("ship1Active", true);
            Config.ship2Active = ApplicationSettingHelper.TryGetValueWithDefault<bool>("ship2Active", false);
            Config.ship3Active = ApplicationSettingHelper.TryGetValueWithDefault<bool>("ship3Active", false);
            Config.ship2Locked = ApplicationSettingHelper.TryGetValueWithDefault<bool>("ship2Locked", true);
            Config.ship3Locked = ApplicationSettingHelper.TryGetValueWithDefault<bool>("ship3Locked", true);

            //Boosts
            Config.boostPrepared = ApplicationSettingHelper.TryGetValueWithDefault<int>("boostPrepared", 0);
            Config.boostShieldsUp = ApplicationSettingHelper.TryGetValueWithDefault<int>("boostShieldsUp", 0);
            Config.boostLastStand = ApplicationSettingHelper.TryGetValueWithDefault<int>("boostLastStand", 0);

            //Powerups
            Config.activeUpgrades = ApplicationSettingHelper.TryGetValueWithDefault<int>("activeUpgrades", 0);

            Config.bHealth5Locked = ApplicationSettingHelper.TryGetValueWithDefault<bool>("bHealth5Locked", true);
            Config.bHealth10Locked = ApplicationSettingHelper.TryGetValueWithDefault<bool>("bHealth10Locked", true);
            Config.bHealth15Locked = ApplicationSettingHelper.TryGetValueWithDefault<bool>("bHealth15Locked", true);

            Config.bShields10Locked = ApplicationSettingHelper.TryGetValueWithDefault<bool>("bShields10Locked", true);
            Config.bShields20Locked = ApplicationSettingHelper.TryGetValueWithDefault<bool>("bShields20Locked", true);
            Config.bShields30Locked = ApplicationSettingHelper.TryGetValueWithDefault<bool>("bShields30Locked", true);

            Config.bDamage1Locked = ApplicationSettingHelper.TryGetValueWithDefault<bool>("bDamage1Locked", true);
            Config.bDamage2Locked = ApplicationSettingHelper.TryGetValueWithDefault<bool>("bDamage2Locked", true);
            Config.bDamage4Locked = ApplicationSettingHelper.TryGetValueWithDefault<bool>("bDamage4Locked", true);

            Config.bRange10Locked = ApplicationSettingHelper.TryGetValueWithDefault<bool>("bRange10Locked", true);
            Config.bRange20Locked = ApplicationSettingHelper.TryGetValueWithDefault<bool>("bRange20Locked", true);
            Config.bRange50Locked = ApplicationSettingHelper.TryGetValueWithDefault<bool>("bRange50Locked", true);

            Config.bPowerup5Locked = ApplicationSettingHelper.TryGetValueWithDefault<bool>("bPowerup5Locked", true);
            Config.bPowerup10Locked = ApplicationSettingHelper.TryGetValueWithDefault<bool>("bPowerup10Locked", true);
            Config.bPowerup20Locked = ApplicationSettingHelper.TryGetValueWithDefault<bool>("bPowerup20Locked", true);

            Config.bHealth5Active = ApplicationSettingHelper.TryGetValueWithDefault<bool>("bHealth5Active", false);
            Config.bHealth10Active = ApplicationSettingHelper.TryGetValueWithDefault<bool>("bHealth10Active", false);
            Config.bHealth15Active = ApplicationSettingHelper.TryGetValueWithDefault<bool>("bHealth15Active", false);

            Config.bShields10Active = ApplicationSettingHelper.TryGetValueWithDefault<bool>("bShields10Active", false);
            Config.bShields20Active = ApplicationSettingHelper.TryGetValueWithDefault<bool>("bShields20Active", false);
            Config.bShields30Active = ApplicationSettingHelper.TryGetValueWithDefault<bool>("bShields30Active", false);

            Config.bDamage1Active = ApplicationSettingHelper.TryGetValueWithDefault<bool>("bDamage1Active", false);
            Config.bDamage2Active = ApplicationSettingHelper.TryGetValueWithDefault<bool>("bDamage2Active", false);
            Config.bDamage4Active = ApplicationSettingHelper.TryGetValueWithDefault<bool>("bDamage4Active", false);

            Config.bRange10Active = ApplicationSettingHelper.TryGetValueWithDefault<bool>("bRange10Active", false);
            Config.bRange20Active = ApplicationSettingHelper.TryGetValueWithDefault<bool>("bRange20Active", false);
            Config.bRange50Active = ApplicationSettingHelper.TryGetValueWithDefault<bool>("bRange50Active", false);

            Config.bPowerup5Active = ApplicationSettingHelper.TryGetValueWithDefault<bool>("bPowerup5Active", false);
            Config.bPowerup10Active = ApplicationSettingHelper.TryGetValueWithDefault<bool>("bPowerup10Active", false);
            Config.bPowerup20Active = ApplicationSettingHelper.TryGetValueWithDefault<bool>("bPowerup20Active", false);

            if (!e.IsApplicationInstancePreserved)
                Config.loadConfiguration();

            CanPlayMusic = true;
            if (!MediaPlayer.GameHasControl)
            {
                //ask user about background music
                MessageBoxResult mbr = MessageBox.Show("press ok if you'd like to use this app's background music (this will stop your current music playback)", "use app background music?", MessageBoxButton.OKCancel);
                if (mbr != MessageBoxResult.OK)
                {
                    CanPlayMusic = false;
                }
                else
                    CanPlayMusic = true;
            }


            (Application.Current as App).TryPlayBackgroundMusic(0);
        }

        // Code to execute when the application is deactivated (sent to background)
        // This code will not execute when the application is closing
        private void Application_Deactivated(object sender, DeactivatedEventArgs e)
        {
            //IsolatedStorageSettings.ApplicationSettings["RampageScores"] = Config.RampageScores;
            ApplicationSettingHelper.AddOrUpdateValue("RampageScores", Config.RampageScores);
            ApplicationSettingHelper.AddOrUpdateValue("RampageTimedScores", Config.RampageTimedScores);
            ApplicationSettingHelper.AddOrUpdateValue("AlterEgoScores", Config.AlterEgoScores);
            ApplicationSettingHelper.AddOrUpdateValue("AlterEgoTimedScores", Config.AlterEgoTimedScores);
            ApplicationSettingHelper.AddOrUpdateValue("TimeBanditScores", Config.TimeBanditScores);
            ApplicationSettingHelper.AddOrUpdateValue("ExterminationScores", Config.ExterminationScores);

            //Game Options
            ApplicationSettingHelper.AddOrUpdateValue("Vibrate", Config.Vibrate);
            ApplicationSettingHelper.AddOrUpdateValue("MusicOn", Config.MusicOn);
            ApplicationSettingHelper.AddOrUpdateValue("SoundFXOn", Config.SoundFXOn);
            ApplicationSettingHelper.AddOrUpdateValue("ControlOption", Config.ControlOption);
            ApplicationSettingHelper.AddOrUpdateValue("ThumbsticksOn", Config.ThumbsticksOn);
            ApplicationSettingHelper.AddOrUpdateValue("AIControlled", Config.AIControlled);

            //Ships
            ApplicationSettingHelper.AddOrUpdateValue("ship1Active", Config.ship1Active);
            ApplicationSettingHelper.AddOrUpdateValue("ship2Active", Config.ship2Active);
            ApplicationSettingHelper.AddOrUpdateValue("ship3Active", Config.ship3Active);
            ApplicationSettingHelper.AddOrUpdateValue("ship2Locked", Config.ship2Locked);
            ApplicationSettingHelper.AddOrUpdateValue("ship3Locked", Config.ship3Locked);

            //Boosts
            ApplicationSettingHelper.AddOrUpdateValue("boostPrepared", Config.boostPrepared);
            ApplicationSettingHelper.AddOrUpdateValue("boostShieldsUp", Config.boostShieldsUp);
            ApplicationSettingHelper.AddOrUpdateValue("boostLastStand", Config.boostLastStand);

            //Powerups
            ApplicationSettingHelper.AddOrUpdateValue("activeUpgrades", Config.activeUpgrades);

            ApplicationSettingHelper.AddOrUpdateValue("bHealth5Locked", Config.bHealth5Locked);
            ApplicationSettingHelper.AddOrUpdateValue("bHealth10Locked", Config.bHealth10Locked);
            ApplicationSettingHelper.AddOrUpdateValue("bHealth15Locked", Config.bHealth15Locked);

            ApplicationSettingHelper.AddOrUpdateValue("bShields10Locked", Config.bShields10Locked);
            ApplicationSettingHelper.AddOrUpdateValue("bShields20Locked", Config.bShields20Locked);
            ApplicationSettingHelper.AddOrUpdateValue("bShields30Locked", Config.bShields30Locked);

            ApplicationSettingHelper.AddOrUpdateValue("bDamage1Locked", Config.bDamage1Locked);
            ApplicationSettingHelper.AddOrUpdateValue("bDamage2Locked", Config.bDamage2Locked);
            ApplicationSettingHelper.AddOrUpdateValue("bDamage4Locked", Config.bDamage4Locked);

            ApplicationSettingHelper.AddOrUpdateValue("bRange10Locked", Config.bRange10Locked);
            ApplicationSettingHelper.AddOrUpdateValue("bRange20Locked", Config.bRange20Locked);
            ApplicationSettingHelper.AddOrUpdateValue("bRange50Locked", Config.bRange50Locked);

            ApplicationSettingHelper.AddOrUpdateValue("bPowerup5Locked", Config.bPowerup5Locked);
            ApplicationSettingHelper.AddOrUpdateValue("bPowerup10Locked", Config.bPowerup10Locked);
            ApplicationSettingHelper.AddOrUpdateValue("bPowerup20Locked", Config.bPowerup20Locked);

            ApplicationSettingHelper.AddOrUpdateValue("bHealth5Active", Config.bHealth5Active);
            ApplicationSettingHelper.AddOrUpdateValue("bHealth10Active", Config.bHealth10Active);
            ApplicationSettingHelper.AddOrUpdateValue("bHealth15Active", Config.bHealth15Active);

            ApplicationSettingHelper.AddOrUpdateValue("bShields10Active", Config.bShields10Active);
            ApplicationSettingHelper.AddOrUpdateValue("bShields20Active", Config.bShields20Active);
            ApplicationSettingHelper.AddOrUpdateValue("bShields30Active", Config.bShields30Active);

            ApplicationSettingHelper.AddOrUpdateValue("bDamage1Active", Config.bDamage1Active);
            ApplicationSettingHelper.AddOrUpdateValue("bDamage2Active", Config.bDamage2Active);
            ApplicationSettingHelper.AddOrUpdateValue("bDamage4Active", Config.bDamage4Active);

            ApplicationSettingHelper.AddOrUpdateValue("bRange10Active", Config.bRange10Active);
            ApplicationSettingHelper.AddOrUpdateValue("bRange20Active", Config.bRange20Active);
            ApplicationSettingHelper.AddOrUpdateValue("bRange50Active", Config.bRange50Active);

            ApplicationSettingHelper.AddOrUpdateValue("bPowerup5Active", Config.bPowerup5Active);
            ApplicationSettingHelper.AddOrUpdateValue("bPowerup10Active", Config.bPowerup10Active);
            ApplicationSettingHelper.AddOrUpdateValue("bPowerup20Active", Config.bPowerup20Active);

            //Coins
            ApplicationSettingHelper.AddOrUpdateValue("Coins", Config.Coins);
        }


        // Code to execute when the application is closing (eg, user hit Back)
        // This code will not execute when the application is deactivated
        private void Application_Closing(object sender, ClosingEventArgs e)
        {
            //IsolatedStorageSettings.ApplicationSettings["RampageScores"] = Config.RampageScores;
            ApplicationSettingHelper.AddOrUpdateValue("RampageScores", Config.RampageScores);
            ApplicationSettingHelper.AddOrUpdateValue("RampageTimedScores", Config.RampageTimedScores);
            ApplicationSettingHelper.AddOrUpdateValue("AlterEgoScores", Config.AlterEgoScores);
            ApplicationSettingHelper.AddOrUpdateValue("AlterEgoTimedScores", Config.AlterEgoTimedScores);
            ApplicationSettingHelper.AddOrUpdateValue("TimeBanditScores", Config.TimeBanditScores);
            ApplicationSettingHelper.AddOrUpdateValue("ExterminationScores", Config.ExterminationScores);

            //Game Options
            ApplicationSettingHelper.AddOrUpdateValue("Vibrate", Config.Vibrate);
            ApplicationSettingHelper.AddOrUpdateValue("MusicOn", Config.MusicOn);
            ApplicationSettingHelper.AddOrUpdateValue("SoundFXOn", Config.SoundFXOn);
            ApplicationSettingHelper.AddOrUpdateValue("ControlOption", Config.ControlOption);
            ApplicationSettingHelper.AddOrUpdateValue("ThumbsticksOn", Config.ThumbsticksOn);
            ApplicationSettingHelper.AddOrUpdateValue("AIControlled", Config.AIControlled);

            //Ships
            ApplicationSettingHelper.AddOrUpdateValue("ship1Active", Config.ship1Active);
            ApplicationSettingHelper.AddOrUpdateValue("ship2Active", Config.ship2Active);
            ApplicationSettingHelper.AddOrUpdateValue("ship3Active", Config.ship3Active);
            ApplicationSettingHelper.AddOrUpdateValue("ship2Locked", Config.ship2Locked);
            ApplicationSettingHelper.AddOrUpdateValue("ship3Locked", Config.ship3Locked);

            //Boosts
            ApplicationSettingHelper.AddOrUpdateValue("boostPrepared", Config.boostPrepared);
            ApplicationSettingHelper.AddOrUpdateValue("boostShieldsUp", Config.boostShieldsUp);
            ApplicationSettingHelper.AddOrUpdateValue("boostLastStand", Config.boostLastStand);

            //Powerups
            ApplicationSettingHelper.AddOrUpdateValue("activeUpgrades", Config.activeUpgrades);

            ApplicationSettingHelper.AddOrUpdateValue("bHealth5Locked", Config.bHealth5Locked);
            ApplicationSettingHelper.AddOrUpdateValue("bHealth10Locked", Config.bHealth10Locked);
            ApplicationSettingHelper.AddOrUpdateValue("bHealth15Locked", Config.bHealth15Locked);

            ApplicationSettingHelper.AddOrUpdateValue("bShields10Locked", Config.bShields10Locked);
            ApplicationSettingHelper.AddOrUpdateValue("bShields20Locked", Config.bShields20Locked);
            ApplicationSettingHelper.AddOrUpdateValue("bShields30Locked", Config.bShields30Locked);

            ApplicationSettingHelper.AddOrUpdateValue("bDamage1Locked", Config.bDamage1Locked);
            ApplicationSettingHelper.AddOrUpdateValue("bDamage2Locked", Config.bDamage2Locked);
            ApplicationSettingHelper.AddOrUpdateValue("bDamage4Locked", Config.bDamage4Locked);

            ApplicationSettingHelper.AddOrUpdateValue("bRange10Locked", Config.bRange10Locked);
            ApplicationSettingHelper.AddOrUpdateValue("bRange20Locked", Config.bRange20Locked);
            ApplicationSettingHelper.AddOrUpdateValue("bRange50Locked", Config.bRange50Locked);

            ApplicationSettingHelper.AddOrUpdateValue("bPowerup5Locked", Config.bPowerup5Locked);
            ApplicationSettingHelper.AddOrUpdateValue("bPowerup10Locked", Config.bPowerup10Locked);
            ApplicationSettingHelper.AddOrUpdateValue("bPowerup20Locked", Config.bPowerup20Locked);

            ApplicationSettingHelper.AddOrUpdateValue("bHealth5Active", Config.bHealth5Active);
            ApplicationSettingHelper.AddOrUpdateValue("bHealth10Active", Config.bHealth10Active);
            ApplicationSettingHelper.AddOrUpdateValue("bHealth15Active", Config.bHealth15Active);

            ApplicationSettingHelper.AddOrUpdateValue("bShields10Active", Config.bShields10Active);
            ApplicationSettingHelper.AddOrUpdateValue("bShields20Active", Config.bShields20Active);
            ApplicationSettingHelper.AddOrUpdateValue("bShields30Active", Config.bShields30Active);

            ApplicationSettingHelper.AddOrUpdateValue("bDamage1Active", Config.bDamage1Active);
            ApplicationSettingHelper.AddOrUpdateValue("bDamage2Active", Config.bDamage2Active);
            ApplicationSettingHelper.AddOrUpdateValue("bDamage4Active", Config.bDamage4Active);

            ApplicationSettingHelper.AddOrUpdateValue("bRange10Active", Config.bRange10Active);
            ApplicationSettingHelper.AddOrUpdateValue("bRange20Active", Config.bRange20Active);
            ApplicationSettingHelper.AddOrUpdateValue("bRange50Active", Config.bRange50Active);

            ApplicationSettingHelper.AddOrUpdateValue("bPowerup5Active", Config.bPowerup5Active);
            ApplicationSettingHelper.AddOrUpdateValue("bPowerup10Active", Config.bPowerup10Active);
            ApplicationSettingHelper.AddOrUpdateValue("bPowerup20Active", Config.bPowerup20Active);

            ApplicationSettingHelper.AddOrUpdateValue("Coins", Config.Coins);
        }

        // Code to execute if a navigation fails
        private void RootFrame_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            if (System.Diagnostics.Debugger.IsAttached)
            {
                // A navigation has failed; break into the debugger
                System.Diagnostics.Debugger.Break();
            }
        }

        // Code to execute on Unhandled Exceptions
        private void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            if (System.Diagnostics.Debugger.IsAttached)
            {
                // An unhandled exception has occurred; break into the debugger
                System.Diagnostics.Debugger.Break();
            }
        }

        #region Phone application initialization

        // Avoid double-initialization
        private bool phoneApplicationInitialized = false;

        // Do not add any additional code to this method
        private void InitializePhoneApplication()
        {
            if (phoneApplicationInitialized)
                return;

            // Create the frame but don't set it as RootVisual yet; this allows the splash
            // screen to remain active until the application is ready to render.
            RootFrame = new PhoneApplicationFrame();
            RootFrame.Navigated += CompleteInitializePhoneApplication;

            // Handle navigation failures
            RootFrame.NavigationFailed += RootFrame_NavigationFailed;

            // Ensure we don't initialize again
            phoneApplicationInitialized = true;
        }

        // Do not add any additional code to this method
        private void CompleteInitializePhoneApplication(object sender, NavigationEventArgs e)
        {
            // Set the root visual to allow the application to render
            if (RootVisual != RootFrame)
                RootVisual = RootFrame;

            // Remove this handler since it is no longer needed
            RootFrame.Navigated -= CompleteInitializePhoneApplication;
        }

        #endregion

        #region XNA application initialization

        // Performs initialization of the XNA types required for the application.
        private void InitializeXnaApplication()
        {
            // Create the service provider
            Services = new AppServiceProvider();

            // Add the SharedGraphicsDeviceManager to the Services as the IGraphicsDeviceService for the app
            foreach (object obj in ApplicationLifetimeObjects)
            {
                if (obj is IGraphicsDeviceService)
                    Services.AddService(typeof(IGraphicsDeviceService), obj);
            }

            // Create the ContentManager so the application can load precompiled assets
            Content = new ContentManager(Services, "Content");

            // Create a GameTimer to pump the XNA FrameworkDispatcher
            FrameworkDispatcherTimer = new GameTimer();
            FrameworkDispatcherTimer.FrameAction += FrameworkDispatcherFrameAction;
            FrameworkDispatcherTimer.Start();
        }

        // An event handler that pumps the FrameworkDispatcher each frame.
        // FrameworkDispatcher is required for a lot of the XNA events and
        // for certain functionality such as SoundEffect playback.
        private void FrameworkDispatcherFrameAction(object sender, EventArgs e)
        {
            FrameworkDispatcher.Update();
        }

        #region Music

        public static MediaElement GlobalMediaElement
        {
            get { return Current.Resources["GlobalMedia"] as MediaElement; }
        }

        //public static bool BackgroundMusicAllowed()
        //{
        //    bool allowed = true;
            

        //    //you can check a stored property here and return false if you want to disable all bgm

        //    if (!MediaPlayer.GameHasControl)
        //    {
        //        //ask user about background music
        //        MessageBoxResult mbr = MessageBox.Show("press ok if you'd like to use this app's background music (this will stop your current music playback)", "use app background music?", MessageBoxButton.OKCancel);
        //        if (mbr != MessageBoxResult.OK)
        //        {
        //            allowed = false;
        //        }
        //    }

        //    return allowed;
        //}

        public void TryPlayBackgroundMusic(int SongNumber)
        {
            if (CanPlayMusic && Config.MusicOn)
            {
                FrameworkDispatcher.Update();
                MediaPlayer.Stop();  //stop to clear any existing bg music

                switch (SongNumber)
                {
                    case 0:
                        GlobalMediaElement.Source = new Uri("BeforeTheStorm.mp3", UriKind.Relative);
                        break;
                    case 1:
                        GlobalMediaElement.Source = new Uri("ThroughTheStars.mp3", UriKind.Relative);
                        break;
                    case 2:
                        GlobalMediaElement.Source = new Uri("GalacticStruggle.mp3", UriKind.Relative);
                        break;
                    case 3:
                        //GlobalMediaElement.Source = new Uri("survival.mp3", UriKind.Relative);
                        GlobalMediaElement.Source = new Uri("Duplicate.mp3", UriKind.Relative);
                        break;
                    case 4:
                        //GlobalMediaElement.Source = new Uri("holo.mp3", UriKind.Relative);
                        GlobalMediaElement.Source = new Uri("Incineration.mp3", UriKind.Relative);
                        break;
                    case 5:
                        //GlobalMediaElement.Source = new Uri("giantrobotsfighting.mp3", UriKind.Relative);
                        GlobalMediaElement.Source = new Uri("WhereSpaceDoesThingsInStyle.mp3", UriKind.Relative);
                        break;
                    case 6:
                        GlobalMediaElement.Source = new Uri("AgainstTheOdds.mp3", UriKind.Relative);
                        break;
                    //default:
                    //    break;
                }

                GlobalMediaElement.MediaOpened += MediaElement_MediaOpened;  //wait until Media is ready before calling .Play()
            }
        }

        private void MediaElement_MediaOpened(object sender, RoutedEventArgs e)
        {
            App.GlobalMediaElement.Play();
        }

        private void MediaElement_MediaEnded(object sender, RoutedEventArgs e)
        {
            if (GlobalMediaElement.CurrentState != System.Windows.Media.MediaElementState.Playing)
            {
                //loop bg music
                GlobalMediaElement.Play();
            }
        }

        #endregion

        #endregion

        private void InitializeStyleChanges()
        {
            //(App.Current.Resources["PhoneBackgroundBrush"] as SolidColorBrush).Color = Colors.White;

            (App.Current.Resources["PhoneAccentBrush"] as SolidColorBrush).Color = Colors.Cyan;
            (App.Current.Resources["PhoneForegroundBrush"] as SolidColorBrush).Color = Colors.White;
            (App.Current.Resources["PhoneBackgroundBrush"] as SolidColorBrush).Color = Colors.Black;
            (App.Current.Resources["PhoneRadioCheckBoxBrush"] as SolidColorBrush).Color = Colors.LightGray;
        }
    }
}