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

namespace Klootzakken.Client.Domain
{
    public class AuthenticationOptions //Options patters
    {
        //add other options
        public Uri BaseUri { get; set; }
    }
}