#region File Description
//-----------------------------------------------------------------------------
// PhoneMainMenuScreen.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

using System;
//using GameStateManagement;
using Microsoft.Xna.Framework;
using System.IO;
using Microsoft.Xna.Framework.Graphics;

namespace AstroFlare
{
    class OldUpgradesMenuScreen : PhoneMenuScreen
    {
        public OldUpgradesMenuScreen()
            : base("Sweet Mod Goodness", true)
        {
            // Create a button to start the game
            Button shieldsUpgrade = new Button("Shields + 10");
            shieldsUpgrade.Tapped += shieldsUpgradeButton_Tapped;
            MenuButtons.Add(shieldsUpgrade);
            //playButton.Position = new Vector2(400, 200);
            //shipUpgrades.Size = new Vector2(150, 25);
            shieldsUpgrade.BorderThickness = 1;
            shieldsUpgrade.BorderColor = Color.Green;
            shieldsUpgrade.FillColor = new Color(0, 0, 0, 100);

            Button healthUpgrade = new Button("Health + 5");
            healthUpgrade.Tapped += healthUpgradeButton_Tapped;
            MenuButtons.Add(healthUpgrade);
            healthUpgrade.BorderThickness = 1;
            healthUpgrade.BorderColor = Color.Green;
            healthUpgrade.FillColor = new Color(0, 0, 0, 100);

            // Create two buttons to toggle sound effects and music. This sample just shows one way
            // of making and using these buttons; it doesn't actually have sound effects or music
            //BooleanButton sfxButton = new BooleanButton("Drone", true);
            //sfxButton.Tapped += sfxButton_Tapped;
            //MenuButtons.Add(sfxButton);
            //sfxButton.Position = new Vector2(400, 350);

            ////BooleanButton musicButton = new BooleanButton("Music", true);
            ////musicButton.Tapped += musicButton_Tapped;
            ////MenuButtons.Add(musicButton);
        }

        void shieldsUpgradeButton_Tapped(object sender, EventArgs e)
        {
            if (Config.ShieldHealth < 230 && Config.Coins >= 1000)
            {
                Config.ShieldHealth += 10;
                Config.Coins -= 1000;
            }

            // When the "Play" button is tapped, we load the GameplayScreen
            //LoadingScreen.Load(ScreenManager, true, PlayerIndex.One, new GameplayScreen());
        }

        void healthUpgradeButton_Tapped(object sender, EventArgs e)
        {
            if (Config.ShipHealth < 115 && Config.Coins >= 1000)
            {
                Config.ShipHealth += 5;
                Config.Coins -= 1000;
            }

            // When the "Play" button is tapped, we load the GameplayScreen
            //LoadingScreen.Load(ScreenManager, true, PlayerIndex.One, new GameplayScreen());
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            ScreenManager.SpriteBatch.Begin();
            ScreenManager.SpriteBatch.DrawString(ScreenManager.Font, Config.Coins.ToString(), new Vector2(400, 440), Color.Yellow, 0f,
                new Vector2(ScreenManager.Font.MeasureString(Config.Coins.ToString()).X / 2, ScreenManager.Font.MeasureString(Config.Coins.ToString()).Y / 2), 1f, SpriteEffects.None, 1.0f);
            ScreenManager.SpriteBatch.End();
        }

        //void sfxButton_Tapped(object sender, EventArgs e)
        //{
        //    BooleanButton button = sender as BooleanButton;

        //    // In a real game, you'd want to store away the value of 
        //    // the button to turn off sounds here. :)
        //}

        //void musicButton_Tapped(object sender, EventArgs e)
        //{
        //    BooleanButton button = sender as BooleanButton;

        //    // In a real game, you'd want to store away the value of 
        //    // the button to turn off music here. :)
        //}

        public override void Unload()
        {
            // make sure the device is ready
            if (GlobalSave.SaveDevice.IsReady)
            {
                // save a file asynchronously. this will trigger IsBusy to return true
                // for the duration of the save process.
                GlobalSave.SaveDevice.SaveAsync(
                    GlobalSave.containerName,
                    GlobalSave.fileName_upgrades,
                    stream =>
                    {
                        using (StreamWriter writer = new StreamWriter(stream))
                        {
                            writer.WriteLine(Config.ShipHealth);
                            writer.WriteLine(Config.ShieldHealth);
                        }
                    });

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
            base.Unload();
        }

        protected override void OnCancel()
        {
            //ScreenManager.Game.Exit();
            ExitScreen();
            base.OnCancel();
        }
    }
}
