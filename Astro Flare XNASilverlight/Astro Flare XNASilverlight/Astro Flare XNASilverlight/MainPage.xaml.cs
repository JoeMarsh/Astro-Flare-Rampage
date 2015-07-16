using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using SkillerSDK.Listeners.Responses;
using SkillerSDK.Operations;
using SkillerSDK;
using SkillerSDK.Listeners;


namespace Astro_Flare_XNASilverlight
{
    

    public partial class MainPage : PhoneApplicationPage
    {
        //public static SKApplication myGame;
        public static String tournamentId;
        public static String tournamentGameId;

        // Constructor
        public MainPage()
        {
            InitializeComponent();
            //myGame = new SKApplication("408973791547", "274d969dd2c94f87acdc6f668cc5a7bb", "c96223f78d52421eb6c4366b27de216a", "1.0", 0);
            SKApplication.Instance.Init("408973791547", "274d969dd2c94f87acdc6f668cc5a7bb", "c96223f78d52421eb6c4366b27de216a", "1.0", 0);

            //if (MediaPlayer.GameHasControl)
            //{
            //    (Application.Current as App).TryPlayBackgroundMusic(0);
            //}
            //(Application.Current as App).TryPlayBackgroundMusic(0);
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            while (this.NavigationService.BackStack.Any())
            {
                this.NavigationService.RemoveBackEntry();
            }
        }

        
        // Simple button Click event handler to take us to the second page
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/LevelSelect2.xaml", UriKind.Relative));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //if (!myGame.IsLoggedIn())
            //    myGame.Login(this, GoSocialAfterLogin);
            //else
            //    myGame.UIManager.ShowScreen(this, SKUIManager.eScreenType.DASHBOARD);
            SKApplication.Instance.UIManager.ShowScreen(this, SKUIManager.eScreenType.DASHBOARD);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            //if (!myGame.IsLoggedIn())
            //    myGame.Login(this, ShowLeaderboardAfterLogin);
            //else
            //    myGame.UIManager.ShowScreen(this, SKUIManager.eScreenType.LEADERBOARD_SINGLEPLAYER);

            SKApplication.Instance.GameManager.ShowLeaderboard(this, null);
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            //if (!myGame.IsLoggedIn())
            //    myGame.Login(this, ShowAchievmentsAfterLogin);
            //else
            //    myGame.UIManager.ShowScreen(this, SKUIManager.eScreenType.ACHIEVEMENTS_GAME);

            SKApplication.Instance.GameManager.ShowAchievements(this);
        }

        //private void GoSocialAfterLogin(SKBaseResponse aResponse)
        //{
        //    if (aResponse.StatusCode != 0)
        //    {
        //        ShowFailedToLoginMessage();
        //        return;
        //    }

        //    myGame.UIManager.ShowScreen(this, SKUIManager.eScreenType.DASHBOARD);
        //}

        //private void ShowLeaderboardAfterLogin(SKBaseResponse aResponse)
        //{
        //    if (aResponse.StatusCode != 0)
        //    {
        //        ShowFailedToLoginMessage();
        //        return;
        //    }

        //    myGame.UIManager.ShowScreen(this, SKUIManager.eScreenType.LEADERBOARD_TURNBASED);
        //}

        //private void ShowAchievmentsAfterLogin(SKBaseResponse aResponse)
        //{
        //    if (aResponse.StatusCode != 0)
        //    {
        //        ShowFailedToLoginMessage();
        //        return;
        //    }

        //    myGame.UIManager.ShowScreen(this, SKUIManager.eScreenType.ACHIEVEMENTS_GAME);
        //}

        //private void ShowFailedToLoginMessage()
        //{
        //    this.Dispatcher.BeginInvoke(() =>
        //    {
        //        MessageBox.Show("Failed to login.");
        //    });
        //}


        //private void JoinTournament(SKTournamentChosenResponse aResponse)
        //{
        //    myGame.GameManager.SinglePlayerTools.JoinTournament(aResponse.TournamentId, OnResponse);
        //}



        //public void OnResponse(SKJoinTournamentResponse response) 
        //{ 
        //    if (response.StatusCode != 0) 
        //    { 
        //        //Handle ERROR return; 
        //    } 
        //    //Handle successful response 
        //    tourGameId = response.TournamentId; 
        //    //The tournament game id that was created 
        //    tourId = response.TournamentGameId; 
        //    //The tournament id 
        //}

        //public void EndTournament(int score)
        //{

        //}

        #region game item management

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            //myGame.GameManager.GetUserGameItems(OnResponse);
            SKApplication.Instance.GameManager.GetUserGameItems(new SKListener<SKAppItemsResponse>(OnItemsReceivedSuccess, OnItemsReceivedFailure));
        }

        private void OnItemsReceivedSuccess(SKAppItemsResponse response)
        {
            foreach (SkillerSDK.Listeners.Items.SKGameItem items in response.GameItems)
            {
                if (items.Id == "100175")
                {
                    for (int i = 0; i < items.Amount; i++)
                    {
                        //AstroFlare.Config.Coins += 5000;
                        SKApplication.Instance.GameManager.UseGameItem("100175", 1, new SKListener<SKStatusResponse>(OnItemUsedSuccess, OnItemUsedFailure));
                    }
                }
            }
        }

        private void OnItemUsedSuccess(SKStatusResponse response)
        {
            AstroFlare.Config.Coins += 5000;
        }

        private void OnItemUsedFailure(SKStatusResponse response)
        {
            //your code
        }

        private void OnItemsReceivedFailure(SKStatusResponse response)
        {
            //your code
        }

        //public void OnResponse(SKGetGameItemResponse response)
        //{
        //    if (response.StatusCode != 0)
        //    {
        //    }

        //    foreach (SkillerSDK.Listeners.Items.SKGameItem items in response.GameItems)
        //    {
        //        if (items.Id == "100175")
        //        {
        //            for (int i = 0; i < items.Amount; i++)
        //            {
        //                AstroFlare.Config.Coins += 5000;
        //                myGame.GameManager.UseGameItem(100175, 1, null);
        //            }
        //        }
        //    }
        //} 

        #endregion
    }
}