using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace AstroFlare
{
    class InstructionsScreen : SingleControlScreen
    {
        //Texture2D background;
        SpriteFont buttonFont;
        SpriteFont buttonFontSmall;
        Texture2D Button;
        ButtonControl practise;
        Control instPanel;
        Control UIPanel;
        Control powerupsPanel;
        PageFlipControl tempControl;

        Texture2D UIBackground;
        //Texture2D powerupsBackground;
        //Sprite coin;

        public override void Activate(bool instancePreserved)
        {
            EnabledGestures = GestureType.Tap | GestureType.Flick | GestureType.HorizontalDrag | GestureType.DragComplete;
            ContentManager content = ScreenManager.Game.Content;

            //background = content.Load<Texture2D>("GameScreens\\Instructions");
            UIBackground = content.Load<Texture2D>("GameScreens\\UIdescriptionBG");
            //powerupsBackground = content.Load<Texture2D>("GameScreens\\Instructions2");
            Button = content.Load<Texture2D>("GameScreens\\Buttons\\B_TransparentMedium");
            buttonFont = content.Load<SpriteFont>("menufont");
            buttonFontSmall = content.Load<SpriteFont>("menufont12");

            //coin = new Sprite(Config.CoinSpriteSheet);

            practise = new ButtonControl(Button, new Vector2(460, 370), "Practise", buttonFont);
            practise.Tapped += new EventHandler<EventArgs>(practiseButton_Tapped);

            //RootControl = new Control();
            RootControl = new PageFlipControl();
 
            instPanel = new LevelDescriptionPanel(content, "GameScreens\\Instructions");
            UIPanel = new LevelDescriptionPanel(content, "GameScreens\\UIdescription");
            powerupsPanel = new LevelDescriptionPanel(content, "GameScreens\\Instructions2");

            RootControl.AddChild(instPanel);
            RootControl.AddChild(powerupsPanel);
            RootControl.AddChild(UIPanel);
            //RootControl.AddChild(practise);
            instPanel.AddChild(practise);
            

            base.Activate(instancePreserved);
        }

        public class LevelDescriptionPanel : PanelControl
        {
            public LevelDescriptionPanel(ContentManager content, string image)
            {
                Texture2D backgroundTexture = content.Load<Texture2D>(image);
                ImageControl background = new ImageControl(backgroundTexture, Vector2.Zero);
                AddChild(background);
            }
        }


        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            //coin.Update(gameTime);

            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
        }

        public override void Draw(GameTime gameTime)
        {
            tempControl = (PageFlipControl)RootControl;

            if (tempControl.Tracker.CurrentPage == 2)
            {
                ScreenManager.SpriteBatch.Begin();
                ScreenManager.SpriteBatch.Draw(UIBackground, Vector2.Zero, Color.White);
                ScreenManager.SpriteBatch.End();
            }

            base.Draw(gameTime);
        }

        public override void Deactivate()
        {
            base.Deactivate();
        }

        public override void Unload()
        {
            base.Unload();
        }

        void practiseButton_Tapped(object sender, EventArgs e)
        {
            Config.tempLevel = Config.Level;
            Config.Level = LevelSelect.Practise;
            ScreenManager.AddScreen(new PhoneModeScreen(), null);
            //LoadingScreen.Load(ScreenManager, true, null,
            //       new GameplayScreen());
        }

    }
}
