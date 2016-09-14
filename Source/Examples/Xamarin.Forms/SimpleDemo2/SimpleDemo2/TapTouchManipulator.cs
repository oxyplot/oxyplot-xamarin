using System;
using System.Collections.Generic;
using System.Windows.Input;
using OxyPlot;

namespace SimpleDemo2
{
	public class TapTouchManipulator : TouchManipulator
	{
		private const int TAP_TOLERANCE = 10;
		private const int HIT_TOLERANCE = 15;

		private ICommand _tapCommand = null;
		private ScreenPoint _startPos;

		public TapTouchManipulator(IPlotView plotView, ICommand tapCommand) : base(plotView)
		{
			_tapCommand = tapCommand;
		}

		public override void Started(OxyTouchEventArgs e)
		{
			base.Started(e);
			_startPos = e.Position;
			//Always handle event so we get Completed below which handles Tap
			e.Handled = true;
		}

		public override void Completed(OxyTouchEventArgs e)
		{
			base.Completed(e);
			if (e.Position.DistanceTo(_startPos) >= TAP_TOLERANCE)
				return;
			IEnumerable<HitTestResult> results = PlotView.ActualModel.HitTest(new HitTestArguments(e.Position, HIT_TOLERANCE));
			if (_tapCommand != null)
				_tapCommand.Execute(results);
		}
	}
}
