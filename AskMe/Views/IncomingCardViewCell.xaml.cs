using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AskMe.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class IncomingCardViewCell : ViewCell
	{
		public IncomingCardViewCell()
		{
			InitializeComponent ();
            //MessagingCenter.Subscribe<Page>(this, "SpeakingCompleted", (sender) =>
            //{
            //    if (IconGrid.Children.Contains(this.animationView))
            //        this.IconGrid.Children.Remove(this.animationView);
            //    MessagingCenter.Unsubscribe<Page>(this, "SpeakingCompleted");
            //});
		}

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            //Device.BeginInvokeOnMainThread(ForceUpdateSize);
            //var parent = this.Parent;
        }

        public void HideIcon()
        {
            this.IconColDef.Width = new GridLength(0);
        }
    }
}