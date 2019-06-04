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
	public partial class IncomingTextViewIconLessCell : ViewCell
	{
		public IncomingTextViewIconLessCell()
		{
			InitializeComponent ();

            //MessagingCenter.Subscribe<Page>(this, "SpeakingCompleted", (sender) =>
            //{
            //    if (IconGrid.Children.Contains(this.animationView))
            //        this.IconGrid.Children.Remove(this.animationView);
            //    MessagingCenter.Unsubscribe<Page>(this, "SpeakingCompleted");
            //});
        }
	}
}