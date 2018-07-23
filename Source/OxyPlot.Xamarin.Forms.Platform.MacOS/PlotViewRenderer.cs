using OxyPlot.Xamarin.Forms;
using OxyPlot.Xamarin.Forms.Platform.MacOS;
using global::Xamarin.Forms;
using global::Xamarin.Forms.Platform.MacOS;

// Exports the renderer.
[assembly: Xamarin.Forms.ExportRenderer(typeof(OxyPlot.Xamarin.Forms.PlotView), typeof(OxyPlot.Xamarin.Forms.Platform.MacOS.PlotViewRenderer))]
namespace OxyPlot.Xamarin.Forms.Platform.MacOS
{
    using System.ComponentModel;
    using OxyPlot.Xamarin.Mac;

    /// <summary>
    /// Provides a custom <see cref="OxyPlot.Xamarin.Forms.PlotView" /> renderer for Xamarin.iOS.
    /// </summary>
    public class PlotViewRenderer : ViewRenderer<Xamarin.Forms.PlotView, PlotView>
    {
        /// <summary>
        /// Initializes static members of the <see cref="PlotViewRenderer"/> class.
        /// </summary>
        static PlotViewRenderer()
        {
            Init();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlotViewRenderer"/> class.
        /// </summary>
        public PlotViewRenderer()
        {
            // Do not delete
        }

        /// <summary>
        /// Initializes the renderer.
        /// </summary>
        /// <remarks>This method must be called before a <see cref="T:PlotView" /> is used.</remarks>
        public static void Init()
        {
            OxyPlot.Xamarin.Forms.PlotView.IsRendererInitialized = true;
        }

        /// <summary>
        /// Raises the element changed event.
        /// </summary>
        /// <param name="e">The event arguments.</param>
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.PlotView> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement != null || this.Element == null)
            {
                return;
            }

            var plotView = new PlotView
            {
                Model = this.Element.Model,
                Controller = this.Element.Controller
            };

            plotView.Layer.BackgroundColor = this.Element.BackgroundColor.ToOxyColor().ToCGColor();

            this.SetNativeControl(plotView);
        }

        /// <summary>
        /// Raises the element property changed event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (this.Element == null || this.Control == null)
            {
                return;
            }

            if (e.PropertyName == Xamarin.Forms.PlotView.ModelProperty.PropertyName)
            {
                this.Control.Model = this.Element.Model;
            }

            if (e.PropertyName == Xamarin.Forms.PlotView.ControllerProperty.PropertyName)
            {
                this.Control.Controller = this.Element.Controller;
            }

            if (e.PropertyName == VisualElement.BackgroundColorProperty.PropertyName)
            {
                this.Control.Layer.BackgroundColor = this.Element.BackgroundColor.ToOxyColor().ToCGColor();
            }

            if (e.PropertyName == VisualElement.IsVisibleProperty.PropertyName) {
                this.Control.InvalidatePlot (false);
            }
        }
    }
}
