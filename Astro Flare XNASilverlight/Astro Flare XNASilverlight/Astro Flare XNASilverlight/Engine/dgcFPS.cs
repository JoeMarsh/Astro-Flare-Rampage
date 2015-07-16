using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace AstroFlare
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class dgcFPS : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private SpriteFont VideoFont;
        private float _ElapsedTime, _TotalFrames, _Fps;
        private bool _ShowFPS;
        private string _FontName = "DefaultFont";

        public bool ShowFPS
        {
            get { return _ShowFPS; }
            set { _ShowFPS = value; }
        }

        public dgcFPS(Game game)
            : base(game)
        {
            // TODO: Construct any child components here
        }

        public dgcFPS(Game game, bool show)
            : base(game)
        {
            _ShowFPS = show;
        }

        public dgcFPS(Game game, string FontName)
            : base(game)
        {
            _FontName = FontName;
        }


        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here

            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            _ElapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            _TotalFrames++;

            if (_ElapsedTime >= 1.0f)
            {
                _Fps = _TotalFrames;
                _TotalFrames = 0;
                _ElapsedTime = 0;
            }
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);

            // Load the required font
            VideoFont = Game.Content.Load<SpriteFont>(_FontName);

            base.LoadContent();
        }

        protected override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            if (ShowFPS)
            {
                spriteBatch.Begin();
                spriteBatch.DrawString(VideoFont,
                    "FPS=" + _Fps.ToString(),
                    new Vector2(10, 170),
                    Color.Red,
                    0f,
                    Vector2.Zero,
                    1.0f,
                    SpriteEffects.None,
                    0);
                spriteBatch.End();
                //spriteBatch.Begin();
                //spriteBatch.DrawString(VideoFont,
                //    "FPS=" + _Fps.ToString(),
                //    new Vector2(0, Game.GraphicsDevice.Viewport.Height - VideoFont.LineSpacing),
                //    Color.Red,
                //    0f,
                //    Vector2.Zero,
                //    1.0f,
                //    SpriteEffects.None,
                //    0);
                //spriteBatch.End();
            }
            base.Draw(gameTime);
        }
    }

}
