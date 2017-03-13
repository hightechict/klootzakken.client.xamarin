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
using System.Net.Http;


namespace Klootzakken.Client.App.Authentication
{
    public class PinAuthenticationException : Exception
    {
        public PinAuthenticationException(string message)
            : base(message)
        {
        }

    }
}