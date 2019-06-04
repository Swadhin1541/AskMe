using AskMe.Model;
using AskMe.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AskMe.Utility
{
    public class ListViewTemplateSelector : DataTemplateSelector
    {
        private DataTemplate outgoingTextTemplate;
        private DataTemplate incomingTextTemplate;
        private DataTemplate incomingCardTemplate;
        private DataTemplate loadingTemplate;
        private DataTemplate incomingTextWithoutIconTemplate;
        public ListViewTemplateSelector()
        {
            outgoingTextTemplate = new DataTemplate(typeof(OutgoingTextViewCell));
            incomingTextTemplate = new DataTemplate(typeof(IncomingTextViewCell));
            incomingCardTemplate = new DataTemplate(typeof(IncomingCardViewCell));
            incomingTextWithoutIconTemplate = new DataTemplate(typeof(IncomingTextViewIconLessCell));
            loadingTemplate = new DataTemplate(typeof(LoadingViewCell));
        }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var message = item as Message;
            if (message == null)
                return incomingTextTemplate;
            switch(message.MessageType)
            {
                case MessageType.OutgoingText:
                    return outgoingTextTemplate;
                case MessageType.IncomingCard:
                    return incomingCardTemplate;
                case MessageType.Loading:
                    return loadingTemplate;
                case MessageType.IncomingTextWithoutIcon:
                    return incomingTextWithoutIconTemplate;
                default:
                    return incomingTextTemplate;
            }
        }
    }
}
