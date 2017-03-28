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
        private RelayCommand _popUpGeneratedPinAndActions;

        public RelayCommand PopUpGeneratedPinAndActions
        {
            get
            {
                return _popUpGeneratedPinAndActions
                    ?? (_popUpGeneratedPinAndActions = new RelayCommand(
                    async () =>
                    {
                        var dialogService = ServiceLocator.Current.GetInstance<IDialogService>();
                        var authenticationController = ServiceLocator.Current.GetInstance<AuthenticationController>();
                        var pinCode = await authenticationController.GetPinCodeAsync();

                        await dialogService.ShowMessage(
                           pinCode,
                            "Pair the following pincode",
                            "Log in",
                            "Cancel",
                            new Action<bool>((isConfirmed) =>
                            {
                                if (isConfirmed) {
                                    authenticationController.SaveBearerAuthTokenAsync(pinCode);
                                }
                            }
                            ));
                    }));
            }
        }
    }
}