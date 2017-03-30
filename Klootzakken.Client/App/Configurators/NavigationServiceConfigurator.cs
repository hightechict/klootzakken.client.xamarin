using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using KlootzakkenClient.Activities;

namespace Klootzakken.Client.App.Configurators
{
    public class NavigationServiceConfigurator
    {
        public const string _authenticationActivityPageKey = "AuthenticationActivity";
        public const string _mainMenuActivityPageKey = "MainMenuActivity";

        public void Configure()
        {
            var navigationService = new NavigationService();

            ConfigurePageKeys(navigationService);

            SimpleIoc.Default.Register<INavigationService>(() => navigationService);
        }

        private static void ConfigurePageKeys(NavigationService nav)
        {
            nav.Configure(_authenticationActivityPageKey, typeof(AuthenticationActivity));
            nav.Configure(_mainMenuActivityPageKey, typeof(MainMenuActivity));
        }
    }
}