using OpenXLive;
using OpenXLive.Forms;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace AstroFlare
{
    public class XLiveStartupForm2 : XLiveStartupForm
    {
        ScreenManager screenManager;
        //InputState input;
        XLiveFormManager manager;

        public XLiveStartupForm2(XLiveFormManager manager, ScreenManager screenManager)
            : base(manager) 
        {
            this.screenManager = screenManager;
            //this.input = input;
            this.manager = manager;
        }

        protected override void HardwareKeyPressed()
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
            {
                //this.Close();
                //manager.ResumeGame();
                manager.ContinueGame();
                //Thread.Sleep(5000);
                screenManager.Enabled = true;
                manager.ChangeActiveForm(null);
            }
        }

        //protected override void HardwareKeyPressed()
        //{
        //    screenManager.Enabled = true;
        //    base.HardwareKeyPressed();
        //}

        //public override void Close()
        //{
        //    this.FormManager.ResumeGame();
        //}

    }
}
