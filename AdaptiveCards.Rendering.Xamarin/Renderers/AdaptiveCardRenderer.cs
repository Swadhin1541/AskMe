using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AdaptiveCards.Rendering.Xamarin
{
    public class AdaptiveCardRenderer : AdaptiveCardRendererBase<View, AdaptiveRenderContext>
    {
        protected Action<object, AdaptiveActionEventArgs> ActionCallback;

        protected Action<object, MissingInputEventArgs> missingDataCallback;

        private ResourceDictionary _resources;

        public AdaptiveActionHandlers ActionHandlers { get; } = new AdaptiveActionHandlers();

        public ResourceDictionary Resources
        {
            get
            {
                if (this._resources == null)
                    this._resources = new ResourceDictionary();
                return this._resources;
            }
            set
            {
                this._resources = value;
            }
        }

        public AdaptiveCardRenderer() : this(new AdaptiveHostConfig())
        {
        }

        public AdaptiveCardRenderer(AdaptiveHostConfig hostConfig)
        {
            this.HostConfig = hostConfig;
            this.SetObjectTypes();
        }

        protected override AdaptiveSchemaVersion GetSupportedSchemaVersion()
        {
            return new AdaptiveSchemaVersion(1, 0);
        }

        public static View RenderAdaptiveCardWrapper(AdaptiveCard card, AdaptiveRenderContext context)
        {
            Grid grid = new Grid()
            {
                Style = context.GetStyle("Adaptive.Card"),
                BackgroundColor = context.GetColor(context.Config.ContainerStyles.Default.BackgroundColor)
            };
            grid.SetBackgroundImage(card.BackgroundImage);
            
            Grid grid1 = new Grid()
            {
                Style = context.GetStyle("Adaptive.InnerCard"),
                //Margin = new Thickness((double)context.Config.Spacing.Padding)
            };
            grid1.ColumnDefinitions.Add(new ColumnDefinition()
            {
                Width = new GridLength(1, GridUnitType.Star)
            });
            AdaptiveContainerRenderer.AddContainerElements(grid1, card.Body, context);
            AdaptiveActionSetRenderer.AddActions(grid1, card.Actions, context);
            grid.Children.Add(grid1);
            return grid;
        }

        public RenderedAdaptiveCard RenderCard(AdaptiveCard card)
        {
            if (card == null)
            {
                throw new ArgumentNullException("card");
            }
            this.EnsureCanRender(card);
            RenderedAdaptiveCard renderedAdaptiveCard1 = null;
            AdaptiveRenderContext adaptiveRenderContext = new AdaptiveRenderContext((object sender, AdaptiveActionEventArgs args) => {
                RenderedAdaptiveCard renderedAdaptiveCard = renderedAdaptiveCard1;
                if (renderedAdaptiveCard == null)
                {
                    return;
                }
                renderedAdaptiveCard.InvokeOnAction(args);
            }, (sender, args)=>
            {
                renderedAdaptiveCard1?.InvokeOnMissingInput(args);
            })
            {
                ActionHandlers = this.ActionHandlers,
                Config = this.HostConfig ?? new AdaptiveHostConfig(),
                Resources = this.Resources,
                ElementRenderers = this.ElementRenderers
            };
            View view = adaptiveRenderContext.Render(card);
            renderedAdaptiveCard1 = new RenderedAdaptiveCard(view, card, adaptiveRenderContext.Warnings, adaptiveRenderContext.UserInputs);
            renderedAdaptiveCard1.HideIcon = adaptiveRenderContext.HideIcon;
            return renderedAdaptiveCard1;
        }

        private void SetObjectTypes()
        {
            this.ElementRenderers.Set<AdaptiveCard>(new Func<AdaptiveCard, AdaptiveRenderContext, View>(AdaptiveCardRenderer.RenderAdaptiveCardWrapper));
            this.ElementRenderers.Set<AdaptiveTextBlock>(new Func<AdaptiveTextBlock, AdaptiveRenderContext, View>(AdaptiveTextBlockRenderer.Render));
            this.ElementRenderers.Set<AdaptiveContainer>(new Func<AdaptiveContainer, AdaptiveRenderContext, View>(AdaptiveContainerRenderer.Render));
            this.ElementRenderers.Set<AdaptiveAction>(new Func<AdaptiveAction, AdaptiveRenderContext, View>(AdaptiveActionRenderer.Render));
            this.ElementRenderers.Set<AdaptiveChoiceSetInput>(new Func<AdaptiveChoiceSetInput, AdaptiveRenderContext, View>(AdaptiveChoiceSetRenderer.Render));
            //this.ElementRenderers.Set<AdaptiveImage>(new Func<AdaptiveImage, AdaptiveRenderContext, View>(AdaptiveImageRenderer.Render));
            //this.ElementRenderers.Set<AdaptiveColumn>(new Func<AdaptiveColumn, AdaptiveRenderContext, View>(AdaptiveColumnRenderer.Render));
            //this.ElementRenderers.Set<AdaptiveColumnSet>(new Func<AdaptiveColumnSet, AdaptiveRenderContext, View>(AdaptiveColumnSetRenderer.Render));
            //this.ElementRenderers.Set<AdaptiveFactSet>(new Func<AdaptiveFactSet, AdaptiveRenderContext, View>(AdaptiveFactSetRenderer.Render));
            //this.ElementRenderers.Set<AdaptiveImageSet>(new Func<AdaptiveImageSet, AdaptiveRenderContext, View>(AdaptiveImageSetRenderer.Render));
            //this.ElementRenderers.Set<AdaptiveTextInput>(new Func<AdaptiveTextInput, AdaptiveRenderContext, View>(AdaptiveTextInputRenderer.Render));
            //this.ElementRenderers.Set<AdaptiveNumberInput>(new Func<AdaptiveNumberInput, AdaptiveRenderContext, View>(AdaptiveNumberInputRenderer.Render));
            //this.ElementRenderers.Set<AdaptiveDateInput>(new Func<AdaptiveDateInput, AdaptiveRenderContext, View>(AdaptiveDateInputRenderer.Render));
            //this.ElementRenderers.Set<AdaptiveTimeInput>(new Func<AdaptiveTimeInput, AdaptiveRenderContext, View>(AdaptiveTimeInputRenderer.Render));
            //this.ElementRenderers.Set<AdaptiveToggleInput>(new Func<AdaptiveToggleInput, AdaptiveRenderContext, View>(AdaptiveToggleInputRenderer.Render));
        }
    }
}
