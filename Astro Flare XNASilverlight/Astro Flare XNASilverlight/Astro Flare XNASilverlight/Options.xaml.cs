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
using AstroFlare;
using System.IO;
using Microsoft.Xna.Framework.Media;

namespace Astro_Flare_XNASilverlight
{
    public partial class Options : PhoneApplicationPage
    {
        public Options()
        {
            InitializeComponent();
        }



        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            //ControlSelectList.SelectedIndex = Config.ControlOption;
        }

        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            switch (ControlSelectList.SelectedIndex)
            {
                case 0:
                    Config.ControlOption = 0;
                    break;
                case 1:
                    Config.ControlOption = 1;
                    break;
                case 2:
                    Config.ControlOption = 2;
                    break;
                case 3:
                    Config.ControlOption = 3;
                    break;
            }

            if (ShowThumbsticksCheckbox.IsChecked == false)
                Config.ThumbsticksOn = false;
            else
                Config.ThumbsticksOn = true;

            if (SoundFX.IsChecked == false)
                Config.SoundFXOn = false;
            else
                Config.SoundFXOn = true;

            if (Music.IsChecked == false)
            {
                Config.MusicOn = false;
                if (App.CanPlayMusic)
                {
                    App.GlobalMediaElement.Stop();
                }
            }
            else
            {
                Config.MusicOn = true;
                (Application.Current as App).TryPlayBackgroundMusic(0);
            }

        }

        private void ControlSelectList_Loaded(object sender, RoutedEventArgs e)
        {
            ControlSelectList.SelectedIndex = Config.ControlOption;
        }

        private void ShowThumbsticksCheckbox_Loaded(object sender, RoutedEventArgs e)
        {
            if (Config.ThumbsticksOn == true)
                ShowThumbsticksCheckbox.IsChecked = true;
            else
                ShowThumbsticksCheckbox.IsChecked = false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/CalibratePage.xaml?appName=Astro Flare", UriKind.Relative));
        }

        private void SoundFX_Loaded(object sender, RoutedEventArgs e)
        {
            if (Config.SoundFXOn == true)
                SoundFX.IsChecked = true;
            else
                SoundFX.IsChecked = false;
        }

        private void Music_Loaded(object sender, RoutedEventArgs e)
        {
            if (Config.MusicOn == true)
                Music.IsChecked = true;
            else
                Music.IsChecked = false;
        }

    }
}