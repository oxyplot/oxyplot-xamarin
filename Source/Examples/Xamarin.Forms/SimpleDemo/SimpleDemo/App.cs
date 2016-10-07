namespace SimpleDemo
{
    using OxyPlot;
    using OxyPlot.Axes;
    using OxyPlot.Series;
    using OxyPlot.Xamarin.Forms;
    using Xamarin.Forms;

    public class App : Application
    {
        public App()
        {
            // The root page of your application
            var content = new ContentPage
            {
                Title = "SimpleDemo",
                Content = new PlotView
                {
                    Model = new PlotModel
                    {
                        Title = "OxyPlot in Xamarin Forms.",
                        Axes =
                        {
                            new CategoryAxis {Position = AxisPosition.Bottom},
                            new LinearAxis {Position = AxisPosition.Left, MinimumPadding = 0}
                        },
                        Series =
                        {
                            new ColumnSeries
                            {
                                Items =
                                {
                                    new ColumnItem {Value = 3},
                                    new ColumnItem {Value = 14},
                                    new ColumnItem {Value = 11},
                                    new ColumnItem {Value = 12},
                                    new ColumnItem {Value = 7}
                                }
                            }
                        }
                    },
                    VerticalOptions = LayoutOptions.Fill,
                    HorizontalOptions = LayoutOptions.Fill
                }
            };

            this.MainPage = new NavigationPage(content);
        }
    }
}
