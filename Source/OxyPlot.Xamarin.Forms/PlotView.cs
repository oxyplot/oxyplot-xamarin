// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PlotView.cs" company="OxyPlot">
//   Copyright (c) 2014 OxyPlot contributors
// </copyright>
// <summary>
//   Represents a visual element that displays a PlotModel.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OxyPlot.Xamarin.Forms
{
    using System;

    using OxyPlot;

    using global::Xamarin.Forms;

    /// <summary>
    /// Represents a visual element that displays a <see cref="PlotModel" />.
    /// </summary>
    public class PlotView : View
    {
        /// <summary>
        /// Identifies the <see cref="Controller" />  bindable property.
        /// </summary>
        public static readonly BindableProperty ControllerProperty = BindableProperty.Create(nameof(Controller), typeof(PlotController), typeof(PlotView));

        /// <summary>
        /// Identifies the <see cref="Model" />  bindable property.
        /// </summary>
        public static readonly BindableProperty ModelProperty = BindableProperty.Create(nameof(Model), typeof(PlotModel), typeof(PlotView));

        /// <summary>
        /// Initializes a new instance of the <see cref="PlotView"/> class.
        /// </summary>
        /// <exception cref="InvalidOperationException">Renderer is not initialized</exception>
        public PlotView()
        {
            if (!IsRendererInitialized && !DesignMode.IsDesignModeEnabled)
            {
                var message = "Renderer is not initialized.";
                switch (Device.RuntimePlatform)
                {
                    case Device.UWP:
                        message +=
                            "\nRemember to call `OxyPlot.Xamarin.Forms.Platform.UWP.PlotViewRenderer.Init();` after `Xamarin.Forms.Forms.Init(e);` in the Universal Windows app project.";
                        break;
                    case Device.Android:
                        message +=
                            "\nRemember to call `OxyPlot.Xamarin.Forms.Platform.Android.PlotViewRenderer.Init();` after `Xamarin.Forms.Forms.Init();` in the Android app project.";
                        break;
                    case Device.iOS:
                        message +=
                            "\nRemember to call `OxyPlot.Xamarin.Forms.Platform.iOS.PlotViewRenderer.Init();` after `Xamarin.Forms.Forms.Init();` in the iOS app project.";
                        break;
                    case Device.macOS:
                        message +=
                            "\nRemember to call `OxyPlot.Xamarin.Forms.Platform.MacOS.PlotViewRenderer.Init();` after `Xamarin.Forms.Forms.Init();` in the iOS app project.";
                        break;
                    case Device.GTK:
                    case Device.Tizen:
                    case Device.WPF:
                        message +=
                            "\nRemember to call `OxyPlot.Xamarin.Forms.Platform.*.PlotViewRenderer.Init();` after `Xamarin.Forms.Forms.Init();` in the iOS app project.";
                        break;
                }

                throw new InvalidOperationException(message);
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="PlotModel"/> to view.
        /// </summary>
        /// <value>The model.</value>
        public PlotModel Model
        {
            get { return (PlotModel)this.GetValue(ModelProperty); }
            set { this.SetValue(ModelProperty, value); }
        }

        /// <summary>
        /// Gets or sets the <see cref="PlotController"/> for the view.
        /// </summary>
        /// <value>The controller.</value>
        public PlotController Controller
        {
            get { return (PlotController)this.GetValue(ControllerProperty); }
            set { this.SetValue(ControllerProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the renderer is "initialized".
        /// </summary>
        /// <value><c>true</c> if the renderer is initialized; otherwise, <c>false</c>.</value>
        public static bool IsRendererInitialized { get; set; }
    }
}
