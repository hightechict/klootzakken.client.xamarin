using Android.App;
using Android.Content;
using Klootzakken.Client.Utils.Interfaces;

namespace Klootzakken.Client.Utils 
{
    public class SharedPreferenceHandler : ISharedPreferenceHandler
    {
        private ISharedPreferences _sharedPreference;

        public SharedPreferenceHandler()
        {
            _sharedPreference =  Application.Context.GetSharedPreferences("KlootzakkenClientApp", FileCreationMode.Private);
        }

        public string GetPreference(string key) {
            return _sharedPreference.GetString(key, null);
        }

        public void SavePreference(string key, string value)
        {
            var preferenceEditor = _sharedPreference.Edit();
            preferenceEditor.PutString(key, value);
            preferenceEditor.Commit();
        }
    }
}