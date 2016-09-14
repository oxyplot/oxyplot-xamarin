namespace IssueDemos.Pages.TapDemo
{
    using System.Windows.Input;
    using OxyPlot;


    public class TapTouchManipulator : TouchManipulator
    {
        private const int TAP_TOLERANCE = 10;
        private const int HIT_TOLERANCE = 15;

        private readonly ICommand tapCommand;
        private ScreenPoint startPos;

        public TapTouchManipulator(IPlotView plotView, ICommand tapCommand) : base(plotView)
        {
            this.tapCommand = tapCommand;
        }

        public override void Started(OxyTouchEventArgs e)
        {
            base.Started(e);
            this.startPos = e.Position;

            //Always handle event so we get Completed below which handles Tap
            e.Handled = true;
        }

        public override void Completed(OxyTouchEventArgs e)
        {
            base.Completed(e);
            if (e.Position.DistanceTo(this.startPos) >= TAP_TOLERANCE)
            {
                return;
            }

            var results = PlotView.ActualModel.HitTest(new HitTestArguments(e.Position, HIT_TOLERANCE));
            if (this.tapCommand != null)
            {
                this.tapCommand.Execute(results);
            }
        }
    }
}
