using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;

namespace FixedVirtualThumbstick
{
    /// <summary>
    /// Add a Virtual Thumbstick at a specific fixed location. 
    /// Unlike other XNA input classes, this won't be a singelton since you may want more than
    /// thumbstick on the screen. It is also implemented as a DrawableGameComponent.
    /// </summary>
    public class FixedVirtualThumbstick : DrawableGameComponent
    {
        public enum DeadZoneType
        {
            IndependentAxis,
            Circular
        }

        SpriteBatch batch;
        private Vector2 position;
        private Vector2 stickPosition;  //The actual position of the stick on screen, in pixels.
        private float deadZone;

        // The distance in screen pixels that represents a thumbstick value of 1f.
        private const float maxThumbstickDistance = 20f;

        public DeadZoneType DeadZone { get; set; }

        /// <summary>
        /// The amount the stick has moved in each direction, 0 being no movement at all 
        /// and 1 being pressed all the way in that direction.
        /// </summary>
        public Vector2 Position 
        {
            get
            {
                // Calculate the scaled vector from the touch position to the center,
                // scaled by the maximum thumbstick distance
                Vector2 scaledVector = (stickPosition - position) / maxThumbstickDistance;

                // If the length is more than 1, normalize the vector
                if (scaledVector.LengthSquared() > 1f)
                    scaledVector.Normalize();

                //Observe the dead zone
                if (DeadZone == DeadZoneType.IndependentAxis)
                {
                    if (scaledVector.X < deadZone && scaledVector.X > -deadZone)
                        scaledVector.X = 0;
                    if (scaledVector.Y < deadZone && scaledVector.Y > -deadZone)
                        scaledVector.Y = 0;
                }
                else if (DeadZone == DeadZoneType.Circular)
                {
                    if (scaledVector.Length() < deadZone)
                        scaledVector = Vector2.Zero;
                }

                return scaledVector;
            }
            private set { }
        }

        /// <summary>
        /// The actual amount the stick has moved in each direction between 0 and 1, ignoring dead zones.
        /// Use this internally to draw the stick texture at the touched position.
        /// </summary>
        private Vector2 StickPosition
        {
            get
            {
                // Calculate the scaled vector from the touch position to the center,
                // scaled by the maximum thumbstick distance
                Vector2 scaledVector = (stickPosition - position) / maxThumbstickDistance;

                // If the length is more than 1, normalize the vector
                if (scaledVector.LengthSquared() > 1f)
                    scaledVector.Normalize();

                return scaledVector;
            }
        }

        // The ID of the touch we are tracking for the thumbstick
        private int stickId = -1;

        private Texture2D stickBaseTexture;
        private Texture2D stickTexture;

        private BoundingSphere stickCollision;

        /// <summary>
        /// Add a Virtual Thumbstick component at a specific fixed location.
        /// </summary>
        /// <param name="game"></param>
        /// <param name="position">The position of the center of the thumbstick.</param>
        /// <param name="batch"></param>
        public FixedVirtualThumbstick(Game game, Vector2 position, SpriteBatch batch)
            : base(game)
        {
            this.position = position;
            stickPosition = position;
            this.deadZone = 0.20f;
            this.batch = batch;

            DeadZone = DeadZoneType.IndependentAxis;
        }

        /// <summary>
        /// Add a Virtual Thumbstick component at a specific fixed location.
        /// </summary>
        /// <param name="game"></param>
        /// <param name="center">The position of the center of the thumbstick.</param>
        /// <param name="deadZone">The percentage (between 0 and 1) of the thumbstick deadzone, where input is ignored. Default is 15%.</param>
        /// /// <param name="batch"></param>
        public FixedVirtualThumbstick(Game game, Vector2 position, float deadZone, SpriteBatch batch)
            : base(game)
        {
            this.position = position;
            stickPosition = position;
            this.deadZone = MathHelper.Clamp(deadZone, 0.0f, 1.0f);
            this.batch = batch;

            DeadZone = DeadZoneType.IndependentAxis;
        }

        protected override void LoadContent()
        {
            //Load the stick textures;
            stickBaseTexture = Game.Content.Load<Texture2D>(@"thumbstickBase");
            stickTexture = Game.Content.Load<Texture2D>(@"thumbstick");

            //Set up a collision circle for the thumbstick.
            stickCollision = new BoundingSphere(new Vector3(position, 0), stickTexture.Width / 2.0f);

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            TouchLocation? touch = null;
            TouchCollection touches = TouchPanel.GetState();

            foreach (var t in touches)
            {
                if (t.Id == stickId)
                {
                    // This is a motion of a stick touch that we're already tracking
                    touch = t;
                    continue;
                }

                if (stickId == -1)
                {
                    // If we are not currently tracking a thumbstick and this touch is on the stick, start tracking.
                    if (IsTouchingStick(t.Position))
                    {
                        touch = t;
                        continue;
                    }
                }
            }

            // if we have a touch
            if (touch.HasValue)
            {
                // Move the stick to the touched position.
                stickPosition = touch.Value.Position;

                // Save the ID of the touch.
                stickId = touch.Value.Id;
            }
            else
            {
                // Otherwise set our ID to not track any touches.
                stickId = -1;

                //Move the stick back into the center position, don't just go there.
                stickPosition += (position - stickPosition) * 0.5f;
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            Vector2 originBase = new Vector2(stickBaseTexture.Width / 2.0f, stickBaseTexture.Height / 2.0f);
            Vector2 originStick = new Vector2(stickTexture.Width / 2.0f, stickTexture.Height / 2.0f);

            //Get the current position of the stick, keeping the texture clamped to the maxThumbstickDistance.
            Vector2 currentStickPosition = new Vector2(
                position.X + StickPosition.X * maxThumbstickDistance,
                position.Y + StickPosition.Y * maxThumbstickDistance);

            //Make the stick a little transparent.
            Color stickColor = new Color(Color.Gray.R, Color.Gray.G, Color.Gray.B, 128);

            batch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
            
            //Draw the fixed-in place thumbstick base texture.
            batch.Draw(stickBaseTexture, position, null, stickColor, 0.0f, originBase, 1.0f, SpriteEffects.None, 1.0f);
            
            //Draw the stick at its current position.
            batch.Draw(stickTexture, currentStickPosition, null, stickColor, 0.0f, originStick, 1.0f, SpriteEffects.None, 1.0f);

            batch.End();

            base.Draw(gameTime);
        }

        private bool IsTouchingStick(Vector2 point)
        {
            ContainmentType t = stickCollision.Contains(new Vector3(point, 0));
            return (t == ContainmentType.Contains);
        }
    }
}
