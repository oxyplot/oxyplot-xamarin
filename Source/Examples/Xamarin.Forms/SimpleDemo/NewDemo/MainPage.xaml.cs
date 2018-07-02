using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using Xamarin.Forms;

namespace NewDemo
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            BindingContext = new SampleViewModel();

            //PlotView.Model = new PlotModel
            //{
            //    Title = "OxyPlot in Xamarin Forms.",
            //    Axes =
            //            {
            //                new CategoryAxis {Position = AxisPosition.Bottom},
            //                new LinearAxis {Position = AxisPosition.Left, MinimumPadding = 0}
            //            },
            //    Series =
            //            {
            //                new ColumnSeries
            //                {
            //                    Items =
            //                    {
            //                        new ColumnItem {Value = 3},
            //                        new ColumnItem {Value = 14},
            //                        new ColumnItem {Value = 11},
            //                        new ColumnItem {Value = 12},
            //                        new ColumnItem {Value = 7}
            //                    }
            //                }
            //            }
            //};
        }
    }
}
