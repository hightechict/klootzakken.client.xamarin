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
using Newtonsoft.Json.Linq;

namespace Klootzakken.Client.Utils
{
    public static class JsonStringParser
    {
        public static string GetValue(string jsonParameterName, string jsonString)
        {
            return JObject.Parse(jsonString)[jsonParameterName].ToString();
        }
    }
}