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
    public partial class LevelSelectPage : PhoneApplicationPage
    {
        public LevelSelectPage()
        {
            InitializeComponent();
        }



        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            LevelSelectList.SelectedIndex = AstroFlare.Config.level;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            switch (LevelSelectList.SelectedIndex)
            {
                case 0:
                    AstroFlare.Config.level = 0;
                    break;
                case 1:
                    AstroFlare.Config.level = 1;
                    break;
                case 2:
                    AstroFlare.Config.level = 2;
                    break;
                case 3:
                    AstroFlare.Config.level = 3;
                    break;
                case 4:
                    AstroFlare.Config.level = 4;
                    break;
                case 5:
                    AstroFlare.Config.level = 5;
                    break;
            }
            AstroFlare.Config.Level = (AstroFlare.LevelSelect)AstroFlare.Config.level + 1;
        }




    }
}