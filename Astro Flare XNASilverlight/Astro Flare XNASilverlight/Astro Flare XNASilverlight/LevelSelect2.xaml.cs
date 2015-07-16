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
using System.Windows.Media.Imaging;
using SkillerSDK.Operations;
using SkillerSDK.Listeners.Responses;
using SkillerSDK.Listeners;

namespace Astro_Flare_XNASilverlight
{
    public partial class PivotPage1 : PhoneApplicationPage
    {
        BitmapImage Image1;
        BitmapImage Image2;
        BitmapImage Image3;
        BitmapImage Image4;
        BitmapImage Image5;
        BitmapImage Image6;

        public PivotPage1()
        {
            InitializeComponent();
            Image1 = new BitmapImage(new Uri("MenuBackground1.jpg", UriKind.Relative));
            Image2 = new BitmapImage(new Uri("MenuBackground2.jpg", UriKind.Relative));
            Image3 = new BitmapImage(new Uri("MenuBackground3.jpg", UriKind.Relative));
            Image4 = new BitmapImage(new Uri("MenuBackground4.jpg", UriKind.Relative));
            Image5 = new BitmapImage(new Uri("MenuBackground5.jpg", UriKind.Relative));
            Image6 = new BitmapImage(new Uri("MenuBackground6.jpg", UriKind.Relative));


            for (int i = 0; i < 5; i++)
			{
                RampageScoreTextBlock.Text += "\n" + AstroFlare.Config.RampageScores[i].ToString();
                RampageTimedScoreTextBlock.Text += "\n" + AstroFlare.Config.RampageTimedScores[i].ToString();
                AlterEgoScoreTextBlock.Text += "\n" + AstroFlare.Config.AlterEgoScores[i].ToString();
                AlterEgoTimedScoreTextBlock.Text += "\n" + AstroFlare.Config.AlterEgoTimedScores[i].ToString();
                TimeBanditScoreTextBlock.Text += "\n" + AstroFlare.Config.TimeBanditScores[i].ToString();
                ExterminationScoreTextBlock.Text += "\n" + AstroFlare.Config.ExterminationScores[i].ToString();
			}
            
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            UpdateButtonText();

            if (AstroFlare.Config.AIControlled)
            {
                AutoFireButton.Content = "Auto Aim/Fire: On";
                AutoFireButton2.Content = "Auto Aim/Fire: On";
                AutoFireButton3.Content = "Auto Aim/Fire: On";
                AutoFireButton4.Content = "Auto Aim/Fire: On";
                AutoFireButton5.Content = "Auto Aim/Fire: On";
                AutoFireButton6.Content = "Auto Aim/Fire: On";
            }
            else
            {
                AutoFireButton.Content = "Auto Aim/Fire: Off";
                AutoFireButton2.Content = "Auto Aim/Fire: Off";
                AutoFireButton3.Content = "Auto Aim/Fire: Off";
                AutoFireButton4.Content = "Auto Aim/Fire: Off";
                AutoFireButton5.Content = "Auto Aim/Fire: Off";
                AutoFireButton6.Content = "Auto Aim/Fire: Off";
            }

            base.OnNavigatedTo(e);
        }

        private void RampageStart_Click(object sender, RoutedEventArgs e)
        {
            AstroFlare.Config.level = 0;
            AstroFlare.Config.Level = (AstroFlare.LevelSelect)AstroFlare.Config.level + 1;
            NavigationService.Navigate(new Uri("/GamePage.xaml", UriKind.Relative));
        }

        private void RampageTimedStart_Click(object sender, RoutedEventArgs e)
        {
            AstroFlare.Config.level = 1;
            AstroFlare.Config.Level = (AstroFlare.LevelSelect)AstroFlare.Config.level + 1;
            NavigationService.Navigate(new Uri("/GamePage.xaml", UriKind.Relative));
        }

        private void AlterEgoStart_Click(object sender, RoutedEventArgs e)
        {
            AstroFlare.Config.level = 2;
            AstroFlare.Config.Level = (AstroFlare.LevelSelect)AstroFlare.Config.level + 1;
            NavigationService.Navigate(new Uri("/GamePage.xaml", UriKind.Relative));
        }

        private void AlterEgoTimedStart_Click(object sender, RoutedEventArgs e)
        {
            AstroFlare.Config.level = 3;
            AstroFlare.Config.Level = (AstroFlare.LevelSelect)AstroFlare.Config.level + 1;
            NavigationService.Navigate(new Uri("/GamePage.xaml", UriKind.Relative));
        }

        private void TimeBanditStart_Click(object sender, RoutedEventArgs e)
        {
            AstroFlare.Config.level = 4;
            AstroFlare.Config.Level = (AstroFlare.LevelSelect)AstroFlare.Config.level + 1;
            NavigationService.Navigate(new Uri("/GamePage.xaml", UriKind.Relative));
        }

        private void ExterminationStart_Click(object sender, RoutedEventArgs e)
        {
            AstroFlare.Config.level = 5;
            AstroFlare.Config.Level = (AstroFlare.LevelSelect)AstroFlare.Config.level + 1;
            NavigationService.Navigate(new Uri("/GamePage.xaml", UriKind.Relative));
        }

        private void LayoutRoot_ManipulationCompleted(object sender, System.Windows.Input.ManipulationCompletedEventArgs e)
        {

        }

        private void PivotLevelSelect_ManipulationCompleted(object sender, ManipulationCompletedEventArgs e)
        {
            switch (PivotLevelSelect.SelectedIndex)
            {
                case 0:
                    BackgroundPicture.Source = Image1;
                    break;
                case 1:
                    BackgroundPicture.Source = Image2;
                    break;
                case 2:
                    BackgroundPicture.Source = Image3;
                    break;
                case 3:
                    BackgroundPicture.Source = Image4;
                    break;
                case 4:
                    BackgroundPicture.Source = Image5;
                    break;
                case 5:
                    BackgroundPicture.Source = Image6;
                    break;
            }
        }

        private void AutoFireButton_Loaded(object sender, RoutedEventArgs e)
        {
            if (AstroFlare.Config.AIControlled)
            {
                AutoFireButton.Content = "Auto Aim/Fire: On";
            }
            else
                AutoFireButton.Content = "Auto Aim/Fire: Off";
        }

        private void AutoFireButton_Click(object sender, RoutedEventArgs e)
        {
            AstroFlare.Config.AIControlled = !AstroFlare.Config.AIControlled;

            if (AstroFlare.Config.AIControlled)
            {
                AutoFireButton.Content = "Auto Aim/Fire: On";
                AutoFireButton2.Content = "Auto Aim/Fire: On";
                AutoFireButton3.Content = "Auto Aim/Fire: On";
                AutoFireButton4.Content = "Auto Aim/Fire: On";
                AutoFireButton5.Content = "Auto Aim/Fire: On";
                AutoFireButton6.Content = "Auto Aim/Fire: On";
            }
            else
            {
                AutoFireButton.Content = "Auto Aim/Fire: Off";
                AutoFireButton2.Content = "Auto Aim/Fire: Off";
                AutoFireButton3.Content = "Auto Aim/Fire: Off";
                AutoFireButton4.Content = "Auto Aim/Fire: Off";
                AutoFireButton5.Content = "Auto Aim/Fire: Off";
                AutoFireButton6.Content = "Auto Aim/Fire: Off";
            }
        }

        private void AutoFireButton2_Loaded(object sender, RoutedEventArgs e)
        {
            if (AstroFlare.Config.AIControlled)
            {
                AutoFireButton.Content = "Auto Aim/Fire: On";
            }
            else
                AutoFireButton.Content = "Auto Aim/Fire: Off";
        }

        private void AutoFireButton2_Click(object sender, RoutedEventArgs e)
        {
            AstroFlare.Config.AIControlled = !AstroFlare.Config.AIControlled;

            if (AstroFlare.Config.AIControlled)
            {
                AutoFireButton.Content = "Auto Aim/Fire: On";
                AutoFireButton2.Content = "Auto Aim/Fire: On";
                AutoFireButton3.Content = "Auto Aim/Fire: On";
                AutoFireButton4.Content = "Auto Aim/Fire: On";
                AutoFireButton5.Content = "Auto Aim/Fire: On";
                AutoFireButton6.Content = "Auto Aim/Fire: On";
            }
            else
            {
                AutoFireButton.Content = "Auto Aim/Fire: Off";
                AutoFireButton2.Content = "Auto Aim/Fire: Off";
                AutoFireButton3.Content = "Auto Aim/Fire: Off";
                AutoFireButton4.Content = "Auto Aim/Fire: Off";
                AutoFireButton5.Content = "Auto Aim/Fire: Off";
                AutoFireButton6.Content = "Auto Aim/Fire: Off";
            }
        }

        private void AutoFireButton3_Loaded(object sender, RoutedEventArgs e)
        {
            if (AstroFlare.Config.AIControlled)
            {
                AutoFireButton.Content = "Auto Aim/Fire: On";
            }
            else
                AutoFireButton.Content = "Auto Aim/Fire: Off";
        }

        private void AutoFireButton3_Click(object sender, RoutedEventArgs e)
        {
            AstroFlare.Config.AIControlled = !AstroFlare.Config.AIControlled;

            if (AstroFlare.Config.AIControlled)
            {
                AutoFireButton.Content = "Auto Aim/Fire: On";
                AutoFireButton2.Content = "Auto Aim/Fire: On";
                AutoFireButton3.Content = "Auto Aim/Fire: On";
                AutoFireButton4.Content = "Auto Aim/Fire: On";
                AutoFireButton5.Content = "Auto Aim/Fire: On";
                AutoFireButton6.Content = "Auto Aim/Fire: On";
            }
            else
            {
                AutoFireButton.Content = "Auto Aim/Fire: Off";
                AutoFireButton2.Content = "Auto Aim/Fire: Off";
                AutoFireButton3.Content = "Auto Aim/Fire: Off";
                AutoFireButton4.Content = "Auto Aim/Fire: Off";
                AutoFireButton5.Content = "Auto Aim/Fire: Off";
                AutoFireButton6.Content = "Auto Aim/Fire: Off";
            }
        }

        private void AutoFireButton4_Loaded(object sender, RoutedEventArgs e)
        {
            if (AstroFlare.Config.AIControlled)
            {
                AutoFireButton.Content = "Auto Aim/Fire: On";
            }
            else
                AutoFireButton.Content = "Auto Aim/Fire: Off";
        }

        private void AutoFireButton4_Click(object sender, RoutedEventArgs e)
        {
            AstroFlare.Config.AIControlled = !AstroFlare.Config.AIControlled;

            if (AstroFlare.Config.AIControlled)
            {
                AutoFireButton.Content = "Auto Aim/Fire: On";
                AutoFireButton2.Content = "Auto Aim/Fire: On";
                AutoFireButton3.Content = "Auto Aim/Fire: On";
                AutoFireButton4.Content = "Auto Aim/Fire: On";
                AutoFireButton5.Content = "Auto Aim/Fire: On";
                AutoFireButton6.Content = "Auto Aim/Fire: On";
            }
            else
            {
                AutoFireButton.Content = "Auto Aim/Fire: Off";
                AutoFireButton2.Content = "Auto Aim/Fire: Off";
                AutoFireButton3.Content = "Auto Aim/Fire: Off";
                AutoFireButton4.Content = "Auto Aim/Fire: Off";
                AutoFireButton5.Content = "Auto Aim/Fire: Off";
                AutoFireButton6.Content = "Auto Aim/Fire: Off";
            }
        }

        private void AutoFireButton5_Loaded(object sender, RoutedEventArgs e)
        {
            if (AstroFlare.Config.AIControlled)
            {
                AutoFireButton.Content = "Auto Aim/Fire: On";
            }
            else
                AutoFireButton.Content = "Auto Aim/Fire: Off";
        }

        private void AutoFireButton5_Click(object sender, RoutedEventArgs e)
        {
            AstroFlare.Config.AIControlled = !AstroFlare.Config.AIControlled;

            if (AstroFlare.Config.AIControlled)
            {
                AutoFireButton.Content = "Auto Aim/Fire: On";
                AutoFireButton2.Content = "Auto Aim/Fire: On";
                AutoFireButton3.Content = "Auto Aim/Fire: On";
                AutoFireButton4.Content = "Auto Aim/Fire: On";
                AutoFireButton5.Content = "Auto Aim/Fire: On";
                AutoFireButton6.Content = "Auto Aim/Fire: On";
            }
            else
            {
                AutoFireButton.Content = "Auto Aim/Fire: Off";
                AutoFireButton2.Content = "Auto Aim/Fire: Off";
                AutoFireButton3.Content = "Auto Aim/Fire: Off";
                AutoFireButton4.Content = "Auto Aim/Fire: Off";
                AutoFireButton5.Content = "Auto Aim/Fire: Off";
                AutoFireButton6.Content = "Auto Aim/Fire: Off";
            }
        }

        private void AutoFireButton6_Loaded(object sender, RoutedEventArgs e)
        {
            if (AstroFlare.Config.AIControlled)
            {
                AutoFireButton.Content = "Auto Aim/Fire: On";
            }
            else
                AutoFireButton.Content = "Auto Aim/Fire: Off";
        }

        private void AutoFireButton6_Click(object sender, RoutedEventArgs e)
        {
            AstroFlare.Config.AIControlled = !AstroFlare.Config.AIControlled;

            if (AstroFlare.Config.AIControlled)
            {
                AutoFireButton.Content = "Auto Aim/Fire: On";
                AutoFireButton2.Content = "Auto Aim/Fire: On";
                AutoFireButton3.Content = "Auto Aim/Fire: On";
                AutoFireButton4.Content = "Auto Aim/Fire: On";
                AutoFireButton5.Content = "Auto Aim/Fire: On";
                AutoFireButton6.Content = "Auto Aim/Fire: On";
            }
            else
            {
                AutoFireButton.Content = "Auto Aim/Fire: Off";
                AutoFireButton2.Content = "Auto Aim/Fire: Off";
                AutoFireButton3.Content = "Auto Aim/Fire: Off";
                AutoFireButton4.Content = "Auto Aim/Fire: Off";
                AutoFireButton5.Content = "Auto Aim/Fire: Off";
                AutoFireButton6.Content = "Auto Aim/Fire: Off";
            }
        }



        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            //myGame.UIManager.ShowSinglePlayerTournamentsScreen(this, JoinTournament);
            //if (MainPage.tournamentId == null && MainPage.tournamentGameId == null)
                ShowLobby();

            UpdateButtonText();
        }

        private void UpdateButtonText()
        {
            if (MainPage.tournamentId != null && MainPage.tournamentGameId != null)
            {
                TournamentButton.Content = "Tournament Registered";
            }
            else
                TournamentButton.Content = "Skiller Tournaments";
        }

        private void ShowLobby()
        {
            SKApplication.Instance.GameManager.SinglePlayerTools.ShowTournamentLobby(this, new SKListener<SKTournamentChosenResponse>(OnChosenTournamentSuccess, OnChosenTournamentFailure));
        }

        private void OnChosenTournamentSuccess(SKTournamentChosenResponse response)
        {
            //your code
            MainPage.tournamentId = response.TournamentId;
            SKApplication.Instance.GameManager.SinglePlayerTools.JoinTournament(MainPage.tournamentId, new SKListener<SKJoinTournamentResponse>(OnJoinTournamentSuccess, OnJoinTournamentFailure));
        }

        private void OnChosenTournamentFailure(SKStatusResponse response)
        {
            //your code
        }

        private void OnJoinTournamentSuccess(SKJoinTournamentResponse response)
        {
            //your code
            MainPage.tournamentGameId = response.TournamentGameId;
            UpdateButtonText();
        }

        private void OnJoinTournamentFailure(SKStatusResponse response)
        {
            //your code
        }

    }
}