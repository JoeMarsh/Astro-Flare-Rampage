// Version 0.1
// Copyright 2011 Michael B. McLaughlin. License conditional on your acceptance of the terms of the Microsoft Public License,
// a copy of which should be included with this and which can also be found at http://www.bobtacoindustries.com/developers/Ms-PL.aspx .
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
// You must add a reference to "Microsoft.Devices.Sensors" to your game project's References in Solution Explorer
// to get this namespace.
using Microsoft.Devices.Sensors;

namespace AstroFlare
{
	/// <summary>
	/// A helper class used to get accelerometer input for an XNA-based Windows Phone 7 game. You must add a reference
	/// to "Microsoft.Devices.Sensors" to your game project's References in Solution Explorer in order
	/// to use this class. This class currently only measures tilt-style input. Significant motion input will cause
	/// anomalies in the readings.
	/// </summary>
	public class AccelerometerInput : GameComponent
	{
		/// <summary>
		/// Our accelerometer instance.
		/// </summary>
		private Accelerometer accelerometer;

		/// <summary>
		/// Used to detect whether the accelerometer has started up; also used as part of the
		/// mechanism to fail out gracefully in the event that the accelerometer could not be started.
		/// </summary>
		private bool isAccelerometerRunning;

		/// <summary>
		/// Used to detect whether the accelerometer has started up.
		/// </summary>
		public bool IsAccelerometerRunning
		{
			get
			{
				return isAccelerometerRunning;
			}
		}

		/// <summary>
		/// We only try to startup the accelerometer so many times, then we exit out gracefully to prevent
		/// the program from flailing about indefinitely. 100 is a magic number. Adjust it based on your
		/// own experience. 100 gives a minimum of 3.33333 seconds worth of attempts (more, actually, since
		/// the first attempt is made in LoadContent and then there's going to be some delay before the next
		/// attempt is made in the game loop (assuming it didn't succeed in LoadContent, of course)).
		/// </summary>
		const int numberOfAccelerometerStartAttemptsToMake = 100;

		/// <summary>
		/// Tracks how many times we've tried to start the accelerometer. Used as part of the mechanism
		/// to fail out gracefully in the event that the accelerometer could not be started.
		/// </summary>
		int accelerometerStartAttemptsCount;

		/// <summary>
		/// If true, the accelerometer could not be started and you should exit after displaying a suitable
		/// message to the user and giving them some time to read it.
		/// </summary>
		private bool exitDueToAccelerometerFailure;

		/// <summary>
		/// If true, the accelerometer could not be started and you should exit after displaying a suitable
		/// message to the user and giving them some time to read it.
		/// </summary>
		public bool ExitDueToAccelerometerFailure
		{
			get
			{
				return exitDueToAccelerometerFailure;
			}
		}

		/// <summary>
		/// Stores our accelerometer readings in a FIFO list so that we can average the
		/// last <see cref="numberOfReadingsToAverage"/> readings in order to smooth out movement.
		/// </summary>
		Queue<Vector3> accelerometerDataQueue;

		/// <summary>
		/// Stores our modified accelerometer readings in a FIFO list 
		/// </summary>
		Queue<Vector2> adjustedAccelerometerDataQueue;

		/// <summary>
		/// The number of accelerometer readings to average. Too many makes your controls seem sluggish; too
		/// few makes them seem jumpy. The default is 20. 20 is a magic number and you should adjust it based
		/// on playtesting your game. The value should be greater than zero or this will never work.
		/// </summary>
		const int numberOfReadingsToAverage = 15;

		/// <summary>
		/// A lock used to make sure we aren't reading and updating the accelerometer data at the same time.
		/// </summary>
		private static readonly object accelerometerDataLock = new object();

		/// <summary>
		/// Set to true the first time we receive accelerometer data. Until then, the data will be all zeroes.
		/// </summary>
		public bool HasReceivedAccelerometerData { get; private set; }

		/// <summary>
		/// The accelerometer data in an adjusted form where the X component represents
		/// the amount of left/right tilt (regardless of orientation) and the Y component
		/// represents the amount of toward/away tilt (regardless of orientation). The
		/// input is averaged and massaged using 
		/// </summary>
		private Vector2 adjustedAccelerometerData;

		/// <summary>
		/// The accelerometer data in an adjusted form where the X component represents
		/// the amount of left/right tilt (regardless of orientation) and the Y component
		/// represents the amount of toward/away tilt (regardless of orientation). The
		/// input is averaged and massaged using 
		/// </summary>
		public Vector2 AdjustedAccelerometerData
		{
			get
			{
				lock (accelerometerDataLock)
				{
					return adjustedAccelerometerData;
				}
			}
		}

		/// <summary>
		/// The average of a maximum of <see cref="numberOfReadingsToAverage"/> readings of the raw accelerometer data.
		/// </summary>
		private Vector3 rawAccelerometerData;

		/// <summary>
		/// The average of a maximum of <see cref="numberOfReadingsToAverage"/> readings of the raw accelerometer data.
		/// </summary>
		public Vector3 RawAccelerometerData
		{
			get
			{
				lock (accelerometerDataLock)
				{
					return rawAccelerometerData;
				}
			}
		}

		/// <summary>
		/// Values for the X value of <see cref="AdjustedAccelerometerData"/> that are within +/- this
		/// value from zero will be reported as false when querying
		/// <see cref="IsTiltedLeft"/> and <see cref="IsTiltedRight"/>. The default value is 4.
		/// A value of zero will produce no dead zone. A value above 40 will require significant
		/// tilting to produce a result. Values above 70 begin to run the risk of never registering
		/// a reading at all. The underlying values of <see cref="AdjustedAccelerometerData"/> are
		/// unaffected by changes to this.
		/// </summary>
		private int tiltLeftRightDeadZone = 0;

		/// <summary>
		/// Values for the X value of <see cref="AdjustedAccelerometerData"/> that are within +/- this
		/// value from zero will be reported as false when querying
		/// <see cref="IsTiltedLeft"/> and <see cref="IsTiltedRight"/>. The default value is 4.
		/// A value of zero will produce no dead zone. A value above 40 will require significant
		/// tilting to produce a result. Values above 70 begin to run the risk of never registering
		/// a reading at all. The underlying values of <see cref="AdjustedAccelerometerData"/> are
		/// unaffected by changes to this.
		/// </summary>
		public int TiltLeftRightDeadZone
		{
			get
			{
				return tiltLeftRightDeadZone;
			}
			set
			{
				lock (accelerometerDataLock)
				{
					tiltLeftRightDeadZone = value;
				}
			}
		}

		/// <summary>
		/// Returns true if the orientation-adjusted left side of the device is tilted down.
		/// </summary>
		public bool IsTiltedLeft
		{
			get
			{
				lock (accelerometerDataLock)
				{
					return adjustedAccelerometerData.X + tiltLeftRightDeadZone < 0;
				}
			}
		}

		/// <summary>
		/// Returns true if the orientation-adjusted right side of the device is tilted down.
		/// </summary>
		public bool IsTiltedRight
		{
			get
			{
				lock (accelerometerDataLock)
				{
					return adjustedAccelerometerData.X - tiltLeftRightDeadZone > 0;
				}
			}
		}

		/// <summary>
		/// Values for the Y value of <see cref="AdjustedAccelerometerData"/> that are within +/- this
		/// value from zero will be reported as false when querying
		/// <see cref="IsTiltedToward"/> and <see cref="IsTiltedAway"/>. The default value is 0.
		/// A value of zero will produce no dead zone. A value above 20 will require significant
		/// tilting to produce a result. Values above 40 begin to run the risk of never registering
		/// a reading at all. The underlying values of <see cref="AdjustedAccelerometerData"/> are
		/// unaffected by changes to this.
		/// </summary>
		private int tiltTowardAwayDeadZone = 0;

		/// <summary>
		/// Values for the Y value of <see cref="AdjustedAccelerometerData"/> that are within +/- this
		/// value from zero will be reported as false when querying
		/// <see cref="IsTiltedToward"/> and <see cref="IsTiltedAway"/>. The default value is 0.
		/// A value of zero will produce no dead zone. A value above 20 will require significant
		/// tilting to produce a result. Values above 40 begin to run the risk of never registering
		/// a reading at all. The underlying values of <see cref="AdjustedAccelerometerData"/> are
		/// unaffected by changes to this.
		/// </summary>
		public int TiltTowardAwayDeadZone
		{
			get
			{
				return tiltTowardAwayDeadZone;
			}
			set
			{
				lock (accelerometerDataLock)
				{
					tiltTowardAwayDeadZone = value;
				}
			}
		}

		/// <summary>
		/// Returns true if the orientation-adjusted top of the device is tilted towards the user.
		/// </summary>
		public bool IsTiltedToward
		{
			get
			{
				lock (accelerometerDataLock)
				{
					return adjustedAccelerometerData.Y - tiltTowardAwayDeadZone > 0;
				}
			}
		}

		/// <summary>
		/// Returns true if the orientation-adjusted top of the device is tilted away from the user.
		/// </summary>
		public bool IsTiltedAway
		{
			get
			{
				lock (accelerometerDataLock)
				{
					return adjustedAccelerometerData.Y + tiltTowardAwayDeadZone < 0;
				}
			}
		}

		/// <summary>
		/// When true, writes out the current raw data using System.Diagnostics.Debug.WriteLine
		/// as {X} {Y} {Z} with F2 formatting whenever a new accelerometer reading is received.
		/// </summary>
		public bool WriteOutDiagnostics { get; set; }

		/// <summary>
		/// An integer value between -100 (Left) and 100 (Right) indicating which way the user is tilting the phone.
		/// </summary>
		public int TiltLeftRightReading
		{
			get
			{
				lock (accelerometerDataLock)
				{
					return (int)adjustedAccelerometerData.X;
				}
			}
		}

		/// <summary>
		/// An integer value between approximately -100 (Away) and approximately +100 (Toward) indicating
		/// which way the user is tilting the "top"
		/// of the phone in relation to the user.
		/// </summary>
		public int TiltTowardAwayReading
		{
			get
			{
				lock (accelerometerDataLock)
				{
					return (int)adjustedAccelerometerData.Y;
				}
			}
		}

		/// <summary>
		/// The amount to add to the default tilt left/right value before massaging the input. Default is 0.08f.
		/// </summary>
		private float tiltLeftRightAdjustment = 0.00f;

		/// <summary>
		/// The amount to add to the default tilt left/right value before massaging the input. Default is 0.08f.
		/// </summary>
		public float TiltLeftRightAdjustment
		{
			get
			{
				return tiltLeftRightAdjustment;
			}
			set
			{
				lock (accelerometerDataLock)
				{
					tiltLeftRightAdjustment = value;
				}
			}
		}

		/// <summary>
		/// The amount to add to the raw tilt toward/away value before massaging the input in landscape mode.
		/// Default is 0.38f.
		/// </summary>
		private const float tiltTowardAwayLandscapeAdjustment = 0.83f;

		/// <summary>
		/// The amount to add to the raw tilt toward/away value before massaging the input in landscape mode.
		/// Default is 0.48f.
		/// </summary>
		private const float tiltTowardAwayPortraitAdjustment = 0.48f;

		// TODO: Change the default value of tiltTowardAwayAdjustment depending on your game's orientation!
		/// <summary>
		/// The amount to add to the raw tilt toward/away value before massaging the input.
		/// Default is the landscape adjustment amount. Change this if your game will be in portrait mode.
		/// </summary>
		private float tiltTowardAwayAdjustment = tiltTowardAwayLandscapeAdjustment;

		/// <summary>
		/// The amount to add to the raw tilt toward/away value before massaging the input. Default is 0.48f in
		/// portrait mode and 0.38f in landscape mode. This should be a value that allows the user to hold the
		/// phone tilted comfortably and both tilt toward and away without contorting in strange ways.
		/// </summary>
		public float TiltTowardAwayAdjustment
		{
			get
			{
				return tiltTowardAwayAdjustment;
			}
			set
			{
				lock (accelerometerDataLock)
				{
					tiltTowardAwayAdjustment = value;
				}
			}
		}

		/// <summary>
		/// The power curve value to apply. See: http://blogs.msdn.com/b/shawnhar/archive/2007/03/30/massaging-thumbsticks.aspx
		/// </summary>
		private float powerCurveValue = 1;

		/// <summary>
		/// The power curve value to apply. See: http://blogs.msdn.com/b/shawnhar/archive/2007/03/30/massaging-thumbsticks.aspx .
		/// The default value is 3.0f, which lessens the impact of minor reading changes.
		/// </summary>
		public float PowerCurveValue
		{
			get
			{
				return powerCurveValue;
			}
			set
			{
				powerCurveValue = value;
			}
		}

		/// <summary>
		/// A hack to work around the fact that, initially, the XNA Framework will report Portrait for the DisplayOrientation
		/// even when the back buffer width is greater than the height (and thus LandscapeLeft should thus be reported).
		/// </summary>
		private bool isActuallyLandscapeLeft;

		/// <summary>
		/// If true then this object has been disposed.
		/// </summary>
		private bool isDisposed;

		/// <summary>
		/// If true then this object has been disposed.
		/// </summary>
		public bool IsDisposed
		{
			get
			{
				return isDisposed;
			}
		}

		/// <summary>
		/// A static accessor to the instance of this class. You cannot have more than one instance so this is an effective way of exposing it.
		/// </summary>
		private static AccelerometerInput _instance;

		/// <summary>
		/// A static accessor to the instance of this class. You cannot have more than one instance so this is an effective way of exposing it.
		/// </summary>
		public static AccelerometerInput Instance
		{
			get
			{
				return _instance;
			}
		}

		/// <summary>
		/// The constructor for this game component. Do not create more than one instance of this.
		/// </summary>
		/// <param name="game">The game this component is being created for.</param>
		/// <exception cref="InvalidOperationException">Thrown when you try to create more than one instance of this game component.</exception>
		public AccelerometerInput()
			: base(null)
		{
			// Create the accelerometer data queue. We create it with numberOfReadingsToAverage + 1 so that
			// we can just add readings and then if it's over the count, dequeue the first one without creating
			// any allocations.
			accelerometerDataQueue = new Queue<Vector3>(numberOfReadingsToAverage + 1);
			adjustedAccelerometerDataQueue = new Queue<Vector2>(numberOfReadingsToAverage + 1);

			// Create our accelerometer instance
			accelerometer = new Accelerometer();

			// Hook up the ReadingChanged event handler.
			accelerometer.ReadingChanged += new EventHandler<AccelerometerReadingEventArgs>(accelerometer_ReadingChanged);

			if (_instance != null)
			{
				throw new InvalidOperationException("You must only have one instance of this game component. You have tried to create a second one.");
			}
			_instance = this;
		}

		/// <summary>
		/// Releases the unmanaged resources used by the AccelerometerInput and optionally
		/// releases the managed resources.
		/// </summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
		protected override void Dispose(bool disposing)
		{
			if (!isDisposed)
			{
                if (disposing)
                {
                    // Properly dispose of the accelerometer
                    try
                    {
                        if (accelerometer != null)
                        {
                            lock (accelerometerDataLock)
                            {
                                // Always unregister event handlers to avoid a zombie objects, even where you're sure it'll die anyway.
                                accelerometer.ReadingChanged -= accelerometer_ReadingChanged;
                                accelerometer.Dispose();
                                accelerometer = null;
                            }
                        }
                    }
                    catch
                    {
                        // Goonies never say die and Dispose never throws exceptions!
                    }
                }
				_instance = null;
				isDisposed = true;
			}
			try
			{
				base.Dispose(disposing);
			}
			catch
			{
			}
		}

		/// <summary>
		/// Attempts to start the accelerometer.
		/// </summary>
		private void TryStart()
		{
			// Attempt to start the accelerometer. As the documentation warns, this could throw an exception, ergo the
			// try-catch block and the elaborate machinery to retry this a number of times before failing out gracefully.
			try
			{
				// Handle the fact that the framework initially reports the wrong orientation when in landscape.
				if (Game.GraphicsDevice != null)
				{
					if (Game.GraphicsDevice.PresentationParameters.BackBufferWidth > Game.GraphicsDevice.PresentationParameters.BackBufferHeight)
					{
						isActuallyLandscapeLeft = true;
					}
				}
				// Start the accelerometer
				accelerometer.Start();
				// If that didn't throw an exception, set isAccelerometerRunning to true.
				isAccelerometerRunning = true;

				//// Here is where you would disable idle detection. The following line does that, however no code is present
				//// that would compensate for it. You should implement your own timeout code if you disable idle detection.
				//// As such, this line is commented out. Simply uncommenting it without adding in a suitable replacement
				//// is a recipe for failure. This code requires that you add a reference to Microsoft.Phone to your game
				//// project's references.
				//Microsoft.Phone.Shell.PhoneApplicationService.Current.UserIdleDetectionMode = Microsoft.Phone.Shell.IdleDetectionMode.Disabled;
			}
			catch
			{
				// It failed, so increment the start attempts count.
				accelerometerStartAttemptsCount++;
				// If the number of start attempts has passed the number we said to allow, begin exiting out gracefully.
				if (accelerometerStartAttemptsCount > numberOfAccelerometerStartAttemptsToMake)
				{
					exitDueToAccelerometerFailure = true;
				}
			}
		}

		/// <summary>
		/// Our accelerometer ReadingChanged event handler.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The AccelerometerReadingEventArgs.</param>
		private void accelerometer_ReadingChanged(object sender, AccelerometerReadingEventArgs e)
		{
			Vector2 result;
			double x, y;
			switch (Game.Window.CurrentOrientation)
			{
				case DisplayOrientation.LandscapeLeft:
					{
						//System.Diagnostics.Debug.WriteLine("LandscapeLeft");
						x = -(e.Y + tiltLeftRightAdjustment);
						y = e.Z + tiltTowardAwayAdjustment;
					}
					break;

				case DisplayOrientation.LandscapeRight:
					//System.Diagnostics.Debug.WriteLine("LandscapeRight");
					x = e.Y + tiltLeftRightAdjustment;
					y = e.Z + tiltTowardAwayAdjustment;
					break;

				case DisplayOrientation.Portrait:
					// A bug in the framework requires this bit of hack-y workaround code.
					if (isActuallyLandscapeLeft)
					{
						//System.Diagnostics.Debug.WriteLine("LandscapeLeft portrait hack");
						x = -(e.Y + tiltLeftRightAdjustment);
						y = e.Z + tiltTowardAwayAdjustment;
					}
					else
					{
						//System.Diagnostics.Debug.WriteLine("Portrait");
						x = e.X + tiltLeftRightAdjustment;
						y = e.Z + tiltTowardAwayAdjustment;
					}
					break;

				default:
					System.Diagnostics.Debug.WriteLine("DisplayOrientation.Default? This will not end well...");
					x = e.Y + tiltLeftRightAdjustment;
					y = -(e.Z + tiltTowardAwayAdjustment);
					break;
			}

			if (WriteOutDiagnostics)
			{
				System.Diagnostics.Debug.WriteLine("{0} {1} {2}", e.X.ToString("F2"), e.Y.ToString("F2"), e.Z.ToString("F2"));
			}

			// Create the adjusted data result. We apply a power curve, multiply by 100, and then cast to
			// an int to ditch remainders and avoid excess precision that we don't really care about. You can
			// adjust this as desired.
			result = new Vector2(
				(int)((Math.Pow((x < 0 ? -x : x), powerCurveValue) * (x < 0 ? -1 : 1)) * 100),
				(int)((Math.Pow((y < 0 ? -y : y), powerCurveValue) * (y < 0 ? -1 : 1)) * 100));

			// Lock while we update rawAccelerometerData with the new reading.
			lock (accelerometerDataLock)
			{
                // If accelerometer is null and we should thus ignore this reading.
                if (accelerometer == null)
                {
                    return;
                }

				// Set hasReceivedAccelerometerData to true so that we know that we're getting data.
				HasReceivedAccelerometerData = true;

				// Add the latest raw data to the raw data queue.
				accelerometerDataQueue.Enqueue(new Vector3((float)e.X, (float)e.Y, (float)e.Z));

				// Add the latest data to the adjusted data queue.
				adjustedAccelerometerDataQueue.Enqueue(result);

				// If the length of the queues are greater than numberOfReadingsToAverage, remove the earliest readings.
				if (accelerometerDataQueue.Count > numberOfReadingsToAverage)
				{
					accelerometerDataQueue.Dequeue();
				}
				if (adjustedAccelerometerDataQueue.Count > numberOfReadingsToAverage)
				{
					adjustedAccelerometerDataQueue.Dequeue();
				}

				// Reset rawAccelerometerData to all zeroes so that we can average it.
				rawAccelerometerData = Vector3.Zero;
				// Reset adjustedAccelerometerData to all zeroes so that we can average it.
				adjustedAccelerometerData = Vector2.Zero;

				// Add each reading to rawAccelerometerData
				foreach (var item in accelerometerDataQueue)
				{
					rawAccelerometerData += item;
				}

				foreach (var item in adjustedAccelerometerDataQueue)
				{
					adjustedAccelerometerData += item;
				}


				// Divide each element by the number of items contained in the queue to get the average
				// and cast as an int to get rid of excess precision that we don't want.
				rawAccelerometerData.X = (rawAccelerometerData.X / accelerometerDataQueue.Count);
				rawAccelerometerData.Y = (rawAccelerometerData.Y / accelerometerDataQueue.Count);
				rawAccelerometerData.Z = (rawAccelerometerData.Z / accelerometerDataQueue.Count);

				adjustedAccelerometerData.X = (int)(adjustedAccelerometerData.X / adjustedAccelerometerDataQueue.Count);
				adjustedAccelerometerData.Y = (int)(adjustedAccelerometerData.Y / adjustedAccelerometerDataQueue.Count);
			}
		}

		/// <summary>
		/// The Update method for this GameComponent. It tries to start the accelerometer either until it succeeds
		/// or until 
		/// </summary>
		/// <param name="gameTime"></param>
		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);

			if (!isAccelerometerRunning && !exitDueToAccelerometerFailure)
			{
				TryStart();
			}
		}
	}
}
