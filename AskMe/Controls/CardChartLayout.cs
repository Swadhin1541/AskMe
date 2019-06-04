using AdaptiveCards;
using AdaptiveCards.Rendering;
using AdaptiveCards.Rendering.Xamarin;
using AskMe.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;

namespace AskMe.Controls
{
    public class CardChartLayout : StackLayout
    {
        public void InvokeOnAction(object sender, AdaptiveActionEventArgs args)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                MessagingCenter.Send(sender as RenderedAdaptiveCard, "AdaptiveCardAction", args);
            });
        }
    }
}
