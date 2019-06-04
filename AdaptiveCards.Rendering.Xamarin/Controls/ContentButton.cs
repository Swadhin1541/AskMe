using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AdaptiveCards.Rendering.Xamarin
{
    public class ContentButton : ContentView
    {
        public event EventHandler Click;

        public string Text
        {
            get
            {
                return this.TextBlock.Text;
            }
            set
            {
                this.TextBlock.Text = value;
            }
        }

        internal Label TextBlock
        {
            get
            {
                if (!(base.Content is Label textBlock))
                {
                    base.Content = textBlock = new Label();
                }
                return textBlock;
            }
        }

        public ContentButton()
        {
            TapGestureRecognizer tapGesture = new TapGestureRecognizer
            {
                Command = new Command(() =>
                {
                    Click?.Invoke(this, EventArgs.Empty);
                })
            };
            GestureRecognizers.Add(tapGesture);
        }
    }
}
