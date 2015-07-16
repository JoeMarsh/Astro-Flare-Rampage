#region File Description
//-----------------------------------------------------------------------------
// MainMenuScreen.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.GamerServices;
using System;
using System.Collections.Generic;


#endregion

namespace AstroFlare
{
    /// <summary>
    /// The main menu screen is the first thing displayed when the game starts up.
    /// </summary>
    class MainMenuScreen : MenuScreen
    {
        #region Initialization

        Song titleScreenmusic;

        //InputAction menuCancel;

        /// <summary>
        /// Constructor fills in the menu contents.
        /// </summary>
        public MainMenuScreen()
            : base("Astro Flare")
        {

            // Create our menu entries.
            MenuEntry playGameMenuEntry = new MenuEntry("Play Game");
            MenuEntry optionsMenuEntry = new MenuEntry("Options");
            MenuEntry leaderboardMenuEntry = new MenuEntry("Online Leaderboards");
            MenuEntry creditsMenuEntry = new MenuEntry("Credits");

            // Hook up menu event handlers.
            playGameMenuEntry.Selected += PlayGameMenuEntrySelected;
            optionsMenuEntry.Selected += OptionsMenuEntrySelected;
            leaderboardMenuEntry.Selected += LeaderBoardMenuEntrySelected;
            creditsMenuEntry.Selected += CreditsMenuEntrySelected;

            // Add entries to the menu.
            MenuEntries.Add(playGameMenuEntry);
            MenuEntries.Add(optionsMenuEntry);
            MenuEntries.Add(leaderboardMenuEntry);
            MenuEntries.Add(creditsMenuEntry);
        }

        #endregion

        public override void Activate(bool instancePreserved)
        {
            titleScreenmusic = ScreenManager.Game.Content.Load<Song>(@"Music\BeforeTheStorm");

            if (Config.MusicOn && MediaPlayer.GameHasControl)
                GameStateManagementGame.Instance.musicManager.Play(titleScreenmusic);

            base.Activate(instancePreserved);
        }

        #region Handle Input


        /// <summary>
        /// Event handler for when the Play Game menu entry is selected.
        /// </summary>
        void PlayGameMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            //LoadingScreen.Load(ScreenManager, true, e.PlayerIndex,
            //new LevelMenuScreen());

            //LoadingScreen.Load(ScreenManager, true, e.PlayerIndex,
            //       new GameplayScreen());

            ScreenManager.AddScreen(new LevelMenuScreen(), e.PlayerIndex);
        }


        /// <summary>
        /// Event handler for when the Options menu entry is selected.
        /// </summary>
        void OptionsMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.AddScreen(new OptionsMenuScreen(), e.PlayerIndex);
        }

        void LeaderBoardMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            //using this to fix the way back button is working when exiting XLiveStartupForm, need to find a less hackish method.
            ScreenManager.AddScreen(new LevelMenuScreen(), e.PlayerIndex);

            //this.ScreenManager.Enabled = false;
            //ScreenManager.XLiveStartupForm = new XLiveStartupForm2(ScreenManager.XLiveManager, ScreenManager);
            ////ScreenManager.XLiveStartupForm.Background = backgroundTexture;
            //ScreenManager.XLiveStartupForm.ExitButton = false;
            //ScreenManager.XLiveStartupForm.ContinueButton = false;
            //ScreenManager.XLiveStartupForm.NewgameButton = false;
            //ScreenManager.XLiveStartupForm.HelpButton = false;
            //ScreenManager.XLiveStartupForm.Show();

            OpenXLive.Forms.XLiveFormFactory.Factory.ShowForm("Lobby");
            //OpenXLive.Forms.XLiveFormFactory.Factory.ShowForm("Lobby");
        }

        void CreditsMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.AddScreen(new CreditsScreen(), e.PlayerIndex);
        }


         //<summary>
         //When the user cancels the main menu, we exit the game.
         //</summary>
        protected override void OnCancel(PlayerIndex playerIndex)
        {
            if (Guide.IsTrialMode)
            {
                List<String> mbList = new List<string>();
                mbList.Add("OK");
                mbList.Add("Cancel");
                Guide.BeginShowMessageBox("Purchase", "Thanks for playing! Would you like to purchase the game?", mbList, 0,
                                                MessageBoxIcon.None, PromptPurchase, null);
            }
            else
                ScreenManager.Game.Exit();
        }

        private void PromptPurchase(IAsyncResult ar)
        {
            // Complete the ShowMessageBox operation and get the index of the button that was clicked.
            int? result = Guide.EndShowMessageBox(ar);

            // Clicked "OK", so bring the user to the application's Marketplace page to buy the application.
            if (result.HasValue && result == 0)
            {
                Guide.ShowMarketplace(PlayerIndex.One);
            }

            if (result.HasValue && result == 1)
            {
                ScreenManager.Game.Exit();
            }
        }


        #endregion

    }
}
