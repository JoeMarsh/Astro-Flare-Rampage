/* Class: SpriteFontFloatScore 
 * Author: Michael P. Scott
 * Date Created: 01/09/2009
 * Purpose: Provides "floating" scores (or simply text) that appear and disappear after a time frame.
 * 
 * Class: SpriteFontFloatScores
 * Author: Michael P. Scott
 * Date Created: 01/09/2009
 * Purpose: Encapsulates a collection of SpriteFontFloatScore(s).
 * 
 * Permissions: You may use this code in any way you see fit, including any modifications that enhance
 * the original behvior of the class.  However, please retain the original author's name.
 */

using System.Collections;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
//using Microsoft.Xna.Framework.Net;
//using Microsoft.Xna.Framework.Storage;

namespace AstroFlare
{
    /// <summary>  
    /// Class useful in creating "floating scores" in a game environment.  This effect is seen in many games.  For example,  
    /// when an enemy is shot or killed in a shooter game, typically a score associated with that kill appears above the   
    /// incident.  The score then "floats" in a direction (typically upwards) and disappears after a period of time.  
    /// </summary>  
    internal class SpriteFontFloatScore
    {
        #region Private Properties

        private string _score;
        private Vector2 _position;
        private bool _alive;
        private SpriteFont _spriteFont;
        private Color _color;
        private float _colorChange;
        private Vector2 _endPosition;
        private Vector2 _startPosition;
        private float _lifeSpan;
        private float _lifeLeft;
        private float _sizeTime;
        private float _sizeTimeLeft;
        private Vector3 _spawn3DCoordinates;
        private Vector2 _scale;
        private float _rotationDegrees;
        private float _rotationRadians;
        private float _layerDepth;
        private bool _shadowEffect;
       

        #endregion

        #region Constructors

        /// <summary>  
        /// Creates a new instance of the SpriteFontFloatScore class.  
        /// </summary>  
        internal SpriteFontFloatScore()
        {
            this.Score = "";
            this.Position = Vector2.Zero;
            this.Alive = false;
            this.SpriteFont = null;
            this.Color = Color.White;
            this.EndPosition = Vector2.Zero;
            this.StartPosition = Vector2.Zero;
            this.LifeSpan = (float)0.0;
            this.SizeTime = (float)0.0;
            this.Spawn3DCoordinates = Vector3.Zero;
            this.Scale = Vector2.One;
            this.RotationDegrees = (float)0.0;
            this.LayerDepth = (float)0.0;
            this.ShadowEffect = false;
            this.ColorChange = 1.0f;
        }

        /// <summary>  
        /// Creates a new instance of the SpriteFontFloatScore class, copying from the source.  
        /// </summary>  
        /// <param name="copySource">The source SpriteFontFloatScore to copy from.</param>  
        internal SpriteFontFloatScore(SpriteFontFloatScore copySource)
        {
            this.Score = copySource.Score;
            this.Position = copySource.Position;
            this.Alive = copySource.Alive;
            this.SpriteFont = copySource.SpriteFont;
            this.Color = copySource.Color;
            this.EndPosition = copySource.EndPosition;
            this.StartPosition = copySource.StartPosition;
            this.LifeSpan = copySource.LifeSpan;
            this.LifeLeft = copySource.LifeLeft;
            this.SizeTime = copySource.SizeTime;
            this.SizeTimeLeft = copySource.SizeTimeLeft;
            this.Spawn3DCoordinates = copySource.Spawn3DCoordinates;
            this.Scale = copySource.Scale;
            this.RotationDegrees = copySource.RotationDegrees;
            this.LayerDepth = copySource.LayerDepth;
            this.ShadowEffect = copySource.ShadowEffect;
            this.ColorChange = copySource.ColorChange;
        }

        #endregion

        #region Property Accessors

        /// <summary>  
        /// Gets/Sets the string "score" property.  This is the string text to be printed.  
        /// </summary>  
        internal string Score
        {
            get { return _score; }
            set { _score = value; }
        }

        /// <summary>  
        /// Gets the Vector2 Position property.  This is the current Vector2 coordinates of the string text.  
        /// </summary>  
        internal Vector2 Position
        {
            get { return _position; }
            private set { _position = value; }
        }

        /// <summary>  
        /// Gets/Sets the Vector2 Start Position property.  This is the starting position Vector2 coordinates of the string text.  
        /// </summary>  
        internal Vector2 StartPosition
        {
            get { return _startPosition; }
            set { _startPosition = value; }
        }

        /// <summary>  
        /// Gets/Sets the Vector2 End Position property.  This is the ending position Vector2 coordinates of the string text.  
        /// </summary>  
        internal Vector2 EndPosition
        {
            get { return _endPosition; }
            set { _endPosition = value; }
        }

        /// <summary>  
        /// Gets/Sets the bool Alive property.  During the UpdateAnimatedPosition method, the LifeLeft property is  
        /// reduced by the amout of gameTime elapsed and the Position property is updated according to the direction  
        /// of the floating score.  When either the LifeLeft reaches zero or the EndPosition equals Position, the   
        /// Alive property is set to false.  
        /// </summary>  
        internal bool Alive
        {
            get { return _alive; }
            set { _alive = value; }
        }

        /// <summary>  
        /// Gets/Sets the SpriteFont SpriteFont property.  This is the SpriteFont used to render the string text.  
        /// </summary>  
        internal SpriteFont SpriteFont
        {
            get { return _spriteFont; }
            set { _spriteFont = value; }
        }

        /// <summary>  
        /// Gets/Sets the Color Color property.  This is the color used to render the string text.  
        /// </summary>  
        internal Color Color
        {
            get { return _color; }
            set { _color = value; }
        }

        /// <summary>  
        /// Gets/Sets the Color Change Time property, what percent of lifeleft the color changes.  0 = Color, 1 = adjusted color.  
        /// </summary>  
        internal float ColorChange
        {
            get { return _colorChange; }
            set { _colorChange = value; }
        }

        /// <summary>  
        /// Gets/Sets the float LifeSpan property.  This is the length of GameTime the string text is to persist.  
        /// The value is expressed in terms of Milliseconds.  1 Second = 1000 Milliseconds.  
        /// </summary>  
        internal float LifeSpan
        {
            get { return _lifeSpan; }
            set
            {
                _lifeSpan = value;
                this.LifeLeft = value;
            }
        }

        /// <summary>  
        /// Gets the float LifeLeft property.  This is the remaining length of GameTime the string text will continute to persist.  
        /// The value is expressed in terms of Milliseconds.  1 Second = 1000 Milliseconds.  
        /// </summary>  
        internal float LifeLeft
        {
            get { return _lifeLeft; }
            private set
            {
                _lifeLeft = value;
                if (_lifeLeft <= (float)0.0)
                {
                    _lifeLeft = (float)0.0;
                    _alive = false;
                }
            }
        }

        /// <summary>  
        /// Gets/Sets the float SizeTime property.  This is the length of GameTime the string text will take to achieve its full scale size.  
        /// A value of zero results in no sizing effect, as the string text will always be rendered full-size.  
        /// The value is expressed in terms of Milliseconds.  1 Second = 1000 Milliseconds.  
        /// </summary>  
        internal float SizeTime
        {
            get { return this._sizeTime; }
            set
            {
                this._sizeTime = value;
                if (this._sizeTime > this._lifeSpan) this._sizeTime = this._lifeSpan;
                this._sizeTimeLeft = this._sizeTime;
            }
        }

        /// <summary>  
        /// Gets the float SizeTimeLeft property.  This is the length of remaining GameTime the string text will take to achieve its full scale size.  
        /// The value is expressed in terms of Milliseconds.  1 Second = 1000 Milliseconds.  
        /// </summary>  
        internal float SizeTimeLeft
        {
            get { return this._sizeTimeLeft; }
            private set
            {
                this._sizeTimeLeft = value;
                if (this._sizeTimeLeft <= (float)0.0) this._sizeTimeLeft = (float)0.0;
            }
        }

        /// <summary>  
        /// Gets/Sets the Vector3 Spawn3DCoordinates property.  This is the originating game world coordinates of the string text.  
        /// This value should be set at creation time.  The value is used in conjunction with the RecalculatePositionFromCamera method.   
        /// Using the origin point, the class can determine the new world coordinates based on new camera coordinates.  
        /// This is helpful when the user rotates or moves the camera while the floating text is still persisting.  
        /// </summary>  
        internal Vector3 Spawn3DCoordinates
        {
            get { return _spawn3DCoordinates; }
            set { _spawn3DCoordinates = value; }
        }

        /// <summary>  
        /// Gets/Sets the Vector2 Scale property.  Use Vector2.One to retain original SpriteFont dimensions.  
        /// Scale.X can be set to increase/decrease width-wise and Scale.Y can be set to increase/decrease height-wise.  
        /// </summary>  
        internal Vector2 Scale
        {
            get { return this._scale; }
            set { this._scale = value; }
        }

        /// <summary>  
        /// Gets/Sets float RotationDegrees property.  A value of zero will result in zero rotation.  
        /// The value is expressed in degrees.  
        /// </summary>  
        internal float RotationDegrees
        {
            get { return this._rotationDegrees; }
            set
            {
                this._rotationDegrees = value;
                this._rotationRadians = value * (MathHelper.Pi / 180);
            }
        }

        /// <summary>  
        /// Gets the float RotationRadians property.  This is the conversion of RotationDegrees as expressed in terms of radians.  
        /// </summary>  
        internal float RotationRadians
        {
            get { return this._rotationRadians; }
        }

        /// <summary>  
        /// Gets/Sets the float LayerDepth property.  This is the sorting depth of the SpriteFont.    
        /// 0.0 is Front, 1.0 is Back.  
        /// </summary>  
        internal float LayerDepth
        {
            get { return this._layerDepth; }
            set { this._layerDepth = value; }
        }

        /// <summary>  
        /// Gets/Sets the bool ShadowEffect property.  If set to true, the string text is rendered twice.    
        /// The first rendering is a black color with transparency equal to the Color property.  
        /// This render occurs 3 pixels to the right and down of the Position property.    
        /// The second rendering is the string text in the color of the Color property with no offset.  
        /// </summary>  
        internal bool ShadowEffect
        {
            get { return this._shadowEffect; }
            set { this._shadowEffect = value; }
        }

        #endregion

        #region Methods

        /// <summary>  
        /// Draws the string text using the spriteBatch specified.  This method needs to be called in the XNA game.Draw method.  
        /// Only floating scores that are "Alive" are drawn.  
        /// </summary>  
        /// <param name="spriteBatch">The SpriteBatch to add draw the string text in.</param>  
        internal void Draw(SpriteBatch spriteBatch)
        {
            Vector2 screenOrigin = Vector2.Zero;
            this.Draw(spriteBatch, screenOrigin);
        }

        /// <summary>  
        /// Draws the string text using the spriteBatch specified.  This method needs to be called in the XNA game.Draw method.  
        /// Only floating scores that are "Alive" are drawn.  
        /// </summary>  
        /// <param name="spriteBatch">The SpriteBatch to add draw the string text in.</param>  
        /// <param name="screenOrigin">In 2D games, an offset against the game-screen origin.  Use Vector2.Zero for no offset.</param>  
        internal void Draw(SpriteBatch spriteBatch, Vector2 screenOrigin)
        {
            if (!this.Alive) return;

            Vector2 textOrigin = new Vector2((this.SpriteFont.MeasureString(this.Score).X * this.Scale.X) / 2, (this.SpriteFont.MeasureString(this.Score).Y * this.Scale.Y) / 2);
            Vector2 scale = new Vector2(this.Scale.X, this.Scale.Y);

            if (this.SizeTimeLeft > (float)0.0)
            {
                scale *= (1 - (this.SizeTimeLeft / this.SizeTime));
            }

            //change (float)1.0f to determine when to start changing color
            if (this.LifeLeft <= this.ColorChange * this.LifeSpan)
            {
                Microsoft.Xna.Framework.Color drawColor = new Color(this.Color.R, (float)MathHelper.Clamp((this.LifeLeft / ((float)1.0 * this.LifeSpan)) * 255, 0, 255), 0.0f, this.Color.A);

                if (this.ShadowEffect)
                {
                    spriteBatch.DrawString(this.SpriteFont, this.Score, new Vector2(this.Position.X + screenOrigin.X + 3, this.Position.Y + screenOrigin.Y + 3), new Color(Color.Black.R, Color.Black.G, Color.Black.B, drawColor.A), this.RotationRadians, textOrigin, scale, SpriteEffects.None, this.LayerDepth + (float)0.01);
                }
                spriteBatch.DrawString(this.SpriteFont, this.Score, new Vector2(this.Position.X + screenOrigin.X, this.Position.Y + screenOrigin.Y), drawColor, this.RotationRadians, textOrigin, scale, SpriteEffects.None, this.LayerDepth);
            }
            else
            {
                if (this.ShadowEffect)
                {
                    spriteBatch.DrawString(this.SpriteFont, this.Score, new Vector2(this.Position.X + screenOrigin.X + 3, this.Position.Y + screenOrigin.Y + 3), new Color(Color.Black.R, Color.Black.G, Color.Black.B, this.Color.A), this.RotationRadians, textOrigin, scale, SpriteEffects.None, this.LayerDepth + (float)0.01);
                }
                spriteBatch.DrawString(this.SpriteFont, this.Score, new Vector2(this.Position.X + screenOrigin.X, this.Position.Y + screenOrigin.Y), this.Color, this.RotationRadians, textOrigin, scale, SpriteEffects.None, this.LayerDepth);
            }
        }

        /// <summary>  
        /// Updates the position of the floating score string text.  This method needs to be called in the XNA game.Update method.  
        /// Only floating scores that are "Alive" are updated.  
        /// </summary>  
        /// <param name="gameTime">The gameTime value provided by the XNA framework in the game.Update method.</param>  
        internal void UpdateAnimatedPosition(GameTime gameTime)
        {
            if (!this.Alive) return;

            this.LifeLeft -= gameTime.ElapsedGameTime.Milliseconds;
            this.SizeTimeLeft -= gameTime.ElapsedGameTime.Milliseconds;
            //float x = (float)Math.Abs(this.StartPosition.X - this.EndPosition.X);
            //float y = (float)Math.Abs(this.StartPosition.Y - this.EndPosition.Y);
            _position = StartPosition;

            _position += EndPosition * (((this.LifeSpan) - (this.LifeLeft)) / (this.LifeSpan));

            //x *= (((this.LifeSpan) - (this.LifeLeft)) / (this.LifeSpan));

            //if (this.EndPosition.X > this.Position.X)
            //{
            //    _position.X = this.StartPosition.X + x;
            //    _position.X = MathHelper.Clamp(_position.X, this.StartPosition.X, this.EndPosition.X);
            //}
            //else
            //{
            //    _position.X = this.StartPosition.X - x;
            //    _position.X = MathHelper.Clamp(_position.X, this.EndPosition.X, this.StartPosition.X);
            //}

            //y *= (((this.LifeSpan) - (this.LifeLeft)) / (this.LifeSpan));

            //if (this.StartPosition.Y <= this.EndPosition.Y)
            //{
            //    if (this.EndPosition.Y > this.Position.Y)
            //    {
            //        _position.Y = this.StartPosition.Y + y;
            //        _position.Y = MathHelper.Clamp(_position.Y, this.StartPosition.Y, this.EndPosition.Y);
            //    }
            //    else
            //    {
            //        _position.Y = this.StartPosition.Y - y;
            //        _position.Y = MathHelper.Clamp(_position.Y, this.EndPosition.Y, this.StartPosition.Y);
            //    }
            //}
            //else
            //{
            //    if (this.StartPosition.Y > this.Position.Y)
            //    {
            //        _position.Y = this.StartPosition.Y - y;
            //        _position.Y = MathHelper.Clamp(_position.Y, this.EndPosition.Y, this.StartPosition.Y);
            //    }
            //    else
            //    {
            //        _position.Y = this.StartPosition.Y + y;
            //        _position.Y = MathHelper.Clamp(_position.Y, this.StartPosition.Y, this.EndPosition.Y);
            //    }
            //}

            if (this.EndPosition.Equals(this.Position) && !this.EndPosition.Equals(this.StartPosition)) this.Alive = false;
        }

        /// <summary>  
        /// Recalculates string text position in relation to any camera adjustments.  The Spawn3DCoordinates property must contain  
        /// the origin of the string text at creation time.  
        /// Only floating scores that are "Alive" are recalculated.  
        /// </summary>  
        /// <param name="graphics">The GraphicsDeviceManager with the appropriate Viewport setting.  The Project method is  
        /// used in conjunction with the Spawn3DCoordinates property to calculate the new origin.  Then, the current Position is   
        /// determined relatively.</param>  
        /// <param name="cmaeraProjectionMatrix">The Matrix of the Projection Matrix of the camera.  Used with the  
        /// Project method of the GraphicsDeviceManager.</param>  
        /// /// <param name="cmaeraViewMatrix">The Matrix of the View Matrix of the camera.  Used with the  
        /// Project method of the GraphicsDeviceManager.</param>  
        internal void RecalculatePositionFromCamera(GraphicsDeviceManager graphics, Matrix cameraProjectionMatrix, Matrix cameraViewMatrix)
        {
            if (!this.Alive) return;

            Vector3 newPosition = graphics.GraphicsDevice.Viewport.Project(this.Spawn3DCoordinates, cameraProjectionMatrix, cameraViewMatrix, Matrix.Identity);

            Vector2 scoreStartPosition = new Vector2(newPosition.X, newPosition.Y);
            Vector2 relative = this.StartPosition - this.EndPosition;
            Vector2 scoreEndPosition = new Vector2(scoreStartPosition.X + relative.X, scoreStartPosition.Y + relative.Y);

            this.StartPosition = scoreStartPosition;
            this.EndPosition = scoreEndPosition;

            UpdateAnimatedPosition(new GameTime());
        }

        #endregion
    }
}

    /// <summary>  
    /// Class that contains methods operating over more than one SpriteFontFloatScore class.  
    /// </summary>  
    internal class SpriteFontFloatScores : IEnumerable
    {
        #region Private Properties

        private List<AstroFlare.SpriteFontFloatScore> _spriteFontFloatScores;

        #endregion

        #region Constructors

        /// <summary>  
        /// Creates a new empty set of floating scores.  
        /// </summary>  
        internal SpriteFontFloatScores()
        {
            _spriteFontFloatScores = new List<AstroFlare.SpriteFontFloatScore>();
        }

        /// <summary>  
        /// Creates a set of floating scores, copying from the source scores.  
        /// </summary>  
        /// <param name="copySource">Collection of SpriteFontFloatScore(s) to replicate.</param>  
        internal SpriteFontFloatScores(SpriteFontFloatScores copySource)
            : this()
        {
            foreach (AstroFlare.SpriteFontFloatScore entry in copySource)
            {
                AstroFlare.SpriteFontFloatScore newEntry = new AstroFlare.SpriteFontFloatScore(entry);
                this.Add(newEntry);
            }
        }

        #endregion

        #region Indexers

        /// <summary>  
        /// Provides indexer support.  
        /// </summary>  
        /// <param name="index">Zero-based index of the collection retrieve.</param>  
        /// <returns>The SpriteFontFloatScore at the specified index, or null if not found.</returns>  
        internal AstroFlare.SpriteFontFloatScore this[int index]
        {
            get
            {
                try
                {
                    return (AstroFlare.SpriteFontFloatScore)_spriteFontFloatScores[index];
                }
                catch
                {
                    return null;
                }
            }
        }

        #endregion

        #region Methods

        /// <summary>  
        /// Adds one or more new SpriteFontFloatScore(s) to the collection.  
        /// </summary>  
        /// <param name="spriteFontFloatScore">The SpriteFontFloatScore to add to the collection.</param>  
        /// <returns>The integer index of the newly added element.</returns>  
        internal int Add(AstroFlare.SpriteFontFloatScore spriteFontFloatScore)
        {
            _spriteFontFloatScores.Add(spriteFontFloatScore);
            return _spriteFontFloatScores.Count;
        }

        /// <summary>  
        /// Adds one or more new SpriteFontFloatScore(s) to the collection.  
        /// </summary>  
        /// <param name="spriteFontFloatScoreList">A list of two or more comma-delimited SpriteFontFloatScore(s) to   
        /// add to the collection.  You can also specify an array of SpriteFontFloatScore(s).</param>  
        /// <returns>The array of integers representing the indices of the newly added element(s).</returns>  
        internal int[] Add(params AstroFlare.SpriteFontFloatScore[] spriteFontFloatScoreList)
        {
            int[] returnInts = new int[spriteFontFloatScoreList.Length];

            for (int i = 0; i < spriteFontFloatScoreList.Length; i++)
            {
                _spriteFontFloatScores.Add(spriteFontFloatScoreList[i]);
                returnInts[i] = _spriteFontFloatScores.IndexOf(spriteFontFloatScoreList[i]);                    
            }

            return returnInts;
        }

        /// <summary>  
        /// Removes one SpriteFontFloatScore from the collection.  
        /// </summary>  
        /// <param name="index">Zero-based int index of the score to remove.</param>  
        internal void Remove(int index)
        {
            _spriteFontFloatScores.RemoveAt(index);
        }

        /// <summary>  
        /// Removes one or more SpriteFontFloatScore(s) from the collection.  
        /// </summary>  
        /// <param name="index">Zero-based comma-delimited list of int indices of the score(s) to remove.  
        /// You can also specify an array of indices.</param>  
        internal void Remove(params int[] indexList)
        {
            for (int i = 0; i < indexList.Length; i++)
            {
                _spriteFontFloatScores.RemoveAt(indexList[i]);
            }
        }

        /// <summary>  
        /// Removes one SpriteFontFloatScore from the collection.  
        /// </summary>  
        /// <param name="spriteFontFloatScore">The object to remove from the collection.  The object will  
        /// be searched for and removed, if present.</param>  
        internal void Remove(AstroFlare.SpriteFontFloatScore spriteFontFloatScore)
        {
            _spriteFontFloatScores.Remove(spriteFontFloatScore);
        }

        /// <summary>  
        /// Removes one or more SpriteFontFloatScore(s) from the collection.  
        /// </summary>  
        /// <param name="spriteFontFloatScore">The comma-delimited list of object(s) to remove from the collection.  The object(s) will  
        /// be searched for and removed, if present.  You can also specify an array of objects.</param>  
        internal void Remove(params AstroFlare.SpriteFontFloatScore[] spriteFontFloatScoreList)
        {
            for (int i = 0; i < spriteFontFloatScoreList.Length; i++)
            {
                _spriteFontFloatScores.Remove(spriteFontFloatScoreList[i]);
            }
        }

        /// <summary>  
        /// Clears the entire collection of floating scores.  
        /// </summary>  
        internal void Clear()
        {
            _spriteFontFloatScores.Clear();
        }

        /// <summary>  
        /// Provides C# foreach support.  
        /// </summary>  
        /// <returns>Enumerator useful in the foreach statement.</returns>  
        IEnumerator IEnumerable.GetEnumerator()
        {
            return _spriteFontFloatScores.GetEnumerator();
        }

        /// <summary>  
        /// Updates all positions of the SpriteFontFloatScore(s) in the collection.  This method needs to be called in the XNA game.Update method.  
        /// Only floating scores that are "Alive" are updated.  Any floating scores not "Alive" are automatically removed  
        /// from the collection.  
        /// </summary>  
        /// <param name="gameTime">The gameTime value provided by the XNA framework in the game.Update method.</param>  
        internal void UpdateAllAnimatedPositions(GameTime gameTime)
        {
            bool restart;
            do
            {
                restart = false;
                foreach (AstroFlare.SpriteFontFloatScore floatScore in this)
                {
                    floatScore.UpdateAnimatedPosition(gameTime);
                    if (!floatScore.Alive)
                    {
                        this.Remove(floatScore);
                        restart = true;
                        break;
                    }
                }
            }
            while (restart);
        }

        /// <summary>  
        /// Draws all the string text in the collection using the spriteBatch specified.  This method needs to be called in the XNA game.Draw method.  
        /// Only floating scores that are "Alive" are drawn.  
        /// </summary>  
        /// <param name="spriteBatch">The SpriteBatch to add draw the string text in.</param>  
        internal void DrawAll(SpriteBatch spriteBatch)
        {
            foreach (AstroFlare.SpriteFontFloatScore floatScore in this)
            {
                floatScore.Draw(spriteBatch);
            }
        }

        /// <summary>  
        /// Draws all the string text in the collection using the spriteBatch specified.  This method needs to be called in the XNA game.Draw method.  
        /// Only floating scores that are "Alive" are drawn.  
        /// </summary>  
        /// <param name="spriteBatch">The SpriteBatch to add draw the string text in.</param>  
        /// <param name="screenOrigin">In 2D games, an offset against the game-screen origin.  Use Vector2.Zero for no offset.</param>  
        internal void DrawAll(SpriteBatch spriteBatch, Vector2 screenOrigin)
        {
            foreach (AstroFlare.SpriteFontFloatScore floatScore in this)
            {
                floatScore.Draw(spriteBatch, screenOrigin);
            }
        }

        /// <summary>  
        /// Recalculates all string text positions in the collection in relation to any camera adjustments.  The Spawn3DCoordinates property must contain  
        /// the origin of the string text at creation time.  
        /// Only floating scores that are "Alive" are recalculated.  
        /// </summary>  
        /// <param name="graphics">The GraphicsDeviceManager with the appropriate Viewport setting.  The Project method is  
        /// used in conjunction with the Spawn3DCoordinates property to calculate the new origin.  Then, the current Position is   
        /// determined relatively.</param>  
        /// <param name="cmaeraProjectionMatrix">The Matrix of the Projection Matrix of the camera.  Used with the  
        /// Project method of the GraphicsDeviceManager.</param>  
        /// /// <param name="cmaeraViewMatrix">The Matrix of the View Matrix of the camera.  Used with the  
        /// Project method of the GraphicsDeviceManager.</param>  
        internal void RecalculateAllPositionsFromCamera(GraphicsDeviceManager graphics, Matrix cameraProjectionMatrix, Matrix cameraViewMatrix)
        {
            foreach (AstroFlare.SpriteFontFloatScore floatScore in this)
            {
                floatScore.RecalculatePositionFromCamera(graphics, cameraProjectionMatrix, cameraViewMatrix);
            }
        }

        #endregion
    }

