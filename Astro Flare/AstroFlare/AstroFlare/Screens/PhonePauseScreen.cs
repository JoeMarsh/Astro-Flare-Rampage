#region File Description
//-----------------------------------------------------------------------------
// PhonePauseScreen.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

using System;

namespace AstroFlare
{
    /// <summary>
    /// A basic pause screen for Windows Phone
    /// </summary>
    class PhonePauseScreen : PhoneMenuScreen
    {
        public PhonePauseScreen()
            : base("Paused", true)
        {
            // Create the "Resume" and "Exit" buttons for the screen

            Button resumeButton = new Button("Resume");
            resumeButton.Tapped += resumeButton_Tapped;
            MenuButtons.Add(resumeButton);

            Button exitButton = new Button("Exit");
            exitButton.Tapped += exitButton_Tapped;
            MenuButtons.Add(exitButton);          
        }

        /// <summary>
        /// The "Resume" button handler just calls the OnCancel method so that 
        /// pressing the "Resume" button is the same as pressing the hardware back button.
        /// </summary>
        void resumeButton_Tapped(object sender, EventArgs e)
        {
            OnCancel();
        }

        /// <summary>
        /// The "Exit" button handler uses the LoadingScreen to take the user out to the main menu.
        /// </summary>
        void exitButton_Tapped(object sender, EventArgs e)
        {
            //if (Config.Level == LevelSelect.Practise)
            //{
            //    LoadingScreen.Load(ScreenManager, false, null, new BackgroundScreen(),
            //                       new InstructionsScreen());
            //}
            //else
                LoadingScreen.Load(ScreenManager, false, null, new BackgroundScreen(),
                                                 new MainMenuScreen());
        }

        protected override void OnCancel()
        {
            ExitScreen();
            base.OnCancel();
        }
    }
}
