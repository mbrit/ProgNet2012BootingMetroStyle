using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace StreetFooClient.UI
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class LogonPage : StreetFooClient.UI.Common.LayoutAwarePage
    {
        public LogonPage()
        {
            this.InitializeComponent();

            // initialize the model...
            this.Username = string.Empty;
            this.Password = string.Empty;
        }

        public string Username
        {
            get { return this.GetDataModelValue<string>();  }
            set { this.SetDataModelValue(value); }
        }

        public string Password
        {
            get { return this.GetDataModelValue<string>(); }
            set { this.SetDataModelValue(value); }
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void HandleLogonClick(object sender, RoutedEventArgs e)
        {
            this.ShowAlert(string.Format("Username: {0}\r\nPassword: {1}", this.Username, this.Password));
        }

        private void HandleRegisterClick(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(RegisterPage));
        }
    }
}
