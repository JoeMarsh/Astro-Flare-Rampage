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

namespace Astro_Flare_XNASilverlight
{
    public partial class Page1 : PhoneApplicationPage
    {
        public Page1()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            while (this.NavigationService.BackStack.Any())
            {
                this.NavigationService.RemoveBackEntry();
            }
        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            //base.OnBackKeyPress(e);
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }

        private void Score_Loaded(object sender, RoutedEventArgs e)
        {
            Score.Text = "Score: \n \n" + Math.Round(AstroFlare.Config.Score - AstroFlare.Config.AIScore, 0).ToString("#,#");
        }

        private void EnemiesKilled_Loaded(object sender, RoutedEventArgs e)
        {
            EnemiesKilled.Text = "Enemies Killed: \n \n" + Math.Round(AstroFlare.Config.EmemiesKilled, 0).ToString("#,#");
        }

        private void CoinsCollected_Loaded(object sender, RoutedEventArgs e)
        {
            CoinsCollected.Text = "Coins Collected: \n \n" + AstroFlare.Config.CoinsCollected.ToString("#,#");
        }

        private void TopScoresTextBlock_Loaded(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 5; i++)
            {
                switch (AstroFlare.Config.level)
                {
                    case 0:
                        TopScoresTextBlock.Text += "\n" + AstroFlare.Config.RampageScores[i].ToString();
                        break;
                    case 1:
                        TopScoresTextBlock.Text += "\n" + AstroFlare.Config.RampageTimedScores[i].ToString();
                        break;
                    case 2:
                        TopScoresTextBlock.Text += "\n" + AstroFlare.Config.AlterEgoScores[i].ToString();
                        break;
                    case 3:
                        TopScoresTextBlock.Text += "\n" + AstroFlare.Config.AlterEgoTimedScores[i].ToString();
                        break;
                    case 4:
                        TopScoresTextBlock.Text += "\n" + AstroFlare.Config.TimeBanditScores[i].ToString();
                        break;
                    case 5:
                        TopScoresTextBlock.Text += "\n" + AstroFlare.Config.ExterminationScores[i].ToString();
                        break;
                }
            }
        }
    }
}