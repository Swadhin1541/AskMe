using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AdaptiveCards.Rendering.Xamarin
{
    public static class AdaptiveActionRenderer
    {
        public static Button CreateActionButton(AdaptiveAction action, AdaptiveRenderContext context)
        {
            //var button = new ContentButton()
            //{
            //    Style = context.GetStyle(string.Format("Adaptive.{0}", action.Type)),
            //    Padding = new Thickness(6, 4, 6, 4),
            //    Content = new Label()
            //    {
            //        Text = action.Title,
            //        FontSize = (double)context.Config.FontSizes.Default,
            //        Style = context.GetStyle("Adaptive.Action.Title")
            //    }
            //};
            var button = new Button()
            {
                //Style = context.GetStyle(string.Format("Adaptive.{0}", action.Type)),
                Text = action.Title,
                FontSize = (double)context.Config.FontSizes.Medium,
                FontFamily = "Segoe UI",
            };
            context.GetType().Name.Replace("Action", string.Empty);

            MessagingCenter.Subscribe<Page>(button, "HidePreviousButtons", (sender) =>
                {
                    button.IsVisible = false;
                    button.HeightRequest = 0;
                    MessagingCenter.Unsubscribe<Page>(button, "HidePreviousButtons");
                });

            return button;
        }

        public static View Render(AdaptiveAction action, AdaptiveRenderContext context)
        {
            if (!context.Config.SupportsInteractivity || !context.ActionHandlers.IsSupported(action.GetType()))
            {
                return null;
            }
            var button = CreateActionButton(action, context);
            var view = CreateActionButton(action, context);
            
            button.Clicked += new EventHandler((object sender, EventArgs e) =>
            {
                bool isInputMissing = false;
                if(context.InputBindings.Count > 0)
                {
                    isInputMissing = true;
                    var dict = context.UserInputs.AsDictionary();
                    foreach(var binding in dict.Values)
                    {
                        if (!string.IsNullOrWhiteSpace(binding))
                        {
                            isInputMissing = false;
                            break;
                        }
                    }
                }
                if (isInputMissing)
                {
                    var missingInput = new MissingAdaptiveInput();
                    missingInput.Message = "Select at least one option.";
                    context.InvokeMissingInput(action, new MissingInputEventArgs(missingInput, button));
                }
                else
                {
                    context.InvokeAction(button, new AdaptiveActionEventArgs(action));
                    button.IsEnabled = false;
                }
            });
            return button;
        }
    }
}
