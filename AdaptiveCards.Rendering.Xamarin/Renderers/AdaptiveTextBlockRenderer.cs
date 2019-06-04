using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AdaptiveCards.Rendering.Xamarin
{
    class AdaptiveTextBlockRenderer
    {
        public static Label CreateControl(AdaptiveTextBlock textBlock, AdaptiveRenderContext context)
        {
            var label1 = new Label()
            {
                Style = context.GetStyle("Adaptive.TextBlock"),
                LineBreakMode = LineBreakMode.TailTruncation
            };

            switch (textBlock.HorizontalAlignment)
            {
                case AdaptiveHorizontalAlignment.Left:
                    {
                        label1.HorizontalTextAlignment = TextAlignment.Start;
                        break;
                    }
                case AdaptiveHorizontalAlignment.Center:
                    {
                        label1.HorizontalTextAlignment = TextAlignment.Center;
                        break;
                    }
                case AdaptiveHorizontalAlignment.Right:
                    {
                        label1.HorizontalTextAlignment = TextAlignment.End;
                        break;
                    }
            }
            label1.TextColor = context.Resources.TryGetValue<Color>(string.Format("Adaptive.{0}", textBlock.Color));
            if (textBlock.Weight == AdaptiveTextWeight.Bolder)
            {
                label1.FontAttributes = FontAttributes.Bold;
            }
            if (textBlock.Wrap)
            {
                label1.LineBreakMode = LineBreakMode.WordWrap;
            }
            return label1;
        }

        public static View Render(AdaptiveTextBlock textBlock, AdaptiveRenderContext context)
        {
            FontColorConfig fontColorConfig;
            var label = CreateControl(textBlock, context);
            switch (textBlock.Color)
            {
                case AdaptiveTextColor.Default:
                    {
                        fontColorConfig = context.Config.ContainerStyles.Default.ForegroundColors.Default;
                        break;
                    }
                case AdaptiveTextColor.Dark:
                    {
                        fontColorConfig = context.Config.ContainerStyles.Default.ForegroundColors.Dark;
                        break;
                    }
                case AdaptiveTextColor.Light:
                    {
                        fontColorConfig = context.Config.ContainerStyles.Default.ForegroundColors.Light;
                        break;
                    }
                case AdaptiveTextColor.Accent:
                    {
                        fontColorConfig = context.Config.ContainerStyles.Default.ForegroundColors.Accent;
                        break;
                    }
                case AdaptiveTextColor.Good:
                    {
                        fontColorConfig = context.Config.ContainerStyles.Default.ForegroundColors.Good;
                        break;
                    }
                case AdaptiveTextColor.Warning:
                    {
                        fontColorConfig = context.Config.ContainerStyles.Default.ForegroundColors.Warning;
                        break;
                    }
                case AdaptiveTextColor.Attention:
                    {
                        fontColorConfig = context.Config.ContainerStyles.Default.ForegroundColors.Attention;
                        break;
                    }
                default:
                    {
                        goto case AdaptiveTextColor.Default;
                    }
            }

            if (!textBlock.IsSubtle)
                label.SetColor(fontColorConfig.Default, context);
            else
                label.SetColor(fontColorConfig.Subtle, context);

            switch (textBlock.Size)
            {
                case AdaptiveTextSize.Default:
                    {
                        label.FontSize = (double)context.Config.FontSizes.Default;
                        break;
                    }
                case AdaptiveTextSize.Small:
                    {
                        label.FontSize = (double)context.Config.FontSizes.Small;
                        break;
                    }
                case AdaptiveTextSize.Medium:
                    {
                        label.FontSize = (double)context.Config.FontSizes.Medium;
                        break;
                    }
                case AdaptiveTextSize.Large:
                    {
                        label.FontSize = (double)context.Config.FontSizes.Large;
                        break;
                    }
                case AdaptiveTextSize.ExtraLarge:
                    {
                        label.FontSize = (double)context.Config.FontSizes.ExtraLarge;
                        break;
                    }
                default:
                    {
                        goto case AdaptiveTextSize.Default;
                    }
            }

            var text = RendererUtilities.ApplyTextFunctions(textBlock.Text);
            label.FormattedText = text.GetFormattedString(label.FontSize);

            if (textBlock.MaxLines <= 0)
            {
                return label;
            }
            Grid grid = new Grid();
            grid.RowDefinitions.Add(new RowDefinition()
            {
                Height = GridLength.Auto
            });
            grid.Children.Add(label);
            return grid;
        }
    }
}
