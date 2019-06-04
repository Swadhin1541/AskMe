using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AskMe
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
	{
		public LoginPage ()
		{
			InitializeComponent ();
		}

        private void ContinueBtn_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(userNameEntry.Text))
                this.DisplayAlert("", "Enter the username...", "OK");
            else if (string.IsNullOrWhiteSpace(emailEntry.Text))
                this.DisplayAlert("", "Enter the email id...", "OK");
            else if (!IsValidEmail(emailEntry.Text))
                this.DisplayAlert("", "Enter the valid email id...", "OK");
            else
            {
                App.Current.Properties.Add("Username", userNameEntry.Text);
                App.Current.Properties.Add("Email", emailEntry.Text);
                App.Current.SavePropertiesAsync();
                App.Current.MainPage = new MainPage();
            }
        }

        bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}