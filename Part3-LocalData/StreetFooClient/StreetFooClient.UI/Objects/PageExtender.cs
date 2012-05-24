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
        public static void ShowAlert(this Page page, string message)
        {
            page.SafeCall(async () =>
            {
                // show...
                MessageDialog dialog = new MessageDialog(message);
                await dialog.ShowAsync();
            });
        }

        internal static void SafeCall(this Page page, Action callback)
        {
            if (!(page.Dispatcher.HasThreadAccess))
            {
                // flip...
                page.Dispatcher.Invoke(Windows.UI.Core.CoreDispatcherPriority.Normal, (sender, e) =>
                {
                    callback();

                }, page, null);
            }
            else
                callback();
        }

        public static void SafeNavigate(this Page page, Type type)
        {
            page.SafeCall(() =>
            {
                page.Frame.Navigate(type);
            });
        }
    }
}
