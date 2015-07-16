using System;
using Microsoft.Xna.Framework;
using Microsoft.Devices.Sensors;

namespace AstroFlare
{
    static class Player
    {
        //public static Player[] Players;
        //public static Player Player1;

        public static PlayerShip Ship;
        public static EnemyPlayerShip EnemyPlayer;
        public static Timer enemySpawnTimer = new Timer();
        public static Timer ComboTimer = new Timer();

        
        //public static PlayerShip Ship2;

        public static bool NotFiring = true;

        //public float directionModifier;
        //float shipRotationAngle;
        public static float shipRotationSpeed = 0;
        public static double thrustModifier;
        public static double accelerationModifier;
        public static double shipBaseAcceleration = 4;
        public static Vector2 directionVector;
        public static Vector2 accelerationVector = new Vector2(0, 0);
        public static Vector2 frictionVector = new Vector2(0, 0);
        public static int shipFriction = 0;
        public static Vector2 shipSpeedVector;
        //public Vector2 shipPositionVector;
        public static Vector2 speedCap = new Vector2(0, 0);

        //static Player()
        //{

        //    //Players = new Player[4];
        //    //for (int i = 0; i < Players.Length; i++)
        //    //{
        //    //    Players[i] = new Player();
        //    //}
        //}

        static Player()
        {
            ComboTimer.Fire += new NotifyHandler(ComboTimer_Fire);
            //Accelerometer = new Accelerometer();
            //if (Accelerometer.State == SensorState.Ready)
            //{
            //    Accelerometer.ReadingChanged += (s, e) =>
            //    {
            //        accelState = e;
            //    };
            //    Accelerometer.Start();
            //}

        }

        static void ComboTimer_Fire()
        {
            if (Config.KillStreak > 2)
                Config.KillStreakBuildpoints += Config.KillStreak * ((Math.Min(Config.KillStreak, 50) / 50) + 1);

            Config.KillStreak = 0;
            ComboTimer.Stop();
        }

        public static void SpawnShip()
        {

            if (Ship == null)
            {
                Ship = new PlayerShip(Config.CurrentShipTop);
                enemySpawnTimer.Fire += new NotifyHandler(enemySpawnTimer_Fire);
            }
            

            //if (Ship2 == null)
            //    Ship2 = new PlayerShip(Config.CurrentShipTop);

            //if (Players[(int)playerIndex].Ship == null)
            //    Players[(int)playerIndex].Ship = new PlayerShip(Config.ShipSpriteSheet);
        }

        static void enemySpawnTimer_Fire()
        {
            SpawnEnemyShip();
            enemySpawnTimer.Stop();
        }

        public static void SpawnEnemyShip()
        {
            if (EnemyPlayer == null)
            {
                EnemyPlayer = new EnemyPlayerShip(Config.CurrentShipTop);
            }
        }

        public static void RemoveShip(PlayerShip ship)
        {
            if (Ship == ship)
            {
                Ship = null;
            }
            //for (int i = 0; i < Players.Length; i++)
            //    if (Players[i].Ship == ship)
            //        Players[i].Ship = null;
        }

        public static void RemoveAIShip(EnemyPlayerShip ship)
        {
            if (EnemyPlayer == ship)
            {
                EnemyPlayer = null;
            }
            //for (int i = 0; i < Players.Length; i++)
            //    if (Players[i].Ship == ship)
            //        Players[i].Ship = null;
        }

        //static AccelerometerReadingEventArgs accelState;
        //static Accelerometer Accelerometer;

        public static void ProcessInput()
        {
            if (Ship != null)
            {
                if (!Config.AIControlled)
                {
                    if (Config.ControlOption == 3)
                    {
                        if (VirtualThumbsticks.RightThumbstickCenter.HasValue)
                            Ship.Weapon.Direction = Vector2.Normalize(VirtualThumbsticks.RightThumbstick);
                        else if (VirtualThumbsticks.LeftThumbstickCenter.HasValue)
                            Ship.Weapon.Direction = Vector2.Normalize(VirtualThumbsticks.LeftThumbstick);

                        if (VirtualThumbsticks.RightThumbstick.Length() > 0.0f && NotFiring)
                        {
                            //Ship.Perform(RecursiveShipFire.Action_StartFire, EventArgs.Empty);
                            Ship.StartFire();
                            NotFiring = false;
                        }

                        if (VirtualThumbsticks.LeftThumbstick.Length() > 0.0f && NotFiring)
                        {
                            //Ship.Perform(RecursiveShipFire.Action_StartFire, EventArgs.Empty);
                            Ship.StartFire();
                            NotFiring = false;
                        }
                        
                        if (VirtualThumbsticks.RightThumbstick.Length() <= 0.0f && VirtualThumbsticks.LeftThumbstick.Length() <= 0.0f)
                        {
                            //Ship.Perform(RecursiveShipFire.Action_StopFire, EventArgs.Empty);
                            Ship.StopFire();
                            NotFiring = true;
                        }
                    }
                    else
                    {
                        if (VirtualThumbsticks.RightThumbstickCenter.HasValue)
                            Ship.Weapon.Direction = Vector2.Normalize(VirtualThumbsticks.RightThumbstick);

                        if (VirtualThumbsticks.RightThumbstick.Length() > 0.0f && NotFiring)
                        {
                            //Ship.Perform(RecursiveShipFire.Action_StartFire, EventArgs.Empty);
                            Ship.StartFire();
                            NotFiring = false;
                        }
                        if (VirtualThumbsticks.RightThumbstick.Length() <= 0.0f)
                        {
                            //Ship.Perform(RecursiveShipFire.Action_StopFire, EventArgs.Empty);
                            Ship.StopFire();
                            NotFiring = true;
                        }
                    }
                }

                if (Config.ControlOption != 3)
                {
                    if (Config.AIControlled)
                        if (VirtualThumbsticks.RightThumbstickCenter.HasValue)
                            Ship.Rotation = (float)Math.Atan2(VirtualThumbsticks.RightThumbstick.X, -VirtualThumbsticks.RightThumbstick.Y);

                    if (VirtualThumbsticks.LeftThumbstickCenter.HasValue)
                        Ship.Rotation = (float)Math.Atan2(VirtualThumbsticks.LeftThumbstick.X, -VirtualThumbsticks.LeftThumbstick.Y);

                    thrustModifier = VirtualThumbsticks.LeftThumbstick.Length();

                    if (Config.AIControlled)
                        if (VirtualThumbsticks.RightThumbstickCenter.HasValue)
                            thrustModifier = VirtualThumbsticks.RightThumbstick.Length();
                }

                //if (accelState != null)
                //{
                //    if (Math.Abs(accelState.X) > 0.05f)
                //    {
                //        accelerationVector.Y = -(float)accelState.X * 20;
                //        Ship.Rotation = (float)Math.Atan2(accelerationVector.X, -accelerationVector.Y);
                //    }
                //    if (Math.Abs(accelState.Y) > 0.05f)
                //    {
                //        accelerationVector.X = -(float)accelState.Y * 20;
                //        Ship.Rotation = (float)Math.Atan2(accelerationVector.X, -accelerationVector.Y);
                //    }
                //}

                // If using AccelerometerInput.cs
                //if (accelerometer.State == SensorState.Ready && Config.ControlOption == 3)
                //{
                //    UpdateAccelerometerInput(accelerometer);
                //}
                if (Config.ControlOption == 3)
                {
                    
                }
                else
                {
                    accelerationModifier = (shipBaseAcceleration * thrustModifier);
                    directionVector = new Vector2((float)Math.Sin(Ship.Rotation), -(float)Math.Cos(Ship.Rotation));
                    //accelerationVector = Vector2(directionVector.x * System.Convert.ToDouble(accelerationModifier), directionVector.y * System.Convert.ToDouble(accelerationModifier));
                    accelerationVector.X = directionVector.X * (float)accelerationModifier;
                    accelerationVector.Y = directionVector.Y * (float)accelerationModifier;
                    // Set friction based on how "floaty" controls you want
                    shipSpeedVector.X *= 0.8f; //Use a variable here
                    shipSpeedVector.Y *= 0.8f; //<-- as well
                    shipSpeedVector += accelerationVector;

                    Ship.Direction = shipSpeedVector;
                }
            }
        }

        public static void AccelerometerInput(double x, double y, double thrust)
        {
            if (Config.ControlOption == 3 && Ship != null)
            {
                //accelerationVector.Y = AccelerometerInput.Instance.AdjustedAccelerometerData.Y;
                //accelerationVector.X = AccelerometerInput.Instance.AdjustedAccelerometerData.X;
                //Ship.Rotation = (float)Math.Atan2(accelerationVector.X, -accelerationVector.Y);
                Ship.Rotation = (float)Math.Atan2(-y, x);
                directionVector = new Vector2((float)Math.Sin(Ship.Rotation), -(float)Math.Cos(Ship.Rotation));
                //thrustModifier = directionVector.Length() * 4;
                //Vector2 temp = new Vector2((float)x, (float)y);
                //float length = temp.Length();
                //thrustModifier = length * 20;
                //thrustModifier /= 5;
                thrustModifier = thrust * 5;
                if (thrustModifier > 5)
                    thrustModifier = 5;
                accelerationModifier = (shipBaseAcceleration * thrustModifier);
                accelerationVector.X = directionVector.X * (float)accelerationModifier;
                accelerationVector.Y = directionVector.Y * (float)accelerationModifier;

                Ship.Direction = accelerationVector;
            }
        }

        private static void UpdateAccelerometerInput(Accelerometer accelerometer)
        {
            //accelerationVector.Y = AccelerometerInput.Instance.AdjustedAccelerometerData.Y;
            //accelerationVector.X = AccelerometerInput.Instance.AdjustedAccelerometerData.X;
            //Ship.Rotation = (float)Math.Atan2(accelerationVector.X, -accelerationVector.Y);
            Ship.Rotation = (float)Math.Atan2(-accelerometer.CurrentValue.Acceleration.Y, accelerometer.CurrentValue.Acceleration.X);
            directionVector = new Vector2((float)Math.Sin(Ship.Rotation), -(float)Math.Cos(Ship.Rotation));
            thrustModifier = directionVector.Length() * 4;
            Vector2 temp = new Vector2(accelerometer.CurrentValue.Acceleration.X, accelerometer.CurrentValue.Acceleration.Y);
            float length = temp.Length();
            thrustModifier = length * 20;
            //thrustModifier /= 5;
            if (thrustModifier > 5)
                thrustModifier = 5;
            accelerationModifier = (shipBaseAcceleration * thrustModifier);
            accelerationVector.X = directionVector.X * (float)accelerationModifier;
            accelerationVector.Y = directionVector.Y * (float)accelerationModifier;

            Ship.Direction = accelerationVector;
        }

        //static AccelerometerReadingEventArgs accelState;
        //static Accelerometer Accelerometer;


        //public void ProcessInput()
        //{
        //    //ProcessAccelerometerInput();
        //    ProcessTouchInput();
        //}

        //private void ProcessTouchInput()
        //{
        //    //if (this.Ship == null)
        //    //{
        //    //    if (VirtualThumbsticks.RightThumbstickCenter.HasValue)
        //    //    {
        //    //        SpawnShip();
        //    //    }
        //    //}

        //    if (Ship != null)
        //    {
        //        //Ship.Direction = Vector2.Zero;
        //        //this.Ship.StartFire();

        //        if (VirtualThumbsticks.RightThumbstick.Length() > 0.0f && firing)
        //        {
        //            //Ship.Perform(RecursiveShipFire.Action_StartFire, EventArgs.Empty);
        //            Ship.StartFire();
        //            firing = false;
        //        }
        //        if (VirtualThumbsticks.RightThumbstick.Length() <= 0.0f)
        //        {
        //            //Ship.Perform(RecursiveShipFire.Action_StopFire, EventArgs.Empty);
        //            Ship.StopFire();
        //            firing = true;
        //        }

        //        if (VirtualThumbsticks.RightThumbstickCenter.HasValue)
        //            Ship.Weapon.Direction = Vector2.Normalize(VirtualThumbsticks.RightThumbstick);

        //        if (VirtualThumbsticks.LeftThumbstickCenter.HasValue)
        //            Ship.Rotation = (float)Math.Atan2(VirtualThumbsticks.LeftThumbstick.X, -VirtualThumbsticks.LeftThumbstick.Y);

        //        //Ship.Direction += VirtualThumbsticks.LeftThumbstick;

        //        //if (!VirtualThumbsticks.LeftThumbstickCenter.HasValue)
        //        //    Ship.Direction = Vector2.Zero;

        //        //if (Ship.Direction.Length() > 0f)
        //        //    Ship.Direction.Normalize();


        //        thrustModifier = VirtualThumbsticks.LeftThumbstick.Length();

        //        accelerationModifier = (shipBaseAcceleration * thrustModifier);

        //        directionVector = new Vector2((float)Math.Sin(Ship.Rotation), -(float)Math.Cos(Ship.Rotation));
        //        //accelerationVector = Vector2(directionVector.x * System.Convert.ToDouble(accelerationModifier), directionVector.y * System.Convert.ToDouble(accelerationModifier));
        //        accelerationVector.X = directionVector.X * (float)accelerationModifier;
        //        accelerationVector.Y = directionVector.Y * (float)accelerationModifier;
        //        // Set friction based on how "floaty" controls you want

        //        shipSpeedVector.X *= 0.8f; //Use a variable here
        //        shipSpeedVector.Y *= 0.8f; //<-- as well
        //        shipSpeedVector += accelerationVector;


        //        //shipPositionVector += shipSpeedVector;

        //        Ship.Direction = shipSpeedVector;

        //        //Ship.Position += shipSpeedVector;
        //    }
        //}

        //private void ProcessAccelerometerInput()
        //{
        //    //poll the acceleration value
        //    if (Ship != null)
        //    {
        //        if (VirtualThumbsticks.RightThumbstick.Length() > 0.0f && firing)
        //        {
        //            Ship.Perform(RecursiveShipFire.Action_StartFire, EventArgs.Empty);
        //            firing = false;
        //        }
        //        if (VirtualThumbsticks.RightThumbstick.Length() <= 0.0f)
        //        {
        //            //Ship.Perform(RecursiveShipFire.Action_StopFire, EventArgs.Empty);
        //            firing = true;
        //        }

        //        if (VirtualThumbsticks.RightThumbstickCenter.HasValue)
        //            Ship.Weapon.Direction = Vector2.Normalize(VirtualThumbsticks.RightThumbstick);

        //        Vector3 acceleration = Accelerometer.GetState().Acceleration;



        //        Ship.Rotation = (float)Math.Atan2(- acceleration.Y, acceleration.X);

        //        Ship.Direction = new Vector2((float)Math.Sin(Ship.Rotation), -(float)Math.Cos(Ship.Rotation));

        //        Ship.Direction *= (float)shipBaseAcceleration * 5;

        //        //this.Ship.Direction *= (float)shipBaseAcceleration;

        //    }
        //}
    }
}
