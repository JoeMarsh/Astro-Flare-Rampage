using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace AstroFlare
{
    class OldCreditsScreen : SingleControlScreen
    {
        SpriteFont titleFont;
        SpriteFont descriptionFont18;

        //Sprite coin;

        public override void Activate(bool instancePreserved)
        {
            EnabledGestures = GestureType.Tap;
            ContentManager content = ScreenManager.Game.Content;

            titleFont = content.Load<SpriteFont>("Fonts\\Pericles22");
            descriptionFont18 = content.Load<SpriteFont>("Fonts\\Andy18");

            RootControl = new Control();

            base.Activate(instancePreserved);
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            //coin.Update(gameTime);
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
        }

        public override void Draw(GameTime gameTime)
        {
            ScreenManager.SpriteBatch.Begin();
            ScreenManager.SpriteBatch.DrawString(titleFont, "Music", new Vector2(400, 100) + new Vector2(2, 2), Color.Black,
                0f, new Vector2(titleFont.MeasureString("Music").X / 2, titleFont.MeasureString("Music").Y / 2), 1f, SpriteEffects.None, 1f);

            ScreenManager.SpriteBatch.DrawString(titleFont, "Music", new Vector2(400, 100), Color.White,
                0f, new Vector2(titleFont.MeasureString("Music").X / 2, titleFont.MeasureString("Music").Y / 2), 1f, SpriteEffects.None, 1f);

            ScreenManager.SpriteBatch.DrawString(descriptionFont18, "Intro: Before the Storm", new Vector2(400, 160), Color.White,
                0f, new Vector2(descriptionFont18.MeasureString("Intro: Before the Storm").X / 2, descriptionFont18.MeasureString("Intro: Before the Storm").Y / 2), 1f, SpriteEffects.None, 1f);

            ScreenManager.SpriteBatch.DrawString(descriptionFont18, "Rampage: Through the Stars", new Vector2(400, 200), Color.White,
                0f, new Vector2(descriptionFont18.MeasureString("Rampage: Through the Stars").X / 2, descriptionFont18.MeasureString("Rampage: Through the Stars").Y / 2), 1f, SpriteEffects.None, 1f);

            ScreenManager.SpriteBatch.DrawString(descriptionFont18, "Rampage - Timed: Galactic Struggle", new Vector2(400, 240), Color.White,
                0f, new Vector2(descriptionFont18.MeasureString("Rampage - Timed: Galactic Struggle").X / 2, descriptionFont18.MeasureString("Rampage - Timed: Galactic Struggle").Y / 2), 1f, SpriteEffects.None, 1f);

            ScreenManager.SpriteBatch.DrawString(descriptionFont18, "Extermination: Against the Odds", new Vector2(400, 280), Color.White,
                0f, new Vector2(descriptionFont18.MeasureString("Extermination: Against the Odds").X / 2, descriptionFont18.MeasureString("Extermination: Against the Odds").Y / 2), 1f, SpriteEffects.None, 1f);

            ScreenManager.SpriteBatch.DrawString(descriptionFont18, "Brian Doyle - www.brian-doyle.com", new Vector2(400, 340), Color.White,
                0f, new Vector2(descriptionFont18.MeasureString("Brian Doyle - www.brian-doyle.com").X / 2, descriptionFont18.MeasureString("Brian Doyle - www.brian-doyle.com").Y / 2), 1f, SpriteEffects.None, 1f);

            ScreenManager.SpriteBatch.End();

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
            Config.Level = LevelSelect.Practise;
            LoadingScreen.Load(ScreenManager, true, null,
                   new GameplayScreen());
        }
    }
}
