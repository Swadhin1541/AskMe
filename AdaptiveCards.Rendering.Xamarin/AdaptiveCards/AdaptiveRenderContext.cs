using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AdaptiveCards.Rendering.Xamarin
{
    public class AdaptiveRenderContext
    {
        public Dictionary<string, Func<string>> InputBindings = new Dictionary<string, Func<string>>();
        public RenderedAdaptiveCardInputs UserInputs { get; private set; }

        public AdaptiveActionHandlers ActionHandlers
        {
            get;
            set;
        }

        public bool HideIcon { get; set; }

        public AdaptiveHostConfig Config { get; set; } = new AdaptiveHostConfig();

        public AdaptiveElementRenderers<View, AdaptiveRenderContext> ElementRenderers
        {
            get;
            set;
        }

        public ResourceDictionary Resources
        {
            get;
            set;
        }

        public IList<AdaptiveWarning> Warnings { get; } = new List<AdaptiveWarning>();

        public AdaptiveRenderContext(Action<object, AdaptiveActionEventArgs> actionCallback, Action<object, MissingInputEventArgs> missingDataCallback)
        {
            UserInputs = new RenderedAdaptiveCardInputs(InputBindings);
            if (actionCallback != null)
            {
                this.OnAction += new EventHandler<AdaptiveActionEventArgs>((object obj, AdaptiveActionEventArgs args) => actionCallback(obj, args));
            }
            if (missingDataCallback != null)
            {
                this.OnMissingInput += new EventHandler<MissingInputEventArgs>((object obj, MissingInputEventArgs args) => missingDataCallback(obj, args));
            }
        }

        public virtual Style GetStyle(string styleName)
        {
            while (!string.IsNullOrEmpty(styleName))
            {
                Style style = this.Resources.TryGetValue<Style>(styleName);
                if (style != null)
                {
                    return style;
                }
                int num = styleName.LastIndexOf('.');
                if (num <= 0)
                {
                    break;
                }
                styleName = styleName.Substring(0, num);
            }
            return null;
        }

        internal Color GetColor(string color)
        {
            return Color.FromHex(color);
        }

        public void InvokeAction(View ui, AdaptiveActionEventArgs args)
        {
            OnAction?.Invoke(ui, args);
        }

        public void InvokeMissingInput(AdaptiveAction sender, MissingInputEventArgs args)
        {
            OnMissingInput?.Invoke(sender, args);
        }

        public View Render(AdaptiveTypedElement element)
        {
            if (!this.Config.SupportsInteractivity)
            {
                AdaptiveInput adaptiveInput = element as AdaptiveInput;
                AdaptiveInput adaptiveInput1 = adaptiveInput;
                if (adaptiveInput != null)
                {
                    AdaptiveTextBlock nonInteractiveValue = AdaptiveTypedElementConverter.CreateElement<AdaptiveTextBlock>(null);
                    nonInteractiveValue.Text = adaptiveInput1.GetNonInteractiveValue() ?? "*[Input]*";
                    nonInteractiveValue.Color = AdaptiveTextColor.Accent;
                    nonInteractiveValue.Wrap = true;
                    this.Warnings.Add(new AdaptiveWarning(-1, string.Format("Rendering non-interactive input element '{0}'", element.Type)));
                    return this.Render(nonInteractiveValue);
                }
            }
            Func<AdaptiveTypedElement, AdaptiveRenderContext, View> func = this.ElementRenderers.Get(element.GetType());
            if (func != null)
            {
                return func(element, this);
            }
            this.Warnings.Add(new AdaptiveWarning(-1, string.Format("No renderer for element '{0}'", element.Type)));
            return null;
        }

        public event EventHandler<AdaptiveActionEventArgs> OnAction;

        public event EventHandler<MissingInputEventArgs> OnMissingInput;
    }
}