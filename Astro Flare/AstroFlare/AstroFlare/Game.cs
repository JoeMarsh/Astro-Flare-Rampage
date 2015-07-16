#region File Description
//-----------------------------------------------------------------------------
// Game.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Media;
using System.IO;
using System.Windows;
using System.Collections.Generic;


#endregion

namespace AstroFlare
{
    /// <summary>
    /// Sample showing how to manage different game states, with transitions
    /// between menu screens, a loading screen, the game itself, and a pause
    /// menu. This main game class is extremely simple: all the interesting
    /// stuff happens in the ScreenManager component.
    /// </summary>
    public class GameStateManagementGame : Microsoft.Xna.Framework.Game
    {
        #region Fields

        GraphicsDeviceManager graphics;
        ScreenManager screenManager;
        ScreenFactory screenFactory;

        // A simple component to help us manage background music
        public MusicManager musicManager;
        public SoundEffectManager soundManager;

        public static GameStateManagementGame Instance;


        #endregion

        #region Initialization

        /// <summary>
        /// The main game constructor.
        /// </summary>
        public GameStateManagementGame()
        {
            Application.Current.UnhandledException += (s, e) =>
            {
                if (!System.Diagnostics.Debugger.IsAttached)
                {
                    try
                    {
                        LittleWatson.ReportException(e.ExceptionObject, GetType().Assembly.FullName);
                    }
                    catch
                    {
                        // We do not want to throw exceptions in our exception handler  
                    }
                }
            };
            LittleWatson.CheckForPreviousException();

            Content.RootDirectory = "Content";

            //// test if any issues with different language settings
            //System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            //System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("pl-PL");

            //Set up save system
            GlobalSave.Initialize();
            Config.Initialize();
            Config.loadSavedData();

            Instance = this;

            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = true;
            TargetElapsedTime = TimeSpan.FromTicks(333333);

            // you can choose whether you want a landscape or portait
            // game by using one of the two helper functions.
            InitializeLandscapeGraphics();
            // InitializeLandscapeGraphics();

            // Create the screen factory and add it to the Services
            screenFactory = new ScreenFactory();
            Services.AddService(typeof(IScreenFactory), screenFactory);
            // Create the screen manager component.
            screenManager = new ScreenManager(this, graphics);
            Components.Add(screenManager);


            //// Load and display FPS counter
            //dgcFPS FPSComponent = new dgcFPS(this, @"Fonts\Tahoma14");
            //this.Components.Add(FPSComponent);

            // Create our music manager component and add it to the game.
            musicManager = new MusicManager(this);
            musicManager.PromptGameHasControl += MusicManagerPromptGameHasControl;
            musicManager.PlaybackFailed += MusicManagerPlaybackFailed;
            Components.Add(musicManager);

            soundManager = new SoundEffectManager(this);
            Components.Add(soundManager);

            this.Components.Add(new AccelerometerInput(this));

            if (Config.ControlOption == 3)
                AccelerometerInput.Instance.Enabled = true;
            else
                AccelerometerInput.Instance.Enabled = false;
////////////
            IsFixedTimeStep = true;
////////////
            //FPSComponent.ShowFPS = false;

//#if DEBUG
//            Guide.SimulateTrialMode = true;
//#endif


            soundManager.LoadSound("Shot", @"SoundEffects\Shot2");
            //Instance.soundManager.LoadSound("coin", @"SoundEffects\coin");
            //Instance.soundManager.LoadSound("ShipSpawn", @"SoundEffects\ShipSpawn");
            Instance.soundManager.LoadSound("ShipExplode", @"SoundEffects\Explosions\explodeice3");

            //Instance.soundManager.LoadSound("HardKick", @"SoundEffects\Explosions\HardKick");
            //Instance.soundManager.LoadSound("Kick1", @"SoundEffects\Explosions\Kick1");
            //Instance.soundManager.LoadSound("Kick2", @"SoundEffects\Explosions\Kick2");
            //Instance.soundManager.LoadSound("Kick3", @"SoundEffects\Explosions\Kick3");
            //Instance.soundManager.LoadSound("Kick4", @"SoundEffects\Explosions\Kick4");
            //Instance.soundManager.LoadSound("Kick5", @"SoundEffects\Explosions\Kick5");
            //Instance.soundManager.LoadSound("Kick6", @"SoundEffects\Explosions\Kick6");

            //Instance.soundManager.LoadSound("hat10", @"SoundEffects\Powerup\hat10");
            //Instance.soundManager.LoadSound("hat2", @"SoundEffects\Powerup\hat2");
            Instance.soundManager.LoadSound("FX1", @"SoundEffects\Powerup\FX1");
            //Instance.soundManager.LoadSound("Coin", @"SoundEffects\Powerup\item_pickup");

#if WINDOWS_PHONE
            // Hook events on the PhoneApplicationService so we're notified of the application's life cycle
            Microsoft.Phone.Shell.PhoneApplicationService.Current.Launching +=
                new EventHandler<Microsoft.Phone.Shell.LaunchingEventArgs>(GameLaunching);
            Microsoft.Phone.Shell.PhoneApplicationService.Current.Activated +=
                new EventHandler<Microsoft.Phone.Shell.ActivatedEventArgs>(GameActivated);
            Microsoft.Phone.Shell.PhoneApplicationService.Current.Deactivated +=
                new EventHandler<Microsoft.Phone.Shell.DeactivatedEventArgs>(GameDeactivated);
#else
            // On Windows and Xbox we just add the initial screens
            AddInitialScreens();
#endif
        }

        private void AddInitialScreens()
        {
            // Activate the first screens.
            screenManager.AddScreen(new BackgroundScreen(), null);

            // We have different menus for Windows Phone to take advantage of the touch interface
#if WINDOWS_PHONE
            //screenManager.AddScreen(new PhoneMainMenuScreen(), null);
            screenManager.AddScreen(new MainMenuScreen(), null);
#else
            screenManager.AddScreen(new MainMenuScreen(), null);
#endif
        }


        #endregion

        #region Draw

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.Black);

            // The real drawing happens inside the screen manager component.
            base.Draw(gameTime);
        }

        #endregion

#if WINDOWS_PHONE
        /// <summary>
        /// Helper method to the initialize the game to be a portrait game.
        /// </summary>
        private void InitializePortraitGraphics()
        {
            graphics.PreferredBackBufferWidth = 480;
            graphics.PreferredBackBufferHeight = 800;
        }

        /// <summary>
        /// Helper method to initialize the game to be a landscape game.
        /// </summary>
        private void InitializeLandscapeGraphics()
        {
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 480;
        }

        void GameLaunching(object sender, Microsoft.Phone.Shell.LaunchingEventArgs e)
        {
            AddInitialScreens();
        }

        void GameActivated(object sender, Microsoft.Phone.Shell.ActivatedEventArgs e)
        {
            // Try to deserialize the screen manager
            //if (!screenManager.Activate(e.IsApplicationInstancePreserved))
            //{
            // If the screen manager fails to deserialize, add the initial screens
            AddInitialScreens();
            //}
        }

        void GameDeactivated(object sender, Microsoft.Phone.Shell.DeactivatedEventArgs e)
        {
            // Serialize the screen manager when the game deactivated
            screenManager.Deactivate();
        }
#endif


        #region Music Manager Event Handlers

        /// <summary>
        /// Invoked if the user is listening to music when we tell the music manager to play our song.
        /// We can respond by prompting the user to turn off their music, which will cause our game music
        /// to start playing.
        /// </summary>
        private void MusicManagerPromptGameHasControl(object sender, EventArgs e)
        {
            // Show a message box to see if the user wants to turn off their music for the game's music.
            Guide.BeginShowMessageBox(
                "Use game music?",
                "Would you like to turn off your music to listen to the game's music?",
                new[] { "Yes", "No" },
                0,
                MessageBoxIcon.None,
                result =>
                {
                    // Get the choice from the result
                    int? choice = Guide.EndShowMessageBox(result);

                    // If the user hit the yes button, stop the media player. Our music manager will
                    // see that we have a song the game wants to play and that the game will now have control
                    // and will automatically start playing our game song.
                    if (choice.HasValue && choice.Value == 0)
                        MediaPlayer.Stop();
                    //else if (choice.HasValue && choice.Value == 1)
                    //    musicManager.Stop();
                },
                null);
        }

        /// <summary>
        /// Invoked if music playback fails. The most likely case for this is that the Phone is connected to a PC
        /// that has Zune open, such as while debugging. Most games can probably just ignore this event, but we 
        /// can prompt the user so that they know why we're not playing any music.
        /// </summary>
        private void MusicManagerPlaybackFailed(object sender, EventArgs e)
        {
            //// We're going to show a message box so the user knows why music didn't start playing.
            //Guide.BeginShowMessageBox(
            //    "Music playback failed",
            //    "Music playback cannot begin if the phone is connected to a PC running Zune.",
            //    new[] { "Ok" },
            //    0,
            //    MessageBoxIcon.None,
            //    null,
            //    null);
        }

        #endregion

    }

    

    //#region Entry Point

    ///// <summary>
    ///// The main entry point for the application.
    ///// </summary>
    //static class Program
    //{
    //    static void Main()
    //    {
    //        using (GameStateManagementGame game = new GameStateManagementGame())
    //        {
    //            game.Run();
    //        }
    //    }
    //}

    //#endregion

    

}
