#region File Description
//-----------------------------------------------------------------------------
// OptionsMenuScreen.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System.IO;
#endregion

namespace AstroFlare
{
    /// <summary>
    /// The options screen is brought up over the top of the main menu
    /// screen, and gives the user a chance to configure the game
    /// in various hopefully useful ways.
    /// </summary>
    class LevelMenuScreen : MenuScreen
    {
        #region Fields

        MenuEntry Play;
        MenuEntry Level;
        //MenuEntry Difficulty;
        MenuEntry Shop;
        MenuEntry Instructions;
        string[] modes;

        enum Ungulate
        {
            BactrianCamel,
            Dromedary,
            Llama,
        }

        //static Ungulate currentUngulate = Ungulate.Dromedary;

        static string[] difficulty = { "easy", "still easy", "WTF" };
        static int currentDifficulty = 0;

        //static bool frobnicate = true;
        //public int level = 0;

        #endregion

        #region Initialization


        /// <summary>
        /// Constructor.
        /// </summary>
        public LevelMenuScreen()
            : base("LevelSelect")
        {
            // Create our menu entries.
            Play = new MenuEntry(string.Empty);
            Level = new MenuEntry(string.Empty);
            //Difficulty = new MenuEntry(string.Empty);
            Shop = new MenuEntry(string.Empty);
            Instructions = new MenuEntry(string.Empty);

            // Hook up menu event handlers.
            Play.Selected += PlayMenuEntrySelected;
            Level.Selected += LevelMenuEntrySelected;
            //Difficulty.Selected += DifficultyMenuEntrySelected;
            Shop.Selected += UpgradeMenuEntrySelected;
            Instructions.Selected += InstructionsMenuEntrySelected;

            modes = new string[6];
            modes[0] = "Rampage";
            modes[1] = "Rampage - Timed";
            modes[2] = "Alter Ego";
            modes[3] = "Alter Ego - Timed";
            modes[4] = "Time Bandit";
            modes[5] = "Extermination";

            //if (GlobalSave.SaveDevice.FileExists(GlobalSave.containerName, GlobalSave.fileName_level))
            //{
            //    GlobalSave.SaveDevice.Load(
            //        GlobalSave.containerName,
            //        GlobalSave.fileName_level,
            //        stream =>
            //        {
            //            using (StreamReader reader = new StreamReader(stream))
            //            {
            //                level = int.Parse(reader.ReadLine());
            //            }
            //        });
            //}

            //Config.Level = (LevelSelect)level + 1;

            SetMenuEntryText();
            // Add entries to the menu.
            MenuEntries.Add(Play);
            MenuEntries.Add(Level);
            //MenuEntries.Add(Difficulty);
            MenuEntries.Add(Shop);
            menuEntries.Add(Instructions);

            Config.Level = (LevelSelect)Config.level + 1;
        }


        /// <summary>
        /// Fills in the latest values for the options screen menu text.
        /// </summary>
        void SetMenuEntryText()
        {
            Play.Text = "Start!";
            Level.Text = "Select Mode: " + modes[Config.level];
            //Difficulty.Text = "Difficulty: " + difficulty[currentDifficulty];
            Shop.Text = "Upgrades";
            Instructions.Text = "Instructions";
        }


        #endregion

        #region Handle Input


        /// <summary>
        /// Event handler for when the Ungulate menu entry is selected.
        /// </summary>
        void PlayMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            //LoadingScreen.Load(ScreenManager, true, null,
            //                   new GameplayScreen());
            if (Config.Level == LevelSelect.Practise)
                Config.Level = Config.tempLevel;

            ScreenManager.AddScreen(new PhoneModeScreen(), null);
        }


        /// <summary>
        /// Event handler for when the Language menu entry is selected.
        /// </summary>
        void LevelMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            Config.level = (Config.level + 1) % 6;
            SetMenuEntryText();
            Config.Level = (LevelSelect)Config.level + 1;
            BackgroundScreen.BackgroundTransition = true;
        }


        /// <summary>
        /// Event handler for when the Frobnicate menu entry is selected.
        /// </summary>
        void DifficultyMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            currentDifficulty = (currentDifficulty + 1) % difficulty.Length;

            SetMenuEntryText();
        }


        /// <summary>
        /// Event handler for when the Elf menu entry is selected.
        /// </summary>
        void UpgradeMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.AddScreen(new ShipSelectScreen(), null);
            //SetMenuEntryText();
        }

        void InstructionsMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.AddScreen(new InstructionsScreen(), null);
            //ScreenManager.AddScreen(new PhoneScoreScreen(), null);
            //SetMenuEntryText();
        }

        //protected override void UpdateMenuEntryLocations()
        //{
        //                // Make the menu slide into place during transitions, using a
        //    // power curve to make things look more interesting (this makes
        //    // the movement slow down as it nears the end).
        //    float transitionOffset = (float)Math.Pow(TransitionPosition, 2);

        //    // start at Y = 175; each X value is generated per entry
        //    Vector2 position = new Vector2(0f, 200f);

        //    // update each menu entry's location in turn
        //    for (int i = 0; i < menuEntries.Count; i++)
        //    {
        //        MenuEntry menuEntry = menuEntries[i];

        //        // each entry is to be centered horizontally
        //        position.X = ScreenManager.GraphicsDevice.Viewport.Width / 2 - menuEntry.GetWidth(this) / 2;

        //        if (ScreenState == ScreenState.TransitionOn)
        //            position.X -= transitionOffset * 256;
        //        else
        //            position.X += transitionOffset * 512;

        //        // set the entry's position
        //        menuEntry.Position = position;

        //        // move down for the next entry the size of this entry plus our padding
        //        position.Y += menuEntry.GetHeight(this) + (menuEntryPadding * 2);
        //    }
        //}

        public override void Unload()
        {
            //Save the player score
            // make sure the device is ready
            if (GlobalSave.SaveDevice.IsReady)
            {
                // save a file asynchronously. this will trigger IsBusy to return true
                // for the duration of the save process.
                GlobalSave.SaveDevice.SaveAsync(
                    GlobalSave.containerName,
                    GlobalSave.fileName_level,
                    stream =>
                    {
                        using (StreamWriter writer = new StreamWriter(stream))
                        {
                            writer.WriteLine(Config.level);
                        }
                    });
            }
            base.Unload();
        }

        #endregion
    }
}
