using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Klootzakken.Client.Activities
{
    [Activity(Label = "MainGame")]
    public class MainGame : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            //SetContentView(Resource.Layout.GameDeckView);
            var IApiClient = savedInstanceState ;


            /*
            button.Click += delegate
            {
                var imageView =
                       FindViewById<ImageView>(Resource.Id.demoImageView);
                imageView.SetImageResource(Resource.Drawable.sample2);
            };*/
        }
    }
}