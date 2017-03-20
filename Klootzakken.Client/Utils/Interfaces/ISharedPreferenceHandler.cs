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

namespace Klootzakken.Client.Utils.Interfaces
{
    public interface ISharedPreferenceHandler
    {
        string GetPreference(string key);

        void SavePreference(string key, string value);
    }
}