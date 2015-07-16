//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//#if  WINDOWS_PHONE
//using Microsoft.Phone;
//using Microsoft.Phone.Info;
//#endif
//using Microsoft.Xna.Framework.Graphics;
//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.GamerServices;

//namespace AstroFlare
//{
//    static class MemoryMonitor
//    {
//        public static long DeviceMemory;
//        public static long CurrentMemoryUseage;
//        public static long PeakMemoryUsage;

//        static double conv = 9.5367431640625e-07;


//        public static void Update()
//        {
//#if WINDOWS_PHONE
//            try
//            {
//                DeviceMemory = (long)DeviceExtendedProperties.GetValue("DeviceTotalMemory");
//                CurrentMemoryUseage = (long)DeviceExtendedProperties.GetValue("ApplicationCurrentMemoryUsage");
//                PeakMemoryUsage = (long)DeviceExtendedProperties.GetValue("ApplicationPeakMemoryUsage");
//            }
//            catch
//            {
//                //List<string> MBOPTIONS = new List<string>();
//                //MBOPTIONS.Add("OK");
//                //string msg = "Error reading memory usage.\nClick OK to continue...";
//                //Guide.BeginShowMessageBox("Pause", msg, MBOPTIONS, 0, MessageBoxIcon.Alert, null, null);
//            }
//#endif
//        }

//        public static void Draw(SpriteBatch spriteBatch, SpriteFont font)
//        {
            
//            //spriteBatch.DrawString(font, string.Format("TMemory: {0:0.00}MB", DeviceMemory * conv), new Vector2(300, 0) + new Vector2(2, 2), Color.Black);
//            //spriteBatch.DrawString(font, string.Format("CMemory: {0:0.00}MB", CurrentMemoryUseage * conv), new Vector2(300, font.LineSpacing) + new Vector2(2, 2), Color.Black);
//            //spriteBatch.DrawString(font, string.Format("PMemory: {0:0.00}MB", PeakMemoryUsage * conv), new Vector2(300, font.LineSpacing * 2) + new Vector2(2, 2), Color.Black);

//            spriteBatch.DrawString(font, string.Format("TMemory: {0:0.00}MB", DeviceMemory * conv), new Vector2(5, 300), Color.Orange, 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 1.0f);
//            spriteBatch.DrawString(font, string.Format("CMemory: {0:0.00}MB", CurrentMemoryUseage * conv), new Vector2(5, 300 + font.LineSpacing), Color.Orange, 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 1.0f);
//            spriteBatch.DrawString(font, string.Format("PMemory: {0:0.00}MB", PeakMemoryUsage * conv), new Vector2(5, 300 + font.LineSpacing * 2), Color.Orange, 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 1.0f);
//        }
//    }
//}