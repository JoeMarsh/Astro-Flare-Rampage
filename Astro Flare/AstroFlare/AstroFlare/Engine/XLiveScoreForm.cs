using System;
using OpenXLive.Controls;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using OpenXLive.Features;
using Microsoft.Xna.Framework.Input;

namespace OpenXLive.Forms
{
    public enum XLiveScoreFormMode
    {
        Win,
        Lose
    }

    public enum ScoreFormResult
    {
        None,
        Retry,
        NextLevel,
        ExitGame
    }

    public class ScoreFormResultEventArgs : EventArgs
    {
        public ScoreFormResultEventArgs(ScoreFormResult result)
        {
            this.Result = result;
        }

        public ScoreFormResult Result { get; protected set; }
    }

    public class XLiveScoreForm : XLiveAnimationForm
    {
        #region Callback Methods

        public event EventHandler<ScoreFormResultEventArgs> FormResultEvent;

        protected void OnScoreFormResult(ScoreFormResult result)
        {
            if (FormResultEvent != null)
            {
                FormResultEvent(this, new ScoreFormResultEventArgs(result));
            }
        }

        public override void Close()
        {
            OnScoreFormResult(ScoreFormResult.None);

            base.Close();
        }

        public virtual void Close(ScoreFormResult result)
        {
            OnScoreFormResult(result);

            base.Close();
        }

        #endregion

        #region Construct Method and Field

        public Score Score { get; protected set; }

        XLiveScoreFormMode m_ScoreFormMode;
        string m_LevelTitle;

        XLiveButton m_ButtonOK;
        XLiveButton m_ButtonBack;
        XLiveLabel m_LabelLevel;
        XLiveLabel m_LabelScore;
        XLiveLabel m_LabelSubmit;
        XLiveLabel m_LabelScoreTitle;
        XLiveLabel m_LabelEnemiesKilled;
        XLiveLabel m_LabelCoinsCollected;

        XLivePivot m_Pivot;
        XLivePhotoControl m_Photo;
        XLiveProgressBar m_ProgressBar;
        XLiveLabel m_ProgressTips;

        public XLiveScoreForm(XLiveFormManager manager/*, Score score, XLiveScoreFormMode mode, string LevelTitle*/)
            : base(manager)
        {
            // Set form title
            m_Title = "Score";

            m_Pivot = new XLivePivot(this);
            m_Pivot.Rectangle = (this.FormManager.ScreenMode == ScreenMode.Landscape)
                                    ?
                                    new Rectangle(0, 60, 800, 95)
                                    :
                                    new Rectangle(0, 70, 480, 135);
            m_Pivot.LeftSpace = (this.FormManager.ScreenMode == ScreenMode.Landscape) ? 230 : 15;
            this.AddControl(m_Pivot);

            m_LabelLevel = new XLiveLabel(this);
            this.AddControl(m_LabelLevel);

            m_Photo = new XLivePhotoControl(this);

            if (this.FormManager.CurrentSession.Profile == null)
            {
                this.FormManager.CurrentSession.GameGetProfileCompleted += new Features.AsyncEventHandler(CurrentSession_GameGetProfileCompleted);
                this.FormManager.CurrentSession.GetProfile();
            }
            else if (this.FormManager.CurrentSession.Profile.Image != null)
            {
                m_Photo.Photo = this.FormManager.CurrentSession.Profile.Image;
                m_LabelLevel.Text = this.FormManager.CurrentSession.Profile.Name;
            }
            //m_Photo.Click += new XLivePhotoControl.EventHandler(m_Photo_Click);
            this.AddControl(m_Photo);

            // Button Control
            m_ButtonOK = new XLiveButton(this);
            m_ButtonOK.FontScale = 0.9f;
            m_ButtonOK.Click += new EventHandler(m_ButtonOK_Click);
            this.AddControl(m_ButtonOK);

            m_ButtonBack = new XLiveButton(this);
            m_ButtonBack.FontScale = 0.9f;
            m_ButtonBack.Click += new EventHandler(m_ButtonCancel_Click);
            this.AddControl(m_ButtonBack);

            m_LabelScoreTitle = new XLiveLabel(this);
            m_LabelScoreTitle.Text = "Your Score: ";
            this.AddControl(m_LabelScoreTitle);

            m_LabelEnemiesKilled = new XLiveLabel(this);
            m_LabelEnemiesKilled.Text = "Enemies Killed: " + AstroFlare.Config.EmemiesKilled.ToString();
            this.AddControl(m_LabelEnemiesKilled);

            m_LabelCoinsCollected = new XLiveLabel(this);
            m_LabelCoinsCollected.Text = "Coins Collected: " + AstroFlare.Config.CoinsCollected.ToString();
            this.AddControl(m_LabelCoinsCollected);

            m_LabelScore = new XLiveLabel(this);
            m_LabelScore.Text = "00:25:23";
            this.AddControl(m_LabelScore);

            m_LabelSubmit = new XLiveLabel(this);
            m_LabelSubmit.Text = "Submit your score to OpenXLive";  //Resources.Strings.SubmitScore; //"Submit your score to OpenXLive";
            m_LabelSubmit.FontScale = 0.7f;
            this.AddControl(m_LabelSubmit);

            m_ProgressBar = new XLiveProgressBar(this);
            m_ProgressBar.Rectangle = (this.FormManager.ScreenMode == ScreenMode.Landscape)
                                        ?
                                        new Rectangle(160, 460, 50, 50)
                                        :
                                        new Rectangle(0, 740, 50, 50);
            m_ProgressBar.Visible = false;
            this.AddControl(m_ProgressBar);

            m_ProgressTips = new XLiveLabel(this);
            m_ProgressTips.Text = "Submitting";
            m_ProgressTips.FontScale = 0.9f;
            m_ProgressTips.Visible = false;
            m_ProgressTips.Rectangle = (this.FormManager.ScreenMode == ScreenMode.Landscape)
                            ?
                            new Rectangle(360, 400, 380, 55)
                            :
                            new Rectangle(200, 680, 380, 55);
            this.AddControl(m_ProgressTips);

            if (this.FormManager.ScreenMode == ScreenMode.Landscape)
            {
                m_LabelLevel.Rectangle = new Rectangle(150, 190, 370, 50);
                m_LabelScoreTitle.Rectangle = new Rectangle(200, 280, 370, 50);
                m_LabelScore.Rectangle = new Rectangle(400, 280, 370, 50);
                m_LabelSubmit.Rectangle = new Rectangle(200, 330, 550, 50);

                m_LabelEnemiesKilled.Rectangle = new Rectangle(200, 300, 370, 50);
                m_LabelCoinsCollected.Rectangle = new Rectangle(200, 315, 370, 50);

                m_ButtonOK.Rectangle = new Rectangle(50, 410, 240, 50);
                m_ButtonBack.Rectangle = new Rectangle(520, 410, 240, 50);
                m_Photo.Rectangle = new Rectangle(30, 170, 90, 90);
            }
            else
            {
                m_LabelLevel.Rectangle = new Rectangle(180, 380, 370, 50);
                m_LabelScoreTitle.Rectangle = new Rectangle(60, 460, 370, 50);
                m_LabelScore.Rectangle = new Rectangle(260, 460, 370, 50);
                m_LabelSubmit.Rectangle = new Rectangle(60, 530, 550, 50);

                m_ButtonOK.Rectangle = new Rectangle(15, 730, 240, 50);
                m_ButtonBack.Rectangle = new Rectangle(255, 730, 240, 50);
                m_Photo.Rectangle = new Rectangle(210, 250, 90, 90);
            }
        }

        #endregion

        #region Control Methods

        void CurrentSession_GameGetProfileCompleted(object sender, AsyncEventArgs e)
        {
            if (e.Result.ReturnValue)
            {
                if (this.FormManager.CurrentSession.Profile != null
                    &&
                    this.FormManager.CurrentSession.Profile.Image != null)
                {
                    m_Photo.Photo = this.FormManager.CurrentSession.Profile.Image;
                    m_LabelLevel.Text = this.FormManager.CurrentSession.Profile.Name;
                }
            }
        }

        void m_ButtonCancel_Click(object sender, EventArgs e)
        {
            if (this.m_ScoreFormMode == XLiveScoreFormMode.Win)
            {
                if (m_ButtonBack.Text == "Continue" || m_ButtonBack.Text == "Cancel")
                {
                    this.Close(ScoreFormResult.NextLevel);
                    this.FormManager.ResumeGame();

                    m_LabelSubmit.Text = "";
                }
                //else if (m_ButtonBack.Text == "Next Level")
                //{
                //    this.Close(ScoreFormResult.NextLevel);
                //    this.FormManager.ResumeGame(); // "Next Level"
                //}
            }
            else if (this.m_ScoreFormMode == XLiveScoreFormMode.Lose)
            {
                if (m_ButtonBack.Text == "Play Again")
                {
                    this.Close(ScoreFormResult.Retry);
                    this.FormManager.ResumeGame(); // "Retry"
                }
                else if (m_ButtonBack.Text == "Cancel")
                {
                    m_ButtonBack.Text = "Play Again";
                    m_ButtonOK.Text = "Exit Game";

                    m_LabelSubmit.Text = "";
                }
            }
        }

        void m_ButtonOK_Click(object sender, EventArgs e)
        {
            if (m_ButtonOK.Text == "Submit" || m_ButtonOK.Text == "Re-Submit")
            {
                SubmitScore();
            }
            else if (m_ButtonOK.Text == "Play Again")
            {
                this.Close(ScoreFormResult.Retry);
                this.FormManager.ResumeGame();
            }
        }

        private void ExitGame()
        {
            this.Close(ScoreFormResult.ExitGame);

            //this.FormManager.LeaveGame();
        }

        private void SubmitScore()
        {
            if (Score != null)
            {
                m_ButtonBack.Visible = false;
                m_ButtonOK.Visible = false;
                m_ProgressBar.Visible = true;
                m_ProgressTips.Visible = true;

                Leaderboard leaderboard = new Leaderboard(this.FormManager.CurrentSession, Score.LeaderboardId);
                leaderboard.SubmitScoreCompleted += new AsyncEventHandler(leaderboard_SubmitScoreCompleted);

                switch (Score.Type)
                {
                    case LeaderboardType.Int:
                        leaderboard.SubmitScore(Convert.ToInt32(Score.Value), Score.UserRating, Score.Comments);
                        break;

                    case LeaderboardType.Float:
                        leaderboard.SubmitScore(Convert.ToSingle(Score.Value), Score.UserRating, Score.Comments);
                        break;

                    case LeaderboardType.DateTime:
                        if (Score.Value is TimeSpan)
                        {
                            leaderboard.SubmitScore((TimeSpan)Score.Value, Score.UserRating, Score.Comments);
                        }
                        break;
                }
            }
        }

        void leaderboard_SubmitScoreCompleted(object sender, AsyncEventArgs e)
        {
            m_ButtonBack.Visible = true;
            m_ButtonOK.Visible = true;
            m_ProgressBar.Visible = false;
            m_ProgressTips.Visible = false;

            if (e.Result.ReturnValue)
            {
                Score score = e.Result.Tag as Score;

                if (score != null)
                {
                    m_LabelSubmit.Text = string.Format("Score {0} has been submit to OpenXLive!", score.Value); //"Score {0} has been submit to OpenXLive!"
                }

                if (m_ScoreFormMode == XLiveScoreFormMode.Win)
                {
                    m_ButtonBack.Text = "Continue";
                    m_ButtonOK.Text = "Play Again";
                }
                else if (m_ScoreFormMode == XLiveScoreFormMode.Lose)
                {
                    m_ButtonBack.Text = "Play Again";
                    m_ButtonOK.Text = "Exit Game";
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

                m_LabelSubmit.Text = string.Format("Would you like to re-submit the Score?"); //string.Format("Would you like to re-submit the Score?");
                // Retry
                m_ButtonBack.Text = "Cancel";
                m_ButtonOK.Text = "Re-Submit";
            }
        }

        #endregion

        #region Show Method

        //internal override bool IsChangeGameState
        //{
        //    get
        //    {
        //        return false;
        //    }
        //}

        public override void Show()
        {
            this.FormManager.PauseGame();

            base.Show();
        }

        public virtual void Show(string LeaderboardId,
                                int score
                                )
        {
            Show(LeaderboardId, score, null, null, XLiveScoreFormMode.Win, "");
        }

        public virtual void Show(string LeaderboardId,
                                int score,
                                float? UserRating,
                                string Comments
                                )
        {
            Show(LeaderboardId, score, UserRating, Comments, XLiveScoreFormMode.Win, "");
        }

        public virtual void Show(string LeaderboardId,
                                int score,
                                float? UserRating,
                                string Comments,
                                XLiveScoreFormMode mode,
                                string LevelTitle
                                )
        {
            if (LeaderboardId == null || LeaderboardId.Trim() == "")
            {
                throw new NullReferenceException("Leaderboard Id can not be null");
            }

            m_ScoreFormMode = mode;
            m_LevelTitle = LevelTitle;

            Score myScore = new Score();
            myScore.LeaderboardId = LeaderboardId;
            myScore.Type = LeaderboardType.Int;
            myScore.Value = score;

            if (UserRating != null)
            {
                myScore.UserRating = (float)UserRating;
            }

            if (Comments != null)
            {
                myScore.Comments = Comments;
            }

            this.Score = myScore;

            this.FormManager.PauseGame();

            base.Show();
        }

        public virtual void Show(string LeaderboardId,
                                float score
                                )
        {
            Show(LeaderboardId, score, null, null, XLiveScoreFormMode.Win, "");
        }

        public virtual void Show(string LeaderboardId,
                                float score,
                                float? UserRating,
                                string Comments
                                )
        {
            Show(LeaderboardId, score, UserRating, Comments, XLiveScoreFormMode.Win, "");
        }

        public virtual void Show(string LeaderboardId,
                                float score,
                                float? UserRating,
                                string Comments,
                                XLiveScoreFormMode mode,
                                string LevelTitle
                                )
        {
            if (LeaderboardId == null || LeaderboardId.Trim() == "")
            {
                throw new NullReferenceException("Leaderboard Id can not be null");
            }

            m_ScoreFormMode = mode;
            m_LevelTitle = LevelTitle;

            Score myScore = new Score();
            myScore.LeaderboardId = LeaderboardId;
            myScore.Type = LeaderboardType.Float;
            myScore.Value = score;

            if (UserRating != null)
            {
                myScore.UserRating = (float)UserRating;
            }

            if (Comments != null)
            {
                myScore.Comments = Comments;
            }

            this.Score = myScore;

            this.FormManager.PauseGame();

            base.Show();
        }

        public virtual void Show(string LeaderboardId,
                                TimeSpan score
                                )
        {
            Show(LeaderboardId, score, null, null, XLiveScoreFormMode.Win, "");
        }

        public virtual void Show(string LeaderboardId,
                                TimeSpan score,
                                float? UserRating,
                                string Comments
                                )
        {
            Show(LeaderboardId, score, UserRating, Comments, XLiveScoreFormMode.Win, "");
        }

        public virtual void Show(string LeaderboardId,
                                TimeSpan score,
                                float? UserRating,
                                string Comments,
                                XLiveScoreFormMode mode,
                                string LevelTitle
                                )
        {
            if (LeaderboardId == null || LeaderboardId.Trim() == "")
            {
                throw new NullReferenceException("Leaderboard Id can not be null");
            }

            m_ScoreFormMode = mode;
            m_LevelTitle = LevelTitle;

            Score myScore = new Score();
            myScore.LeaderboardId = LeaderboardId;
            myScore.Type = LeaderboardType.DateTime;
            myScore.Value = score;

            if (UserRating != null)
            {
                myScore.UserRating = (float)UserRating;
            }

            if (Comments != null)
            {
                myScore.Comments = Comments;
            }

            this.Score = myScore;

            this.FormManager.PauseGame();

            base.Show();
        }

        #endregion

        #region DrawableGameComponent

        public override void Initialize()
        {
            if (m_LevelTitle == null || m_LevelTitle.Trim() == "")
            {
                //m_LabelLevel.Text = this.FormManager.CurrentSession.Profile.Name;
            }
            else
            {
                m_LabelLevel.Text = m_LevelTitle;
            }

            if (Score != null)
            {
                m_LabelScore.Text = Score.Value.ToString();
            }

            if (m_ScoreFormMode == XLiveScoreFormMode.Win)
            {
                m_ButtonOK.Text = "Submit";
                m_ButtonBack.Text = "Continue";
                //m_LabelSubmit.Visible = true;
                m_Pivot.AddPage("Congratulations");
            }
            else
            {
                m_ButtonOK.Text = "Submit";
                m_ButtonBack.Text = "Play Again";
                //m_LabelSubmit.Visible = false;
                m_Pivot.AddPage("You Lose");
            }

            base.Initialize();
        }

        public override void LoadContent()
        {
            ContentManager VikingsContent = this.FormManager.Content;
            m_ButtonOK.Image = VikingsContent.Load<Texture2D>("Images/ButtonOk_50");
            m_ButtonBack.Image = VikingsContent.Load<Texture2D>("Images/ButtonBack_50");

            // Create SpriteBatch object
            m_spriteBatch = new SpriteBatch(this.FormManager.GraphicsDevice);

            // Create MaskTexture of Black Color
            m_MaskTexture = XLiveGraphicsHelper.CreatePixelTexture(this.FormManager.GraphicsDevice, Color.Black);

            base.LoadContent();
        }

        SpriteBatch m_spriteBatch;
        Texture2D m_MaskTexture;

        protected override void DrawBackground()
        {
            m_spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);

            if (Background != null)
            {
                Color MaskColor = Color.Gray;
                MaskColor.A = Convert.ToByte(MaskColor.A / 2);
                m_spriteBatch.Draw(m_MaskTexture, BackgroundRectangle, MaskColor);

                m_spriteBatch.Draw(Background, BackgroundRectangle, /*Background.Bounds*/ BackgroundSourceRectangle, Color.Gray);
            }

            m_spriteBatch.End();

            //base.DrawBackground();
        }

        #endregion

        #region Back Button Event

        bool bInit = true;

        protected override void HardwareKeyPressed()
        {
            // Do Nothing, because Back button will process twice, Form will direct back to Startup Form.
            // Bug fix: On Pause States, press Back button will return game states.
            if (this.FormManager.GameState == XLiveGameState.Form && this.FormManager.ActiveForm is XLiveScoreForm)
            {
                if (bInit)
                {
                    if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Released)
                    {
                        bInit = false;
                    }
                }
                else
                {
                    if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                    {
                        //m_ButtonReturn_Click(this, EventArgs.Empty);
                        ExitGame();
                    }
                }
            }

            //base.HardwareKeyPressed();
        }

        #endregion
    }
}
