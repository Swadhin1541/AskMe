using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AdaptiveCards.Rendering.Xamarin
{
    public class RenderedAdaptiveCard : RenderedAdaptiveCardBase
    {
        public event TypedEventHandler<RenderedAdaptiveCard, AdaptiveActionEventArgs> OnAction;

        public event TypedEventHandler<RenderedAdaptiveCard, MissingInputEventArgs> OnMissingInput;

        public View View { get; private set; }

        public bool HideIcon { get; set; }

        public RenderedAdaptiveCard(View view, AdaptiveCard originatingCard, IList<AdaptiveWarning> warnings, RenderedAdaptiveCardInputs userInputs) : base(originatingCard, warnings)
        {
            View = view;
            UserInputs = userInputs;
        }

        internal void InvokeOnAction(AdaptiveActionEventArgs args)
        {
            OnAction?.Invoke(this, args);
        }

        internal void InvokeOnMissingInput(MissingInputEventArgs args)
        {
            OnMissingInput?.Invoke(this, args);
        }
    }
}
