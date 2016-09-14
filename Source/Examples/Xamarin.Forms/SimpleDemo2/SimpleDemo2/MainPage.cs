using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.Xamarin.Forms;
using Xamarin.Forms;

namespace SimpleDemo2
{
	public class MainPage : ContentPage
	{
		private Command<IEnumerable<HitTestResult>> _tapCommand = null;
		public ICommand TapCommand {
			get {
				_tapCommand = _tapCommand ?? new Command<IEnumerable<HitTestResult>>(DoTapCommand);
				return (_tapCommand);
			}
		}

		public MainPage()
		{
			var plotModel = new PlotModel {
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

			Padding = new Thickness(0, 20, 0, 0);
			PlotView plotV = new PlotView {
				Model = plotModel,
				Controller = new PlotController(),
				VerticalOptions = LayoutOptions.Fill,
				HorizontalOptions = LayoutOptions.Fill,
			};
			//Rebind touch to be TapTouchManipulator that will call an ICommand on a tap gesture
			plotV.Controller.BindTouchDown(new DelegatePlotCommand<OxyTouchEventArgs>((view, controller, args) => controller.AddTouchManipulator(view, new TapTouchManipulator(view, TapCommand), args)));
			Content = plotV;
		}

		private void DoTapCommand(IEnumerable<HitTestResult> results)
		{
			if (!results.Any())
				return;
			HitTestResult result = results.First();
			if (result == null)
				return;
			ColumnItem dp = result.Item as ColumnItem;
			if (dp == null)
				return;
			DisplayAlert("Info", "Touched: " + dp.Value, "Close");
		}
	}
}
