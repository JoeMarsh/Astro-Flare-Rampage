using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;

namespace AstroFlare
{
    class ShipSelectScreen : SingleControlScreen
    {
        Texture2D background;

        //SpriteFont titleFont;
        SpriteFont buttonFont;
        SpriteFont buttonFontSmall;

        Texture2D Button;
        Texture2D RoundButton;
        Texture2D ship1;
        Texture2D ship2;
        Texture2D ship3;
        Sprite coin;

        String coinCap;

        Color buttonSelectedColor = Color.Black;

        //Texture2D shipsTexture;
        ButtonControl ship1Upgrade;
        ButtonControl ship2Upgrade;
        ButtonControl ship3Upgrade;

        ButtonControl ship1Mods;
        //ButtonControl ship2Mods;
        //ButtonControl ship3Mods;

        public override void Activate(bool instancePreserved)
        {
            EnabledGestures = GestureType.Tap;
            ContentManager content = ScreenManager.Game.Content;

            Button = content.Load<Texture2D>("GameScreens\\Buttons\\B_Transparent");
            RoundButton = content.Load<Texture2D>("GameScreens\\Buttons\\B_Round");
            background = content.Load<Texture2D>("Background\\stars6");
            buttonFont = content.Load<SpriteFont>("menufont");
            buttonFontSmall = content.Load<SpriteFont>("menufont12");

            ship1 = content.Load<Texture2D>("GameScreens\\ship1");
            ship2 = content.Load<Texture2D>("GameScreens\\ship2");
            ship3 = content.Load<Texture2D>("GameScreens\\ship3");

            if (Guide.IsTrialMode)
            {
                coinCap = "/5,000 - (10,000 in full game)";
                if (Config.Coins > 5000)
                    Config.Coins = 5000;
            }
            else
            {
                coinCap = "/10,000";
                if (Config.Coins > 10000)
                    Config.Coins = 10000;
            }

            coin = new Sprite(Config.CoinSpriteSheet);

            ParticleEffects.Initialize(ScreenManager.GraphicsDeviceManager, ScreenManager.GraphicsDevice);
            ParticleEffects.LoadContent(content);

            RootControl = new Control();

            if (Config.ship1Active)
                ship1Upgrade = new ButtonControl(Button, new Vector2(200, 60), "Active", buttonFont, "Ability: Heal 10 HP + 20 Shields\nPassive: Shield Powerups +25%", buttonFontSmall, true);
            else
                ship1Upgrade = new ButtonControl(Button, new Vector2(200, 60), "Select", buttonFont, "Ability: Heal 10 HP + 20 Shields\nPassive: Shield Powerups +25%", buttonFontSmall, true);
            ship1Upgrade.Tapped += new EventHandler<EventArgs>(ship1UpgradeButton_Tapped);

            if (Config.ship2Locked)
                ship2Upgrade = new ButtonControl(Button, new Vector2(200, 190), "Unlock - 10,000g", buttonFont, "Ability: Area Damage Attack\nPassive: Rapid Fire +20% duration", buttonFontSmall, true);
            else if (Config.ship2Active)
                ship2Upgrade = new ButtonControl(Button, new Vector2(200, 190), "Active", buttonFont, "Ability: Area Damage Attack\nPassive: Rapid Fire +20% duration", buttonFontSmall, true);
            else
                ship2Upgrade = new ButtonControl(Button, new Vector2(200, 190), "Select", buttonFont, "Ability: Area Damage Attack\nPassive: Rapid Fire +20% duration", buttonFontSmall, true);
            ship2Upgrade.Tapped += new EventHandler<EventArgs>(ship2UpgradeButton_Tapped);

            if (Config.ship3Locked)
                ship3Upgrade = new ButtonControl(Button, new Vector2(200, 320), "Unlock - 10,000g", buttonFont, "Ability: Immune + Pull all powerups\nPassive: Powerup attract range +25%", buttonFontSmall, true);
            else if (Config.ship3Active)
                ship3Upgrade = new ButtonControl(Button, new Vector2(200, 320), "Active", buttonFont, "Ability: Immune + Pull all powerups\nPassive: Powerup attract range +25%", buttonFontSmall, true);
            else
                ship3Upgrade = new ButtonControl(Button, new Vector2(200, 320), "Select", buttonFont, "Ability: Immune + Pull all powerups\nPassive: Powerup attract range +25%", buttonFontSmall, true);
            ship3Upgrade.Tapped += new EventHandler<EventArgs>(ship3UpgradeButton_Tapped);

            ship1Mods = new ButtonControl(RoundButton, new Vector2(650, 60), "Mods", buttonFont);
            ship1Mods.Tapped += new EventHandler<EventArgs>(ship1Mods_Tapped);

            //ship2Mods = new ButtonControl(RoundButton, new Vector2(650, 190), "Mods", buttonFont);
            //ship2Mods.Tapped += new EventHandler<EventArgs>(ship2Mods_Tapped);

            //ship3Mods = new ButtonControl(RoundButton, new Vector2(650, 320), "Mods", buttonFont);
            //ship3Mods.Tapped += new EventHandler<EventArgs>(ship3Mods_Tapped);

            RootControl.AddChild(ship1Upgrade);
            RootControl.AddChild(ship2Upgrade);
            RootControl.AddChild(ship3Upgrade);
            RootControl.AddChild(ship1Mods);
            //RootControl.AddChild(ship2Mods);
            //RootControl.AddChild(ship3Mods);

            base.Activate(instancePreserved);
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            ParticleEffects.Update(gameTime);
            //90, 220, 350
            ParticleEffects.TriggerMenuShipTrailGreen(new Vector2(125, 115));
            ParticleEffects.TriggerMenuShipTrailPurple(new Vector2(125, 245));
            ParticleEffects.TriggerMenuShipTrailBlue(new Vector2(125, 375));

            if (Config.ship1Active)
            {
                Config.ship2Active = false;
                Config.ship3Active = false;
                ship1Upgrade.Text = "Active";
                ship1Upgrade.Color = buttonSelectedColor;
            }
            else
            {
                ship1Upgrade.Text = "Select";
                ship1Upgrade.Color = Color.White;
            }

            if (Config.ship2Active)
            {
                Config.ship1Active = false;
                Config.ship3Active = false;
                ship2Upgrade.Text = "Active";
                ship2Upgrade.Color = buttonSelectedColor;
            }
            else if (!Config.ship2Locked)
            {
                ship2Upgrade.Text = "Select";
                ship2Upgrade.Color = Color.White;
            }

            if (Config.ship3Active)
            {
                Config.ship1Active = false;
                Config.ship2Active = false;
                ship3Upgrade.Text = "Active";
                ship3Upgrade.Color = buttonSelectedColor;
            }
            else if (!Config.ship3Locked)
            {
                ship3Upgrade.Text = "Select";
                ship3Upgrade.Color = Color.White;
            }
            coin.Update(gameTime);
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
        }

        public override void Draw(GameTime gameTime)
        {
            //ScreenManager.SpriteBatch.Begin();
            //ScreenManager.SpriteBatch.Draw(background, Vector2.Zero, Color.White);
            //ScreenManager.SpriteBatch.End();

            ParticleEffects.Draw();

            ScreenManager.SpriteBatch.Begin();
            ScreenManager.SpriteBatch.Draw(ship1, new Vector2(100, 90), Color.White);
            ScreenManager.SpriteBatch.Draw(ship2, new Vector2(100, 220), Color.White);
            ScreenManager.SpriteBatch.Draw(ship3, new Vector2(100, 350), Color.White);

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

            coin.Draw(ScreenManager.SpriteBatch, new Vector2(25, 20), 0f);
            ScreenManager.SpriteBatch.End();
            base.Draw(gameTime);
        }

        public override void Deactivate()
        {
            base.Deactivate();
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

        void ship1UpgradeButton_Tapped(object sender, EventArgs e)
        {
            if (!Config.ship1Active)
            {
                Config.ship1Active = true;
                Config.ship2Active = false;
                Config.ship3Active = false;
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
                if (Config.Coins >= 10000)
                {
                    Config.ship2Locked = false;
                    Config.Coins -= 10000;
                }

            }
            else if (!Config.ship2Active)
            {
                Config.ship1Active = false;
                Config.ship2Active = true;
                Config.ship3Active = false;
                Config.CurrentProjectile = Config.BulletSheetShip2Laser;
                Config.CurrentShipTop = Config.Ship2SpriteSheet;
                Config.CurrentShipBase = Config.Ship2SpriteSheetBase;
                Config.CurrentShipColor = Config.Ship2Color;
            }

            // When the "Play" button is tapped, we load the GameplayScreen
            //LoadingScreen.Load(ScreenManager, true, PlayerIndex.One, new GameplayScreen());
        }

        void ship3UpgradeButton_Tapped(object sender, EventArgs e)
        {
            if (Config.ship3Locked)
            {
                if (Config.Coins >= 10000)
                {
                    Config.ship3Locked = false;
                    Config.Coins -= 10000;
                }
            }
            else if (!Config.ship3Active)
            {
                Config.ship1Active = false;
                Config.ship2Active = false;
                Config.ship3Active = true;
                Config.CurrentProjectile = Config.BulletSheetShip3Laser;
                Config.CurrentShipTop = Config.Ship3SpriteSheet;
                Config.CurrentShipBase = Config.Ship3SpriteSheetBase;
                Config.CurrentShipColor = Config.Ship3Color;
            }

            // When the "Play" button is tapped, we load the GameplayScreen
            //LoadingScreen.Load(ScreenManager, true, PlayerIndex.One, new GameplayScreen());
        }
    }
}
