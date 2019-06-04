using AskMe.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AskMe.Behaviors
{
    public class MainPageBehavior : Behavior<MainPage>
    {
        Entry entry;
        MainPageViewModel viewModel;
        protected override void OnAttachedTo(MainPage bindable)
        {
            base.OnAttachedTo(bindable);
            entry = bindable.FindByName<Entry>("entry");
            entry.TextChanged += Entry_TextChanged;
            viewModel = entry.BindingContext as MainPageViewModel;
        }

        private void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(viewModel == null)
                viewModel = entry.BindingContext as MainPageViewModel;

            if (!string.IsNullOrEmpty(e.NewTextValue))
            {
                if (!viewModel.CanSend)
                    viewModel.CanSend = true;
            }
            else
            {
                if (viewModel.CanSend)
                    viewModel.CanSend = false;
            }
        }

        protected override void OnDetachingFrom(MainPage bindable)
        {
            base.OnDetachingFrom(bindable);
            entry.TextChanged -= Entry_TextChanged;
            entry = null;
            viewModel = null;
        }
    }
}
