using AskMe.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace AskMe.Utility
{
    public static class AskMeExtensions
    {
        /// <summary>
        /// Get chat listview's item index. If message parameter is null, then returns last item index.
        /// </summary>
        /// <param name="element"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static int GetListViewItemIndex(this Element element, object item = null)
        {
            while (element != null)
            {
                if (element is ListView listview)
                {
                    if (listview.ItemsSource is ObservableCollection<Message> messages)
                    {
                        return item is Message message ? messages.IndexOf(message) : messages.Count - 1;
                    }
                }
                element = element.Parent;
            }
            return -1;
        }
    }
}
