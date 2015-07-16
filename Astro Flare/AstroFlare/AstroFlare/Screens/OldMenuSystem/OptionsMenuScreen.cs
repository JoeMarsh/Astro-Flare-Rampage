#region File Description
//-----------------------------------------------------------------------------
// OptionsMenuScreen.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using Microsoft.Xna.Framework;
using System.IO;
using Microsoft.Xna.Framework.Media;
#endregion

namespace AstroFlare
{
    /// <summary>
    /// The options screen is brought up over the top of the main menu
    /// screen, and gives the user a chance to configure the game
    /// in various hopefully useful ways.
    /// </summary>
    class OptionsMenuScreen : MenuScreen
    {
        #region Fields

        Song titleScreenmusic;
        //MenuEntry ungulateMenuEntry;
        //MenuEntry languageMenuEntry;
        MenuEntry VibrateMenuEntry;
        MenuEntry MusicMenuEntry;
        MenuEntry SFXMenuEntry;
        MenuEntry ControlsEntry;
        MenuEntry ThumbsticksEntry;
        //MenuEntry elfMenuEntry;

        enum Ungulate
        {
            BactrianCamel,
            Dromedary,
            Llama,
        }

        //static Ungulate currentUngulate = Ungulate.Dromedary;

        static string[] languages = { "C#", "French", "Deoxyribonucleic acid" };
        //static int currentLanguage = 0;

        static bool Vibration = true;
        //static bool Music = true;
        //static bool SFX = true;

        //static int elf = 23;

        static string[] controls = { "Dynamic (Recommended)", "Static", "Fixed Position", "Tilt / Motion" };

        

        #endregion

        #region Initialization


        /// <summary>
        /// Constructor.
        /// </summary>
        public OptionsMenuScreen()
            : base("Options")
        {
            GameTitleEnabled = false;
            StartPosition = new Vector2(0f, 100f);
            // Create our menu entries.
            //ungulateMenuEntry = new MenuEntry(string.Empty);
            //languageMenuEntry = new MenuEntry(string.Empty);
            VibrateMenuEntry = new MenuEntry(string.Empty);
            MusicMenuEntry = new MenuEntry(string.Empty);
            SFXMenuEntry = new MenuEntry(string.Empty);
            ControlsEntry = new MenuEntry(string.Empty);
            ThumbsticksEntry = new MenuEntry(string.Empty);
            //elfMenuEntry = new MenuEntry(string.Empty);

            SetMenuEntryText();

            // Hook up menu event handlers.
            //ungulateMenuEntry.Selected += UngulateMenuEntrySelected;
            //languageMenuEntry.Selected += LanguageMenuEntrySelected;
            VibrateMenuEntry.Selected += VibrateMenuEntrySelected;
            MusicMenuEntry.Selected += MusicMenuEntrySelected;
            SFXMenuEntry.Selected += SFXMenuEntrySelected;
            ControlsEntry.Selected += ControlsEntrySelected;
            ThumbsticksEntry.Selected += ThumbsticksEntrySelected;
            //elfMenuEntry.Selected += ElfMenuEntrySelected;

            // Add entries to the menu.
            //MenuEntries.Add(ungulateMenuEntry);
            //MenuEntries.Add(languageMenuEntry);
            MenuEntries.Add(SFXMenuEntry);
            MenuEntries.Add(MusicMenuEntry);
            MenuEntries.Add(VibrateMenuEntry);
            MenuEntries.Add(ControlsEntry);
            MenuEntries.Add(ThumbsticksEntry);
            //MenuEntries.Add(elfMenuEntry);
        }

        public override void Activate(bool instancePreserved)
        {
            titleScreenmusic = ScreenManager.Game.Content.Load<Song>(@"Music\BeforeTheStorm");

            base.Activate(instancePreserved);
        }
        /// <summary>
        /// Fills in the latest values for the options screen menu text.
        /// </summary>
        void SetMenuEntryText()
        {
            //ungulateMenuEntry.Text = "Preferred ungulate: " + currentUngulate;
            //languageMenuEntry.Text = "Language: " + languages[currentLanguage];
            VibrateMenuEntry.Text = "Vibration: " + (Config.Vibrate ? "on" : "off");
            MusicMenuEntry.Text = "Music: " + (Config.MusicOn ? "on" : "off");
            SFXMenuEntry.Text = "SFX: " + (Config.SoundFXOn ? "on" : "off");
            ControlsEntry.Text = "Controls: " + controls[Config.ControlOption];
            if (Config.ControlOption == 3)
                AccelerometerInput.Instance.Enabled = true;
            else
                AccelerometerInput.Instance.Enabled = false;
            ThumbsticksEntry.Text = "Show Thumbsticks: " + (Config.ThumbsticksOn ? "on" : "off");
            //elfMenuEntry.Text = "elf: " + elf;
        }


        #endregion

        #region Handle Input

        void VibrateMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            Vibration = !Vibration;

            Config.Vibrate = !Config.Vibrate;

            SetMenuEntryText();
        }

        void MusicMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            Config.MusicOn = !Config.MusicOn;
            GameStateManagementGame.Instance.musicManager.Enabled = !GameStateManagementGame.Instance.musicManager.Enabled;

            if (!Config.MusicOn)
                GameStateManagementGame.Instance.musicManager.Stop();
            else
                GameStateManagementGame.Instance.musicManager.Play(titleScreenmusic);
            
            SetMenuEntryText();
        }

        void SFXMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            Config.SoundFXOn = !Config.SoundFXOn;
            SetMenuEntryText();
        }

        void ControlsEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            Config.ControlOption = (Config.ControlOption + 1) % 4;
            SetMenuEntryText();
        }

        void ThumbsticksEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            Config.ThumbsticksOn = !Config.ThumbsticksOn;
            SetMenuEntryText();
        }

        public override void Unload()
        {
            if (GlobalSave.SaveDevice.IsReady)
            {
                // save a file asynchronously. this will trigger IsBusy to return true
                // for the duration of the save process.
                GlobalSave.SaveDevice.SaveAsync(
                    GlobalSave.containerName,
                    GlobalSave.fileName_options,
                    stream =>
                    {
                        using (StreamWriter writer = new StreamWriter(stream))
                        {
                            writer.WriteLine(Config.Vibrate);
                            writer.WriteLine(Config.MusicOn);
                            writer.WriteLine(Config.SoundFXOn);
                            writer.WriteLine(Config.ControlOption);
                            writer.WriteLine(Config.ThumbsticksOn);
                        }
                    });
            }
            base.Unload();
        }



        ///// <summary>
        ///// Event handler for when the Ungulate menu entry is selected.
        ///// </summary>
        //void UngulateMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        //{
        //    currentUngulate++;

        //    if (currentUngulate > Ungulate.Llama)
        //        currentUngulate = 0;

        //    SetMenuEntryText();
        //}


        ///// <summary>
        ///// Event handler for when the Language menu entry is selected.
        ///// </summary>
        //void LanguageMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        //{
        //    currentLanguage = (currentLanguage + 1) % languages.Length;

        //    SetMenuEntryText();
        //}


        ///// <summary>
        ///// Event handler for when the Elf menu entry is selected.
        ///// </summary>
        //void ElfMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        //{
        //    elf++;

        //    SetMenuEntryText();
        //}


        #endregion
    }
}
