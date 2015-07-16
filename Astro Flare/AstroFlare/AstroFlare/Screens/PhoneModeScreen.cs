#region File Description
//-----------------------------------------------------------------------------
// PhonePauseScreen.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

using System;
using Microsoft.Xna.Framework;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.GamerServices;
using System.Collections.Generic;

namespace AstroFlare
{
    /// <summary>
    /// A basic pause screen for Windows Phone
    /// </summary>
    class PhoneModeScreen : SingleControlScreen
    {
        ButtonControl beginButton;
        ButtonControl autoFireButton;

        Texture2D background;
        SpriteFont buttonFont;
        SpriteFont buttonFontSmall;
        Texture2D Button;

        SpriteFont titleFont;
        SpriteFont topScoreFont;
        SpriteFont scoreFont;
        SpriteFont descriptionFont16;
        SpriteFont descriptionFont18;

        Color buttonSelectedColor = Color.Gray;

        Sprite coin;
        String coinCap;

        public PhoneModeScreen()
            : base()
        {
        }

        public override void Activate(bool instancePreserved)
        {
            EnabledGestures = GestureType.Tap;
            ContentManager content = ScreenManager.Game.Content;

            background = content.Load<Texture2D>("GameScreens\\BackgroundPanel");
            Button = content.Load<Texture2D>("GameScreens\\Buttons\\B_TransparentMedium");
            buttonFont = content.Load<SpriteFont>("menufont");
            buttonFontSmall = content.Load<SpriteFont>("menufont12");

            titleFont = content.Load<SpriteFont>("Fonts\\Pericles22");
            topScoreFont = content.Load<SpriteFont>("Fonts\\Pericles");
            scoreFont = content.Load<SpriteFont>("Fonts\\Andy18");
            descriptionFont16 = content.Load<SpriteFont>("Fonts\\Andy16");
            descriptionFont18 = content.Load<SpriteFont>("Fonts\\Andy18");

            coin = new Sprite(Config.CoinSpriteSheet);

            autoFireButton = new ButtonControl(Button, new Vector2(60, 350), "Auto Aim/Fire:", buttonFont, string.Empty, buttonFont, false);
            autoFireButton.Tapped += new EventHandler<EventArgs>(autoFireButton_Tapped);

            beginButton = new ButtonControl(Button, new Vector2(440, 350), "Start!", buttonFont);
            beginButton.Tapped += new EventHandler<EventArgs>(beginButton_Tapped);

            SetMenuEntryText();

            RootControl = new Control();
            RootControl.AddChild(autoFireButton);
            RootControl.AddChild(beginButton);

            if (Guide.IsTrialMode)
                coinCap = "/5,000 - (10,000 in full version)";
            else
                coinCap = "/10,000";

            base.Activate(instancePreserved);
        }

        public override void Unload()
        {
            if (GlobalSave.SaveDevice.IsReady)
            {
                // save a file asynchronously. this will trigger IsBusy to return true
                // for the duration of the save process.
                GlobalSave.SaveDevice.SaveAsync(
                    GlobalSave.containerName,
                    GlobalSave.fileName_autofire,
                    stream =>
                    {
                        using (StreamWriter writer = new StreamWriter(stream))
                        {
                            writer.WriteLine(Config.AIControlled);
                        }
                    });
            }
            base.Unload();
        }

        /// <summary>
        /// Fills in the latest values for the options screen menu text.
        /// </summary>
        void SetMenuEntryText()
        {
            autoFireButton.Text2 = (Config.AIControlled ? "ON" : "OFF");

            if (Config.AIControlled)
            {
                autoFireButton.Color = buttonSelectedColor;
            }
            else
                autoFireButton.Color = Color.White;
        }

        //public override void Activate(bool instancePreserved)
        //{
        //    Leaderboard lb = new Leaderboard(ScreenManager.XLiveManager.CurrentSession, "4aa6d7ac-2cf0-44cc-8083-b397079db4af");
        //    lb.SubmitScore((float)(Math.Round(Config.Score - Config.AIScore)));
        //    lb.SubmitScoreCompleted += new AsyncEventHandler(lb_SubmitScoreCompleted);
        //    base.Activate(instancePreserved);
        //}

        //void lb_SubmitScoreCompleted(object sender, OpenXLive.AsyncEventArgs e)
        //{
        //    bool scoreSubmitted = true;
        //}

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            coin.Update(gameTime);
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            ScreenManager.SpriteBatch.Begin();

            //ScreenManager.SpriteBatch.Draw(background, Vector2.Zero, Color.White);
            coin.Draw(ScreenManager.SpriteBatch, new Vector2(20, 20), 0f);

            //ScreenManager.SpriteBatch.DrawString(buttonFont, Config.Coins.ToString("#,#"), new Vector2(70, 30), Color.Gold);

            if (Config.Coins == 0)
            {
                ScreenManager.SpriteBatch.DrawString(buttonFont, Config.Coins.ToString() + coinCap, new Vector2(50, 5), Color.Gold, 0f,
                    Vector2.Zero, 1f, SpriteEffects.None, 1.0f);
            }
            else
            {
                ScreenManager.SpriteBatch.DrawString(buttonFont, Config.Coins.ToString("#,#") + coinCap, new Vector2(50, 5), Color.Gold, 0f,
                    Vector2.Zero, 1f, SpriteEffects.None, 1.0f);
            }

            string score = Math.Round(Config.Score - Config.AIScore, 0).ToString("#,#");
            string enemies = Math.Round(Config.EmemiesKilled, 0).ToString("#,#");
            string coinsCollected = Config.CoinsCollected.ToString("#,#");

            ////////////////////////////


            //ScreenManager.SpriteBatch.DrawString(titleFont, "Enemies Killed:", new Vector2(200, 185) + new Vector2(2, 2), Color.Black,
            //    0f, new Vector2(titleFont.MeasureString("Enemies Killed:").X / 2, titleFont.MeasureString("Enemies Killed:").Y / 2), 1f, SpriteEffects.None, 1f);


            //ScreenManager.SpriteBatch.DrawString(titleFont, "Coins Collected:", new Vector2(200, 260) + new Vector2(2, 2), Color.Black,
            //    0f, new Vector2(titleFont.MeasureString("Coins Collected:").X / 2, titleFont.MeasureString("Coins Collected:").Y / 2), 1f, SpriteEffects.None, 1f);


            ScreenManager.SpriteBatch.DrawString(topScoreFont, "Top Local Scores:", new Vector2(600, 100) + new Vector2(2, 2), Color.Black,
                0f, new Vector2(topScoreFont.MeasureString("Top Local Scores:").X / 2, topScoreFont.MeasureString("Top Local Scores:").Y / 2), 1f, SpriteEffects.None, 1f);


            //////////////////////////////

            //ScreenManager.SpriteBatch.DrawString(scoreFont, score, new Vector2(200, 150), Color.Green,
            //    0f, new Vector2(scoreFont.MeasureString(score).X / 2, scoreFont.MeasureString(score).Y / 2), 1f, SpriteEffects.None, 1f);


            //ScreenManager.SpriteBatch.DrawString(titleFont, "Enemies Killed:", new Vector2(200, 185), Color.White,
            //    0f, new Vector2(titleFont.MeasureString("Enemies Killed:").X / 2, titleFont.MeasureString("Enemies Killed:").Y / 2), 1f, SpriteEffects.None, 1f);

            //ScreenManager.SpriteBatch.DrawString(scoreFont, enemies, new Vector2(200, 225), Color.Green,
            //    0f, new Vector2(scoreFont.MeasureString(enemies).X / 2, scoreFont.MeasureString(enemies).Y / 2), 1f, SpriteEffects.None, 1f);


            //ScreenManager.SpriteBatch.DrawString(titleFont, "Coins Collected:", new Vector2(200, 260), Color.White,
            //    0f, new Vector2(titleFont.MeasureString("Coins Collected:").X / 2, titleFont.MeasureString("Coins Collected:").Y / 2), 1f, SpriteEffects.None, 1f);

            //ScreenManager.SpriteBatch.DrawString(scoreFont, coinsCollected, new Vector2(200, 300), Color.Gold,
            //    0f, new Vector2(scoreFont.MeasureString(coinsCollected).X / 2, scoreFont.MeasureString(coinsCollected).Y / 2), 1f, SpriteEffects.None, 1f);


            ScreenManager.SpriteBatch.DrawString(topScoreFont, "Top Local Scores:", new Vector2(600, 100), Color.White,
                0f, new Vector2(topScoreFont.MeasureString("Top Local Scores:").X / 2, topScoreFont.MeasureString("Top Local Scores:").Y / 2), 1f, SpriteEffects.None, 1f);

            ScreenManager.SpriteBatch.DrawString(descriptionFont16, "Switch between tilt and thumbstick controls in the options menu.", new Vector2(400, 450) + new Vector2(2, 2), Color.Black,
                0f, descriptionFont16.MeasureString("Switch between tilt and thumbstick controls in the options menu.") / 2, 1f, SpriteEffects.None, 1f);
            ScreenManager.SpriteBatch.DrawString(descriptionFont16, "Switch between tilt and thumbstick controls in the options menu.", new Vector2(400, 450), Color.White,
                0f, descriptionFont16.MeasureString("Switch between tilt and thumbstick controls in the options menu.") / 2, 1f, SpriteEffects.None, 1f);



            int padding = 0;

            for (int i = 0; i < 5; i++)
            {
                switch (Config.Level)
                {
                    case LevelSelect.One:
                        ScreenManager.SpriteBatch.DrawString(ScreenManager.Font, Config.RampageScores[i].ToString("#,#"), new Vector2(600, 140 + padding), Color.Green,
                        0f, new Vector2(ScreenManager.Font.MeasureString(Config.RampageScores[i].ToString("#,#")).X / 2, ScreenManager.Font.MeasureString(Config.RampageScores[i].ToString("#,#")).Y / 2), 1f, SpriteEffects.None, 1f);
                        padding += 40;

                        ScreenManager.SpriteBatch.DrawString(titleFont, "Rampage", new Vector2(80, 100) + new Vector2(2, 2), Color.Black);
                            //0f, new Vector2(titleFont.MeasureString("Rampage").X / 2, titleFont.MeasureString("Rampage").Y / 2), 1f, SpriteEffects.None, 1f);

                        ScreenManager.SpriteBatch.DrawString(titleFont, "Rampage", new Vector2(80, 100), Color.White);
                            //0f, new Vector2(titleFont.MeasureString("Rampage").X / 2, titleFont.MeasureString("Rampage").Y / 2), 1f, SpriteEffects.None, 1f);

                        ScreenManager.SpriteBatch.DrawString(descriptionFont18, "Battle against ever increasing\nwaves of enemy ships.\n\nHow long can you survive?", new Vector2(80, 150) + new Vector2(2,2), Color.Black);
                        ScreenManager.SpriteBatch.DrawString(descriptionFont18, "Battle against ever increasing\nwaves of enemy ships.\n\nHow long can you survive?", new Vector2(80, 150), Color.Green);
                        break;
                    case LevelSelect.Two:
                        ScreenManager.SpriteBatch.DrawString(ScreenManager.Font, Config.RampageTimedScores[i].ToString("#,#"), new Vector2(600, 140 + padding), Color.Green,
                        0f, new Vector2(ScreenManager.Font.MeasureString(Config.RampageTimedScores[i].ToString("#,#")).X / 2, ScreenManager.Font.MeasureString(Config.RampageTimedScores[i].ToString("#,#")).Y / 2), 1f, SpriteEffects.None, 1f);
                        padding += 40;

                        ScreenManager.SpriteBatch.DrawString(titleFont, "Rampage: Timed", new Vector2(100, 110) + new Vector2(2, 2), Color.Black);
                            //0f, new Vector2(titleFont.MeasureString("Rampage - Timed").X / 2, titleFont.MeasureString("Rampage - Timed").Y / 2), 1f, SpriteEffects.None, 1f);

                        ScreenManager.SpriteBatch.DrawString(titleFont, "Rampage: Timed", new Vector2(100, 110), Color.White);
                            //0f, new Vector2(titleFont.MeasureString("Rampage - Timed").X / 2, titleFont.MeasureString("Rampage - Timed").Y / 2), 1f, SpriteEffects.None, 1f);

                        ScreenManager.SpriteBatch.DrawString(descriptionFont16, "Battle against ever increasing\nwaves of enemy ships.\n\nYou have 5 minutes to\ndestroy as many as you can.", new Vector2(100, 160) + new Vector2(2, 2), Color.Black);
                        ScreenManager.SpriteBatch.DrawString(descriptionFont16, "Battle against ever increasing\nwaves of enemy ships.\n\nYou have 5 minutes to\ndestroy as many as you can.", new Vector2(100, 160), Color.Green);
                        break;
                    case LevelSelect.Three:
                        ScreenManager.SpriteBatch.DrawString(ScreenManager.Font, Config.AlterEgoScores[i].ToString("#,#"), new Vector2(600, 140 + padding), Color.Green,
                        0f, new Vector2(ScreenManager.Font.MeasureString(Config.AlterEgoScores[i].ToString("#,#")).X / 2, ScreenManager.Font.MeasureString(Config.AlterEgoScores[i].ToString("#,#")).Y / 2), 1f, SpriteEffects.None, 1f);
                        padding += 40;

                        ScreenManager.SpriteBatch.DrawString(titleFont, "Alter Ego", new Vector2(100, 80) + new Vector2(2, 2), Color.Black);
                            //0f, new Vector2(titleFont.MeasureString("Alter Ego").X / 2, titleFont.MeasureString("Alter Ego").Y / 2), 1f, SpriteEffects.None, 1f);

                        ScreenManager.SpriteBatch.DrawString(titleFont, "Alter Ego", new Vector2(100, 80), Color.White);
                            //0f, new Vector2(titleFont.MeasureString("Alter Ego").X / 2, titleFont.MeasureString("Alter Ego").Y / 2), 1f, SpriteEffects.None, 1f);

                        ScreenManager.SpriteBatch.DrawString(descriptionFont16, "Battle an AI controlled clone\nof your ship who will\nfight you as well as collect/steal\npowerups and kill enemies for score\n\n(Your final score is determined\nby the difference between\nyour score and the AI).", new Vector2(100, 130) + new Vector2(2, 2), Color.Black);
                        ScreenManager.SpriteBatch.DrawString(descriptionFont16, "Battle an AI controlled clone\nof your ship who will\nfight you as well as collect/steal\npowerups and kill enemies for score\n\n(Your final score is determined\nby the difference between\nyour score and the AI).", new Vector2(100, 130), Color.Green);
                        break;
                    case LevelSelect.Four:
                        ScreenManager.SpriteBatch.DrawString(ScreenManager.Font, Config.AlterEgoTimedScores[i].ToString("#,#"), new Vector2(600, 140 + padding), Color.Green,
                        0f, new Vector2(ScreenManager.Font.MeasureString(Config.AlterEgoTimedScores[i].ToString("#,#")).X / 2, ScreenManager.Font.MeasureString(Config.AlterEgoTimedScores[i].ToString("#,#")).Y / 2), 1f, SpriteEffects.None, 1f);
                        padding += 40;

                        ScreenManager.SpriteBatch.DrawString(titleFont, "Alter Ego: Timed", new Vector2(100, 80) + new Vector2(2, 2), Color.Black);
                            //0f, new Vector2(titleFont.MeasureString("Alter Ego - Timed").X / 2, titleFont.MeasureString("Alter Ego - Timed").Y / 2), 1f, SpriteEffects.None, 1f);

                        ScreenManager.SpriteBatch.DrawString(titleFont, "Alter Ego: Timed", new Vector2(100, 80), Color.White);
                            //0f, new Vector2(titleFont.MeasureString("Alter Ego - Timed").X / 2, titleFont.MeasureString("Alter Ego - Timed").Y / 2), 1f, SpriteEffects.None, 1f);

                        ScreenManager.SpriteBatch.DrawString(descriptionFont16, "Battle an AI controlled clone\nof your ship who will\nfight you as well as collect/steal\npowerups and kill enemies for score\n\n(Your final score is determined\nby the difference between\nyour score and the AI).", new Vector2(100, 130) + new Vector2(2, 2), Color.Black);
                        ScreenManager.SpriteBatch.DrawString(descriptionFont16, "Battle an AI controlled clone\nof your ship who will\nfight you as well as collect/steal\npowerups and kill enemies for score\n\n(Your final score is determined\nby the difference between\nyour score and the AI).", new Vector2(100, 130), Color.Green);
                        break;
                    case LevelSelect.Five:
                        ScreenManager.SpriteBatch.DrawString(ScreenManager.Font, Config.TimeBanditScores[i].ToString("#,#"), new Vector2(600, 140 + padding), Color.Green,
                        0f, new Vector2(ScreenManager.Font.MeasureString(Config.TimeBanditScores[i].ToString("#,#")).X / 2, ScreenManager.Font.MeasureString(Config.TimeBanditScores[i].ToString("#,#")).Y / 2), 1f, SpriteEffects.None, 1f);
                        padding += 40;

                        ScreenManager.SpriteBatch.DrawString(titleFont, "Time Bandit", new Vector2(100, 100) + new Vector2(2, 2), Color.Black);
                            //0f, new Vector2(titleFont.MeasureString("Time Bandit").X / 2, titleFont.MeasureString("Time Bandit").Y / 2), 1f, SpriteEffects.None, 1f);

                        ScreenManager.SpriteBatch.DrawString(titleFont, "Time Bandit", new Vector2(100, 100), Color.White);
                            //0f, new Vector2(titleFont.MeasureString("Time Bandit").X / 2, titleFont.MeasureString("Time Bandit").Y / 2), 1f, SpriteEffects.None, 1f);

                        ScreenManager.SpriteBatch.DrawString(descriptionFont16, "Starts with a 60sec countdown.\n\nCollect time powerups\nthat will briefly slow time\naround you as well as\nincrease your countdown timer.", new Vector2(100, 150) + new Vector2(2,2), Color.Black);
                        ScreenManager.SpriteBatch.DrawString(descriptionFont16, "Starts with a 60sec countdown.\n\nCollect time powerups\nthat will briefly slow time\naround you as well as\nincrease your countdown timer.", new Vector2(100, 150), Color.Green);
                        break;
                    case LevelSelect.Six:
                        ScreenManager.SpriteBatch.DrawString(ScreenManager.Font, Config.ExterminationScores[i].ToString("#,#"), new Vector2(600, 140 + padding), Color.Green,
                        0f, new Vector2(ScreenManager.Font.MeasureString(Config.ExterminationScores[i].ToString("#,#")).X / 2, ScreenManager.Font.MeasureString(Config.ExterminationScores[i].ToString("#,#")).Y / 2), 1f, SpriteEffects.None, 1f);
                        padding += 40;

                        ScreenManager.SpriteBatch.DrawString(titleFont, "Extermination", new Vector2(90, 80) + new Vector2(2, 2), Color.Black);
                            //0f, new Vector2(titleFont.MeasureString("Extermination").X / 2, titleFont.MeasureString("Extermination").Y / 2), 1f, SpriteEffects.None, 1f);

                        ScreenManager.SpriteBatch.DrawString(titleFont, "Extermination", new Vector2(90, 80), Color.White);
                            //0f, new Vector2(titleFont.MeasureString("Extermination").X / 2, titleFont.MeasureString("Extermination").Y / 2), 1f, SpriteEffects.None, 1f);

                        ScreenManager.SpriteBatch.DrawString(descriptionFont16, "Pit yourself against a horde of\nalien space bugs that break down\nand split into additional enemies when killed.\n\n(Laser charges have been\nrecalibrated to be able to\nshoot down the smallest\nalien bugs)", new Vector2(90, 130) + new Vector2(2,2), Color.Black);
                        ScreenManager.SpriteBatch.DrawString(descriptionFont16, "Pit yourself against a horde of\nalien space bugs that break down\nand split into additional enemies when killed.\n\n(Laser charges have been\nrecalibrated to be able to\nshoot down the smallest\nalien bugs)", new Vector2(90, 130), Color.Green);
                        break;
                    case LevelSelect.Practise:
                        //ScreenManager.SpriteBatch.DrawString(ScreenManager.Font, Config.RampageScores[i].ToString("#,#"), new Vector2(600, 140 + padding), Color.Green,
                        //    0f, new Vector2(ScreenManager.Font.MeasureString(Config.RampageScores[i].ToString("#,#")).X / 2, ScreenManager.Font.MeasureString(Config.RampageScores[i].ToString("#,#")).Y / 2), 1f, SpriteEffects.None, 1f);
                        padding += 40;

                        ScreenManager.SpriteBatch.DrawString(titleFont, "Practise", new Vector2(100, 100) + new Vector2(2, 2), Color.Black);
                            //0f, new Vector2(titleFont.MeasureString("Rampage").X / 2, titleFont.MeasureString("Rampage").Y / 2), 1f, SpriteEffects.None, 1f);

                        ScreenManager.SpriteBatch.DrawString(titleFont, "Practise", new Vector2(100, 100), Color.White);
                            //0f, new Vector2(titleFont.MeasureString("Rampage").X / 2, titleFont.MeasureString("Rampage").Y / 2), 1f, SpriteEffects.None, 1f);

                        ScreenManager.SpriteBatch.DrawString(descriptionFont18, "In practise mode you will\nnot lose any health\n\nScore and coins collected\nare not saved", new Vector2(100, 150) + new Vector2(2,2), Color.Black);
                        ScreenManager.SpriteBatch.DrawString(descriptionFont18, "In practise mode you will\nnot lose any health\n\nScore and coins collected\nare not saved", new Vector2(100, 150), Color.Green);
                        break;
                }
            }

            ScreenManager.SpriteBatch.End();
            base.Draw(gameTime);
        }

        /// <summary>
        /// The "Resume" button handler just calls the OnCancel method so that 
        /// pressing the "Resume" button is the same as pressing the hardware back button.
        /// </summary>
        void beginButton_Tapped(object sender, EventArgs e)
        {
            //ExitScreen();
            if (Guide.IsTrialMode && Config.Level != LevelSelect.One && Config.Level != LevelSelect.Practise)
            {
                List<String> mbList = new List<string>();
                mbList.Add("OK");
                mbList.Add("Cancel");
                Guide.BeginShowMessageBox("Only Rampage mode is available in trial version", "Click OK to buy the game.", mbList, 0,
                                                MessageBoxIcon.None, PromptPurchase, null);
            }
            else
            {
                LoadingScreen.Load(ScreenManager, true, null,
                    new GameplayScreen());
            }
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
        }

        /// <summary>
        /// The "Exit" button handler uses the LoadingScreen to take the user out to the main menu.
        /// </summary>
        void autoFireButton_Tapped(object sender, EventArgs e)
        {
            //LoadingScreen.Load(ScreenManager, false, null, new BackgroundScreen(),
            //                                               new PhoneMainMenuScreen());
            Config.AIControlled = !Config.AIControlled;
            SetMenuEntryText();
        }

        public override void HandleInput(GameTime gameTime, InputState input)
        {

            PlayerIndex player;
            if (input.IsNewButtonPress(Buttons.Back, null, out player))
            {
                ExitScreen();
                //LoadingScreen.Load(ScreenManager, false, null, new BackgroundScreen(), new MainMenuScreen());
            }

            RootControl.HandleInput(input);
        }

        //protected override void OnCancel()
        //{
        //    //ExitScreen();
        //    LoadingScreen.Load(ScreenManager, false, null, new BackgroundScreen(),
        //                           new MainMenuScreen());
        //    base.OnCancel();
        //}
    }
}
