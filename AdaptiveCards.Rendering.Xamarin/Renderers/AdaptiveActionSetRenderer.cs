using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace AdaptiveCards.Rendering.Xamarin
{
    public static class AdaptiveActionSetRenderer
    {
        public static void AddActions(Grid uiContainer, IList<AdaptiveAction> actions, AdaptiveRenderContext context)
        {
            if (!context.Config.SupportsInteractivity)
                return;

            ActionsConfig actionsConfig = context.Config.Actions;
            int maxActions = actionsConfig.MaxActions;
            List<AdaptiveAction> list = actions.Take<AdaptiveAction>(maxActions).ToList<AdaptiveAction>();
            if (!list.Any<AdaptiveAction>())
                return;

            var flexLayout = new FlexLayout() { JustifyContent = FlexJustify.SpaceEvenly };

            if (actionsConfig.ActionsOrientation != ActionsOrientation.Horizontal)
                flexLayout.Direction = FlexDirection.Row;
            else
                flexLayout.Direction = FlexDirection.Column;

            flexLayout.AlignItems = FlexAlignItems.Center;
            flexLayout.Style = context.GetStyle("Adaptive.Actions");
            flexLayout.Margin = new Thickness(0, (double)((actionsConfig.ActionsOrientation == ActionsOrientation.Horizontal ? context.Config.GetSpacing(actionsConfig.Spacing) : context.Config.GetSpacing(actionsConfig.Spacing) - actionsConfig.ButtonSpacing)), 0, 0);
            uiContainer.RowDefinitions.Add(new RowDefinition()
            {
                Height = GridLength.Auto
            });

            Grid.SetRow(flexLayout, uiContainer.RowDefinitions.Count - 1);
            uiContainer.Children.Add(flexLayout);
            bool actionMode = actionsConfig.ShowCard.ActionMode == ShowCardActionMode.Inline;
            if (actionMode)
            {
                if (list.Any<AdaptiveAction>((AdaptiveAction a) => a is AdaptiveShowCardAction))
                {
                    uiContainer.RowDefinitions.Add(new RowDefinition()
                    {
                        Height = GridLength.Auto
                    });
                }
            }
            int num = 0;
            List<View> views = new List<View>();
            foreach (AdaptiveAction adaptiveAction in list)
            {
                var button = (Button)context.Render(adaptiveAction);
                if (actionsConfig.ActionsOrientation != ActionsOrientation.Horizontal)
                {
                    button.Margin = new Thickness(0, (double)actionsConfig.ButtonSpacing, 0, 0);
                }
                else if (flexLayout.Children.Count > 0)
                {
                    button.Margin = new Thickness((double)actionsConfig.ButtonSpacing, 0, 0, 0);
                }
                if (actionsConfig.ActionsOrientation == ActionsOrientation.Horizontal)
                {
                    int num1 = num;
                    num = num1 + 1;
                    Grid.SetColumn(button, num1);
                }
                flexLayout.Children.Add(button);
                AdaptiveShowCardAction adaptiveShowCardAction = adaptiveAction as AdaptiveShowCardAction;
                AdaptiveShowCardAction adaptiveShowCardAction1 = adaptiveShowCardAction;
                if (adaptiveShowCardAction == null || !actionMode)
                {
                    continue;
                }
                var grid = new Grid()
                {
                    Style = context.GetStyle("Adaptive.Actions.ShowCard"),
                    BindingContext = adaptiveShowCardAction1,
                    Margin = new Thickness(0, (double)actionsConfig.ShowCard.InlineTopMargin, 0, 0),
                    IsVisible = false
                };

                var showCardGrid = (Grid)context.Render(adaptiveShowCardAction1.Card);
                showCardGrid.BackgroundColor = Color.Transparent;
                showCardGrid.BindingContext = adaptiveShowCardAction1;
                showCardGrid.Children[0].Margin = new Thickness(0);

                grid.Children.Add(showCardGrid);
                views.Add(grid);
                Grid.SetRow(grid, uiContainer.RowDefinitions.Count - 1);
                uiContainer.Children.Add(grid);

                button.Clicked += new EventHandler((object sender, EventArgs e) =>
                {
                    foreach (var actionBarCard in views)
                    {
                        actionBarCard.IsVisible = false;
                    }
                    if (!grid.IsVisible)
                        grid.IsVisible = true;
                });
            }
        }
    }
}
