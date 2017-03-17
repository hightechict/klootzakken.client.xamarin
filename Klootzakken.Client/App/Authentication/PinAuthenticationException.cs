using System;


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