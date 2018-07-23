using System;

using AppKit;
using CoreGraphics;
using Foundation;

using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;

using OxyPlot.Xamarin.Mac;

namespace SimpleDemo
{
    public partial class ViewController : NSViewController
    {
        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var frame = new CGRect (10, 10, 600, 400);
            var plotView = new PlotView (frame);
            var model = new PlotModel{ Title = "Hello Mac!" };
            model.Axes.Add (new LinearAxis { Position = AxisPosition.Bottom });
            model.Axes.Add (new LinearAxis { Position = AxisPosition.Left });
            model.Series.Add (new FunctionSeries (Math.Sin, 0, 10, 200));
            plotView.Model = model;

            this.View = plotView;
        }

        public override NSObject RepresentedObject
        {
            get
            {
                return base.RepresentedObject;
            }
            set
            {
                base.RepresentedObject = value;
                // Update the view, if already loaded.
            }
        }
    }
}
