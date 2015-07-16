using System;
using Microsoft.Xna.Framework;

namespace AstroFlare
{
    static class Steering
    {
        static Random random = new Random();
        /// <summary>
        /// Wander contains functionality that is shared between both the mouse and the
        /// tank, and does just what its name implies: makes them wander around the
        /// screen. The specifics of the function are described in more detail in the
        /// accompanying doc.
        /// </summary>
        /// <param name="position">the position of the character that is wandering
        /// </param>
        /// <param name="wanderDirection">the direction that the character is currently
        /// wandering. this parameter is passed by reference because it is an input and
        /// output parameter: Wander accepts it as input, and will update it as well.
        /// </param>
        /// <param name="orientation">the character's orientation. this parameter is
        /// also passed by reference and is an input/output parameter.</param>
        /// <param name="turnSpeed">the character's maximum turning speed.</param>
        public static void Wander(Vector2 position, ref Vector2 wanderDirection,
            ref float orientation, float turnSpeed)
        {
            // The wander effect is accomplished by having the character aim in a random
            // direction. Every frame, this random direction is slightly modified.
            // Finally, to keep the characters on the center of the screen, we have them
            // turn to face the screen center. The further they are from the screen
            // center, the more they will aim back towards it.

            // the first step of the wander behavior is to use the random number
            // generator to offset the current wanderDirection by some random amount.
            // .25 is a bit of a magic number, but it controls how erratic the wander
            // behavior is. Larger numbers will make the characters "wobble" more,
            // smaller numbers will make them more stable. we want just enough
            // wobbliness to be interesting without looking odd.
            wanderDirection.X +=
                MathHelper.Lerp(-.25f, .25f, (float)random.NextDouble());
            wanderDirection.Y +=
                MathHelper.Lerp(-.25f, .25f, (float)random.NextDouble());

            // we'll renormalize the wander direction, ...
            if (wanderDirection != Vector2.Zero)
            {
                wanderDirection.Normalize();
            }
            // ... and then turn to face in the wander direction. We don't turn at the
            // maximum turning speed, but at 15% of it. Again, this is a bit of a magic
            // number: it works well for this sample, but feel free to tweak it.
            orientation = TurnToFace(position, position + wanderDirection, orientation,
                .15f * turnSpeed);


            // next, we'll turn the characters back towards the center of the screen, to
            // prevent them from getting stuck on the edges of the screen.
            Vector2 screenCenter = Vector2.Zero;
            screenCenter.X = Config.WorldBoundsX / 2;
            screenCenter.Y = Config.WorldBoundsY / 2;

            // Here we are creating a curve that we can apply to the turnSpeed. This
            // curve will make it so that if we are close to the center of the screen,
            // we won't turn very much. However, the further we are from the screen
            // center, the more we turn. At most, we will turn at 30% of our maximum
            // turn speed. This too is a "magic number" which works well for the sample.
            // Feel free to play around with this one as well: smaller values will make
            // the characters explore further away from the center, but they may get
            // stuck on the walls. Larger numbers will hold the characters to center of
            // the screen. If the number is too large, the characters may end up
            // "orbiting" the center.
            float distanceFromScreenCenter = Vector2.Distance(screenCenter, position);
            float MaxDistanceFromScreenCenter =
                Math.Min(screenCenter.Y, screenCenter.X);

            float normalizedDistance =
                distanceFromScreenCenter / MaxDistanceFromScreenCenter;

            float turnToCenterSpeed = 0.2f * normalizedDistance * normalizedDistance *
                turnSpeed;

            // once we've calculated how much we want to turn towards the center, we can
            // use the TurnToFace function to actually do the work.
            orientation = TurnToFace(position, screenCenter, orientation,
                turnToCenterSpeed);
        }

        /// <summary>
        /// Calculates the angle that an object should face, given its position, its
        /// target's position, its current angle, and its maximum turning speed.
        /// </summary>
        public static float TurnToFace(Vector2 position, Vector2 faceThis, float currentAngle, float turnSpeed)
        {
            // consider this diagram:
            //         B 
            //        /|
            //      /  |
            //    /    | y
            //  / o    |
            // A--------
            //     x
            // 
            // where A is the position of the object, B is the position of the target,
            // and "o" is the angle that the object should be facing in order to 
            // point at the target. we need to know what o is. using trig, we know that
            //      tan(theta)       = opposite / adjacent
            //      tan(o)           = y / x
            // if we take the arctan of both sides of this equation...
            //      arctan( tan(o) ) = arctan( y / x )
            //      o                = arctan( y / x )
            // so, we can use x and y to find o, our "desiredAngle."
            // x and y are just the differences in position between the two objects.
            float x = faceThis.X - position.X;
            float y = faceThis.Y - position.Y;

            // we'll use the Atan2 function. Atan will calculates the arc tangent of 
            // y / x for us, and has the added benefit that it will use the signs of x
            // and y to determine what cartesian quadrant to put the result in.
            // http://msdn2.microsoft.com/en-us/library/system.math.atan2.aspx
            float desiredAngle = (float)Math.Atan2(y, x);

            // so now we know where we WANT to be facing, and where we ARE facing...
            // if we weren't constrained by turnSpeed, this would be easy: we'd just 
            // return desiredAngle.
            // instead, we have to calculate how much we WANT to turn, and then make
            // sure that's not more than turnSpeed.

            // first, figure out how much we want to turn, using WrapAngle to get our
            // result from -Pi to Pi ( -180 degrees to 180 degrees )
            float difference = WrapAngle(desiredAngle - currentAngle);

            // clamp that between -turnSpeed and turnSpeed.
            difference = MathHelper.Clamp(difference, -turnSpeed, turnSpeed);

            // so, the closest we can get to our target is currentAngle + difference.
            // return that, using WrapAngle again.
            return WrapAngle(currentAngle + difference);
        }

        /// <summary>
        /// Returns the angle expressed in radians between -Pi and Pi.
        /// <param name="radians">the angle to wrap, in radians.</param>
        /// <returns>the input value expressed in radians from -Pi to Pi.</returns>
        /// </summary>
        private static float WrapAngle(float radians)
        {
            while (radians < -MathHelper.Pi)
            {
                radians += MathHelper.TwoPi;
            }
            while (radians > MathHelper.Pi)
            {
                radians -= MathHelper.TwoPi;
            }
            return radians;
        }
    }
}
