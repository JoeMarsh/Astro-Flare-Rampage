using System;
using System.ComponentModel;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Tasks;

namespace Astro_Flare_XNASilverlight
{
    [Description("Buy or view the details of the current app in the Marketplace")]
    public class MarketPlaceDetailsAction : System.Windows.Interactivity.TriggerAction<UIElement>
    {
        protected override void Invoke(object parameter)
        {
            IsEnabled = false;
            MarketplaceDetailTask task = new MarketplaceDetailTask();
            task.Show();
        }

    }
}
