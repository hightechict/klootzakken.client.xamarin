namespace Klootzakken.Client.App.Authentication
{
    public class AuthenticationStatusCode
    {
        private AuthenticationStatusCode(string value)
        {
            Value = value;
        }

        private string Value { get; set; }

        public static AuthenticationStatusCode NotPairedYet => new AuthenticationStatusCode("404");
        public static AuthenticationStatusCode InvalidPinOrExpired => new AuthenticationStatusCode("400");
    }
}


