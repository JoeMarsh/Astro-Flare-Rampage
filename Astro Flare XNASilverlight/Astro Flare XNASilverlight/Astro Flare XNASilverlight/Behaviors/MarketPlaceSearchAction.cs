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
    [Description("Search for a string (e.g. publisher name or app) in the Marketplace")]
    public class MarketPlaceSearchAction : System.Windows.Interactivity.TriggerAction<UIElement>
    {
        private bool invoked = false;
        protected override void Invoke(object parameter)
        {
            if (invoked) //avoid double call by double tapping as that leads to an exception
                return;

            invoked = true;
            MarketplaceSearchTask task = new MarketplaceSearchTask();
            task.SearchTerms = SearchTerm;
            task.Show();
        }

        public string SearchTerm { get; set; }

    }
}
