using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Graphics;

namespace AstroFlare
{
    class CreditsScreen : SingleControlScreen
    {
        Texture2D scrollIndicator;

        public override void Activate(bool instancePreserved)
        {
            EnabledGestures = GestureType.Flick | GestureType.VerticalDrag | GestureType.DragComplete | GestureType.Tap;

            ContentManager content = ScreenManager.Game.Content;

            scrollIndicator = content.Load<Texture2D>("GameScreens\\Scrollindicator");

            RootControl = new CreditsPanel(content);

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
            ScreenManager.SpriteBatch.Draw(scrollIndicator, new Vector2(760, 415), Color.White);
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
    }
}
