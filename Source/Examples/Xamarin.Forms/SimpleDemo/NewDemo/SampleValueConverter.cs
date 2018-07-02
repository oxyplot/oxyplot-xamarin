using System;
using System.Globalization;
using System.Linq;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using Xamarin.Forms;

namespace NewDemo
{
    public class SampleValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var svm = value as SampleViewModel;
            if (svm == null) return null;
            var series = new ColumnSeries();

            foreach(var v in svm.Values)
            {
                series.Items.Add(new ColumnItem { Value = v });
            }

            var items = svm.Values.Select(v => new ColumnItem { Value = v }).ToArray();
            var model = new PlotModel
            {
                Title = "OxyPlot in Xamarin Forms.",
                Axes =
                {
                    new CategoryAxis {Position = AxisPosition.Bottom},
                    new LinearAxis {Position = AxisPosition.Left, MinimumPadding = 0}
                }
            };

            model.Series.Add(series);

            return model;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
