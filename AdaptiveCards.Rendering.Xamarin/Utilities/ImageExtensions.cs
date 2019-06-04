using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AdaptiveCards.Rendering.Xamarin
{
    public static class ImageExtensions
    {
        public static void SetBackgroundSource(this Grid grid, string url, AdaptiveRenderContext context)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                return;
            }
            grid.SetBackgroundImage(new Uri(url));
        }

        public static void SetImageProperties(this Image imageview, AdaptiveImage image, AdaptiveRenderContext context)
        {
            switch (image.Size)
            {
                case AdaptiveImageSize.Auto:
                case AdaptiveImageSize.Stretch:
                    {
                        imageview.VerticalOptions = LayoutOptions.FillAndExpand;
                        imageview.HorizontalOptions = LayoutOptions.FillAndExpand;
                        return;
                    }
                case AdaptiveImageSize.Small:
                    {
                        imageview.WidthRequest = (double)context.Config.ImageSizes.Small;
                        imageview.HeightRequest = (double)context.Config.ImageSizes.Small;
                        return;
                    }
                case AdaptiveImageSize.Medium:
                    {
                        imageview.WidthRequest = (double)context.Config.ImageSizes.Medium;
                        imageview.HeightRequest = (double)context.Config.ImageSizes.Medium;
                        return;
                    }
                case AdaptiveImageSize.Large:
                    {
                        imageview.WidthRequest = (double)context.Config.ImageSizes.Large;
                        imageview.HeightRequest = (double)context.Config.ImageSizes.Large;
                        return;
                    }
                default:
                    {
                        return;
                    }
            }
        }

        public static void SetSource(this Image image, string url, AdaptiveRenderContext context)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                return;
            }
            image.SetSource(new Uri(url));
        }

        public static void SetBackgroundImage(this Grid grid, Uri uri)
        {
            if (uri == null)
                return;

            grid.Children.Add(new Image()
            {
                Source = ImageSource.FromUri(uri)
            });
        }

        public static void SetHorizontalAlignment(this Image image, AdaptiveHorizontalAlignment alignment)
        {
            switch (alignment)
            {
                case AdaptiveHorizontalAlignment.Left:
                    {
                        image.HorizontalOptions = LayoutOptions.Start;
                        return;
                    }
                case AdaptiveHorizontalAlignment.Center:
                    {
                        image.HorizontalOptions = LayoutOptions.Center;
                        return;
                    }
                case AdaptiveHorizontalAlignment.Right:
                    {
                        image.HorizontalOptions = LayoutOptions.End;
                        return;
                    }
            }
            image.HorizontalOptions = LayoutOptions.FillAndExpand;
        }

        public static void SetSource(this Image image, Uri uri)
        {
            image.Source = ImageSource.FromUri(uri);
        }
    }
}
