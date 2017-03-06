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

namespace Klootzakken.Client.Data
{
    public class ApiClientOptions
    {
        public Uri BaseUri { get; set; } //it is a property thats why use capital
    }
}