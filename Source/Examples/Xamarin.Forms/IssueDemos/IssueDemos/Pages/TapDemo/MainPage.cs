namespace IssueDemos.Pages.TapDemo
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Input;
    using OxyPlot;
    using OxyPlot.Axes;
    using OxyPlot.Series;
    using OxyPlot.Xamarin.Forms;
    using Xamarin.Forms;

    [DemoPage("TabbedPage with ItemTemplate", "Pages defined in ItemTemplate")]
    public class MainPage : ContentPage
    {
        private Command<IEnumerable<HitTestResult>> tapCommand;

        public ICommand TapCommand
        {
            get
            {
                return this.tapCommand = this.tapCommand ?? new Command<IEnumerable<HitTestResult>>(this.DoTapCommand);
            }
        }

        public MainPage()
        {
            var plotModel = new PlotModel
            {
                Title = "OxyPlot in Xamarin.Forms",
                Subtitle = string.Format("OS: {0}, Idiom: {1}", Device.OS, Device.Idiom),
                Background = OxyColors.LightYellow,
                PlotAreaBackground = OxyColors.LightGray
            };
            var categoryAxis = new CategoryAxis { Position = AxisPosition.Bottom };
            var valueAxis = new LinearAxis { Position = AxisPosition.Left, MinimumPadding = 0 };
            plotModel.Axes.Add(categoryAxis);
            plotModel.Axes.Add(valueAxis);
            var series = new ColumnSeries();
            series.Items.Add(new ColumnItem { Value = 3 });
            series.Items.Add(new ColumnItem { Value = 14 });
            series.Items.Add(new ColumnItem { Value = 11 });
            series.Items.Add(new ColumnItem { Value = 12 });
            series.Items.Add(new ColumnItem { Value = 7 });
            plotModel.Series.Add(series);

            this.Padding = new Thickness(0, 20, 0, 0);
            var plotView = new PlotView
            {
                Model = plotModel,
                Controller = new PlotController(),
                VerticalOptions = LayoutOptions.Fill,
                HorizontalOptions = LayoutOptions.Fill,
            };

            //Rebind touch to be TapTouchManipulator that will call an ICommand on a tap gesture
            plotView.Controller.BindTouchDown(new DelegatePlotCommand<OxyTouchEventArgs>((view, controller, args) => controller.AddTouchManipulator(view, new TapTouchManipulator(view, this.TapCommand), args)));
            this.Content = plotView;
        }

        private void DoTapCommand(IEnumerable<HitTestResult> results)
        {
            var resultsArray = results.ToArray();
            if (!resultsArray.Any())
                return;
            var result = resultsArray.First();
            if (result == null)
                return;
            var dp = result.Item as ColumnItem;
            if (dp == null)
                return;
            this.DisplayAlert("Info", "Touched: " + dp.Value, "Close");
        }
    }
}
