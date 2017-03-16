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
using System.Net.Http;

namespace Klootzakken.Client.Data
{
    public interface IApiClient
    {
        Task<T> GetAsync<T>(string path);
        Task<bool> PostAsync(string path, StringContent postParameters);
        Task PutAsync();
        Task DeleteAsync(); //NOTE: ALWAYS ASYNC IN CASE OF DBs and APIs (INPUT AND OUTPUT OPERATIONS)
    }
    //NOTE: DO NOT USE BOOLEANSE. throw exceptions instead!!!
}