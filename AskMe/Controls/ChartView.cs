using AskMe.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace AskMe.Controls
{
	public class ChartView : ContentView
	{
        private List<ResponseDatum> dataSource;
        public List<ResponseDatum> DataSource
        {
            get { return dataSource; }
            set
            {
                dataSource = value;
                OnPropertyChanged();
            }
        }

        private double maximumValue;
        public double MaximumValue
        {
            get { return maximumValue; }
            set
            {
                maximumValue = value;
                OnPropertyChanged();
            }
        }

        private string title;
        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                OnPropertyChanged();
            }
        }

    }
}