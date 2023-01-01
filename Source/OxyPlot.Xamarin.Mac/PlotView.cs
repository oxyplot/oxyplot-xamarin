// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PlotView.cs" company="OxyPlot">
//   Copyright (c) 2014 OxyPlot contributors
// </copyright>
// <summary>
//   Provides a view that can show a <see cref="PlotModel" />.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OxyPlot.Xamarin.Mac
{
    using AppKit;
    using Foundation;

    using OxyPlot;

    /// <summary>
    /// Provides a view that can show a <see cref="PlotModel" />. 
    /// </summary>
    [Register("PlotView")]
    public class PlotView : NSView, IPlotView
    {
        /// <summary>
        /// The current plot model.
        /// </summary>
        private PlotModel model;

        /// <summary>
        /// The default plot controller.
        /// </summary>
        private IPlotController defaultController;
               
        /// <summary>
        /// Initializes a new instance of the <see cref="OxyPlot.Xamarin.Mac.PlotView"/> class.
        /// </summary>
        public PlotView()
        {
            this.Initialize ();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OxyPlot.Xamarin.Mac.PlotView"/> class.
        /// </summary>
        /// <param name="frame">The initial frame.</param>
        public PlotView(CoreGraphics.CGRect frame) : base(frame)
        {
            this.Initialize ();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OxyPlot.Xamarin.Mac.PlotView"/> class.
        /// </summary>
        /// <param name="coder">Coder.</param>
        [Export ("initWithCoder:")]
        public PlotView(NSCoder coder) : base (coder)
        {
            this.Initialize ();
        }

        /// <summary>
        /// Uses the new layout.
        /// </summary>
        /// <returns><c>true</c>, if new layout was used, <c>false</c> otherwise.</returns>
        [Export ("requiresConstraintBasedLayout")]
        bool UseNewLayout ()
        {
            return true;
        }

        /// <summary>
        /// Initialize the view.
        /// </summary>
        private void Initialize() {
            this.WantsLayer = true;
        }

        /// <summary>
        /// Dispose the instance.
        /// </summary>
        /// <param name="disposing">Is the instance disposing.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.Model = null;
            }

            base.Dispose(disposing);
        }

        /// <summary>
        /// Gets or sets the <see cref="PlotModel"/> to show in the view. 
        /// </summary>
        /// <value>The <see cref="PlotModel"/>.</value>
        public PlotModel Model
        {
            get
            {
                return this.model;
            }

            set
            {
                if (this.model != value)
                {
                    if (this.model != null)
                    {
                        ((IPlotModel)this.model).AttachPlotView(null);
                        this.model = null;
                    }

                    if (value != null)
                    {
                        ((IPlotModel)value).AttachPlotView(this);
                        this.model = value;
                    }
					
                    this.InvalidatePlot();
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="IPlotController"/> that handles input events.
        /// </summary>
        /// <value>The <see cref="IPlotController"/>.</value>
        public IPlotController Controller { get; set; }

        /// <summary>
        /// Gets the actual model in the view.
        /// </summary>
        /// <value>
        /// The actual model.
        /// </value>
        Model IView.ActualModel
        {
            get
            {
                return this.Model;
            }
        }

        /// <summary>
        /// Gets the actual <see cref="PlotModel"/> to show.
        /// </summary>
        /// <value>The actual model.</value>
        public PlotModel ActualModel
        {
            get
            {
                return this.Model;
            }
        }

        /// <summary>
        /// Gets the actual controller.
        /// </summary>
        /// <value>
        /// The actual <see cref="IController" />.
        /// </value>
        IController IView.ActualController
        {
            get
            {
                return this.ActualController;
            }
        }

        /// <summary>
        /// Gets the coordinates of the client area of the view.
        /// </summary>
        public OxyRect ClientArea
        {
            get
            {
                // TODO
                return new OxyRect(0, 0, 100, 100);
            }
        }

        /// <summary>
        /// Gets the actual <see cref="IPlotController"/>.
        /// </summary>
        /// <value>The actual plot controller.</value>
        public IPlotController ActualController
        {
            get
            {
                return this.Controller ?? (this.defaultController ?? (this.defaultController = CreateDefaultController()));
            }
        }

        private PlotController CreateDefaultController(){
            var c = new PlotController ();
            c.UnbindMouseDown (OxyMouseButton.Left);
            c.BindMouseDown (OxyMouseButton.Left, PlotCommands.PanAt);
            return c;
        }

        /// <summary>
        /// Hides the tracker.
        /// </summary>
        public void HideTracker()
        {
        }

        /// <summary>
        /// Hides the zoom rectangle.
        /// </summary>
        public void HideZoomRectangle()
        {
        }

        /// <summary>
        /// Invalidates the plot (not blocking the UI thread)
        /// </summary>
        /// <param name="updateData">If set to <c>true</c> update data.</param>
        public void InvalidatePlot(bool updateData = true)
        {
            var actualModel = this.model;
            if (actualModel != null)
            {
                // TODO: update the model on a background thread
                ((IPlotModel)actualModel).Update(updateData);
            }

            if (actualModel != null && !actualModel.Background.IsUndefined())
            {
                this.WantsLayer = true;
                this.Layer.BackgroundColor = actualModel.Background.ToCGColor();
            }
            else
            {
                // Use white as default background color
                this.Layer.BackgroundColor = OxyColors.White.ToCGColor();
            }

            this.NeedsDisplay = true;
        }

        /// <summary>
        /// Sets the cursor type.
        /// </summary>
        /// <param name="cursorType">The cursor type.</param>
        public void SetCursorType(CursorType cursorType)
        {
            this.ResetCursorRects ();
            var cursor = Convert (cursorType);
            if (cursor!=null)
            this.AddCursorRect (this.Bounds, cursor);
        }

		/// <summary>
		/// Convert the specified cursor type.
		/// </summary>
		/// <param name="cursorType">Cursor type.</param>
		/// <returns>The converted cursor.</returns>
        public static NSCursor Convert(CursorType cursorType){
            switch (cursorType) {
            case CursorType.Default:
                return null;
            case CursorType.Pan:
                return NSCursor.PointingHandCursor;
            case CursorType.ZoomHorizontal:
                return NSCursor.ResizeUpDownCursor;
            case CursorType.ZoomVertical:
                return NSCursor.ResizeLeftRightCursor;
            case CursorType.ZoomRectangle:
                return NSCursor.CrosshairCursor;
            default:
                return null;
            }
        }

        /// <summary>
        /// Shows the tracker.
        /// </summary>
        /// <param name="trackerHitResult">The tracker data.</param>
        public void ShowTracker(TrackerHitResult trackerHitResult)
        {
            // TODO
        }

        /// <summary>
        /// Shows the zoom rectangle.
        /// </summary>
        /// <param name="rectangle">The rectangle.</param>
        public void ShowZoomRectangle(OxyRect rectangle)
        {
            // TODO
        }

        /// <summary>
        /// Stores text on the clipboard.
        /// </summary>
        /// <param name="text">The text.</param>
        public void SetClipboardText(string text)
        {
            // TODO
            // UIPasteboard.General.SetValue(new NSString(text), "public.utf8-plain-text");
        }

        /// <summary>
        /// Draws the content of the view.
        /// </summary>
        /// <param name="dirtyRect">The rectangle to draw.</param>
        public override void DrawRect(CoreGraphics.CGRect dirtyRect)
        {
            if (this.model != null)
            {
                var context = NSGraphicsContext.CurrentContext.GraphicsPort;
                context.TranslateCTM(0f, dirtyRect.Height);
                context.ScaleCTM(1f, -1f);
                // TODO: scale font matrix??
                using (var renderer = new CoreGraphicsRenderContext(context))
                {
                        ((IPlotModel)this.model).Render(renderer, dirtyRect.Width, dirtyRect.Height);
                    }
            }
        }

		/// <summary>
		/// Handles the mouse down event.
		/// </summary>
		/// <param name="theEvent">The event.</param>
        public override void MouseDown (NSEvent theEvent)
        {
            base.MouseDown (theEvent);
            this.ActualController.HandleMouseDown (this, this.ToMouseDownEventArgs (theEvent));
        }

		/// <summary>
		/// Handles the mouse dragged event.
		/// </summary>
		/// <param name="theEvent">The event.</param>
        public override void MouseDragged (NSEvent theEvent)
        {
            base.MouseDragged (theEvent);
            this.ActualController.HandleMouseMove (this, this.ToMouseEventArgs (theEvent));
        }

		/// <summary>
		/// Handles the mouse moved event.
		/// </summary>
		/// <param name="theEvent">The event.</param>
        public override void MouseMoved (NSEvent theEvent)
        {
            base.MouseMoved (theEvent);
            this.ActualController.HandleMouseMove (this, this.ToMouseEventArgs (theEvent));
        }

		/// <summary>
		/// Handles the mouse up event.
		/// </summary>
		/// <param name="theEvent">The event.</param>
        public override void MouseUp (NSEvent theEvent)
        {
            base.MouseUp (theEvent);
            this.ActualController.HandleMouseUp (this, this.ToMouseEventArgs (theEvent));
        }

		/// <summary>
		/// Handles the mouse entered event.
		/// </summary>
		/// <param name="theEvent">The event.</param>
        public override void MouseEntered (NSEvent theEvent)
        {
            base.MouseEntered (theEvent);
            this.ActualController.HandleMouseEnter (this, this.ToMouseEventArgs (theEvent));
        }

		/// <summary>
		/// Handles the mouse exited event.
		/// </summary>
		/// <param name="theEvent">The event.</param>
        public override void MouseExited (NSEvent theEvent)
        {
            base.MouseExited (theEvent);
            this.ActualController.HandleMouseLeave (this, this.ToMouseEventArgs (theEvent));
        }

		/// <summary>
		/// Handles the mouse wheel event.
		/// </summary>
		/// <param name="theEvent">The event.</param>
        public override void ScrollWheel (NSEvent theEvent)
        {
            // TODO: use scroll events to pan?
            base.ScrollWheel (theEvent);
            this.ActualController.HandleMouseWheel (this, this.ToMouseWheelEventArgs (theEvent));
        }

		/// <summary>
		/// Handles the other mouse down event.
		/// </summary>
		/// <param name="theEvent">The event.</param>
        public override void OtherMouseDown (NSEvent theEvent)
        {
            base.OtherMouseDown (theEvent);
        }

		/// <summary>
		/// Handles the right mouse down event.
		/// </summary>
		/// <param name="theEvent">The event.</param>
        public override void RightMouseDown (NSEvent theEvent)
        {
            base.RightMouseDown (theEvent);
        }

		/// <summary>
		/// Handles the key down event.
		/// </summary>
		/// <param name="theEvent">The event.</param>
        public override void KeyDown (NSEvent theEvent)
        {
            base.KeyDown (theEvent);
            this.ActualController.HandleKeyDown (this, this.ToKeyEventArgs(theEvent));
        }

		/// <summary>
		/// Handles the touches began event.
		/// </summary>
		/// <param name="theEvent">The event.</param>
        public override void TouchesBeganWithEvent (NSEvent theEvent)
        {
            base.TouchesBeganWithEvent (theEvent);
        }

		/// <summary>
		/// Handles the magnify event.
		/// </summary>
		/// <param name="theEvent">The event.</param>
        public override void MagnifyWithEvent (NSEvent theEvent)
        {
            base.MagnifyWithEvent (theEvent);
            // TODO: handle pinch event
            // https://developer.apple.com/library/mac/documentation/cocoa/conceptual/eventoverview/HandlingTouchEvents/HandlingTouchEvents.html
        }

		/// <summary>
		/// Handles the smart magnify event.
		/// </summary>
		/// <param name="withEvent">The event.</param>
        public override void SmartMagnify (NSEvent withEvent)
        {
            base.SmartMagnify (withEvent);
        }

		/// <summary>
		/// Handles the swipe event.
		/// </summary>
		/// <param name="theEvent">The event.</param>
        public override void SwipeWithEvent (NSEvent theEvent)
        {
            base.SwipeWithEvent (theEvent);
        }

        private OxyMouseDownEventArgs ToMouseDownEventArgs (NSEvent theEvent)
        {
            // https://developer.apple.com/library/mac/documentation/Cocoa/Reference/ApplicationKit/Classes/NSEvent_Class/Reference/Reference.html
            return new OxyMouseDownEventArgs {
                Position = this.GetRelativePosition(theEvent),
                ChangedButton = theEvent.Type.ToButton (),
                ModifierKeys = theEvent.ModifierFlags.ToModifierKeys (),
                ClickCount = (int)theEvent.ClickCount
            };
        }

        private OxyMouseEventArgs ToMouseEventArgs (NSEvent theEvent)
        {
            return new OxyMouseEventArgs {
                Position = this.GetRelativePosition(theEvent),
                ModifierKeys = theEvent.ModifierFlags.ToModifierKeys ()
            };
        }

        private OxyMouseWheelEventArgs ToMouseWheelEventArgs (NSEvent theEvent)
        {
            return new OxyMouseWheelEventArgs {
                Delta = (int)theEvent.ScrollingDeltaY,
                Position = this.GetRelativePosition(theEvent),
                ModifierKeys = theEvent.ModifierFlags.ToModifierKeys ()
            };
        }

        private OxyKeyEventArgs ToKeyEventArgs (NSEvent theEvent)
        {
            return new OxyKeyEventArgs {
                Key = theEvent.KeyCode.ToKey (),
                ModifierKeys = theEvent.ModifierFlags.ToModifierKeys ()
            };
        }

        /// <summary>
        /// Gets the event's position relative to this view.
        /// </summary>
        private ScreenPoint GetRelativePosition (NSEvent p)
        {
            // OSX has the origin in the lower left corner
            var relativePoint = this.ConvertPointFromView (p.LocationInWindow, null);
            var y = this.Bounds.Height - relativePoint.Y;
            return new ScreenPoint (relativePoint.X, y);
        }
    }
}