using Syncfusion.SfChart.XForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AskMe.Controls
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PieChartView : ChartView
	{
        public PieChartView()
        {
            InitializeComponent();
        }

        public void AddChart(SfChart chart)
        {
            Grid.SetRow(chart, 1);
            this.ParentLayout.Children.Add(chart);
        }
	}
}