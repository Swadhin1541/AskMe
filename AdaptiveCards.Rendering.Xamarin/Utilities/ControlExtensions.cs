using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AdaptiveCards.Rendering.Xamarin
{
    public static class ControlExtensions
    {
        public static void Add(this StackLayout control, View item)
        {
            control.Children.Add(item);
        }

        public static object GetContext(this View element)
        {
            if (element == null)
            {
                return null;
            }
            return element.BindingContext;
        }

        public static bool GetState(this Switch control)
        {
            return control.IsToggled;
        }

        public static void SetBackgroundColor(this View view, string color, AdaptiveRenderContext context)
        {
            view.BackgroundColor = Color.FromHex(color);
        }

        public static void SetBorderColor(this Button view, string color, AdaptiveRenderContext context)
        {
        }

        public static void SetColor(this Label label, string color, AdaptiveRenderContext context)
        {
            label.TextColor = Color.FromHex(color);
        }

        public static void SetContext(this View element, object value)
        {
            element.BindingContext = value;
        }

        public static void SetFontWeight(this Label label, int weight)
        {
            //text.FontAttributes = 
        }

        public static void SetHeight(this View view, double height)
        {
            view.HeightRequest = height;
        }

        public static void SetPlaceholder(this Entry entry, string placeholder)
        {
            entry.Placeholder = placeholder;
        }

        public static void SetState(this Switch control, bool value)
        {
            control.IsToggled = value;
        }

        public static void SetThickness(this Button view, double thickness)
        {
        }

        public static FormattedString GetFormattedString(this string text, double defaultFontSize)
        {
            var boldFormat = "**";
            var italicFormat = "_";
            var formatString = new FormattedString();
            var temp = text;
            while(!string.IsNullOrWhiteSpace(temp))
            {
                try
                {
                    var boldIndex = temp.IndexOf(boldFormat);
                    var italicIndex = temp.IndexOf(italicFormat);

                    if (italicIndex >= 0 && (italicIndex < boldIndex || boldIndex < 0))
                    {
                        if (italicIndex > 0)
                        {
                            var t = temp.Substring(0, italicIndex);
                            formatString.Spans.Add(new Span() { Text = t });
                        }
                        temp = temp.Substring(italicIndex + 1);
                        var next = temp.IndexOf(italicFormat);
                        var t1 = temp.Substring(0, next);
                        formatString.Spans.Add(new Span() { Text = t1, FontAttributes = FontAttributes.Italic, FontSize = defaultFontSize, FontFamily = "Segoe UI" });
                        temp = temp.Substring(next + 1);
                    }
                    else if (boldIndex >= 0)
                    {
                        if (boldIndex > 0)
                        {
                            var t = temp.Substring(0, boldIndex);
                            formatString.Spans.Add(new Span() { Text = t });
                        }
                        temp = temp.Substring(boldIndex + 2);
                        var next = temp.IndexOf(boldFormat);
                        var t1 = temp.Substring(0, next);
                        formatString.Spans.Add(new Span() { Text = t1, FontAttributes = FontAttributes.Bold, FontSize = defaultFontSize, FontFamily = "Segoe UI" });
                        temp = temp.Substring(next + 2);
                    }
                    else
                    {
                        formatString.Spans.Add(new Span() { Text = temp, FontSize = defaultFontSize });
                        break;
                    }
                }
                catch (Exception)
                {
                    formatString = new FormattedString();
                    formatString.Spans.Add(new Span() { Text = text, FontSize = defaultFontSize, FontFamily = "Segoe UI" });
                    break;
                }
            }
            return formatString;
        }
    }
}
