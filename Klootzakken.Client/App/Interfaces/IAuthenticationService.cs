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
using System.Threading.Tasks;

namespace Klootzakken.Client.Domain
{
    public interface IAuthenticationService //TODO: remove it from the client and solve it ....later issue...
    {
        Task<string> GetPinAsync();

        Task<string> GetTemporaryAuthToken(string pin);
        //expose - it can be called by someone else

        Task<string> GetBearerTokenAsync(string temporaruAuthToken);
    }
}