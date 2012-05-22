using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;

namespace StreetFooClient.UI
{
    public static class PageExtender
    {
        public static async void ShowAlert(this Page page, string message)
        {
            if (!(page.Dispatcher.HasThreadAccess))
            {
                // flip...
                page.Dispatcher.Invoke(Windows.UI.Core.CoreDispatcherPriority.Normal, (sender, e) =>
                {
                    page.ShowAlert(message);

                }, page, null);
                return;
            }

            // show...
            MessageDialog dialog = new MessageDialog(message);
            await dialog.ShowAsync();
        }
    }
}
