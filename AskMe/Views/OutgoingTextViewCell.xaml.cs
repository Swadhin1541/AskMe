using AskMe.Utility;
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
	public partial class OutgoingTextViewCell : ViewCell
	{
		public OutgoingTextViewCell ()
		{
			InitializeComponent ();
            //userLabel.Text = Constants.Username[0].ToString();
		}
	}
}