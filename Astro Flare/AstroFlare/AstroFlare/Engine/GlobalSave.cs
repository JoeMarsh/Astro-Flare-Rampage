using EasyStorage;

namespace AstroFlare
{
    class GlobalSave
    {
        // A generic EasyStorage save device
        public static IAsyncSaveDevice SaveDevice;
        //We can set up different file names for different things we may save.
        //In this example we're going to save the items in the 'Options' menu.
        //I listed some other examples below but commented them out since we
        //don't need them. YOU CAN HAVE MULTIPLE OF THESE
        public static string fileName_options = "AstroFlare_Options";
        public static string fileName_autofire = "AstroFlare_AutoFire";
        public static string fileName_game = "YourGame_Game";
        public static string fileName_level = "AstroFlare_Level";
        public static string fileName_ships = "AstroFlare_Ships";
        public static string fileName_coins = "AstroFlare_Coins";
        public static string fileName_upgrades = "AstroFlare_Upgrades";
        public static string fileName_rampage_scores = "AstroFlare_Rampage_Scores";
        public static string fileName_rampagetimed_scores = "AstroFlare_Rampagetimed_Scores";
        public static string fileName_alterego_scores = "AstroFlare_AlterEgo_Scores";
        public static string fileName_alteregotimed_scores = "AstroFlare_AlterEgoTimed_Scores";
        public static string fileName_timebandit_scores = "AstroFlare_TimeBandit_Scores";
        public static string fileName_extermination_scores = "AstroFlare_Extermination_Scores";

        //public static string fileName_awards = "YourGame_Awards";
        //This is the name of the save file you'll find if you go into your memory
        //options on the Xbox. If you name it something like 'MyGameSave' then
        //people will have no idea what it's for and might delete your save.
        //YOU SHOULD ONLY HAVE ONE OF THESE
        public static string containerName = "AstroFlare_Save";

        public static IsolatedStorageSaveDevice saveDevice;

        //public static void LoadScore(PlayerManager playerManager)
        //{
        //    //load save file
        //    if (GlobalSave.SaveDevice.FileExists(GlobalSave.containerName, GlobalSave.fileName_options))
        //    {
        //        GlobalSave.SaveDevice.Load(
        //            GlobalSave.containerName,
        //            GlobalSave.fileName_options,
        //            stream =>
        //            {
        //                using (StreamReader reader = new StreamReader(stream))
        //                {
        //                    playerManager.PlayerScore = int.Parse(reader.ReadLine());
        //                }
        //            });
        //    }
        //}

        public static void Initialize()
        {
            EasyStorageSettings.SetSupportedLanguages(Language.English, Language.French);
            saveDevice = new IsolatedStorageSaveDevice();
            GlobalSave.SaveDevice = saveDevice;
            //TouchPanel.EnabledGestures = GestureType.Tap;
            saveDevice.SaveCompleted += new SaveCompletedEventHandler(saveDevice_SaveCompleted);
        }

        static void saveDevice_SaveCompleted(object sender, FileActionCompletedEventArgs args)
        {
            //If you want a message along the lines of "Save Completed"
            //this is the place to do that.
        }
    }
}
