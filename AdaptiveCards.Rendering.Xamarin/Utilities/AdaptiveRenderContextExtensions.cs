using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AdaptiveCards.Rendering.Xamarin
{
    public static class AdaptiveRenderContextExtensions
    {
        public static View RenderSelectAction(this AdaptiveRenderContext context, AdaptiveAction selectAction, View uiElement)
        {
            if (!context.Config.SupportsInteractivity)
            {
                return uiElement;
            }
            ContentButton button = (ContentButton)context.Render(selectAction);
            button.HorizontalOptions = LayoutOptions.FillAndExpand;
            button.VerticalOptions = LayoutOptions.FillAndExpand;
            button.BackgroundColor = Color.Transparent;
            button.Margin = 0;
            button.Content = uiElement;
            button.Style = context.GetStyle("Adaptive.Action.Tap");
            return button;
        }
    }
}
