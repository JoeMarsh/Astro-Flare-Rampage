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
    [Description("Send an email")]
    public class SendEmailAction : System.Windows.Interactivity.TriggerAction<UIElement>
    {
        private bool invoked = false;
        protected override void Invoke(object parameter)
        {
            if (invoked) //avoid double call by double tapping as that leads to an exception
                return;

            invoked = true;
            EmailComposeTask task = new EmailComposeTask
                                        {
                                            Body = this.Body,
                                            Cc = this.CC,
                                            Subject = this.Subject,
                                            To = this.To
                                        };
            task.Show();
        }

        public string To { get; set; }
        public string Body { get; set; }
        public string Subject { get; set; }
        public string CC { get; set; }

    }
}
