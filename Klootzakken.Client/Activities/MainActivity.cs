using Android.App;
using Android.Widget;
using Android.OS;
using KlootzakkenClient.Activities;
using Klootzakken.Client;

namespace Klootzakken.Client
{
    [Activity(Label = "KlootzakkenClient", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);

            Button loginButton = FindViewById<Button>(Resource.Id.btnLogIn);

            loginButton.Click += delegate
            {
                StartActivity(typeof(MainMenuActivity));
            };
        }
    }
}