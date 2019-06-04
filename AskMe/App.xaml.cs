using AskMe.Utility;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace AskMe
{
    public partial class App : Application
    {
        public static bool IsNewUser { get; private set; }

		public App ()
		{
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NjMwN0AzMTM2MmUzMjJlMzBWZTVuanlJVEJTRm11VkdZN09UUmRRQ3RVK2VRMGZYdHFPems3LzhHTFJJPQ==");
            InitializeComponent();

            //if (!App.Current.Properties.ContainsKey("Username") || !App.Current.Properties.ContainsKey("Email"))
            {
                IsNewUser = true;
                MainPage = new MainPage();
            }
            //else
            //{
            //    Constants.Username = App.Current.Properties["Username"].ToString();
            //    Constants.Email = App.Current.Properties["Email"].ToString();
            //    IsNewUser = false;
            //    MainPage = new MainPage();// mainPage;
            //}
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
