#region File Description
//-----------------------------------------------------------------------------
// ScreenManager.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System.Diagnostics;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using OpenXLive;
using OpenXLive.Features;
//using AdDuplex.Xna;
#endregion

namespace AstroFlare
{
    /// <summary>
    /// The screen manager is a component which manages one or more GameScreen
    /// instances. It maintains a stack of screens, calls their Update and Draw
    /// methods at the appropriate times, and automatically routes input to the
    /// topmost active screen.
    /// </summary>
    public class ScreenManager : DrawableGameComponent
    {
        #region Fields


        private const string StateFilename = "ScreenManagerState.xml";

        List<GameScreen> screens = new List<GameScreen>();
        List<GameScreen> tempScreensList = new List<GameScreen>();

        InputState input = new InputState();

        GraphicsDeviceManager graphicsDeviceManager;

        SpriteBatch spriteBatch;
        SpriteFont font;
        Texture2D blankTexture;

        public Texture2D Title;
        public Texture2D backgroundTexture;

        bool isInitialized;

        bool traceEnabled;

        public XLiveFormManager XLiveManager;
        //XLiveScoreForm XLiveScoreForm;
        public XLiveStartupForm2 XLiveStartupForm;
        public static Leaderboard lb1;
        public static Leaderboard lb2;
        public static Leaderboard lb3;
        public static Leaderboard lb4;
        public static Leaderboard lb5;
        public static Leaderboard lb6;

        //AdManager adDuplex;


        #endregion

        #region Properties

        public GraphicsDeviceManager GraphicsDeviceManager
        {
            get { return graphicsDeviceManager; }
        }

        /// <summary>
        /// A default SpriteBatch shared by all the screens. This saves
        /// each screen having to bother creating their own local instance.
        /// </summary>
        public SpriteBatch SpriteBatch
        {
            get { return spriteBatch; }
        }


        /// <summary>
        /// A default font shared by all the screens. This saves
        /// each screen having to bother loading their own local copy.
        /// </summary>
        public SpriteFont Font
        {
            get { return font; }
        }

        /// <summary>
        /// If true, the manager prints out a list of all the screens
        /// each time it is updated. This can be useful for making sure
        /// everything is being added and removed at the right times.
        /// </summary>
        public bool TraceEnabled
        {
            get { return traceEnabled; }
            set { traceEnabled = value; }
        }

        /// <summary>
        /// Gets a blank texture that can be used by the screens.
        /// </summary>
        public Texture2D BlankTexture
        {
            get { return blankTexture; }
        }


        #endregion

        #region Initialization


        /// <summary>
        /// Constructs a new screen manager component.
        /// </summary>
        public ScreenManager(Game game, GraphicsDeviceManager graphicsManager)
            : base(game)
        {
            // we must set EnabledGestures before we can query for them, but
            // we don't assume the game wants to read them.
            TouchPanel.EnabledGestures = GestureType.None;

            this.graphicsDeviceManager = graphicsManager;

            XLiveManager = new XLiveFormManager(GameStateManagementGame.Instance, "u7jNRdCKf3ymwSbFY9BHDUhP");
            //XLiveManager = new XLiveFormManager(GameStateManagementGame.Instance, "M3PTvGaAWHFqr6hC8Ewmy4fd");
            GameStateManagementGame.Instance.Components.Add(XLiveManager);
            XLiveManager.OpenSession();
            //lb = new Leaderboard(XLiveManager.CurrentSession, "d94adf00-37d9-40c9-89ce-aa32c415f958");

            lb1 = new Leaderboard(XLiveManager.CurrentSession, "834a6881-e18b-4335-932d-f9bb7109eebe");
            lb1.SubmitScoreCompleted += new AsyncEventHandler(lb_SubmitScoreCompleted);

            lb2 = new Leaderboard(XLiveManager.CurrentSession, "59b11944-f0b3-4a55-b9d0-fc317913c97f");
            lb2.SubmitScoreCompleted += new AsyncEventHandler(lb_SubmitScoreCompleted);

            lb3 = new Leaderboard(XLiveManager.CurrentSession, "cf583dd9-81f1-4191-a885-c9963f007891");
            lb3.SubmitScoreCompleted += new AsyncEventHandler(lb_SubmitScoreCompleted);

            lb4 = new Leaderboard(XLiveManager.CurrentSession, "996e6b9c-d42a-4886-9f5a-32b3c10d30a5");
            lb4.SubmitScoreCompleted += new AsyncEventHandler(lb_SubmitScoreCompleted);

            lb5 = new Leaderboard(XLiveManager.CurrentSession, "2ca96bc0-e2f0-461f-b46b-5ba59445d4c6");
            lb5.SubmitScoreCompleted += new AsyncEventHandler(lb_SubmitScoreCompleted);

            lb6 = new Leaderboard(XLiveManager.CurrentSession, "19a9d790-8941-41f0-b297-5d93d88936b8");
            lb6.SubmitScoreCompleted += new AsyncEventHandler(lb_SubmitScoreCompleted);
        }

        public void SubmitScore(float score, int level)
        {
            switch (level)
            {
                case 1:
                    lb1.SubmitScore(score);
                    break;
                case 2:
                    lb2.SubmitScore(score);
                    break;
                case 3:
                    lb3.SubmitScore(score);
                    break;
                case 4:
                    lb4.SubmitScore(score);
                    break;
                case 5:
                    lb5.SubmitScore(score);
                    break;
                case 6:
                    lb6.SubmitScore(score);
                    break;
            }
        }

        void lb_SubmitScoreCompleted(object sender, AsyncEventArgs e)
        {
            if (e.Result.ReturnValue)
            {
                Score score = e.Result.Tag as Score;

                if (score != null)
                {
                    //m_LabelSubmit.Text = string.Format("Score {0} has been submit to OpenXLive!", score.Value);
                }
            }
            else
            {
                if (OpenXLive.Service.ErrorHandler.IsNetworkError(e.Result.ErrorCode))
                {
                    //MessageBox.Show("There was a problem connecting to the OpenXLive servers. Please check the connection status. Some online features may not be available.");
                    MessageBox.Show("There was a problem connecting to the OpenXLive servers. Please check the connection status. Some online features may not be available.");
                }
                else
                {
                    MessageBox.Show(e.Result.ErrorMessage);
                } 
            }
        }
       
        /// <summary>
        /// Initializes the screen manager component.
        /// </summary>
        public override void Initialize()
        {
            //Accelerometer.Initialize();

            base.Initialize();

            isInitialized = true;
        }


        /// <summary>
        /// Load your graphics content.
        /// </summary>
        protected override void LoadContent()
        {
            // Load content belonging to the screen manager.
            ContentManager content = Game.Content;

            spriteBatch = new SpriteBatch(GraphicsDevice);
            //font = content.Load<SpriteFont>("menufont");
            font = content.Load<SpriteFont>(@"Fonts\Pericles");
            blankTexture = content.Load<Texture2D>("blank");

            Title = content.Load<Texture2D>(@"GameScreens\Title");
            //backgroundTexture = content.Load<Texture2D>(@"GameScreens\LeaderboardBackground");
            //XLiveManager.Background = backgroundTexture;


            //Game.Components.Add(new AstroFlare.extensions.GARBAGE_COLLECTIONS_COUNTER(Game, spriteBatch, font));
            //Game.Components.Add(new AstroFlare.extensions.FPS_COUNTER(Game, spriteBatch, font));


            // Tell each of the screens to load their content.
            foreach (GameScreen screen in screens)
            {
                screen.Activate(false);
            }

            //XLiveStartupForm = new XLiveStartupForm2(this.XLiveManager, this);
            //XLiveStartupForm.Background = backgroundTexture;
            //XLiveStartupForm.ExitButton = false;
            //XLiveStartupForm.ContinueButton = false;
            //XLiveStartupForm.NewgameButton = false;
            //XLiveStartupForm.HelpButton = false;
            
            //XLiveStartupForm.FormManager.ShutdownGameEvent += new EventHandler(FormManager_ShutdownGameEvent);
            //XLiveStartupForm.FormManager.LeaveGameEvent += new EventHandler(FormManager_LeaveGameEvent);

            //adDuplex = new AdManager(this.Game, "3099");
            //adDuplex.LoadContent();
            //adDuplex.IsTest = false;
            //adDuplex.Enabled = false;
        }

        //void FormManager_LeaveGameEvent(object sender, EventArgs e)
        //{
        //    XLiveManager.ResumeGame();
        //}

        //void FormManager_ShutdownGameEvent(object sender, EventArgs e)
        //{
        //    XLiveManager.ResumeGame();
        //}



        /// <summary>
        /// Unload your graphics content.
        /// </summary>
        protected override void UnloadContent()
        {
            // Tell each of the screens to unload their content.
            foreach (GameScreen screen in screens)
            {
                screen.Unload();
            }
        }


        #endregion

        #region Update and Draw

        //public static bool ScreenManagerInput = true;
        /// <summary>
        /// Allows each screen to run logic.
        /// </summary>
        public override void Update(GameTime gameTime)
        {
            if (XLiveManager.IsRunning)
            {
                //adDuplex.Update(gameTime);

                // Read the keyboard and gamepad.
                //if (ScreenManagerInput)
                    input.Update();

                // Make a copy of the master screen list, to avoid confusion if
                // the process of updating one screen adds or removes others.
                tempScreensList.Clear();

                foreach (GameScreen screen in screens)
                    tempScreensList.Add(screen);

                bool otherScreenHasFocus = !Game.IsActive;
                bool coveredByOtherScreen = false;

                // Loop as long as there are screens waiting to be updated.
                while (tempScreensList.Count > 0)
                {
                    // Pop the topmost screen off the waiting list.
                    GameScreen screen = tempScreensList[tempScreensList.Count - 1];

                    tempScreensList.RemoveAt(tempScreensList.Count - 1);

                    // Update the screen.
                    screen.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);

                    if (screen.ScreenState == ScreenState.TransitionOn ||
                        screen.ScreenState == ScreenState.Active)
                    {
                        // If this is the first active screen we came across,
                        // give it a chance to handle input.
                        if (!otherScreenHasFocus)
                        {
                            screen.HandleInput(gameTime, input);

                            otherScreenHasFocus = true;
                        }

                        // If this is an active non-popup, inform any subsequent
                        // screens that they are covered by it.
                        if (!screen.IsPopup)
                            coveredByOtherScreen = true;
                    }
                }

                // Print debug trace?
                if (traceEnabled)
                    TraceScreens();
            }
        }


        /// <summary>
        /// Prints a list of all the screens, for debugging.
        /// </summary>
        void TraceScreens()
        {
            List<string> screenNames = new List<string>();

            foreach (GameScreen screen in screens)
                screenNames.Add(screen.GetType().Name);

            Debug.WriteLine(string.Join(", ", screenNames.ToArray()));
        }


        /// <summary>
        /// Tells each screen to draw itself.
        /// </summary>
        public override void Draw(GameTime gameTime)
        {
            if (XLiveManager.IsRunning)
            {


                int count = screens.Count;
                for (int i = 0; i < count; i++)
                {
                    if (screens[i].ScreenState == ScreenState.Hidden)
                        continue;

                    screens[i].Draw(gameTime);
                }

                //adDuplex.Draw(spriteBatch, new Vector2(320, 0), true);


                //foreach (GameScreen screen in screens)
                //{
                //    if (screen.ScreenState == ScreenState.Hidden)
                //        continue;

                //    screen.Draw(gameTime);
                //}
                
            }
        }


        #endregion

        #region Public Methods


        /// <summary>
        /// Adds a new screen to the screen manager.
        /// </summary>
        public void AddScreen(GameScreen screen, PlayerIndex? controllingPlayer)
        {
            screen.ControllingPlayer = controllingPlayer;
            screen.ScreenManager = this;
            screen.IsExiting = false;

            // If we have a graphics device, tell the screen to load content.
            if (isInitialized)
            {
                screen.Activate(false);
            }

            screens.Add(screen);

            // update the TouchPanel to respond to gestures this screen is interested in
            TouchPanel.EnabledGestures = screen.EnabledGestures;
        }


        /// <summary>
        /// Removes a screen from the screen manager. You should normally
        /// use GameScreen.ExitScreen instead of calling this directly, so
        /// the screen can gradually transition off rather than just being
        /// instantly removed.
        /// </summary>
        public void RemoveScreen(GameScreen screen)
        {
            // If we have a graphics device, tell the screen to unload content.
            if (isInitialized)
            {
                screen.Unload();
            }

            screens.Remove(screen);
            tempScreensList.Remove(screen);

            // if there is a screen still in the manager, update TouchPanel
            // to respond to gestures that screen is interested in.
            if (screens.Count > 0)
            {
                TouchPanel.EnabledGestures = screens[screens.Count - 1].EnabledGestures;
            }
        }


        /// <summary>
        /// Expose an array holding all the screens. We return a copy rather
        /// than the real master list, because screens should only ever be added
        /// or removed using the AddScreen and RemoveScreen methods.
        /// </summary>
        public GameScreen[] GetScreens()
        {
            return screens.ToArray();
        }


        /// <summary>
        /// Helper draws a translucent black fullscreen sprite, used for fading
        /// screens in and out, and for darkening the background behind popups.
        /// </summary>
        public void FadeBackBufferToBlack(float alpha)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(blankTexture, GraphicsDevice.Viewport.Bounds, Color.Black * alpha);
            spriteBatch.End();
        }

        /// <summary>
        /// Informs the screen manager to serialize its state to disk.
        /// </summary>
        public void Deactivate()
        {
            return;
//#if !WINDOWS_PHONE
//            return;
//#else
//            // Open up isolated storage
//            using (IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForApplication())
//            {
//                // Create an XML document to hold the list of screen types currently in the stack
//                XDocument doc = new XDocument();
//                XElement root = new XElement("ScreenManager");
//                doc.Add(root);

//                // Make a copy of the master screen list, to avoid confusion if
//                // the process of deactivating one screen adds or removes others.
//                tempScreensList.Clear();
//                foreach (GameScreen screen in screens)
//                    tempScreensList.Add(screen);

//                // Iterate the screens to store in our XML file and deactivate them
//                foreach (GameScreen screen in tempScreensList)
//                {
//                    // Only add the screen to our XML if it is serializable
//                    if (screen.IsSerializable)
//                    {
//                        // We store the screen's controlling player so we can rehydrate that value
//                        string playerValue = screen.ControllingPlayer.HasValue
//                            ? screen.ControllingPlayer.Value.ToString()
//                            : "";

//                        root.Add(new XElement(
//                            "GameScreen",
//                            new XAttribute("Type", screen.GetType().AssemblyQualifiedName),
//                            new XAttribute("ControllingPlayer", playerValue)));
//                    }

//                    // Deactivate the screen regardless of whether we serialized it
//                    screen.Deactivate();
//                }

//                // Save the document
//                using (IsolatedStorageFileStream stream = storage.CreateFile(StateFilename))
//                {
//                    doc.Save(stream);
//                }
//            }
//#endif
        }

        public bool Activate(bool instancePreserved)
        {
            return false;
//#if !WINDOWS_PHONE
//            return false;
//#else
//            // If the game instance was preserved, the game wasn't dehydrated so our screens still exist.
//            // We just need to activate them and we're ready to go.
//            if (instancePreserved)
//            {
//                // Make a copy of the master screen list, to avoid confusion if
//                // the process of activating one screen adds or removes others.
//                tempScreensList.Clear();

//                foreach (GameScreen screen in screens)
//                    tempScreensList.Add(screen);

//                foreach (GameScreen screen in tempScreensList)
//                    screen.Activate(true);
//            }

//            // Otherwise we need to refer to our saved file and reconstruct the screens that were present
//            // when the game was deactivated.
//            else
//            {
//                // Try to get the screen factory from the services, which is required to recreate the screens
//                IScreenFactory screenFactory = Game.Services.GetService(typeof(IScreenFactory)) as IScreenFactory;
//                if (screenFactory == null)
//                {
//                    throw new InvalidOperationException(
//                        "Game.Services must contain an IScreenFactory in order to activate the ScreenManager.");
//                }

//                // Open up isolated storage
//                using (IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForApplication())
//                {
//                    // Check for the file; if it doesn't exist we can't restore state
//                    if (!storage.FileExists(StateFilename))
//                        return false;

//                    // Read the state file so we can build up our screens
//                    using (IsolatedStorageFileStream stream = storage.OpenFile(StateFilename, FileMode.Open))
//                    {
//                        XDocument doc = XDocument.Load(stream);

//                        // Iterate the document to recreate the screen stack
//                        foreach (XElement screenElem in doc.Root.Elements("GameScreen"))
//                        {
//                            // Use the factory to create the screen
//                            Type screenType = Type.GetType(screenElem.Attribute("Type").Value);
//                            GameScreen screen = screenFactory.CreateScreen(screenType);

//                            // Rehydrate the controlling player for the screen
//                            PlayerIndex? controllingPlayer = screenElem.Attribute("ControllingPlayer").Value != ""
//                                ? (PlayerIndex)Enum.Parse(typeof(PlayerIndex), screenElem.Attribute("ControllingPlayer").Value, true)
//                                : (PlayerIndex?)null;
//                            screen.ControllingPlayer = controllingPlayer;

//                            // Add the screen to the screens list and activate the screen
//                            screen.ScreenManager = this;
//                            screens.Add(screen);
//                            screen.Activate(false);

//                            // update the TouchPanel to respond to gestures this screen is interested in
//                            TouchPanel.EnabledGestures = screen.EnabledGestures;
//                        }
//                    }
//                }
//            }

//            return true;
//#endif
        }

        #endregion
    }
}
