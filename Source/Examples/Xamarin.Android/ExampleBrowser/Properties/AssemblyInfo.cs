// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AssemblyDescription.cs" company="OxyPlot">
//   Copyright (c) 2014 OxyPlot contributors
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Reflection;
using Android.App;

[assembly: AssemblyTitle("OxyPlot ExampleBrowser")]
[assembly: AssemblyDescription("Example browser for Android")]

[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("OxyPlot")]
[assembly: AssemblyProduct("OxyPlot")]
[assembly: AssemblyCopyright("Copyright (c) 2014 OxyPlot contributors")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// The version numbers are updated by the build script. See ~/appveyor.yml
[assembly: AssemblyVersion("0.0.1")]
[assembly: AssemblyInformationalVersion("0.0.1-alpha")]
[assembly: AssemblyFileVersion("0.0.1")]

[assembly: UsesPermission(Android.Manifest.Permission.Internet)]
[assembly: UsesPermission(Android.Manifest.Permission.WriteExternalStorage)]