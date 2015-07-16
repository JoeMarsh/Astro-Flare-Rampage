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

namespace AstroFlare
{
    /// <summary>
    /// A basic pause screen for Windows Phone
    /// </summary>
    class PhoneScoreScreen : SingleControlScreen
    {
        ButtonControl playAgainButton;
        ButtonControl mainMenuButton;

        Texture2D background;
        SpriteFont buttonFont;
        SpriteFont buttonFontSmall;
        Texture2D Button;

        SpriteFont titleFont;
        SpriteFont scoreFont;
        int CoinCollect;
        //double timeBonus;
        //string timeBonusString;

        Sprite coin;
        int padding2 = 0;

        public PhoneScoreScreen()
            : base()
        {
            Config.Coins += Config.CoinsCollected;
            CoinCollect = Config.CoinsCollected;
            Config.CoinsCollected = 0;

            //if (Config.Level == LevelSelect.Two || Config.Level == LevelSelect.Four)
            //    timeBonus = 1 + ((300 - GameplayScreen.levelTime) / 300);

            switch (Config.Level)
            {
                case LevelSelect.One:
                    Config.RampageScores.Sort();
                    Config.RampageScores.Reverse();
                    if (GlobalSave.SaveDevice.IsReady)
                    {
                        // save a file asynchronously. this will trigger IsBusy to return true
                        // for the duration of the save process.
                        GlobalSave.SaveDevice.SaveAsync(
                            GlobalSave.containerName,
                            GlobalSave.fileName_rampage_scores,
                            stream =>
                            {
                                using (StreamWriter writer = new StreamWriter(stream))
                                {
                                    for (int i = 0; i < 6; i++)
                                    {
                                        writer.WriteLine(Config.RampageScores[i]);
                                    }
                                }
                            });
                    }
                    break;
                case LevelSelect.Two:
                    Config.RampageTimedScores.Sort();
                    Config.RampageTimedScores.Reverse();
                    if (GlobalSave.SaveDevice.IsReady)
                    {
                        // save a file asynchronously. this will trigger IsBusy to return true
                        // for the duration of the save process.
                        GlobalSave.SaveDevice.SaveAsync(
                            GlobalSave.containerName,
                            GlobalSave.fileName_rampagetimed_scores,
                            stream =>
                            {
                                using (StreamWriter writer = new StreamWriter(stream))
                                {
                                    for (int i = 0; i < 6; i++)
                                    {
                                        writer.WriteLine(Config.RampageTimedScores[i]);
                                    }
                                }
                            });
                    }
                    break;
                case LevelSelect.Three:
                    Config.AlterEgoScores.Sort();
                    Config.AlterEgoScores.Reverse();
                    if (GlobalSave.SaveDevice.IsReady)
                    {
                        // save a file asynchronously. this will trigger IsBusy to return true
                        // for the duration of the save process.
                        GlobalSave.SaveDevice.SaveAsync(
                            GlobalSave.containerName,
                            GlobalSave.fileName_alterego_scores,
                            stream =>
                            {
                                using (StreamWriter writer = new StreamWriter(stream))
                                {
                                    for (int i = 0; i < 6; i++)
                                    {
                                        writer.WriteLine(Config.AlterEgoScores[i]);
                                    }
                                }
                            });
                    }
                    break;
                case LevelSelect.Four:
                    Config.AlterEgoTimedScores.Sort();
                    Config.AlterEgoTimedScores.Reverse();
                    if (GlobalSave.SaveDevice.IsReady)
                    {
                        // save a file asynchronously. this will trigger IsBusy to return true
                        // for the duration of the save process.
                        GlobalSave.SaveDevice.SaveAsync(
                            GlobalSave.containerName,
                            GlobalSave.fileName_alteregotimed_scores,
                            stream =>
                            {
                                using (StreamWriter writer = new StreamWriter(stream))
                                {
                                    for (int i = 0; i < 6; i++)
                                    {
                                        writer.WriteLine(Config.AlterEgoTimedScores[i]);
                                    }
                                }
                            });
                    }
                    break;
                case LevelSelect.Five:
                    Config.TimeBanditScores.Sort();
                    Config.TimeBanditScores.Reverse();
                    if (GlobalSave.SaveDevice.IsReady)
                    {
                        // save a file asynchronously. this will trigger IsBusy to return true
                        // for the duration of the save process.
                        GlobalSave.SaveDevice.SaveAsync(
                            GlobalSave.containerName,
                            GlobalSave.fileName_timebandit_scores,
                            stream =>
                            {
                                using (StreamWriter writer = new StreamWriter(stream))
                                {
                                    for (int i = 0; i < 6; i++)
                                    {
                                        writer.WriteLine(Config.TimeBanditScores[i]);
                                    }
                                }
                            });
                    }
                    break;
                case LevelSelect.Six:
                    Config.ExterminationScores.Sort();
                    Config.ExterminationScores.Reverse();
                    if (GlobalSave.SaveDevice.IsReady)
                    {
                        // save a file asynchronously. this will trigger IsBusy to return true
                        // for the duration of the save process.
                        GlobalSave.SaveDevice.SaveAsync(
                            GlobalSave.containerName,
                            GlobalSave.fileName_extermination_scores,
                            stream =>
                            {
                                using (StreamWriter writer = new StreamWriter(stream))
                                {
                                    for (int i = 0; i < 6; i++)
                                    {
                                        writer.WriteLine(Config.ExterminationScores[i]);
                                    }
                                }
                            });
                    }
                    break;
                case LevelSelect.Practise:
                    break;
            }



            //if coins over 10k coins = 10k. 5k if trialmode.

            // make sure the device is ready
            if (GlobalSave.SaveDevice.IsReady)
            {
                // save a file asynchronously. this will trigger IsBusy to return true
                // for the duration of the save process.
                GlobalSave.SaveDevice.SaveAsync(
                    GlobalSave.containerName,
                    GlobalSave.fileName_coins,
                    stream =>
                    {
                        using (StreamWriter writer = new StreamWriter(stream))
                        {
                            writer.WriteLine(Config.Coins);
                        }
                    });
            }
        }

        public override void Activate(bool instancePreserved)
        {
            EnabledGestures = GestureType.Tap;
            ContentManager content = ScreenManager.Game.Content;

            background = content.Load<Texture2D>("GameScreens\\BackgroundPanel");
            Button = content.Load<Texture2D>("GameScreens\\Buttons\\B_TransparentMedium");
            buttonFont = content.Load<SpriteFont>("menufont");
            buttonFontSmall = content.Load<SpriteFont>("menufont12");

            titleFont = content.Load<SpriteFont>("Fonts\\Pericles");
            scoreFont = content.Load<SpriteFont>("Fonts\\Andy18");

            coin = new Sprite(Config.CoinSpriteSheet);

            mainMenuButton = new ButtonControl(Button, new Vector2(60, 350), "Main Menu", buttonFont);
            mainMenuButton.Tapped += new EventHandler<EventArgs>(exitButton_Tapped);

            playAgainButton = new ButtonControl(Button, new Vector2(440, 350), "Play Again", buttonFont);
            playAgainButton.Tapped += new EventHandler<EventArgs>(resumeButton_Tapped);

            RootControl = new Control();
            RootControl.AddChild(mainMenuButton);
            RootControl.AddChild(playAgainButton);

            base.Activate(instancePreserved);
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

            ScreenManager.SpriteBatch.Draw(background, Vector2.Zero, Color.White);
            coin.Draw(ScreenManager.SpriteBatch, new Vector2(50, 45), 0f);
            ScreenManager.SpriteBatch.DrawString(buttonFont, Config.Coins.ToString("#,#"), new Vector2(70, 30), Color.Gold);

            string score = Math.Round(Config.Score - Config.AIScore, 0).ToString("#,#");
            string enemies = Math.Round(Config.EmemiesKilled, 0).ToString("#,#");
            string coinsCollected = CoinCollect.ToString("#,#");

            //if (Config.Level == LevelSelect.Two || Config.Level == LevelSelect.Four)
            //        timeBonusString = Math.Round(timeBonus, 1).ToString();

            //////////////////////////////
            //if (Config.Level == LevelSelect.Two || Config.Level == LevelSelect.Four)
            //{
            //    padding2 = 50;
            //    ScreenManager.SpriteBatch.DrawString(titleFont, "Time Bonus:", new Vector2(200, 35 + padding2) + new Vector2(2, 2), Color.Black,
            //                0f, new Vector2(titleFont.MeasureString("Time Bonus: ").X / 2, titleFont.MeasureString("Time Bonus:").Y / 2), 1f, SpriteEffects.None, 1f);
            //}

            ScreenManager.SpriteBatch.DrawString(titleFont, "Score:", new Vector2(200, 110 + padding2) + new Vector2(2, 2), Color.Black,
                0f, new Vector2(titleFont.MeasureString("Score: ").X / 2, titleFont.MeasureString("Score:").Y / 2), 1f, SpriteEffects.None, 1f);


            ScreenManager.SpriteBatch.DrawString(titleFont, "Enemies Killed:", new Vector2(200, 185 + padding2) + new Vector2(2, 2), Color.Black,
                0f, new Vector2(titleFont.MeasureString("Enemies Killed:").X / 2, titleFont.MeasureString("Enemies Killed:").Y / 2), 1f, SpriteEffects.None, 1f);


            ScreenManager.SpriteBatch.DrawString(titleFont, "Coins Collected:", new Vector2(200, 260 + padding2) + new Vector2(2, 2), Color.Black,
                0f, new Vector2(titleFont.MeasureString("Coins Collected:").X / 2, titleFont.MeasureString("Coins Collected:").Y / 2), 1f, SpriteEffects.None, 1f);


            ScreenManager.SpriteBatch.DrawString(titleFont, "Top Local Scores:", new Vector2(600, 100 + padding2) + new Vector2(2, 2), Color.Black,
                0f, new Vector2(titleFont.MeasureString("Top Local Scores:").X / 2, titleFont.MeasureString("Top Local Scores:").Y / 2), 1f, SpriteEffects.None, 1f);


            ////////////////////////////

            //if (Config.Level == LevelSelect.Two || Config.Level == LevelSelect.Four)
            //{
            //    ScreenManager.SpriteBatch.DrawString(titleFont, "Time Bonus:", new Vector2(200, 35 + padding2), Color.Green,
            //        0f, new Vector2(titleFont.MeasureString("Time Bonus: ").X / 2, titleFont.MeasureString("Time Bonus:").Y / 2), 1f, SpriteEffects.None, 1f);

            //    ScreenManager.SpriteBatch.DrawString(scoreFont, timeBonusString + "x", new Vector2(200, 75 + padding2), Color.Green,
            //        0f, new Vector2(scoreFont.MeasureString(timeBonusString + "x").X / 2, scoreFont.MeasureString(timeBonusString + "x").Y / 2), 1f, SpriteEffects.None, 1f);
            //}

            ScreenManager.SpriteBatch.DrawString(titleFont, "Score:", new Vector2(200, 110 + padding2), Color.White,
                0f, new Vector2(titleFont.MeasureString("Score: ").X / 2, titleFont.MeasureString("Score:").Y / 2), 1f, SpriteEffects.None, 1f);

            ScreenManager.SpriteBatch.DrawString(scoreFont, score, new Vector2(200, 150 + padding2), Color.Green,
                0f, new Vector2(scoreFont.MeasureString(score).X / 2, scoreFont.MeasureString(score).Y / 2), 1f, SpriteEffects.None, 1f);


            ScreenManager.SpriteBatch.DrawString(titleFont, "Enemies Killed:", new Vector2(200, 185 + padding2), Color.White,
                0f, new Vector2(titleFont.MeasureString("Enemies Killed:").X / 2, titleFont.MeasureString("Enemies Killed:").Y / 2), 1f, SpriteEffects.None, 1f);

            ScreenManager.SpriteBatch.DrawString(scoreFont, enemies, new Vector2(200, 225 + padding2), Color.Green,
                0f, new Vector2(scoreFont.MeasureString(enemies).X / 2, scoreFont.MeasureString(enemies).Y / 2), 1f, SpriteEffects.None, 1f);


            ScreenManager.SpriteBatch.DrawString(titleFont, "Coins Collected:", new Vector2(200, 260 + padding2), Color.White,
                0f, new Vector2(titleFont.MeasureString("Coins Collected:").X / 2, titleFont.MeasureString("Coins Collected:").Y / 2), 1f, SpriteEffects.None, 1f);

            ScreenManager.SpriteBatch.DrawString(scoreFont, coinsCollected, new Vector2(200, 300 + padding2), Color.Gold,
                0f, new Vector2(scoreFont.MeasureString(coinsCollected).X / 2, scoreFont.MeasureString(coinsCollected).Y / 2), 1f, SpriteEffects.None, 1f);


            ScreenManager.SpriteBatch.DrawString(titleFont, "Top Local Scores:", new Vector2(600, 100 + padding2), Color.White,
                0f, new Vector2(titleFont.MeasureString("Top Local Scores:").X / 2, titleFont.MeasureString("Top Local Scores:").Y / 2), 1f, SpriteEffects.None, 1f);


            int padding = 0;

            for (int i = 0; i < 5; i++)
            {
                switch (Config.Level)
                {
                    case LevelSelect.One:
                        ScreenManager.SpriteBatch.DrawString(ScreenManager.Font, Config.RampageScores[i].ToString("#,#"), new Vector2(600, 140 + padding + padding2), Color.Green,
                        0f, new Vector2(ScreenManager.Font.MeasureString(Config.RampageScores[i].ToString("#,#")).X / 2, ScreenManager.Font.MeasureString(Config.RampageScores[i].ToString("#,#")).Y / 2), 1f, SpriteEffects.None, 1f);
                        padding += 40;
                        break;
                    case LevelSelect.Two:
                        ScreenManager.SpriteBatch.DrawString(ScreenManager.Font, Config.RampageTimedScores[i].ToString("#,#"), new Vector2(600, 140 + padding + padding2), Color.Green,
                        0f, new Vector2(ScreenManager.Font.MeasureString(Config.RampageTimedScores[i].ToString("#,#")).X / 2, ScreenManager.Font.MeasureString(Config.RampageTimedScores[i].ToString("#,#")).Y / 2), 1f, SpriteEffects.None, 1f);
                        padding += 40;
                        break;
                    case LevelSelect.Three:
                        ScreenManager.SpriteBatch.DrawString(ScreenManager.Font, Config.AlterEgoScores[i].ToString("#,#"), new Vector2(600, 140 + padding + padding2), Color.Green,
                        0f, new Vector2(ScreenManager.Font.MeasureString(Config.AlterEgoScores[i].ToString("#,#")).X / 2, ScreenManager.Font.MeasureString(Config.AlterEgoScores[i].ToString("#,#")).Y / 2), 1f, SpriteEffects.None, 1f);
                        padding += 40;
                        break;
                    case LevelSelect.Four:
                        ScreenManager.SpriteBatch.DrawString(ScreenManager.Font, Config.AlterEgoTimedScores[i].ToString("#,#"), new Vector2(600, 140 + padding + padding2), Color.Green,
                        0f, new Vector2(ScreenManager.Font.MeasureString(Config.AlterEgoTimedScores[i].ToString("#,#")).X / 2, ScreenManager.Font.MeasureString(Config.AlterEgoTimedScores[i].ToString("#,#")).Y / 2), 1f, SpriteEffects.None, 1f);
                        padding += 40;
                        break;
                    case LevelSelect.Five:
                        ScreenManager.SpriteBatch.DrawString(ScreenManager.Font, Config.TimeBanditScores[i].ToString("#,#"), new Vector2(600, 140 + padding + padding2), Color.Green,
                        0f, new Vector2(ScreenManager.Font.MeasureString(Config.TimeBanditScores[i].ToString("#,#")).X / 2, ScreenManager.Font.MeasureString(Config.TimeBanditScores[i].ToString("#,#")).Y / 2), 1f, SpriteEffects.None, 1f);
                        padding += 40;
                        break;
                    case LevelSelect.Six:
                        ScreenManager.SpriteBatch.DrawString(ScreenManager.Font, Config.ExterminationScores[i].ToString("#,#"), new Vector2(600, 140 + padding), Color.Green,
                        0f, new Vector2(ScreenManager.Font.MeasureString(Config.ExterminationScores[i].ToString("#,#")).X / 2, ScreenManager.Font.MeasureString(Config.ExterminationScores[i].ToString("#,#")).Y / 2), 1f, SpriteEffects.None, 1f);
                        padding += 40;
                        break;
                    case LevelSelect.Practise:
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
        void resumeButton_Tapped(object sender, EventArgs e)
        {
            ExitScreen();
            LoadingScreen.Load(ScreenManager, true, null,
                   new GameplayScreen());
        }

        /// <summary>
        /// The "Exit" button handler uses the LoadingScreen to take the user out to the main menu.
        /// </summary>
        void exitButton_Tapped(object sender, EventArgs e)
        {
            //LoadingScreen.Load(ScreenManager, false, null, new BackgroundScreen(),
            //                                               new PhoneMainMenuScreen());
            ExitScreen();
            LoadingScreen.Load(ScreenManager, false, null, new BackgroundScreen(),
                                               new MainMenuScreen());
        }

        public override void HandleInput(GameTime gameTime, InputState input)
        {

            PlayerIndex player;
            if (input.IsNewButtonPress(Buttons.Back, null, out player))
            {
                ExitScreen();
                LoadingScreen.Load(ScreenManager, false, null, new BackgroundScreen(), new MainMenuScreen());
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
