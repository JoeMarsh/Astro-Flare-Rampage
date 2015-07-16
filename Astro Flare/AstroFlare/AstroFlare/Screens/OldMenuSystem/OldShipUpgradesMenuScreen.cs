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
using Microsoft.Xna.Framework.Graphics;
using System.IO;

namespace AstroFlare
{
    class ShipUpgradesMenuScreen : PhoneMenuScreen
    {
        Texture2D shipsTexture;
        Button ship1Upgrade;
        Button ship2Upgrade;
        Button ship3Upgrade;

        Button ship1Mods;
        Button ship2Mods;
        Button ship3Mods;

        public ShipUpgradesMenuScreen()
            : base("", false)
        {
            if (Config.ship1Active)
                ship1Upgrade = new Button("Ship1 - Active");
            else
                ship1Upgrade = new Button("Ship1 - Select");
            ship1Upgrade.Tapped += ship1UpgradeButton_Tapped;
            MenuButtons.Add(ship1Upgrade);
            ship1Upgrade.Position = new Vector2(100, 75);
            ship1Upgrade.Size = new Vector2(500, 100);
            ship1Upgrade.BorderThickness = 1;
            ship1Upgrade.BorderColor = Color.Green;
            ship1Upgrade.FillColor = new Color(0, 0, 0, 200);
            ship1Upgrade.TextOffSet = new Vector2(60, 0);

            if (Config.ship2Locked)
                ship2Upgrade = new Button("Unlock - 2,000g");
            else if (Config.ship2Active)
                ship2Upgrade = new Button("Ship2 - Active");
            else
                ship2Upgrade = new Button("Ship2 - Select");
            ship2Upgrade.Tapped += ship2UpgradeButton_Tapped;
            MenuButtons.Add(ship2Upgrade);
            ship2Upgrade.Position = new Vector2(100, 200);
            ship2Upgrade.Size = new Vector2(500, 100);
            ship2Upgrade.BorderThickness = 1;
            ship2Upgrade.BorderColor = Color.Green;
            ship2Upgrade.FillColor = new Color(0, 0, 0, 200);
            ship2Upgrade.TextOffSet = new Vector2(60, 0);

            if (Config.ship3Locked)
                ship3Upgrade = new Button("Unlock - 5,000g");
            else if (Config.ship3Active)
                ship3Upgrade = new Button("Ship3 - Active");
            else
                ship3Upgrade = new Button("Ship3 - Select");
            ship3Upgrade.Tapped += ship3UpgradeButton_Tapped;
            MenuButtons.Add(ship3Upgrade);
            ship3Upgrade.Position = new Vector2(100, 325);
            ship3Upgrade.Size = new Vector2(500, 100);
            ship3Upgrade.BorderThickness = 1;
            ship3Upgrade.BorderColor = Color.Green;
            ship3Upgrade.FillColor = new Color(0, 0, 0, 200);
            ship3Upgrade.TextOffSet = new Vector2(60, 0);

            ship1Mods = new Button("Mods");
            ship1Mods.Tapped += new EventHandler<EventArgs>(ship1Mods_Tapped);
            ship1Mods.Position = new Vector2(610, 75);
            ship1Mods.Size = new Vector2(100, 100);
            ship1Mods.BorderThickness = 1;
            ship1Mods.BorderColor = Color.Green;
            ship1Mods.FillColor = new Color(0, 0, 0, 200);
            if (Config.ship1Active)
                MenuButtons.Add(ship1Mods);

            ship2Mods = new Button("Mods");
            ship2Mods.Tapped += new EventHandler<EventArgs>(ship2Mods_Tapped);
            ship2Mods.Position = new Vector2(610, 200);
            ship2Mods.Size = new Vector2(100, 100);
            ship2Mods.BorderThickness = 1;
            ship2Mods.BorderColor = Color.Green;
            ship2Mods.FillColor = new Color(0, 0, 0, 200);
            if (Config.ship2Active)
                MenuButtons.Add(ship2Mods);

            ship3Mods = new Button("Mods");
            ship3Mods.Tapped += new EventHandler<EventArgs>(ship3Mods_Tapped);
            ship3Mods.Position = new Vector2(610, 325);
            ship3Mods.Size = new Vector2(100, 100);
            ship3Mods.BorderThickness = 1;
            ship3Mods.BorderColor = Color.Green;
            ship3Mods.FillColor = new Color(0, 0, 0, 200);
            if (Config.ship3Active)
                MenuButtons.Add(ship3Mods);

            // individual ship upgrades button

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

        void ship3Mods_Tapped(object sender, EventArgs e)
        {
            ScreenManager.AddScreen(new UpgradesMenuScreen(), null);
        }

        void ship2Mods_Tapped(object sender, EventArgs e)
        {
            ScreenManager.AddScreen(new UpgradesMenuScreen(), null);
        }

        void ship1Mods_Tapped(object sender, EventArgs e)
        {
            ScreenManager.AddScreen(new UpgradesMenuScreen(), null);
        }

        public override void Activate(bool instancePreserved)
        {
            shipsTexture = ScreenManager.Game.Content.Load<Texture2D>(@"GameScreens\ShipSelection");
            base.Activate(instancePreserved);
        }

        void ship1UpgradeButton_Tapped(object sender, EventArgs e)
        {
            if (!Config.ship1Active)
            {
                Config.ship1Active = true;
                Config.ship2Active = false;
                Config.ship3Active = false;
                MenuButtons.Add(ship1Mods);
                MenuButtons.Remove(ship2Mods);
                MenuButtons.Remove(ship3Mods);
                Config.CurrentProjectile = Config.BulletSheetGreenLaser;
                Config.CurrentShipTop = Config.Ship1SpriteSheet;
                Config.CurrentShipBase = Config.Ship1SpriteSheetBase;
                Config.CurrentShipColor = Config.Ship1Color;
            }

            // When the "Play" button is tapped, we load the GameplayScreen
            //LoadingScreen.Load(ScreenManager, true, PlayerIndex.One, new GameplayScreen());
        }

        void ship2UpgradeButton_Tapped(object sender, EventArgs e)
        {
            if (Config.ship2Locked)
            {
                if (Config.Coins >= 2000)
                {
                    Config.ship2Locked = false;
                    Config.Coins -= 2000;
                }
                
            }
            else if (!Config.ship2Active)
            {
                Config.ship1Active = false;
                Config.ship2Active = true;
                Config.ship3Active = false;
                MenuButtons.Add(ship2Mods);
                MenuButtons.Remove(ship1Mods);
                MenuButtons.Remove(ship3Mods);
                Config.CurrentProjectile = Config.BulletSheetGreenLaser;
                Config.CurrentShipTop = Config.Ship2SpriteSheet;
                Config.CurrentShipBase = Config.Ship2SpriteSheetBase;
                Config.CurrentShipColor = Config.Ship1Color;
            }

            // When the "Play" button is tapped, we load the GameplayScreen
            //LoadingScreen.Load(ScreenManager, true, PlayerIndex.One, new GameplayScreen());
        }

        void ship3UpgradeButton_Tapped(object sender, EventArgs e)
        {
            if (Config.ship3Locked)
            {
                if (Config.Coins >= 5000)
                {
                    Config.ship3Locked = false;
                    Config.Coins -= 5000;
                }
            }
            else if (!Config.ship3Active)
            {
                Config.ship1Active = false;
                Config.ship2Active = false;
                Config.ship3Active = true;
                MenuButtons.Add(ship3Mods);
                MenuButtons.Remove(ship1Mods);
                MenuButtons.Remove(ship2Mods);
                Config.CurrentProjectile = Config.BulletSheetGreenLaser;
                Config.CurrentShipTop = Config.Ship3SpriteSheet;
                Config.CurrentShipBase = Config.Ship3SpriteSheetBase;
                Config.CurrentShipColor = Config.Ship1Color;
            }

            // When the "Play" button is tapped, we load the GameplayScreen
            //LoadingScreen.Load(ScreenManager, true, PlayerIndex.One, new GameplayScreen());
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

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            if (Config.ship1Active)
            {
                Config.ship2Active = false;
                Config.ship3Active = false;
                ship1Upgrade.FillColor = new Color(50, 50, 50, 50);
                ship2Upgrade.FillColor = new Color(0, 0, 0, 200);
                ship3Upgrade.FillColor = new Color(0, 0, 0, 200);
                ship1Upgrade.Text = "Ship1 - Active";
            }
            else
                ship1Upgrade.Text = "Ship1 - Select";

            if (Config.ship2Active)
            {
                Config.ship1Active = false;
                Config.ship3Active = false;
                ship1Upgrade.FillColor = new Color(0, 0, 0, 200);
                ship2Upgrade.FillColor = new Color(50, 50, 50, 50);
                ship3Upgrade.FillColor = new Color(0, 0, 0, 200);
                ship2Upgrade.Text = "Ship2 - Active";
            }
            else if (!Config.ship2Locked)
                ship2Upgrade.Text = "Ship2 - Select";

            if (Config.ship3Active)
            {
                Config.ship1Active = false;
                Config.ship2Active = false;
                ship1Upgrade.FillColor = new Color(0, 0, 0, 200);
                ship2Upgrade.FillColor = new Color(0, 0, 0, 200);
                ship3Upgrade.FillColor = new Color(50, 50, 50, 50);
                ship3Upgrade.Text = "Ship3 - Active";
            }
            else if (!Config.ship3Locked)
                ship3Upgrade.Text = "Ship3 - Select";
           
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
        }

        public override void Deactivate()
        {
            base.Deactivate();
        }

        protected override void OnCancel()
        {
            //ScreenManager.Game.Exit();
            ExitScreen();
            base.OnCancel();
        }

        public override void Unload()
        {
            // make sure the device is ready
            if (GlobalSave.SaveDevice.IsReady)
            {
                // save a file asynchronously. this will trigger IsBusy to return true
                // for the duration of the save process.
                GlobalSave.SaveDevice.SaveAsync(
                    GlobalSave.containerName,
                    GlobalSave.fileName_ships,
                    stream =>
                    {
                        using (StreamWriter writer = new StreamWriter(stream))
                        {
                            writer.WriteLine(Config.ship2Locked);
                            writer.WriteLine(Config.ship3Locked);

                            writer.WriteLine(Config.ship1Active);
                            writer.WriteLine(Config.ship2Active);
                            writer.WriteLine(Config.ship3Active);
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

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            ScreenManager.SpriteBatch.Begin();
            ScreenManager.SpriteBatch.Draw(shipsTexture, Vector2.Zero, Color.White);
            ScreenManager.SpriteBatch.DrawString(ScreenManager.Font, Config.Coins.ToString(), new Vector2(400, 440), Color.Yellow, 0f,
                new Vector2(ScreenManager.Font.MeasureString(Config.Coins.ToString()).X / 2, ScreenManager.Font.MeasureString(Config.Coins.ToString()).Y / 2), 1f, SpriteEffects.None, 1.0f);
            ScreenManager.SpriteBatch.End();
        }
    }
}
