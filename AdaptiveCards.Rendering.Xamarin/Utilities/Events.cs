using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AdaptiveCards.Rendering.Xamarin
{
    public class AdaptiveActionEventArgs : EventArgs
    {
        public AdaptiveAction Action
        {
            get;
            set;
        }

        public AdaptiveActionEventArgs(AdaptiveAction action)
        {
            this.Action = action;
        }
    }

    public class MissingInputEventArgs : EventArgs
    {
        public MissingAdaptiveInput AdaptiveInput
        {
            get;
            private set;
        }

        public View View
        {
            get;
            private set;
        }

        public MissingInputEventArgs(MissingAdaptiveInput input, View view)
        {
            this.View = view;
            this.AdaptiveInput = input;
        }
    }
}
