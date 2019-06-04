using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AdaptiveCards.Rendering.Xamarin
{
    public static class AdaptiveContainerRenderer
    {
        public static void AddContainerElements(Grid uiContainer, IList<AdaptiveElement> elements, AdaptiveRenderContext context)
        {
            foreach (AdaptiveElement element in elements)
            {
                View view = context.Render(element);
                if (view == null)
                {
                    continue;
                }
                if (element.Separator && uiContainer.Children.Count > 0)
                {
                    AddSeperator(context, element, uiContainer);
                }
                else if (uiContainer.Children.Count > 0)
                {
                    double spacing = context.Config.GetSpacing(element.Spacing);

                    if (element is AdaptiveTextBlock)
                        spacing = spacing + (spacing / 2);
                    else
                        spacing = spacing / 2;

                    view.Margin = new Thickness(0, spacing, 0, 0);
                }
                uiContainer.RowDefinitions.Add(new RowDefinition()
                {
                    Height = GridLength.Auto
                });
                Grid.SetRow(view, uiContainer.RowDefinitions.Count - 1);
                uiContainer.Children.Add(view);
            }
        }

        public static void AddSeperator(AdaptiveRenderContext context, AdaptiveElement element, Grid uiContainer)
        {
            if (element.Spacing == AdaptiveSpacing.None && !element.Separator)
            {
                return;
            }
            Grid grid = new Grid()
            {
                Style = context.GetStyle("Adaptive.Separator")
            };
            int spacing = context.Config.GetSpacing(element.Spacing);
            SeparatorConfig separator = context.Config.Separator;
            grid.Margin = new Thickness(0, (double)((spacing - separator.LineThickness) / 2), 0, 0);
            grid.SetHeight((double)separator.LineThickness);
            if (!string.IsNullOrWhiteSpace(separator.LineColor))
            {
                grid.SetBackgroundColor(separator.LineColor, context);
            }
            uiContainer.RowDefinitions.Add(new RowDefinition()
            {
                Height = GridLength.Auto
            });
            Grid.SetRow(grid, uiContainer.RowDefinitions.Count - 1);
            uiContainer.Children.Add(grid);
        }

        public static View Render(AdaptiveContainer container, AdaptiveRenderContext context)
        {
            ContainerStyleConfig defaultConfig = context.Config.ContainerStyles.Default;
            Grid grid = new Grid()
            {
                Style = context.GetStyle("Adaptive.Container")
            };
            AdaptiveContainerRenderer.AddContainerElements(grid, container.Items, context);
            if (container.SelectAction != null)
            {
                return context.RenderSelectAction(container.SelectAction, grid);
            }
            Grid grid1 = new Grid()
            {
                BackgroundColor = context.GetColor(defaultConfig.BackgroundColor)
            };
            grid1.Children.Add(grid);
            return grid1;
        }
    }
}
