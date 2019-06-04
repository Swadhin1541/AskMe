using AskMe.Model;
using AskMe.Utility;
using AskMe.ViewModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AskMe.Controls
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HorizontalBarList : ChartView
	{
		public HorizontalBarList ()
		{
			InitializeComponent ();
		}

        private List<ResponseDatum> fullDataSource;
        public List<ResponseDatum> FullDataSource
        {
            get { return fullDataSource; }
            set
            {
                if (fullDataSource != value)
                {
                    fullDataSource = value;
                    SetDataSource();
                    OnPropertyChanged();
                }
            }
        }
        private void SetDataSource()
        {
            if (fullDataSource.Count <= 3)
            {
                DataSource = fullDataSource;
                ShowMoreGrid.IsVisible = false;
            }
            else
                DataSource = fullDataSource;//.Take(3).ToList();

            listView.HeightRequest = listView.RowHeight * DataSource.Count;
        }

        private void ShowMoreButton_Clicked(object sender, EventArgs e)
        {
            if (ShowMoreButton.Text == "Show More")
            {
                //DataSource = FullDataSource;
                listView.HeightRequest = listView.RowHeight * DataSource.Count;
                ShowMoreButton.Text = "Show Less";
                ReloadAndScroll();
            }
            else
            {
                //DataSource = fullDataSource;//.Take(3).ToList();
                listView.HeightRequest = listView.RowHeight * 3;
                ShowMoreButton.Text = "Show More";
                ReloadAndScroll("End");
            }
        }

        private void ReloadAndScroll(string scrollPosition = "Start")
        {
            var hash = new Hashtable();
            var viewModel = App.Current.MainPage.BindingContext as MainPageViewModel;
            hash.Add("Index", viewModel.Messages.IndexOf(this.BindingContext as Message));
            hash.Add("ScrollPosition", scrollPosition);
            MessagingCenter.Send(App.Current.MainPage, "ReloadAndScrollListView", hash);
        }
    }
}