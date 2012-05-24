using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

// The Items Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234233

namespace StreetFooClient.UI
{
    /// <summary>
    /// A page that displays a collection of item previews.  In the Split Application this page
    /// is used to display and select one of the available groups.
    /// </summary>
    public sealed partial class ReportsPage : StreetFooClient.UI.Common.LayoutAwarePage
    {
        public ReportsPage()
        {
            this.InitializeComponent();

            // initialize the view model...
            this.Reports = new ObservableCollection<ReportItem>();
        }

        public ObservableCollection<ReportItem> Reports
        {
            get { return this.GetDataModelValue<ObservableCollection<ReportItem>>(); }
            set { this.SetDataModelValue(value); }
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property provides the collection of items to be displayed.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // refresh the local cache...
            RefreshView();
        }

        private void RefreshView()
        {
            // refresh the local cache...
            this.EnterBusy();
            ReportItem.RefreshCache((result) =>
            {
                if (result.IsOk)
                {
                    // update...
                    this.SafeCall(() =>
                    {
                        ReloadReportsFromDatabase();
                    });
                }
                else
                    this.ShowAlert(result.Error);

                // exit...
                this.ExitBusy();
            });
        }

        private void ReloadReportsFromDatabase()
        {
            // load the reports from the cache...
            var conn = AppRuntime.GetUserDatabase();
            conn.Table<ReportItem>().ToListAsync().ContinueWith((task) =>
            {
                // update...
                this.SafeCall(() => this.Reports.ReplaceContents(task.Result));

            });
        }

        private void HandleRefresh(object sender, RoutedEventArgs e)
        {
            this.RefreshView();
        }
    }
}
