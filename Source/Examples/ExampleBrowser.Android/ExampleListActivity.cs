// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExampleListActivity.cs" company="OxyPlot">
//   Copyright (c) 2014 OxyPlot contributors
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ExampleBrowser
{
    using System.Linq;

    using Android.App;
    using Android.Content;
    using Android.OS;
    using Android.Widget;

    [Activity(Label = "Example list", Icon = "@drawable/icon")]
    public class ExampleListActivity : ListActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            var category = this.Intent.GetStringExtra("category");
            this.Title = category;

            var examples = ExampleLibrary.Examples.GetList();
            var plots = examples.Where(e => e.Category == category).Select(e => e.Title).OrderBy(s => s).ToList();
            this.ListAdapter = new ArrayAdapter<string>(this, Resource.Layout.ListItem, plots);
            this.ListView.TextFilterEnabled = true;
            this.ListView.ItemClick += delegate (object sender, AdapterView.ItemClickEventArgs args)
            {
                // When clicked, show a toast with the TextView text
                Toast.MakeText(this.Application, ((TextView)args.View).Text, ToastLength.Short).Show();
                var second = new Intent(this, typeof(PlotActivity));

                second.PutExtra("category", category);
                second.PutExtra("plot", plots[args.Position]);
                this.StartActivity(second);
            };
        }
    }
}