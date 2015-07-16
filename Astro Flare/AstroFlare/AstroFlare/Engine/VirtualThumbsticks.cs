using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;

namespace AstroFlare
{
    /// <summary>
    /// Represents virtual thumbsticks from touch input. Users can touch the left half of the screen to place the center
    /// of the left thumbstick and the right half for the right thumbstick. The user can then drag away from that center to
    /// simulate thumbstick input.
    ///
    /// This is a static class with static methods to get the thumbstick properties, for consistency with other XNA input
    /// classes like TouchPanel, Gamepad, Keyboard, etc.
    /// </summary>
    public static class VirtualThumbsticks
    {
        #region Fields

        // the distance in screen pixels that represents a thumbstick value of 1f.
        private const float maxThumbstickDistance = 60f;

        // the current positions of the physical touches
        private static Vector2 leftPosition;
        private static Vector2 rightPosition;

        // the IDs of the touches we are tracking for the thumbsticks
        private static int leftId = -1;
        private static int rightId = -1;

        /// <summary>
        /// Gets the center position of the left thumbstick.
        /// </summary>
        public static Vector2? LeftThumbstickCenter { get; private set; }

        /// <summary>
        /// Gets the center position of the right thumbstick.
        /// </summary>
        public static Vector2? RightThumbstickCenter { get; private set; }

        private static Vector2 LeftThumbstickFixed = new Vector2(100, 380);
        private static Vector2 RightThumbstickFixed = new Vector2(700, 380);

        //static TouchCollection touches;

        #endregion

        /// <summary>
        /// Gets the value of the left thumbstick.
        /// </summary>
        public static Vector2 LeftThumbstick
        {
            get
            {
                // if there is no left thumbstick center, return a value of (0, 0)
                if (!LeftThumbstickCenter.HasValue)
                    return Vector2.Zero;

                // calculate the scaled vector from the touch position to the center,
                // scaled by the maximum thumbstick distance
                Vector2 l = (leftPosition - LeftThumbstickCenter.Value) / maxThumbstickDistance;

                // if the length is more than 1, normalize the vector
                if (l.LengthSquared() > 1f)
                    l.Normalize();

                return l;
            }
        }

        /// <summary>
        /// Gets the value of the right thumbstick.
        /// </summary>
        public static Vector2 RightThumbstick
        {
            get
            {
                // if there is no left thumbstick center, return a value of (0, 0)
                if (!RightThumbstickCenter.HasValue)
                    return Vector2.Zero;

                // calculate the scaled vector from the touch position to the center,
                // scaled by the maximum thumbstick distance
                Vector2 r = (rightPosition - RightThumbstickCenter.Value) / maxThumbstickDistance;

                // if the length is more than 1, normalize the vector
                if (r.LengthSquared() > 1f)
                    r.Normalize();

                return r;

            }
        }

        /// <summary>
        /// Updates the virtual thumbsticks based on current touch state. This must be called every frame.
        /// </summary>
        public static void Update(InputState input)
        {
            TouchLocation? leftTouch = null, rightTouch = null;
            //TouchCollection touches = TouchPanel.GetState();
            TouchCollection touches = input.TouchState;

            // Examine all the touches to convert them to virtual dpad positions. Note that the 'touches'
            // collection is the set of all touches at this instant, not a sequence of events. The only
            // sequential information we have access to is the previous location for of each touch.
            foreach (var touch in touches)
            {
                if (touch.Id == leftId)
                {
                    // This is a motion of a left-stick touch that we're already tracking
                    leftTouch = touch;
                    continue;
                }

                if (touch.Id == rightId)
                {
                    // This is a motion of a right-stick touch that we're already tracking
                    rightTouch = touch;
                    continue;
                }

                // We're didn't continue an existing thumbstick gesture; see if we can start a new one.
                //
                // We'll use the previous touch position if possible, to get as close as possible to where
                // the gesture actually began.
                TouchLocation earliestTouch;
                if (!touch.TryGetPreviousLocation(out earliestTouch))
                    earliestTouch = touch;

                if (earliestTouch.Position.X > 300 && earliestTouch.Position.X < 500 && earliestTouch.Position.Y > 430 && earliestTouch.Position.Y < 480)
                {
                    if (GameplayScreen.playerAbilityUses > 0)
                    {
                        if (Config.ship1Active)
                        {
                            if (Player.Ship != null)
                            {
                                //Player.Ship.Health = (int)MathHelper.Min(Player.Ship.Health + 25, Player.Ship.MaxHealth);
                                Player.Ship.Health = (int)MathHelper.Min(Player.Ship.Health + 10, Player.Ship.MaxHealth);
                                Player.Ship.Shield.ShieldRegen(20);
                                GameplayScreen.playerAbilityUses--;
                            }
                        }
                        else if (Config.ship2Active)
                        {
                            if (Player.Ship != null)
                            {
                                PowerupDamageAll AoEDamage = new PowerupDamageAll(Config.PowerupSlowAllSpriteSheet);
                                AoEDamage.Position = Player.Ship.Position;
                                GameplayScreen.playerAbilityUses--;
                            }
                        }
                        else if (Config.ship3Active)
                        {
                            if (Player.Ship != null)
                            {
                                Player.Ship.InvulnerableTimer.Start(10);
                                Player.Ship.isInvulnerable = true;
                                Player.Ship.megaMagnetActive = true;
                                GameplayScreen.playerAbilityUses--;
                                Player.Ship.InvulnAbilityBar = new Bar(100, 20, new Color(255, 255, 255, 5));
                                Player.Ship.InvulnAbilityBar.Position = new Vector2(5, 290);
                            }
                        }
                    }
                }

                if (leftId == -1)
                {
                    // if we are not currently tracking a left thumbstick and this touch is on the left
                    // half of the screen, start tracking this touch as our left stick
                    if (earliestTouch.Position.X < TouchPanel.DisplayWidth / 2)
                    {
                        leftTouch = earliestTouch;
                        continue;
                    }
                }

                if (rightId == -1)
                {
                    // if we are not currently tracking a right thumbstick and this touch is on the right
                    // half of the screen, start tracking this touch as our right stick
                    if (earliestTouch.Position.X >= TouchPanel.DisplayWidth / 2)
                    {
                        rightTouch = earliestTouch;
                        continue;
                    }
                }
            }

            // if we have a left touch
            if (leftTouch.HasValue)
            {
                // if we have no center, this position is our center
                if (Config.ControlOption == 2)
                {
                    LeftThumbstickCenter = LeftThumbstickFixed;
                }
                else if (!LeftThumbstickCenter.HasValue)
                    LeftThumbstickCenter = leftTouch.Value.Position;

                // save the position of the touch
                leftPosition = leftTouch.Value.Position;

                if (Config.ControlOption == 0)
                {
                    if (Vector2.Distance(LeftThumbstickCenter.Value, leftPosition) > 50)
                        LeftThumbstickCenter -= Vector2.Normalize(LeftThumbstickCenter.Value - leftPosition);
                }
                // save the ID of the touch
                leftId = leftTouch.Value.Id;
            }
            else
            {
                // otherwise reset our values to not track any touches
                // for the left thumbstick
                LeftThumbstickCenter = null;
                leftId = -1;
            }

            // if we have a right touch
            if (rightTouch.HasValue)
            {
                // if we have no center, this position is our center
                if (Config.ControlOption == 2)
                {
                    RightThumbstickCenter = RightThumbstickFixed;
                }
                else if (!RightThumbstickCenter.HasValue)
                    RightThumbstickCenter = rightTouch.Value.Position;

                // save the position of the touch
                rightPosition = rightTouch.Value.Position;

                if (Config.ControlOption == 0)
                {
                    if (Vector2.Distance(RightThumbstickCenter.Value, rightPosition) > 50)
                        RightThumbstickCenter -= Vector2.Normalize(RightThumbstickCenter.Value - rightPosition);
                }

                // save the ID of the touch
                rightId = rightTouch.Value.Id;
            }
            else
            {
                // otherwise reset our values to not track any touches
                // for the right thumbstick
                RightThumbstickCenter = null;
                rightId = -1;
            }
        }
    }
}
