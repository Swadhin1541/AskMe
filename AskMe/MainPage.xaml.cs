using AskMe.Services;
using AskMe.Utility;
using AskMe.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace AskMe
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
	{
		public MainPage()
		{
            //Constants.Username = App.Current.Properties["Username"].ToString();
            //Constants.Email = App.Current.Properties["Email"].ToString();

            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (Device.RuntimePlatform == Device.iOS)
            {
                var safeInsets = On<Xamarin.Forms.PlatformConfiguration.iOS>().SafeAreaInsets();
                if (safeInsets.Bottom > 0)
                {
                    safeInsets.Bottom = 0;
                    this.Padding = safeInsets;
                }
            }
        }
    }
}
