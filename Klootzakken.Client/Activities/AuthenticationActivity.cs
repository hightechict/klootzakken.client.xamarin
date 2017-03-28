using Android.App;
using Android.Widget;
using Android.OS;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;
using System;
using Klootzakken.Client.App.Authentication;
using Klootzakken.Client.Utils;
using Klootzakken.Client.MVVM.ViewModel;
using GalaSoft.MvvmLight.Helpers;

namespace Klootzakken.Client
{
    [Activity(Label = "KlootzakkenClient", MainLauncher = false, Icon = "@drawable/icon")]
    public class AuthenticationActivity : ActivityBase
    {
        private PinGenerationViewModel _pinGenerationViewModel;
        private Button _generatePinBtn;

        public PinGenerationViewModel PinGenerationViewModel
        {
            get { return _pinGenerationViewModel ?? (_pinGenerationViewModel = new PinGenerationViewModel()); }
        }

        public Button GeneratePinButton
        {
            get { return _generatePinBtn ?? (_generatePinBtn = FindViewById<Button>(Resource.Id.generatePin)); }
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.AuthenticationView);

            var dialogService = ServiceLocator.Current.GetInstance<IDialogService>();
            var nav = ServiceLocator.Current.GetInstance<INavigationService>();

            GeneratePinButton.SetCommand("Click", PinGenerationViewModel.PopUpGeneratedPinAndConfirmActions);

            RunOnUiThread(() => Toast.MakeText(this, new SharedPreferenceHandler().GetPreference("bearer_token"), ToastLength.Long).Show());
        }
    }
}