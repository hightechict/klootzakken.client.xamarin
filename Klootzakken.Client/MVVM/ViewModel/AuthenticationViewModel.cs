using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace Klootzakken.Client.MVVM.ViewModel
{
    public class PinGenerationViewModel : ViewModelBase
    {
        //relay = közvetit
        private RelayCommand _incrementCommand;

        public RelayCommand IncrementCommand
        {
            get
            {
                return _incrementCommand
                    ?? (_incrementCommand = new RelayCommand(
                    () =>
                    {

                    }));
            }
        }

    }
}