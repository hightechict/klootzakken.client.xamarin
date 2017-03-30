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
using GalaSoft.MvvmLight.Views;
using GalaSoft.MvvmLight.Ioc;

namespace Klootzakken.Client.App.Configurators
{
    public class DialogServiceConfigurator
    {
        public void Configure()
        {
            var dialog = new DialogService();
            SimpleIoc.Default.Register<IDialogService>(() => dialog);
        }
    }
}