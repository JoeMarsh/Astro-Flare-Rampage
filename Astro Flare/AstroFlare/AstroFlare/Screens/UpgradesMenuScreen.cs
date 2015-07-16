using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using System;

namespace AstroFlare
{
    class UpgradesMenuScreen : SingleControlScreen
    {
        //Texture2D background;
        SpriteFont buttonFont;
        Texture2D scrollIndicator;
        Sprite coin;

        String coinCap;

        public override void Activate(bool instancePreserved)
        {
            EnabledGestures = GestureType.Flick | GestureType.VerticalDrag | GestureType.DragComplete | GestureType.Tap;
            ContentManager content = ScreenManager.Game.Content;

            //background = content.Load<Texture2D>("GameScreens\\UpgradeMenu2");
            buttonFont = content.Load<SpriteFont>("menufont");
            scrollIndicator = content.Load<Texture2D>("GameScreens\\Scrollindicator");
            coin = new Sprite(Config.CoinSpriteSheet);

            if (Guide.IsTrialMode)
                coinCap = "/5,000 - (10,000 in full version)";
            else
                coinCap = "/10,000";

            RootControl = new UpgradePanel(content);
            base.Activate(instancePreserved);
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            coin.Update(gameTime);
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
        }

        public override void Draw(GameTime gameTime)
        {

            ScreenManager.SpriteBatch.Begin();
            //ScreenManager.SpriteBatch.Draw(background, Vector2.Zero, Color.White);
            ScreenManager.SpriteBatch.DrawString(buttonFont, "Equipped: " + Config.activeUpgrades.ToString() + "/5", new Vector2(580, 5), Color.White);

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
            
            ScreenManager.SpriteBatch.Draw(scrollIndicator, new Vector2(760, 415), Color.White);
            coin.Draw(ScreenManager.SpriteBatch, new Vector2(25, 20), 0f);
            ScreenManager.SpriteBatch.End();
            base.Draw(gameTime);
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
                    GlobalSave.fileName_upgrades,
                    stream =>
                    {
                        using (StreamWriter writer = new StreamWriter(stream))
                        {
                            writer.WriteLine(Config.activeUpgrades);
        
                            writer.WriteLine(Config.bHealth5Locked);
                            writer.WriteLine(Config.bHealth10Locked);
                            writer.WriteLine(Config.bHealth15Locked);
                            writer.WriteLine(Config.bShields10Locked);
                            writer.WriteLine(Config.bShields20Locked);
                            writer.WriteLine(Config.bShields30Locked);
                            writer.WriteLine(Config.bDamage1Locked);
                            writer.WriteLine(Config.bDamage2Locked);
                            writer.WriteLine(Config.bDamage4Locked);
                            writer.WriteLine(Config.bRange10Locked);
                            writer.WriteLine(Config.bRange20Locked);
                            writer.WriteLine(Config.bRange50Locked);
                            writer.WriteLine(Config.bPowerup5Locked);
                            writer.WriteLine(Config.bPowerup10Locked);
                            writer.WriteLine(Config.bPowerup20Locked);

                            writer.WriteLine(Config.bHealth5Active);
                            writer.WriteLine(Config.bHealth10Active);
                            writer.WriteLine(Config.bHealth15Active);
                            writer.WriteLine(Config.bShields10Active);
                            writer.WriteLine(Config.bShields20Active);
                            writer.WriteLine(Config.bShields30Active);
                            writer.WriteLine(Config.bDamage1Active);
                            writer.WriteLine(Config.bDamage2Active);
                            writer.WriteLine(Config.bDamage4Active);
                            writer.WriteLine(Config.bRange10Active);
                            writer.WriteLine(Config.bRange20Active);
                            writer.WriteLine(Config.bRange50Active);
                            writer.WriteLine(Config.bPowerup5Active);
                            writer.WriteLine(Config.bPowerup10Active);
                            writer.WriteLine(Config.bPowerup20Active);      
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
    }
}
