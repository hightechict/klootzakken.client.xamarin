using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using Klootzakken.Client.App.Authentication;
using Microsoft.Practices.ServiceLocation;
using System;

namespace Klootzakken.Client.MVVM.ViewModel
{
    public class PinGenerationViewModel : ViewModelBase
    {
        //relay = közvetit
        private RelayCommand _popUpGeneratedPinAndConfirmActions;

        public RelayCommand PopUpGeneratedPinAndConfirmActions
        {
            get { return _popUpGeneratedPinAndConfirmActions ?? (_popUpGeneratedPinAndConfirmActions = new RelayCommand(CreatePopUpMessage())); }
        }

        private static Action CreatePopUpMessage()
        {
            return async () =>
            {
                var dialogService = ServiceLocator.Current.GetInstance<IDialogService>();
                var authenticationController = ServiceLocator.Current.GetInstance<AuthenticationController>();
                var pinCode = await authenticationController.GetPinCodeAsync();

                await dialogService.ShowMessage(
                   pinCode,
                    "Pair the following pincode",
                    "Log in",
                    "Cancel",
                    SaveBearerTokenAfterConfirmation(authenticationController, pinCode));
            };
        }

        private static Action<bool> SaveBearerTokenAfterConfirmation(AuthenticationController authenticationController, string pinCode)
        {
            return new Action<bool>((isConfirmed) =>
            {
                if (isConfirmed)
                {
                    authenticationController.SaveBearerAuthTokenAsync(pinCode);
                    var navigationService = ServiceLocator.Current.GetInstance<INavigationService>();
                    navigationService.NavigateTo(_mainMenuActivityPageKey);
                }
            });
        }
    }
}